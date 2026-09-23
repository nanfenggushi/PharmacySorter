-- =============================================
-- 数据库：医院药品分拣系统 (PharmacySorter)
-- 环境：SQL Server
-- =============================================

-- 1. 创建工位动作映射表 (核心控制表)
-- 包含物理工位及其对应的底层串口宏指令
CREATE TABLE StationAction (
    StationId INT PRIMARY KEY,               -- 固定编号：1正前分拣槽，2左侧药位，3右侧药位
    StationName NVARCHAR(50) NOT NULL,       -- 工位名称
    GrabCommand VARCHAR(100) NULL,           -- 抓取触发指令串 (串口指令为ASCII，用VARCHAR)
    DropCommand VARCHAR(100) NULL,           -- 放置触发指令串
    EstTimeMs INT NOT NULL DEFAULT 3500      -- 动作预估耗时(毫秒)，供 C# 线程 Sleep 使用
);

-- 2. 创建药品信息表
CREATE TABLE Drug (
    DrugId INT IDENTITY(1,1) PRIMARY KEY,    -- 药品ID，自增
    DrugName NVARCHAR(100) NOT NULL,         -- 药品名称
    Spec NVARCHAR(100) NULL,                 -- 规格（界面字典维护，可为空）
    StationId INT NULL,                      -- 关联其摆放的物理工位编号
    IsActive BIT NOT NULL CONSTRAINT DF_Drug_IsActive DEFAULT 1, -- 1启用 0停用（软删除）
    CONSTRAINT FK_Drug_Station FOREIGN KEY (StationId) REFERENCES StationAction(StationId)
);

-- 3. 创建处方表
CREATE TABLE Prescription (
    PrescriptionId INT IDENTITY(1000,1) PRIMARY KEY, -- 处方流水号，从1000开始自增
    PatientNo NVARCHAR(50) NOT NULL,                 -- 患者编号
    Status NVARCHAR(20) NOT NULL DEFAULT N'待配药',   -- 状态：待配药 / 配药中 / 部分异常 / 已完成
    CreateTime DATETIME NOT NULL DEFAULT GETDATE(),  -- 处方接收时间
    CompleteTime DATETIME NULL                       -- 配药完成时间
    ,SortNo INT NOT NULL CONSTRAINT DF_Prescription_SortNo DEFAULT 0 -- 队列顺序，越小越优先
);

-- 4. 创建处方明细表
CREATE TABLE PrescriptionItem (
    ItemId INT IDENTITY(1,1) PRIMARY KEY,            -- 明细ID，自增
    PrescriptionId INT NOT NULL,                     -- 关联处方表
    DrugId INT NOT NULL,                             -- 关联药品表
    RequiredQty INT NOT NULL,                        -- 处方要求数量
    ActualQty INT NOT NULL DEFAULT 0,                -- 人工核对后确认的实收数量
    Status NVARCHAR(20) NOT NULL DEFAULT N'待取药',   -- 状态：待取药 / 取药中 / 待核对 / 核对通过 / 异常
    CONSTRAINT FK_Item_Prescription FOREIGN KEY (PrescriptionId) REFERENCES Prescription(PrescriptionId),
    CONSTRAINT FK_Item_Drug FOREIGN KEY (DrugId) REFERENCES Drug(DrugId)
);

-- 5. 创建操作与指令日志表
CREATE TABLE AppLog (
    LogId INT IDENTITY(1,1) PRIMARY KEY,
    ItemId INT NULL,                                 -- 关联处方明细(允许为空，因为有些系统日志不属于具体明细)
    LogType NVARCHAR(50) NOT NULL,                   -- 日志类型：指令下发 / 数量核对 / 异常纠偏
    Content NVARCHAR(MAX) NOT NULL,                  -- 详细内容
    LogTime DATETIME NOT NULL DEFAULT GETDATE()      -- 发生时间
);

-- =============================================
-- 初始化基础配置数据 (必须执行)
-- 将 V2.1 需求文档中的预设动作组合写入数据库
-- =============================================

INSERT INTO StationAction (StationId, StationName, GrabCommand, DropCommand, EstTimeMs)
VALUES 
-- 工位1：正前分拣槽 (主要用于放置投递，也可以配置抓取指令测试用)
(1, N'正前分拣槽', '$DGT:3-7,1!', '$DGT:8-12,1!', 3500),

-- 工位2：左侧药位 (主要执行左转抓取动作)
(2, N'左侧药位', '$DGT:13-17,1!', NULL, 4000),

-- 工位3：右侧药位 (主要执行右转抓取动作)
(3, N'右侧药位', '$DGT:23-27,1!', NULL, 4000);


-- =============================================
-- 插入几条测试药品数据 (供界面开发测试使用)
-- =============================================
INSERT INTO Drug (DrugName, Spec, StationId) VALUES 
(N'阿莫西林胶囊', N'0.25g*50粒', 2), -- 绑定在左侧药位
(N'布洛芬缓释胶囊', N'0.3g*24粒', 3), -- 绑定在右侧药位
(N'维生素C泡腾片', N'1g*10片', NULL); -- 未绑定工位的药品

-- =============================================
-- 已建库升级到 V2.2（表已存在时只执行本段，可重复执行）
-- 连接字符串当前指向的库名以 PharmacySorter\App.config 的 connStr 为准
-- =============================================
IF COL_LENGTH('Drug', 'Spec') IS NULL
    ALTER TABLE Drug ADD Spec NVARCHAR(100) NULL;

IF COL_LENGTH('Drug', 'IsActive') IS NULL
    ALTER TABLE Drug ADD IsActive BIT NOT NULL CONSTRAINT DF_Drug_IsActive DEFAULT 1;

-- 旧数据把规格写在药品名称括号里，拆到 Spec 列
UPDATE Drug
SET Spec = SUBSTRING(DrugName, CHARINDEX('(', DrugName) + 1, CHARINDEX(')', DrugName) - CHARINDEX('(', DrugName) - 1),
    DrugName = LTRIM(RTRIM(LEFT(DrugName, CHARINDEX('(', DrugName) - 1)))
WHERE Spec IS NULL
  AND CHARINDEX('(', DrugName) > 1
  AND CHARINDEX(')', DrugName) > CHARINDEX('(', DrugName);

-- 已有处方表增加队列顺序。旧数据按接收时间补序号。
IF COL_LENGTH('Prescription', 'SortNo') IS NULL
BEGIN
    ALTER TABLE Prescription ADD SortNo INT NOT NULL CONSTRAINT DF_Prescription_SortNo DEFAULT 0;
    ;WITH Ordered AS (
        SELECT PrescriptionId, ROW_NUMBER() OVER (ORDER BY CreateTime, PrescriptionId) AS NewSortNo
        FROM Prescription
    )
    UPDATE p SET SortNo = o.NewSortNo
    FROM Prescription p
    INNER JOIN Ordered o ON p.PrescriptionId = o.PrescriptionId;
END

-- 已有明细增加已执行抓取次数。看板用它显示进度，补抓时继续累加。
IF COL_LENGTH('PrescriptionItem', 'GrabCount') IS NULL
    ALTER TABLE PrescriptionItem ADD GrabCount INT NOT NULL CONSTRAINT DF_PrescriptionItem_GrabCount DEFAULT 0;