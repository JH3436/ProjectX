create database ProjectX
go

use ProjectX
go

--���ʸ�ƪ�
CREATE TABLE MyActivity (
    ActivityID INT identity(1,1) PRIMARY KEY,
    ActivityName NVARCHAR(255) UNIQUE, -- �ߤ@�����A�T�O���ʦW�٪��ߤ@��
    Category NVARCHAR(255),
    Map NVARCHAR(255),
    SuggestedAmount MONEY,
    ActivityContent NVARCHAR(MAX),
    MinAttendee INT,
    VoteDate SMALLDATETIME,
    ExpectedDepartureMonth DATE
);
go

--�|����ƪ�
CREATE TABLE Member (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Nickname NVARCHAR(50) UNIQUE,  
    Account NVARCHAR(50) UNIQUE,  
    [Password] NVARCHAR(255),
    Email NVARCHAR(255) UNIQUE,    
    Phone NVARCHAR(20) UNIQUE,
	Intro NVARCHAR(MAX),
	UserPhoto NVARCHAR(MAX) ,
    LoginMethod NVARCHAR(50)      
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
    DeadDate DATE,                   
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
CREATE TABLE ActivityLikes (
    LikeRecordID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT,
    ActivityID INT,
    CONSTRAINT FK_UserID FOREIGN KEY (UserID) REFERENCES [Member](UserID),
    CONSTRAINT FK_ActivityID FOREIGN KEY (ActivityID) REFERENCES MyActivity(ActivityID),
    CONSTRAINT UQ_UserActivityLike UNIQUE (UserID, ActivityID)
);
go

-- �q����ƪ�
CREATE TABLE Notification (
    NotificationID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL, 
    NotificationContent NVARCHAR(MAX) NOT NULL,
    IsRead BIT NOT NULL DEFAULT 0, --�w�]����Ū
    NotificationDate DATETIME default sysdatetime(),
    CONSTRAINT FK_User_Notification FOREIGN KEY (UserID) REFERENCES [Member](UserID),
	CONSTRAINT CK_IsRead CHECK (IsRead IN (0, 1)) -- IsRead�u��O0��1
);
go

--���ʷӤ���ƪ�
CREATE TABLE Photos (
    PhotoID INT PRIMARY KEY IDENTITY(1,1),
    ActivityID INT,
    GroupID INT,
    PhotoData NVARCHAR(max) NOT NULL,
    CONSTRAINT FK_Activity_Photo FOREIGN KEY (ActivityID) REFERENCES MyActivity(ActivityID),
    CONSTRAINT FK_Group_Photo FOREIGN KEY (GroupID) REFERENCES [Group](GroupID)
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

--�p���ڭ̪���
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

INSERT INTO [dbo].[Member]
           ([Nickname]
           ,[Account]
           ,[Password]
           ,[Email]
           ,[Phone]
           ,[Intro]
		   ,[UserPhoto]
           ,[LoginMethod])
     VALUES
           ('�޲z��', 0000000000, 0000000000, '0000000000@gmail.com', 0900000000, 'admin', null ,'�oԣ��'),
		   ('������', 1111111111, 1111111111, '1111111111@gmail.com', 0911111111, '�j�a�n�A�ڬO�������C�ڨӦۤ@�Ӭ��R�������A�b�o�̧ڦ��״I���g��M�ޯ�C�ڱM�`��n��}�o�A�þ֦��h�~���{���]�p�g��A�ר�b�������ε{���}�o�譱���״I�g��C', null ,'�oԣ��'),
		   ('003', 2222222222, 2222222222, '2222222222@gmail.com', 0922222222, '�j�a�n�A�ڬO003�C�ڨӦۤ@�Ӭ��R�������A�b�o�̧ڦ��״I���g��M�ޯ�C�ڱM�`��n��}�o�A�þ֦��h�~���{���]�p�g��A�ר�b�������ε{���}�o�譱���״I�g��C', null ,'�oԣ��'),
		   ('jerry', 3333333333, 3333333333, '3333333333@gmail.com', 0933333333, '�j�a�n�A�ڬOjerry�C�ڨӦۤ@�Ӭ��R�������A�b�o�̧ڦ��״I���g��M�ޯ�C�ڱM�`��n��}�o�A�þ֦��h�~���{���]�p�g��A�ר�b�������ε{���}�o�譱���״I�g��C', null ,'�oԣ��')
GO

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

INSERT INTO [dbo].[VoteTime]
           ([ActivityID]
           ,[StartDate]
           ,[DeadDate])
     VALUES
           (1, '2023-10-01', '2023-10-06')
GO



INSERT INTO [dbo].[VoteRecord]
           ([UserID]
           ,[ActivityID]
           ,[VoteResult])
     VALUES
           (1, 1, '2023-11-4'),
		   (2, 1, '2023-11-4'),
		   (3, 1, '2023-11-11')
GO


INSERT INTO [dbo].[Registration]
           ([GroupID]
           ,[ParticipantID])
     VALUES
           (1, 1),
		   (1, 2),
		   (1, 3)
GO



INSERT INTO [dbo].[ActivityLikes]
           ([UserID]
           ,[ActivityID])
     VALUES
           (1, 2),
		   (2, 2),
		   (3, 2)
GO



INSERT INTO [dbo].[Notification]
           ([UserID]
           ,[NotificationContent]
           ,[IsRead]
           ,[NotificationDate])
     VALUES
           (2, '�A���@�h�l�ܬ��ʻݭn�벼', 0, '2023-10-01')
GO



INSERT INTO [dbo].[Photos]
           ([ActivityID]
           ,[GroupID]
           ,[PhotoData])
     VALUES
           (1, null, '/IMG/��b��.jpg'),
		   (1, null, '/IMG/surf3.jpg'),
		   (1, null, '/IMG/surf2.jpg'),
		   (1, null, '/IMG/surf.jpg')
GO



INSERT INTO [dbo].[Chat]
           ([ActivityID]
           ,[UserID]
           ,[ChatContent]
           ,[ToWhom]
           ,[ChatTime])
     VALUES
           (1, 1, '�d���O��ܴ���', null, SYSDATETIME()),
		   (1, 1, '�d���O�^�д���', 1, SYSDATETIME()),
		   (1, 1, '�d���O��ܴ���2', null, SYSDATETIME()),
		   (1, 1, '�d���O�^�д���2', 3, SYSDATETIME())
GO
