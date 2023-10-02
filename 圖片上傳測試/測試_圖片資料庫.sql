-- 創建資料庫（如果尚未存在）
CREATE DATABASE PhotoUploadDB;

-- 使用剛剛創建的資料庫
USE PhotoUploadDB;

-- 創建 Photos 資料表
CREATE TABLE Photos
(
    PhotoId INT PRIMARY KEY IDENTITY(1,1),
    PhotoName NVARCHAR(MAX),
    ImageData VARBINARY(MAX)
);
