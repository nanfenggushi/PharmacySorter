-- 示例操作日志。只在还没有“指令下发”日志时插入，避免重复执行产生重复数据。
-- 同时补两张示例处方，日志里的处方号才能在审计页显示和筛选。
IF NOT EXISTS (SELECT 1 FROM AppLog WHERE LogType = N'指令下发')
BEGIN
    DECLARE @DrugLeft INT, @DrugRight INT, @DrugFree INT;
    DECLARE @PrescriptionDone INT, @PrescriptionAbnormal INT;
    DECLARE @ItemLeft INT, @ItemRight INT, @ItemAbnormal INT;

    SELECT @DrugLeft = DrugId FROM Drug WHERE DrugName = N'阿莫西林胶囊';
    SELECT @DrugRight = DrugId FROM Drug WHERE DrugName = N'布洛芬缓释胶囊';
    SELECT @DrugFree = DrugId FROM Drug WHERE DrugName = N'维生素C泡腾片';

    INSERT INTO Prescription (PatientNo, Status, CreateTime, CompleteTime)
    VALUES (N'P20260920', N'已完成', '2026-09-20 09:12:00', '2026-09-20 09:28:00');
    SET @PrescriptionDone = SCOPE_IDENTITY();

    INSERT INTO Prescription (PatientNo, Status, CreateTime, CompleteTime)
    VALUES (N'P20260922', N'部分异常', '2026-09-22 14:05:00', NULL);
    SET @PrescriptionAbnormal = SCOPE_IDENTITY();

    INSERT INTO PrescriptionItem (PrescriptionId, DrugId, RequiredQty, ActualQty, Status)
    VALUES (@PrescriptionDone, @DrugLeft, 3, 3, N'核对通过');
    SET @ItemLeft = SCOPE_IDENTITY();

    INSERT INTO PrescriptionItem (PrescriptionId, DrugId, RequiredQty, ActualQty, Status)
    VALUES (@PrescriptionDone, @DrugRight, 2, 2, N'核对通过');
    SET @ItemRight = SCOPE_IDENTITY();

    INSERT INTO PrescriptionItem (PrescriptionId, DrugId, RequiredQty, ActualQty, Status)
    VALUES (@PrescriptionAbnormal, @DrugFree, 2, 3, N'异常');
    SET @ItemAbnormal = SCOPE_IDENTITY();

    INSERT INTO AppLog (ItemId, LogType, Content, LogTime) VALUES
    (NULL, N'药品维护', N'新增药品【阿莫西林胶囊】，规格：0.25g*50粒，工位：左侧药位', '2026-09-18 08:30:00'),
    (NULL, N'药品维护', N'新增药品【布洛芬缓释胶囊】，规格：0.3g*24粒，工位：右侧药位', '2026-09-18 08:32:00'),
    (NULL, N'药品维护', N'新增药品【维生素C泡腾片】，规格：1g*10片，工位：未绑定', '2026-09-18 08:35:00'),
    (NULL, N'工位配置', N'保存工位指令：正前分拣槽 抓取=$DGT:3-7,1! 放置=$DGT:8-12,1! 延时=3500ms；左侧药位 抓取=$DGT:13-17,1! 放置=空 延时=4000ms；右侧药位 抓取=$DGT:23-27,1! 放置=空 延时=4000ms', '2026-09-18 09:00:00'),
    (NULL, N'工位配置', N'药品【阿莫西林胶囊】绑定工位：左侧药位', '2026-09-18 09:06:00'),
    (NULL, N'工位配置', N'药品【布洛芬缓释胶囊】绑定工位：右侧药位', '2026-09-18 09:07:00'),
    (@ItemLeft, N'指令下发', N'处方开始前下发 G0002 待命指令，机械臂抬起并张开夹爪', '2026-09-20 09:14:00'),
    (@ItemLeft, N'指令下发', N'阿莫西林胶囊第1次抓取，下发 $DGT:13-17,1!', '2026-09-20 09:15:00'),
    (@ItemLeft, N'指令下发', N'阿莫西林胶囊第1次投递，下发 $DGT:8-12,1!', '2026-09-20 09:15:05'),
    (@ItemLeft, N'数量核对', N'阿莫西林胶囊应发3盒，人工清点实收2盒，数量短缺', '2026-09-20 09:18:00'),
    (@ItemLeft, N'异常纠偏', N'阿莫西林胶囊漏抓1盒，确认后自动补抓1次', '2026-09-20 09:18:20'),
    (@ItemLeft, N'指令下发', N'阿莫西林胶囊补抓第1次，下发 $DGT:13-17,1!', '2026-09-20 09:18:30'),
    (@ItemLeft, N'数量核对', N'阿莫西林胶囊应发3盒，人工再次清点实收3盒，核对通过', '2026-09-20 09:20:00'),
    (@ItemRight, N'指令下发', N'布洛芬缓释胶囊第1次抓取，下发 $DGT:23-27,1!', '2026-09-20 09:22:00'),
    (@ItemRight, N'数量核对', N'布洛芬缓释胶囊应发2盒，人工清点实收2盒，核对通过', '2026-09-20 09:26:00'),
    (@ItemAbnormal, N'指令下发', N'维生素C泡腾片第1次抓取，药品尚未绑定药位，本次为人工补录的测试日志', '2026-09-22 14:10:00'),
    (@ItemAbnormal, N'数量核对', N'维生素C泡腾片应发2盒，人工清点实收3盒，超量抓取', '2026-09-22 14:16:00'),
    (@ItemAbnormal, N'异常纠偏', N'维生素C泡腾片多抓1盒，操作员确认已从分拣槽拿走多余药品并放回原处', '2026-09-22 14:18:00'),
    (NULL, N'药品维护', N'停用药品【维生素C泡腾片】', '2026-09-22 16:40:00'),
    (NULL, N'药品维护', N'启用药品【维生素C泡腾片】', '2026-09-23 08:15:00');
END