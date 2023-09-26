create database ProjectX
go

use ProjectX
go

--活動資料表
CREATE TABLE Activity (
    ActivityID INT identity(1,1) PRIMARY KEY,
    ActivityName NVARCHAR(255) UNIQUE, -- 唯一約束，確保活動名稱的唯一性
    Category NVARCHAR(255),
    Map NVARCHAR(255),
    SuggestedAmount MONEY,
    ActivityContent NVARCHAR(MAX),
    MinAttendee INT,
    VoteDate DATETIME,
    ExpectedDepartureMonth DATE
);
go

--會員資料表
CREATE TABLE Member (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Nickname NVARCHAR(50) UNIQUE,  
    Account NVARCHAR(50) UNIQUE,  
    [Password] NVARCHAR(255),
    Email NVARCHAR(255) UNIQUE,    
    Phone NVARCHAR(20) UNIQUE,
	Intro NVARCHAR(MAX),     
    LoginMethod NVARCHAR(50)      
);
go

--成團資料表
CREATE TABLE ActivityAdded (
    ActivityID INT identity(1,1) PRIMARY KEY,
    ActivityName NVARCHAR(255) UNIQUE, -- 唯一約束，確保活動名稱的唯一性
    Category NVARCHAR(50),
    ActivityContent NVARCHAR(MAX),
    MinAttendee INT,
    MaxAttendee INT,
    StartDate DATETIME,
    EndDate DATETIME,
    Organizer INT FOREIGN KEY REFERENCES Member(UserID)
);
go

--投票時間資料表
--設定外鍵約束(FK_VoteTime_Activity)參考到活動資料表的活動ID
CREATE TABLE VoteTime (
	VoteID INT PRIMARY KEY,        
    ActivityID INT,               
    StartDate DATE,               
    VoteCount INT,                   
    CONSTRAINT FK_VoteTime_Activity FOREIGN KEY (ActivityID) REFERENCES Activity(ActivityID)
);
go

--投票紀錄資料表
CREATE TABLE VoteRecord (
    RecordID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
    ActivityID INT,
    VoteResult DATE,
    CONSTRAINT FK_VoteRecord_UserID FOREIGN KEY (UserID) REFERENCES Member(UserID),
    CONSTRAINT FK_VoteRecord_ActivityID FOREIGN KEY (ActivityID) REFERENCES Activity(ActivityID)
);
go





