--1. 活動資料表在成團後，會寫入成團資料表內之判斷邏輯：
-- 活動ID：替換為實際的活動ID
DECLARE @ 活動ID INT;
SET @ 活動ID = '要查詢的活動ID';
DECLARE @ 投票截止日 DATETIME;

-- 使用子查詢從活動資料表中獲取投票日，然後使用DATEADD計算投票截止日(投票日+5天)
SELECT @ 投票截止日 = DATEADD(DAY, 5, 投票日)
FROM 活動資料表
WHERE 活動ID = @ 活動ID;

DECLARE @ 現在時間 DATETIME;
SET @ 現在時間 = GETDATE();

IF @ 現在時間 > @ 投票截止日 
 -- 如果系統時間大於投票截止日，執行相應的操作
BEGIN

-- 找到票數最高的日期
DECLARE @ 最高票數日期 DATE;
SELECT TOP 1 @ 最高票數日期 = 出團起始日
FROM 投票時間資料表
WHERE 活動ID = @活動ID
GROUP BY 出團起始日
ORDER BY SUM(票數) DESC;

-- 插入成團資料
INSERT INTO 成團資料表 (
        成團活動ID,
        成團名稱,
        成團類別,
        成團內容,
        下限人數,
        上限人數,
        出團起始日,
        出團結束日,
        主揪,
        原始活動ID
    )
SELECT 活動ID,
    活動名稱,
    活動類別,
    活動內容,
    下限人數,
    上限人數,
    @ 最高票數日期 AS 出團起始日,
    DATEADD(DAY, 1, @ 最高票數日期) AS 出團結束日, -- 在最高票數日期的基礎上加一天作為結束日
    主揪,
    活動ID AS 原始活動ID
FROM 活動資料表
WHERE 活動ID = '要新增的活動ID';
END
-- 如果系統時間不大於投票截止日，執行其他操作
ELSE BEGIN 
PRINT '這邊可以當作繼續投票的判斷?';
END
------------------------------------------------------------------------------------------
--3. 新增投票紀錄並更新投票時間資料表的票數
DECLARE @UserID INT = 1; -- 使用者ID
DECLARE @ActivityID INT = 1; -- 活動ID
DECLARE @UserInputDate NVARCHAR(10) = '2023-09-01'; -- 假設投票選項頁面抓到的資料是字串

DECLARE @VoteResult DATE; -- 用於存儲轉換後的日期

-- 嘗試將使用者所選的日期字串轉換為日期
BEGIN
    SET @VoteResult = TRY_CAST(@UserInputDate AS DATE);
END


-- 檢查是否已存在投票紀錄
DECLARE @RecordID INT;
SELECT @RecordID = RecordID
FROM VoteRecord
WHERE UserID = @UserID AND ActivityID = @ActivityID;

IF @RecordID IS NOT NULL
BEGIN
    -- 獲取舊的投票結果以便後面處理投票時間紀錄表的票數更新
    DECLARE @OldVoteResult DATE;
    SELECT @OldVoteResult = VoteResult
    FROM VoteRecord
    WHERE RecordID = @RecordID;

    -- 更新投票選項
    UPDATE VoteRecord
    SET VoteResult = @VoteResult
    WHERE RecordID = @RecordID;

    -- 更新投票時間資料表中的投票數
    IF @OldVoteResult <> @VoteResult   -- <>是SQL不等於的運算符號
    BEGIN
        -- 減少舊的投票選項的投票數
        UPDATE VoteTime
        SET VoteCount -= 1
        WHERE ActivityID = @ActivityID AND StartDate = @OldVoteResult

        -- 增加新的投票選項的投票數
        UPDATE VoteTime
        SET VoteCount += 1
        WHERE ActivityID = @ActivityID AND StartDate = @VoteResult
    END;
END
------------------------------------------------------------------------------------------
--4. 投票截止日後，計算最高票。決定"成團資料表"出團起始日跟出團結束日



