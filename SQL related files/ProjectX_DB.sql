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
           ('阿里山眠月線二日遊',
           '登山',
           500,
           '眠月線，位於嘉義縣阿里山鄉，沿途的景色優美，
		   且擁有森林、鐵道、車站、高空鐵橋等多重景觀，且坡度平緩，
		   行走難度平易近人，如今已經成為了深受遊客喜愛的健走路線。',
           3,
           '2023-10-24' ,
           '2023-12-01'),
		   ('奇萊主北假日團',
           '登山',
           1000,
           '你曾到過傳說中的黑色奇萊嗎？
		   從合歡山遠眺位在南投與花蓮之間的奇萊山時，背著光的山脊顯得陰暗神祕,
		   而他錯綜複雜、變幻莫測的氣候更讓山嶺彷彿蒙上了一層神秘的輕紗,
		   險峻的地勢、頻傳的神秘故事使得「黑色奇萊」聲名遠播,
		   在溯溪的過程中,
		   隨著國家公園的成立，越來越多人踏足奇萊、揭開他神祕的面紗,
		   巍峨的奇萊,就這樣成了人們流連忘返、一再拜訪的熱門山區',
           3,
           '2023-11-15',
           '2024-01-01'),
		   ('大小霸假日團',
           '登山',
           1000,
           '如果說聖稜線是盤據在雪山山脈上的巨龍，大小霸就是這條巨龍的雙耳，泰雅族語「Babo Papak」，是雙耳大山的意思。賽夏族稱它「Kapatalayan」。早期漢族稱它「熬酒桶山」，清臺灣輿圖定名「大霸尖山」。

山勢突地拔起危峰孤峙，山形冷峻氣勢宏偉，素有「世紀奇峰」之稱。這裡是傳說中泰雅族與賽夏族的共同起源地，神話故事中他們的祖先都是從這裡走下來的，也因此流傳著許多禁忌。',
           5,
           '2023-12-10',
           '2024-01-01'),

		   ('石門老梅溪溯溪',
           '溯溪',
           1000,
           '老梅溪位於新北市石門鄉，是台北地區不可多得的好玩溯溪地點，而且離台北市區僅一小時車程。老梅溪的上游沒有住家、養殖、種植，水質絕對乾淨，山林也很完整、原始。
		   溯溪過程中還會有一段20分鐘的原始叢林穿越。',
           5,
           '2023-12-10',
           '2024-01-01'),

		   ('七孔瀑布',
           '溯溪',
           20,
           '七孔瀑布 位在屏東縣滿洲鄉，共有七層瀑布。距墾丁老街約30分鐘車車程。我上次在2013年拜訪，需付每人20NT停車的費用。
			只需要走路2分鐘即可到達瀑布。從那裡，用繩索往上爬是最好玩的，手腳並用，不太難，一一探
			訪七個孔。從底部到最後一層是約200公尺。',
           5,
           '2023-12-10',
           '2024-01-01'),

		   ('半屏溪溯溪',
           '溯溪',
           20,
           '位於台灣北部東北角的半屏溪，源頭出於「半屏山」。
		   這邊鄰近瑞芳、九份，以前盛產各種礦產、煤礦，可以看到以前冶煉工廠與廢煙道的痕跡。在礦業公司與人群都離去後，半屏溪成為了台灣少數，整條都杳無人煙、沒有家庭、工廠等建築物的清澈溪流，
		   可以從出海口一路往上溯，沿途會經過數個瀑布',
           5,
           '2024-02-10',
           '2024-03-01'),
		   ('墾丁出水口水肺潛水',
           '潛水',
           500,
           '在出水口海灣內，水質清澈，風平浪靜，最不受風浪影響。而東側的石頭坡，更是水上活動最活耀的區域，裡面的魚幾乎都是吃浮潛教練的南極蝦長大的，都不怕人了，魚群也很多，種類也很多，光是蝶魚就七、八種，這是其他海域都很難看到的耶，簡直就是魚類教室了，每回帶人去，魚類就介紹不完。',
           5,
           '2024-02-10',
           '2024-03-01'),
		   ('小琉球自潛',
           '潛水',
           1500,
           '來小琉球自由潛水最大的特色就是能看到綠蠵龜，且一年四季水溫都很適合下水。',
           5,
           '2024-02-10',
           '2024-03-01'),

		    ('綠島自潛',
           '潛水',
           1000,
           '綠島一年四季都相當適合進行潛水活動，但不同季節適合的潛點會有所差異。秋冬季（約10至4月）受東北季風影響，適合在靠近西南側的潛點，如大白沙、南寮。',
           5,
           '2024-02-10',
           '2024-03-01'),

		   ('水漾森林湖邊野營',
           '露營',
           500,
           '「水漾森林」因921地震阻斷河道，河水淤積使得原本生機茂密的杉木林樹葉凋零，呈現杉木枝幹與鏡子一般的湖面倒映的奇特景色！',
           5,
           '2024-02-10',
           '2024-03-01'),

		    ('免裝備露營 - 半島秘境',
           '露營',
           5000,
           '脫離都市繁忙生活，帶著一顆渴望放鬆的心，來半島秘境貼近自然，體驗奢華露營的樂趣吧！
			冬暖夏涼的科技棉材質帳篷、天幕、訂製獨立筒床墊、高級羽絨枕被躺椅、休閒桌，我們都幫您準備好囉！',
           5,
           '2024-02-10',
           '2024-03-01'),

		    ('踏浪星辰露營Camp',
           '露營',
           5000,
           '來花蓮到踏浪星辰露營去！觀星看海欣賞七星潭的日出，在踏浪星辰露營一次都能看到！',
           5,
           '2024-02-10',
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
           (1003, '/IMG/眠月線軌道.JPG'),
		   (1003, '/IMG/眠月線1.jpeg'),
		   (1003, '/IMG/眠月線2.jpeg'),
		   (1003, '/IMG/眠月線3.jpeg'),

		   (1004, '/IMG/奇萊北.JPG'),
		   (1004, '/IMG/奇萊北黃金.JPG'),
		   (1004, '/IMG/奇萊3.jpeg'),
		   (1004, '/IMG/奇萊4.jpeg'),

		   (1005, '/IMG/test1.JPG'),
		   (1005, '/IMG/大壩.JPG'),
		   (1005, '/IMG/大壩雪.jpg'),
		   (1005, '/IMG/大壩四.jpg'),

		   (1006, '/IMG/老梅1.jpeg'),
		   (1006, '/IMG/老梅2.jpeg'),
		   (1006, '/IMG/老梅3.jpeg'),
		   (1006, '/IMG/老梅4.jpeg'),

		   (1007, '/IMG/七孔1.jpeg'),
		   (1007, '/IMG/七孔2.jpg'),
		   (1007, '/IMG/七孔3.jpg'),
		   (1007, '/IMG/七孔4.jpg'),

		   (1008, '/IMG/半屏1.jpg'),
		   (1008, '/IMG/半屏2.jpeg'),
		   (1008, '/IMG/半屏3.jpeg'),
		   (1008, '/IMG/半屏4.jpeg'),

		   (1009, '/IMG/出水口1.jpeg'),
		   (1009, '/IMG/出水口2.jpeg'),
		   (1009, '/IMG/出水口3.jpeg'),
		   (1009, '/IMG/出水口4.png'),

		   (1010, '/IMG/小琉球1.jpg'),
		   (1010, '/IMG/小琉球2.jpg'),
		   (1010, '/IMG/小琉球3.jpg'),
		   (1010, '/IMG/小琉球4.jpg'),

		   (1011, '/IMG/綠島1.jpeg'),
		   (1011, '/IMG/綠島2.jpeg'),
		   (1011, '/IMG/綠島3.jpeg'),
		   (1011, '/IMG/綠島4.jpg'),

		   (1012, '/IMG/水樣1.jpeg'),
		   (1012, '/IMG/水樣2.jpeg'),
		   (1012, '/IMG/水樣3.jpeg'),
		   (1012, '/IMG/水樣4.jpg'),

		   (1013, '/IMG/半島1.jpeg'),
		   (1013, '/IMG/半島2.jpeg'),
		   (1013, '/IMG/半島3.jpeg'),
		   (1013, '/IMG/半島4.jpeg'),

		   (1014, '/IMG/踏浪1.jpeg'),
		   (1014, '/IMG/踏浪2.jpeg'),
		   (1014, '/IMG/踏浪3.jpeg'),
		   (1014, '/IMG/踏浪4.jpeg'),



		   
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
