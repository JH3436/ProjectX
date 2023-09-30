create database ProjectX
go

use ProjectX
go

--活動資料表
CREATE TABLE MyActivity (
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
CREATE TABLE [Group] (
    GroupID INT IDENTITY(1,1) PRIMARY KEY,
    GroupName NVARCHAR(255) unique,
    GroupCategory NVARCHAR(50),
    GroupContent NVARCHAR(MAX),
    MinAttendee INT,
    MaxAttendee INT,
    StartDate DATE,
    EndDate DATE,
    Organizer INT,  
    OriginalActivityID INT,  
    CONSTRAINT FK_Organizer FOREIGN KEY (Organizer) REFERENCES [Member](UserID),
    CONSTRAINT FK_OriginalActivity FOREIGN KEY (OriginalActivityID) REFERENCES MyActivity(ActivityID)
);
go

--投票時間資料表
CREATE TABLE VoteTime (
	VoteID INT IDENTITY(1,1) PRIMARY KEY,        
    ActivityID INT,               
    StartDate DATE,               
    VoteCount INT,                   
    CONSTRAINT FK_VoteTime_Activity FOREIGN KEY (ActivityID) REFERENCES MyActivity(ActivityID)
);
go

--投票紀錄資料表
CREATE TABLE VoteRecord (
    RecordID INT IDENTITY(1,1) PRIMARY KEY ,
    UserID INT,
    ActivityID INT,
    VoteResult DATE,
    CONSTRAINT FK_VoteRecord_UserID FOREIGN KEY (UserID) REFERENCES [Member](UserID),
    CONSTRAINT FK_VoteRecord_ActivityID FOREIGN KEY (ActivityID) REFERENCES MyActivity(ActivityID)
);
go

--報名紀錄資料表
CREATE TABLE Registration (
    RegistrationID INT IDENTITY(1,1) PRIMARY KEY ,
    GroupID INT,
    ParticipantID INT,
    CONSTRAINT FK_Registration_Group FOREIGN KEY (GroupID) REFERENCES [Group](GroupID),
    CONSTRAINT FK_Registration_User FOREIGN KEY (ParticipantID) REFERENCES [Member](UserID)
);
go

--活動收藏資料表
CREATE TABLE ActivityLikes (
    LikeRecordID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT,
    ActivityID INT,
    CONSTRAINT FK_UserID FOREIGN KEY (UserID) REFERENCES [Member](UserID),
    CONSTRAINT FK_ActivityID FOREIGN KEY (ActivityID) REFERENCES MyActivity(ActivityID),
    CONSTRAINT UQ_UserActivityLike UNIQUE (UserID, ActivityID)
);
go

-- 通知資料表
CREATE TABLE Notification (
    NotificationID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL, 
    NotificationContent NVARCHAR(MAX) NOT NULL,
    IsRead BIT NOT NULL DEFAULT 0, --預設為未讀
    NotificationDate DATETIME default sysdatetime(),
    CONSTRAINT FK_User_Notification FOREIGN KEY (UserID) REFERENCES [Member](UserID),
	CONSTRAINT CK_IsRead CHECK (IsRead IN (0, 1)) -- IsRead只能是0或1
);
go

--活動照片資料表
CREATE TABLE Photos (
    PhotoID INT PRIMARY KEY IDENTITY(1,1),
    ActivityID INT,
    GroupID INT,
    PhotoPath NVARCHAR(255) NOT NULL,
    CONSTRAINT FK_Activity_Photo FOREIGN KEY (ActivityID) REFERENCES MyActivity(ActivityID),
    CONSTRAINT FK_Group_Photo FOREIGN KEY (GroupID) REFERENCES [Group](GroupID)
);
go

--留言板資料表
CREATE TABLE Chat (
    ChatID INT IDENTITY(1,1) PRIMARY KEY,  
    ActivityID INT,               
    UserID INT,                 
    ChatContent NVARCHAR(MAX) NOT NULL,    
    ToWhom INT,                            
    ChatTime DATETIME DEFAULT SYSDATETIME(),
    FOREIGN KEY (ActivityID) REFERENCES MyActivity(ActivityID),
    FOREIGN KEY (UserID) REFERENCES [Member](UserID),
	-- 外鍵約束，參考留言板ID (ChatID) 作為回覆對象
    FOREIGN KEY (ToWhom) REFERENCES Chat(ChatID)
);
go

--聯絡我們表單
CREATE TABLE Contact (
    FormID INT PRIMARY KEY IDENTITY(1,1),
    SenderName NVARCHAR(100) NOT NULL, 
    Email NVARCHAR(255) NOT NULL,
    Phone NVARCHAR(20) NOT NULL,
    EmailTitle NVARCHAR(255) NOT NULL,
    FormContent NVARCHAR(MAX) NOT NULL,
    SendTime DATETIME DEFAULT SYSDATETIME(),
);
go







