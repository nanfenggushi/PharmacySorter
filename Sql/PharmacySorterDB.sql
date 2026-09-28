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
    EstTimeMs INT NOT NULL DEFAULT 6000      -- 动作预估耗时(毫秒)，供 C# 线程 Sleep 使用
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

-- 3. 创建固定处方。处方可以反复选用，不绑定患者。
CREATE TABLE Prescription (
    PrescriptionId INT IDENTITY(1000,1) PRIMARY KEY, -- 处方编号，从1000开始自增
    PrescriptionName NVARCHAR(50) NOT NULL,          -- 处方名称，全库唯一
    CreateTime DATETIME NOT NULL DEFAULT GETDATE(),  -- 处方建立时间
    CONSTRAINT UQ_Prescription_Name UNIQUE (PrescriptionName)
);

-- 4. 创建固定处方明细。只保存药品和数量，不保存某一次配药进度。
CREATE TABLE PrescriptionItem (
    ItemId INT IDENTITY(1,1) PRIMARY KEY,
    PrescriptionId INT NOT NULL,
    DrugId INT NOT NULL,
    RequiredQty INT NOT NULL,
    CONSTRAINT FK_Item_Prescription FOREIGN KEY (PrescriptionId) REFERENCES Prescription(PrescriptionId),
    CONSTRAINT FK_Item_Drug FOREIGN KEY (DrugId) REFERENCES Drug(DrugId),
    CONSTRAINT UQ_PrescriptionItem_Drug UNIQUE (PrescriptionId, DrugId)
);

-- 5. 创建待配任务。患者编号、队列顺序和配药状态都记在这一次任务上。
CREATE TABLE DispenseOrder (
    OrderId INT IDENTITY(1,1) PRIMARY KEY,
    PrescriptionId INT NOT NULL,
    PatientNo NVARCHAR(50) NOT NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT N'待配药', -- 待配药 / 配药中 / 部分异常 / 已完成 / 已撤销
    SortNo INT NOT NULL CONSTRAINT DF_DispenseOrder_SortNo DEFAULT 0,
    CreateTime DATETIME NOT NULL DEFAULT GETDATE(),
    CompleteTime DATETIME NULL,
    CONSTRAINT FK_Order_Prescription FOREIGN KEY (PrescriptionId) REFERENCES Prescription(PrescriptionId)
);

-- 6. 创建待配任务明细。抓取次数和核对结果只属于这一次配药。
CREATE TABLE DispenseOrderItem (
    ItemId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    DrugId INT NOT NULL,
    RequiredQty INT NOT NULL,
    ActualQty INT NOT NULL DEFAULT 0,
    GrabCount INT NOT NULL CONSTRAINT DF_DispenseOrderItem_GrabCount DEFAULT 0,
    Status NVARCHAR(20) NOT NULL DEFAULT N'待取药', -- 待取药 / 取药中 / 待核对 / 核对通过 / 异常
    CONSTRAINT FK_OrderItem_Order FOREIGN KEY (OrderId) REFERENCES DispenseOrder(OrderId),
    CONSTRAINT FK_OrderItem_Drug FOREIGN KEY (DrugId) REFERENCES Drug(DrugId)
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
-- 动作组与《3D打印机械臂-双轴-自研爪子版》INI 一致：
-- 正前抓取 G0003-G0007，正前投递 G0008-G0012，左抓 G0013-G0017，右抓 G0023-G0027。
-- 一组 5 帧，单帧多为 1000ms，含转向和抬臂的帧为 1500-2000ms，默认等待取 6000ms。
-- =============================================

INSERT INTO StationAction (StationId, StationName, GrabCommand, DropCommand, EstTimeMs)
VALUES 
-- 工位1：正前分拣槽。配药投递用 DropCommand，GrabCommand 只用于单动测试。
(1, N'正前分拣槽', '$DGT:3-7,1!', '$DGT:8-12,1!', 6000),

-- 工位2：左侧药位。只配置抓取，放下统一走正前分拣槽。
(2, N'左侧药位', '$DGT:13-17,1!', NULL, 6000),

-- 工位3：右侧药位。只配置抓取，放下统一走正前分拣槽。
(3, N'右侧药位', '$DGT:23-27,1!', NULL, 6000);


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

-- 已有库升级：固定处方与待配任务拆开。旧处方保留成历史任务，处方名称用原处方号生成。
IF OBJECT_ID('DispenseOrder', 'U') IS NULL AND COL_LENGTH('Prescription', 'PatientNo') IS NOT NULL
BEGIN
    CREATE TABLE DispenseOrder (
        OrderId INT IDENTITY(1,1) PRIMARY KEY,
        PrescriptionId INT NOT NULL,
        PatientNo NVARCHAR(50) NOT NULL,
        Status NVARCHAR(20) NOT NULL,
        SortNo INT NOT NULL,
        CreateTime DATETIME NOT NULL,
        CompleteTime DATETIME NULL
    );

    CREATE TABLE DispenseOrderItem (
        ItemId INT IDENTITY(1,1) PRIMARY KEY,
        OrderId INT NOT NULL,
        DrugId INT NOT NULL,
        RequiredQty INT NOT NULL,
        ActualQty INT NOT NULL DEFAULT 0,
        GrabCount INT NOT NULL CONSTRAINT DF_DispenseOrderItem_GrabCount DEFAULT 0,
        Status NVARCHAR(20) NOT NULL
    );

    IF COL_LENGTH('Prescription', 'PrescriptionName') IS NULL
        ALTER TABLE Prescription ADD PrescriptionName NVARCHAR(50) NULL;

    UPDATE Prescription
    SET PrescriptionName = N'历史处方' + CAST(PrescriptionId AS NVARCHAR(20))
    WHERE PrescriptionName IS NULL OR LTRIM(RTRIM(PrescriptionName)) = N'';

    INSERT INTO DispenseOrder (PrescriptionId, PatientNo, Status, SortNo, CreateTime, CompleteTime)
    SELECT PrescriptionId, PatientNo, Status, SortNo, CreateTime, CompleteTime
    FROM Prescription;

    INSERT INTO DispenseOrderItem (OrderId, DrugId, RequiredQty, ActualQty, GrabCount, Status)
    SELECT o.OrderId, i.DrugId, i.RequiredQty, i.ActualQty, ISNULL(i.GrabCount, 0), i.Status
    FROM PrescriptionItem i
    INNER JOIN DispenseOrder o ON o.PrescriptionId = i.PrescriptionId;

    ALTER TABLE PrescriptionItem DROP CONSTRAINT FK_Item_Prescription;
    ALTER TABLE PrescriptionItem DROP COLUMN ActualQty;
    ALTER TABLE PrescriptionItem DROP COLUMN Status;
    DECLARE @GrabDefault NVARCHAR(128);
    SELECT @GrabDefault = dc.name
    FROM sys.default_constraints dc
    INNER JOIN sys.columns c ON c.default_object_id = dc.object_id
    WHERE dc.parent_object_id = OBJECT_ID('PrescriptionItem') AND c.name = 'GrabCount';
    IF @GrabDefault IS NOT NULL
        EXEC('ALTER TABLE PrescriptionItem DROP CONSTRAINT ' + @GrabDefault);
    IF COL_LENGTH('PrescriptionItem', 'GrabCount') IS NOT NULL
        ALTER TABLE PrescriptionItem DROP COLUMN GrabCount;
    ALTER TABLE PrescriptionItem ADD CONSTRAINT FK_Item_Prescription FOREIGN KEY (PrescriptionId) REFERENCES Prescription(PrescriptionId);

    DECLARE @SortDefault NVARCHAR(128);
    SELECT @SortDefault = dc.name
    FROM sys.default_constraints dc
    INNER JOIN sys.columns c ON c.default_object_id = dc.object_id
    WHERE dc.parent_object_id = OBJECT_ID('Prescription') AND c.name = 'SortNo';
    IF @SortDefault IS NOT NULL
        EXEC('ALTER TABLE Prescription DROP CONSTRAINT ' + @SortDefault);

    ALTER TABLE Prescription DROP COLUMN PatientNo;
    ALTER TABLE Prescription DROP COLUMN Status;
    ALTER TABLE Prescription DROP COLUMN CompleteTime;
    ALTER TABLE Prescription DROP COLUMN SortNo;
    ALTER TABLE Prescription ALTER COLUMN PrescriptionName NVARCHAR(50) NOT NULL;
    IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UQ_Prescription_Name' AND object_id = OBJECT_ID('Prescription'))
        ALTER TABLE Prescription ADD CONSTRAINT UQ_Prescription_Name UNIQUE (PrescriptionName);
END

-- 已有任务表补上接收时间的默认值。加入队列不写该列时由数据库填当前时间。
IF OBJECT_ID('DispenseOrder', 'U') IS NOT NULL
   AND NOT EXISTS (
       SELECT 1
       FROM sys.default_constraints dc
       INNER JOIN sys.columns c ON c.default_object_id = dc.object_id
       WHERE dc.parent_object_id = OBJECT_ID('DispenseOrder') AND c.name = 'CreateTime')
    ALTER TABLE DispenseOrder ADD CONSTRAINT DF_DispenseOrder_CreateTime DEFAULT GETDATE() FOR CreateTime;

-- 已有任务表补上队列序号默认值。全新建表已带同名约束，这里不能重复创建。
IF OBJECT_ID('DispenseOrder', 'U') IS NOT NULL
   AND NOT EXISTS (
       SELECT 1
       FROM sys.default_constraints dc
       INNER JOIN sys.columns c ON c.default_object_id = dc.object_id
       WHERE dc.parent_object_id = OBJECT_ID('DispenseOrder') AND c.name = 'SortNo')
    ALTER TABLE DispenseOrder ADD CONSTRAINT DF_DispenseOrder_SortNo DEFAULT 0 FOR SortNo;

-- 已有明细增加已执行抓取次数。看板用它显示进度，补抓时继续累加。
IF OBJECT_ID('DispenseOrderItem', 'U') IS NOT NULL AND COL_LENGTH('DispenseOrderItem', 'GrabCount') IS NULL
    ALTER TABLE DispenseOrderItem ADD GrabCount INT NOT NULL CONSTRAINT DF_DispenseOrderItem_GrabCount DEFAULT 0;

-- 旧库的动作等待偏短。一组动作约 5 到 8 秒，低于 6000ms 的记录补到 6000ms。
-- 界面里已经手工改得更长的延时保持不动。
UPDATE StationAction
SET EstTimeMs = 6000
WHERE EstTimeMs < 6000;
-- 登录账号。密码只保存 PBKDF2 哈希，不保存明文。
IF OBJECT_ID(''AppUser'', ''U'') IS NULL
BEGIN
    CREATE TABLE AppUser (
        UserId INT IDENTITY(1,1) PRIMARY KEY,
        UserName NVARCHAR(50) NOT NULL,
        DisplayName NVARCHAR(50) NOT NULL,
        PasswordHash NVARCHAR(200) NOT NULL,
        RoleName NVARCHAR(20) NOT NULL,
        IsActive BIT NOT NULL CONSTRAINT DF_AppUser_IsActive DEFAULT 1,
        CONSTRAINT UQ_AppUser_UserName UNIQUE (UserName)
    );
END

-- 初始管理员 admin / Admin@123。已有同名账号时不覆盖密码。
IF NOT EXISTS (SELECT 1 FROM AppUser WHERE UserName = N''admin'')
BEGIN
    INSERT INTO AppUser (UserName, DisplayName, PasswordHash, RoleName, IsActive)
    VALUES (
        N''admin'',
        N''系统管理员'',
        N''PBKDF2$100000$dGVzdC1zYWx0LTAwMDAwMQ==$MN9jQ/waBESrVTLeHCG0b4oSf+VO8pwnB5LvbCsQumw='',
        N''管理员'',
        1);
END
