create database ProjectX
go

use ProjectX
go

--���ʸ�ƪ�
CREATE TABLE MyActivity (
    ActivityID INT identity(1,1) PRIMARY KEY,
    ActivityName NVARCHAR(255) UNIQUE, -- �ߤ@�����A�T�O���ʦW�٪��ߤ@��
    Category NVARCHAR(255),
    SuggestedAmount MONEY,
    ActivityContent NVARCHAR(MAX),
    MinAttendee INT,
    VoteDate SMALLDATETIME,
    ExpectedDepartureMonth DATE
);
go

--�|����ƪ�
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

--���θ�ƪ�
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

--�벼�ɶ���ƪ�
CREATE TABLE VoteTime (
	VoteID INT IDENTITY(1,1) PRIMARY KEY,        
    ActivityID INT,               
    StartDate DATE,               
    CONSTRAINT FK_VoteTime_Activity FOREIGN KEY (ActivityID) REFERENCES MyActivity(ActivityID)
);
go

--�벼������ƪ�
CREATE TABLE VoteRecord (
    RecordID INT IDENTITY(1,1) PRIMARY KEY ,
    UserID INT,
    ActivityID INT,
    VoteResult DATE,
    CONSTRAINT FK_VoteRecord_UserID FOREIGN KEY (UserID) REFERENCES [Member](UserID),
    CONSTRAINT FK_VoteRecord_ActivityID FOREIGN KEY (ActivityID) REFERENCES MyActivity(ActivityID)
);
go

--���W������ƪ�
CREATE TABLE Registration (
    RegistrationID INT IDENTITY(1,1) PRIMARY KEY ,
    GroupID INT,
    ParticipantID INT,
    CONSTRAINT FK_Registration_Group FOREIGN KEY (GroupID) REFERENCES [Group](GroupID),
    CONSTRAINT FK_Registration_User FOREIGN KEY (ParticipantID) REFERENCES [Member](UserID)
);
go

--���ʦ��ø�ƪ�
CREATE TABLE LikeRecord (
    LikeRecordID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT,
    ActivityID INT,
    CONSTRAINT FK_UserID FOREIGN KEY (UserID) REFERENCES [Member](UserID),
    CONSTRAINT FK_ActivityID FOREIGN KEY (ActivityID) REFERENCES MyActivity(ActivityID),
    CONSTRAINT UQ_UserActivityLike UNIQUE (UserID, ActivityID)
);
go

-- �q����ƪ�
CREATE TABLE [Notification] (
    NotificationID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL, 
    NotificationContent NVARCHAR(MAX) NOT NULL,
    IsRead BIT NOT NULL DEFAULT 0, --�w�]����Ū
    NotificationDate DATETIME default sysdatetime(),
	NotificationType nvarchar(25),
	NotificationToWhichActivityID INT,
    CONSTRAINT FK_User_Notification FOREIGN KEY (UserID) REFERENCES [Member](UserID),
	CONSTRAINT CK_IsRead CHECK (IsRead IN (0, 1)) -- IsRead�u��O0��1
);
go

--�x�謡�ʷӤ���ƪ�
CREATE TABLE OfficialPhoto (
    OfficialPhotoID INT PRIMARY KEY IDENTITY(1,1),
    ActivityID INT,
    PhotoPath NVARCHAR(max) NOT NULL,
    CONSTRAINT FK_Activity_Photo FOREIGN KEY (ActivityID) REFERENCES MyActivity(ActivityID),
);
go

--�ӤH�}�ά��ʷӤ���ƪ�
CREATE TABLE PersonalPhoto (
    PersonalPhotoID INT PRIMARY KEY IDENTITY(1,1),
    GroupID INT,
    PhotoData VARBINARY(MAX) NOT NULL,
    CONSTRAINT FK_PersonalPhoto_Group FOREIGN KEY (GroupID) REFERENCES [Group](GroupID),
);
go

--�d���O��ƪ�
CREATE TABLE Chat (
    ChatID INT IDENTITY(1,1) PRIMARY KEY,  
    ActivityID INT,               
    UserID INT,                 
    ChatContent NVARCHAR(MAX) NOT NULL,    
    ToWhom INT,                            
    ChatTime DATETIME DEFAULT SYSDATETIME(),
    FOREIGN KEY (ActivityID) REFERENCES [Group](GroupID),
    FOREIGN KEY (UserID) REFERENCES [Member](UserID),
	-- �~������A�Ѧүd���OID (ChatID) �@���^�й�H
    FOREIGN KEY (ToWhom) REFERENCES Chat(ChatID)
);
go

--�p���ڭ̪��
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

--�����

--���ʸ�ƪ� --10/4�p�D:�h�s�W�F�Ӱ���Ƶ��P���P��votedate����
INSERT INTO [dbo].[MyActivity]
           ([ActivityName]
           ,[Category]
           ,[SuggestedAmount]
           ,[ActivityContent]
           ,[MinAttendee]
           ,[VoteDate]
           ,[ExpectedDepartureMonth])
     VALUES
           ('�����Ŭu�g����',
           '����',
           100,
           '���צ��S����~���ʪ��g��
		   ���D�`���˪�����I
		   ����e�нm�|���ԲӸѻ��˳ƪ��ϥΤ覡�A
		   ���H�P��D�`�w�ߡA
		   �b���˪��L�{���A
		   �]�|�Ŷq�έ����{�����p�A
		   �ܤƷ��ˤ覡�A�D�`����I',
           3,
           '2023-10-15 23:59:59' ,
           '2024-01-01'),
		   ('�_�ܥD�_�����',
           '�n�s',
           100,
           '�A����L�ǻ������¦�_�ܶܡH
		   �q�X�w�s������b�n��P�Ὤ�������_�ܤs�ɡA�I�ۥ����s����o���t����,
		   �ӥL��������B�ܤ۲�������ԧ����s���ϩ��X�W�F�@�h����������,
		   �I�m���a�աB�W�Ǫ������G�ƨϱo�u�¦�_�ܡv�n�W����,
		   �b���˪��L�{��,
		   �H�۰�a���骺���ߡA�V�ӶV�h�H�񨬩_�ܡB���}�L����������,
		   �ޮo���_��,�N�o�˦��F�H�̬y�s�Ѫ�B�@�A���X�������s��',
           3,
           '2023-11-15 23:59:59',
           '2024-02-01'),
		   ('���B�ۥѼ��',
           '���',
           100,
           '�p�G�z���|��a�B�����Ȥ��A��ĳ�z��ܪ�ŦۥѼ���A�ҵ{���A���ۥѼ�������ʡF�L�{�|�m���R�A�M�ʺA����B�n������B�ۥѤU�絥�ޥ��C',
           3,
           '2023-12-15 23:59:59',
           '2024-03-01')
GO

--�|����ƪ�   --10/17 ��Ӹ��K�X��²��@�I����n���աA�쥻���|������R���A�]�����~��Ѧ� ~By James
INSERT INTO [dbo].[Member]
([Nickname], [Account], [Password], [Email], [Phone], [Intro], [UserPhoto])
VALUES
('�s���޲z��', 'admin', '1234', 'admin@gmail.com', '0900000000', 'admin', null),
('2���ϥΪ�', 'no2', '1234', 'number2@gmail.com', '0911111111', '�j�a�n�A�ڬO2���ϥΪ̡C�ڨӦۤ@�Ӭ��R�������A�b�o�̧ڦ��״I���g��M�ޯ�C�ڱM�`��n��}�o�A�þ֦��h�~���{���]�p�g��A�ר�b�������ε{���}�o�譱���״I�g��C', null),
('3���ϥΪ�', 'no3', '1234', 'number3@gmail.com', '0922222222', '�j�a�n�A�ڬO3���ϥΪ̡C�ڨӦۤ@�Ӭ��R�������A�b�o�̧ڦ��״I���g��M�ޯ�C�ڱM�`��n��}�o�A�þ֦��h�~���{���]�p�g��A�ר�b�������ε{���}�o�譱���״I�g��C', null),
('�o�O�|��', 'no4', '1234', 'number4@gmail.com', '0933333333', '�j�a�n�A�o�O�|���C�ڨӦۤ@�Ӭ��R�������A�b�o�̧ڦ��״I���g��M�ޯ�C�ڱM�`��n��}�o�A�þ֦��h�~���{���]�p�g��A�ר�b�������ε{���}�o�譱���״I�g��C', null);
go

--���θ�ƪ�
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
           ('�����s�n�s', '�n�s', '123��x�W', 2, 10, '2024-01-01', '2024-01-05', 1, null),
		   ('�����Ŭu�g����', '����', '���צ��S����~���ʪ��g��
		   ���D�`���˪�����I
		   ����e�нm�|���ԲӸѻ��˳ƪ��ϥΤ覡�A
		   ���H�P��D�`�w�ߡA
		   �b���˪��L�{���A
		   �]�|�Ŷq�έ����{�����p�A
		   �ܤƷ��ˤ覡�A�D�`����I', 3, null, '2024-01-01', '2024-01-05', 2, 1)

GO

--�벼�ɶ���ƪ�
INSERT INTO [dbo].[VoteTime]
           ([ActivityID]
           ,[StartDate])
     VALUES
           (1, '2023-09-30'),
           (1, '2023-10-07'),
           (1, '2023-09-14'),
           (1, '2023-09-21')
GO


--�벼������ƪ�
INSERT INTO [dbo].[VoteRecord]
           ([UserID]
           ,[ActivityID]
           ,[VoteResult])
     VALUES
           (1, 1, '2023-09-21'),
		   (2, 1, '2023-09-14'),
		   (3, 1, '2023-09-21')
GO

--���W������ƪ� --10/4�p�D:�����|��1�h���F�X�Ӹոլݰj��
INSERT INTO [dbo].[Registration]
           ([GroupID]
           ,[ParticipantID])
     VALUES
           (1, 1),
		   (1, 2),
		   (1, 3)

GO


--���ʦ��ø�ƪ�   --10/4�p�D:�����|��1�h���F�X�Ӹոլݰj��
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


-- �q����ƪ�
INSERT INTO [dbo].[Notification]
           ([UserID]
           ,[NotificationContent], [NotificationType])
     VALUES
           (2, '�A���@�h�l�ܬ��ʻݭn�벼', 'Vote'),
		   (1,'���H�^�ФF�z���T��!', 'Chat')
GO


--�x�謡�ʷӤ���ƪ�
INSERT INTO [dbo].[OfficialPhoto]
           ([ActivityID]
           ,[PhotoPath])
     VALUES
           (1, '/IMG/��b��.jpg'),
		   (1, '/IMG/surf3.jpg'),
		   (1, '/IMG/surf2.jpg'),
		   (1, '/IMG/surf.jpg')
GO

--�ӤH�}�ά��ʷӤ���ƪ�
--���ݭn����ơA�����b�����W�ާ@�O�_��g�J/Ū����ƪ�Y�i

--�d���O��ƪ�
INSERT INTO [dbo].[Chat]
           ([ActivityID]
           ,[UserID]
           ,[ChatContent]
           ,[ToWhom])
     VALUES
           (1, 1, '�d���O��ܴ���', null),
		   (1, 1, '�d���O�^�д���', 1),
		   (1, 1, '�d���O��ܴ���2', null),
		   (1, 1, '�d���O�^�д���2', 3)
GO


--
