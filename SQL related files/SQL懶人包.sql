
use ProjectX
go

TRUNCATE TABLE Chat;
TRUNCATE TABLE Contact;
TRUNCATE TABLE LikeRecord;
TRUNCATE TABLE MyActivity;
TRUNCATE TABLE [dbo].[Notification];
TRUNCATE TABLE [dbo].[Member];
TRUNCATE TABLE OfficialPhoto;
TRUNCATE TABLE PersonalPhoto;
TRUNCATE TABLE Registration;
TRUNCATE TABLE VoteRecord ;
TRUNCATE TABLE VoteTime;
TRUNCATE TABLE [dbo].[Group];
DELETE FROM [dbo].[Group];
DELETE FROM [dbo].[Member];
DELETE FROM MyActivity;

--group資料表
CREATE TABLE [dbo].[Group](
	[GroupID] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](255) NULL,
	[GroupCategory] [nvarchar](50) NULL,
	[GroupContent] [nvarchar](max) NULL,
	[MinAttendee] [int] NULL,
	[MaxAttendee] [int] NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[Organizer] [int] NULL,
	[OriginalActivityID] [int] NULL,
	[HasSent] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[GroupName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Group] ADD  DEFAULT ((0)) FOR [HasSent]
GO

ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [FK_Organizer] FOREIGN KEY([Organizer])
REFERENCES [dbo].[Member] ([UserID])
GO

ALTER TABLE [dbo].[Group] CHECK CONSTRAINT [FK_Organizer]
GO

ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [FK_OriginalActivity] FOREIGN KEY([OriginalActivityID])
REFERENCES [dbo].[MyActivity] ([ActivityID])
GO

ALTER TABLE [dbo].[Group] CHECK CONSTRAINT [FK_OriginalActivity]
GO

-- member資料表

CREATE TABLE [dbo].[Member](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Nickname] [nvarchar](50) NULL,
	[Account] [nvarchar](50) NULL,
	[Password] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[Phone] [nvarchar](20) NULL,
	[Intro] [nvarchar](max) NULL,
	[UserPhoto] [varbinary](max) NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Account] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Nickname] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Member] ADD  DEFAULT ((0)) FOR [IsActive]
GO

--官方活動
CREATE TABLE [dbo].[MyActivity](
	[ActivityID] [int] IDENTITY(1,1) NOT NULL,
	[ActivityName] [nvarchar](255) NULL,
	[Category] [nvarchar](255) NULL,
	[SuggestedAmount] [money] NULL,
	[ActivityContent] [nvarchar](max) NULL,
	[MinAttendee] [int] NULL,
	[VoteDate] [smalldatetime] NULL,
	[ExpectedDepartureMonth] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[ActivityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ActivityName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


--假資料

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
		   巍峨的奇萊,就這樣成了人們流連忘返、一再拜訪的熱門山區。',
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



--官方活動照片資料表
INSERT INTO [dbo].[OfficialPhoto]
           ([ActivityID]
           ,[PhotoPath])
     VALUES
           (1, '/IMG/眠月線軌道.JPG'),
		   (1, '/IMG/眠月線1.jpeg'),
		   (1, '/IMG/眠月線2.jpeg'),
		   (1, '/IMG/眠月線3.jpeg'),

		   (2 , '/IMG/奇萊北.JPG'),
		   (2 , '/IMG/奇萊北黃金.JPG'),
		   (2 , '/IMG/奇萊3.jpeg'),
		   (2 , '/IMG/奇萊4.jpeg'),

		   (3 , '/IMG/test1.JPG'),
		   (3 , '/IMG/大壩.JPG'),
		   (3 , '/IMG/大壩雪.jpg'),
		   (3 , '/IMG/大壩四.jpg'),

		   (4 , '/IMG/老梅1.jpeg'),
		   (4 , '/IMG/老梅2.jpeg'),
		   (4 , '/IMG/老梅3.jpeg'),
		   (4 , '/IMG/老梅4.jpeg'),

		   (5 , '/IMG/七孔1.jpeg'),
		   (5 , '/IMG/七孔2.jpg'),
		   (5 , '/IMG/七孔3.jpg'),
		   (5 , '/IMG/七孔4.jpg'),

		   (6 , '/IMG/半屏1.jpg'),
		   (6 , '/IMG/半屏2.jpeg'),
		   (6 , '/IMG/半屏3.jpeg'),
		   (6 , '/IMG/半屏4.jpeg'),

		   (7 , '/IMG/出水口1.jpeg'),
		   (7 , '/IMG/出水口2.jpeg'),
		   (7 , '/IMG/出水口3.jpeg'),
		   (7 , '/IMG/出水口4.png'),

		   (8 , '/IMG/小琉球1.jpg'),
		   (8 , '/IMG/小琉球2.jpg'),
		   (8 , '/IMG/小琉球3.jpg'),
		   (8 , '/IMG/小琉球4.jpg'),

		   (9 , '/IMG/綠島1.jpeg'),
		   (9 , '/IMG/綠島2.jpeg'),
		   (9 , '/IMG/綠島3.jpeg'),
		   (9 , '/IMG/綠島4.jpg'),

		   (10 , '/IMG/水樣1.jpeg'),
		   (10 , '/IMG/水樣2.jpeg'),
		   (10 , '/IMG/水樣3.jpeg'),
		   (10 , '/IMG/水樣4.jpg'),

		   (11 , '/IMG/半島1.jpeg'),
		   (11 , '/IMG/半島2.jpeg'),
		   (11 , '/IMG/半島3.jpeg'),
		   (11 , '/IMG/半島4.jpeg'),

		   (12 , '/IMG/踏浪1.jpeg'),
		   (12 , '/IMG/踏浪2.jpeg'),
		   (12 , '/IMG/踏浪3.jpeg'),
		   (12 , '/IMG/踏浪4.jpeg')
		   
GO


--會員資料表
USE [ProjectX]
GO

INSERT INTO [dbo].[Member]
           ([Nickname]
           ,[Account]
           ,[Password]
           ,[Email]
           ,[Phone]
           ,[Intro]
           ,[UserPhoto]
           ,[IsActive])
     VALUES
	 ('吳大偉', '00000', '00000', '00000@gmail.com', 0911111111, NULL, NULL,1),
	 ('陳小明', '00001', '000011', 'chenxiaoming@email.com', 0911111111, NULL, NULL, 1),
	 ('林美玲', '00002', '00002', 'linmeiling@email.com', 0911111111, NULL, NULL, 1),
	 ('王雅琪', '00003', '00003', 'wangyaqi@email.com', 0911111111, NULL, NULL, 1),
	 ('黃志成', '00004', '00004', 'huangzhicheng@email.com', 0911111111, NULL, NULL, 1)


GO

--
