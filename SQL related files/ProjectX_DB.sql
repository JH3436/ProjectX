create database ProjectX
go

use ProjectX
go

--活動資料表
CREATE TABLE MyActivity (
    ActivityID INT identity(1,1) PRIMARY KEY,
    ActivityName NVARCHAR(255) UNIQUE, -- 唯一約束，確保活動名稱的唯一性
    Category NVARCHAR(255),
    SuggestedAmount MONEY,
    ActivityContent NVARCHAR(MAX),
    MinAttendee INT,
    VoteDate SMALLDATETIME,
    ExpectedDepartureMonth DATE
);
go

--會員資料表
CREATE TABLE [Member] (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Nickname NVARCHAR(50) UNIQUE,  
    Account NVARCHAR(50) UNIQUE,  
    [Password] NVARCHAR(255),
    Email NVARCHAR(255) UNIQUE,    
    Phone NVARCHAR(20) UNIQUE,
	Intro NVARCHAR(MAX),
	UserPhoto VARBINARY(MAX),
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
CREATE TABLE LikeRecord (
    LikeRecordID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT,
    ActivityID INT,
    CONSTRAINT FK_UserID FOREIGN KEY (UserID) REFERENCES [Member](UserID),
    CONSTRAINT FK_ActivityID FOREIGN KEY (ActivityID) REFERENCES MyActivity(ActivityID),
    CONSTRAINT UQ_UserActivityLike UNIQUE (UserID, ActivityID)
);
go

-- 通知資料表
CREATE TABLE [Notification] (
    NotificationID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL, 
    NotificationContent NVARCHAR(MAX) NOT NULL,
    IsRead BIT NOT NULL DEFAULT 0, --預設為未讀
    NotificationDate DATETIME default sysdatetime(),
	NotificationType nvarchar(25),
	NotificationToWhichActivityID INT,
    CONSTRAINT FK_User_Notification FOREIGN KEY (UserID) REFERENCES [Member](UserID),
	CONSTRAINT CK_IsRead CHECK (IsRead IN (0, 1)) -- IsRead只能是0或1
);
go

--官方活動照片資料表
CREATE TABLE OfficialPhoto (
    OfficialPhotoID INT PRIMARY KEY IDENTITY(1,1),
    ActivityID INT,
    PhotoPath NVARCHAR(max) NOT NULL,
    CONSTRAINT FK_Activity_Photo FOREIGN KEY (ActivityID) REFERENCES MyActivity(ActivityID),
);
go

--個人開團活動照片資料表
CREATE TABLE PersonalPhoto (
    PersonalPhotoID INT PRIMARY KEY IDENTITY(1,1),
    GroupID INT,
    PhotoData VARBINARY(MAX) NOT NULL,
    CONSTRAINT FK_PersonalPhoto_Group FOREIGN KEY (GroupID) REFERENCES [Group](GroupID),
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
    FOREIGN KEY (ActivityID) REFERENCES [Group](GroupID),
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

--假資料

--活動資料表 --10/4小胖:多新增了個假資料給與不同的votedate測試
INSERT INTO [dbo].[MyActivity]
           ([ActivityName]
           ,[Category]
           ,[SuggestedAmount]
           ,[ActivityContent]
           ,[MinAttendee]
           ,[VoteDate]
           ,[ExpectedDepartureMonth])
     VALUES
           ('雲海溫泉週末團',
           '溯溪',
           100,
           '不論有沒有戶外活動的經驗
		   都非常推薦的體驗！
		   體驗前教練會先詳細解說裝備的使用方式，
		   讓人感到非常安心，
		   在溯溪的過程中，
		   也會衡量團員的現場狀況，
		   變化溯溪方式，非常有趣！',
           3,
           '2023-10-15 23:59:59' ,
           '2024-01-01'),
		   ('奇萊主北假日團',
           '登山',
           100,
           '你曾到過傳說中的黑色奇萊嗎？
		   從合歡山遠眺位在南投與花蓮之間的奇萊山時，背著光的山脊顯得陰暗神祕,
		   而他錯綜複雜、變幻莫測的氣候更讓山嶺彷彿蒙上了一層神秘的輕紗,
		   險峻的地勢、頻傳的神秘故事使得「黑色奇萊」聲名遠播,
		   在溯溪的過程中,
		   隨著國家公園的成立，越來越多人踏足奇萊、揭開他神祕的面紗,
		   巍峨的奇萊,就這樣成了人們流連忘返、一再拜訪的熱門山區',
           3,
           '2023-11-15 23:59:59',
           '2024-02-01'),
		   ('墾丁自由潛水',
           '潛水',
           100,
           '如果您不會游泳、但不怕水，建議您選擇初級自由潛水，課程中適應自由潛水的活動；過程會練習靜態和動態閉氣、鴨式潛水、自由下潛等技巧。',
           3,
           '2023-12-15 23:59:59',
           '2024-03-01')
GO

--會員資料表   --10/17 把照號密碼改簡單一點比較好測試，原本的會員不能刪掉，因為有外鍵參考 ~By James
INSERT INTO [dbo].[Member]
([Nickname], [Account], [Password], [Email], [Phone], [Intro], [UserPhoto])
VALUES
('新的管理員', 'admin', '1234', 'admin@gmail.com', '0900000000', 'admin', null),
('2號使用者', 'no2', '1234', 'number2@gmail.com', '0911111111', '大家好，我是2號使用者。我來自一個美麗的城市，在這裡我有豐富的經驗和技能。我專注於軟體開發，並擁有多年的程式設計經驗，尤其在網路應用程式開發方面有豐富經驗。', null),
('3號使用者', 'no3', '1234', 'number3@gmail.com', '0922222222', '大家好，我是3號使用者。我來自一個美麗的城市，在這裡我有豐富的經驗和技能。我專注於軟體開發，並擁有多年的程式設計經驗，尤其在網路應用程式開發方面有豐富經驗。', null),
('這是四號', 'no4', '1234', 'number4@gmail.com', '0933333333', '大家好，這是四號。我來自一個美麗的城市，在這裡我有豐富的經驗和技能。我專注於軟體開發，並擁有多年的程式設計經驗，尤其在網路應用程式開發方面有豐富經驗。', null);
go

--成團資料表
INSERT INTO [dbo].[Group]
           ([GroupName]
           ,[GroupCategory]
           ,[GroupContent]
           ,[MinAttendee]
           ,[MaxAttendee]
           ,[StartDate]
           ,[EndDate]
           ,[Organizer]
           ,[OriginalActivityID])
     VALUES
           ('阿里山登山', '登山', '123到台灣', 2, 10, '2024-01-01', '2024-01-05', 1, null),
		   ('雲海溫泉週末團', '溯溪', '不論有沒有戶外活動的經驗
		   都非常推薦的體驗！
		   體驗前教練會先詳細解說裝備的使用方式，
		   讓人感到非常安心，
		   在溯溪的過程中，
		   也會衡量團員的現場狀況，
		   變化溯溪方式，非常有趣！', 3, null, '2024-01-01', '2024-01-05', 2, 1)

GO

--投票時間資料表
INSERT INTO [dbo].[VoteTime]
           ([ActivityID]
           ,[StartDate])
     VALUES
           (1, '2023-09-30'),
           (1, '2023-10-07'),
           (1, '2023-09-14'),
           (1, '2023-09-21')
GO


--投票紀錄資料表
INSERT INTO [dbo].[VoteRecord]
           ([UserID]
           ,[ActivityID]
           ,[VoteResult])
     VALUES
           (1, 1, '2023-09-21'),
		   (2, 1, '2023-09-14'),
		   (3, 1, '2023-09-21')
GO

--報名紀錄資料表 --10/4小胖:我讓會員1多按了幾個試試看迴圈
INSERT INTO [dbo].[Registration]
           ([GroupID]
           ,[ParticipantID])
     VALUES
           (1, 1),
		   (1, 2),
		   (1, 3)

GO


--活動收藏資料表   --10/4小胖:我讓會員1多按了幾個試試看迴圈
INSERT INTO [dbo].[LikeRecord]
           ([UserID]
           ,[ActivityID])
     VALUES
           (1, 2),
		   (2, 2),
		   (3, 2),
		   (1, 1),  
		   (1, 3)
GO


-- 通知資料表
INSERT INTO [dbo].[Notification]
           ([UserID]
           ,[NotificationContent], [NotificationType])
     VALUES
           (2, '你有一則追蹤活動需要投票', 'Vote'),
		   (1,'有人回覆了您的訊息!', 'Chat')
GO


--官方活動照片資料表
INSERT INTO [dbo].[OfficialPhoto]
           ([ActivityID]
           ,[PhotoPath])
     VALUES
           (1, '/IMG/手剎車.jpg'),
		   (1, '/IMG/surf3.jpg'),
		   (1, '/IMG/surf2.jpg'),
		   (1, '/IMG/surf.jpg')
GO

--個人開團活動照片資料表
--不需要假資料，直接在頁面上操作是否能寫入/讀取資料表即可

--留言板資料表
INSERT INTO [dbo].[Chat]
           ([ActivityID]
           ,[UserID]
           ,[ChatContent]
           ,[ToWhom])
     VALUES
           (1, 1, '留言板顯示測試', null),
		   (1, 1, '留言板回覆測試', 1),
		   (1, 1, '留言板顯示測試2', null),
		   (1, 1, '留言板回覆測試2', 3)
GO


--
