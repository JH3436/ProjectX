
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

--group��ƪ�
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

-- member��ƪ�

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

--�x�謡��
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
           ('�����s�v��u�G��C',
           '�n�s',
           500,
           '�v��u�A���Ÿq�������s�m�A�u�~�������u���A
		   �B�֦��˪L�B�K�D�B�����B�����K�����h�����[�A�B�Y�ץ��w�A
		   �樫���ץ�����H�A�p���w�g�����F�`���C�ȳ߷R���������u�C',
           3,
           '2023-10-24' ,
           '2023-12-01'),
		   ('�_�ܥD�_�����',
           '�n�s',
           1000,
           '�A����L�ǻ������¦�_�ܶܡH
		   �q�X�w�s������b�n��P�Ὤ�������_�ܤs�ɡA�I�ۥ����s����o���t����,
		   �ӥL��������B�ܤ۲�������ԧ����s���ϩ��X�W�F�@�h����������,
		   �I�m���a�աB�W�Ǫ������G�ƨϱo�u�¦�_�ܡv�n�W����,
		   �b���˪��L�{��,
		   �H�۰�a���骺���ߡA�V�ӶV�h�H�񨬩_�ܡB���}�L����������,
		   �ޮo���_��,�N�o�˦��F�H�̬y�s�Ѫ�B�@�A���X�������s�ϡC',
           3,
           '2023-11-15',
           '2024-01-01'),
		   ('�j�p�Q�����',
           '�n�s',
           1000,
           '�p�G���t�W�u�O�L�ڦb���s�s�ߤW�����s�A�j�p�Q�N�O�o�����s�����աA�����ڻy�uBabo Papak�v�A�O���դj�s���N��C�ɮL�ں٥��uKapatalayan�v�C�����~�ں٥��u���s��s�v�A�M�O�W�ֹϩw�W�u�j�Q�y�s�v�C

�s�լ�a�ް_�M�p�t�ϡA�s�ΧN�m��է����A�����u�@���_�p�v���١C�o�̬O�ǻ��������ڻP�ɮL�ڪ��@�P�_���a�A���ܬG�Ƥ��L�̪��������O�q�o�̨��U�Ӫ��A�]�]���y�ǵ۳\�h�T�ҡC',
           5,
           '2023-12-10',
           '2024-01-01'),

		   ('�۪��ѱ��˷���',
           '����',
           1000,
           '�ѱ��˦��s�_���۪��m�A�O�x�_�a�Ϥ��i�h�o���n�����˦a�I�A�ӥB���x�_���϶Ȥ@�p�ɨ��{�C�ѱ��˪��W��S����a�B�i�ޡB�شӡA���赴�ﰮ�b�A�s�L�]�ܧ���B��l�C
		   ���˹L�{���ٷ|���@�q20��������l�O�L��V�C',
           5,
           '2023-12-10',
           '2024-01-01'),

		   ('�C���r��',
           '����',
           20,
           '�C���r�� ��b�̪F�����w�m�A�@���C�h�r���C�Z���B�ѵ��30���������{�C�ڤW���b2013�~���X�A�ݥI�C�H20NT�������O�ΡC
			�u�ݭn����2�����Y�i��F�r���C�q���̡A��÷�����W���O�̦n�����A��}�åΡA�������A�@�@��
			�X�C�ӤաC�q������̫�@�h�O��200���ءC',
           5,
           '2023-12-10',
           '2024-01-01'),

		   ('�b�̷˷���',
           '����',
           20,
           '���x�W�_���F�_�����b�̷ˡA���Y�X��u�b�̤s�v�C
		   �o��F���ڡB�E���A�H�e�����U���q���B���q�A�i�H�ݨ�H�e�M�Ҥu�t�P�o�ϹD������C�b�q�~���q�P�H�s�����h��A�b�̷˦����F�x�W�ּơA������H�L�H�ϡB�S���a�x�B�u�t���ؿv�����M���ˬy�A
		   �i�H�q�X���f�@�����W���A�u�~�|�g�L�ƭ��r��',
           5,
           '2024-02-10',
           '2024-03-01'),
		   ('���B�X���f���ͼ��',
           '���',
           500,
           '�b�X���f���W���A����M���A�������R�A�̤��������v�T�C�ӪF�������Y�Y�A��O���W���ʳ̬�ģ���ϰ�A�̭������X�G���O�Y�B��нm���n�������j���A�����ȤH�F�A���s�]�ܦh�A�����]�ܦh�A���O�����N�C�B�K�ءA�o�O��L���쳣�����ݨ쪺�C�A²���N�O�����ЫǤF�A�C�^�a�H�h�A�����N���Ф����C',
           5,
           '2024-02-10',
           '2024-03-01'),
		   ('�p�[�y�ۼ�',
           '���',
           1500,
           '�Ӥp�[�y�ۥѼ���̤j���S��N�O��ݨ�����t�A�B�@�~�|�u���ų��ܾA�X�U���C',
           5,
           '2024-02-10',
           '2024-03-01'),

		    ('��q�ۼ�',
           '���',
           1000,
           '��q�@�~�|�u���۷�A�X�i�������ʡA�����P�u�`�A�X�����I�|���Үt���C��V�u�]��10��4��^���F�_�u���v�T�A�A�X�b�a���n�������I�A�p�j�ըF�B�n�d�C',
           5,
           '2024-02-10',
           '2024-03-01'),

		   ('���y�˪L���䳥��',
           '�S��',
           500,
           '�u���y�˪L�v�]921�a�_���_�e�D�A�e���J�n�ϱo�쥻�;��Z�K������L�𸭭�s�A�e�{����K�F�P��l�@�몺�򭱭ˬM���_�S����I',
           5,
           '2024-02-10',
           '2024-03-01'),

		    ('�K�˳��S�� - �b�q����',
           '�S��',
           5000,
           '���������c���ͬ��A�a�ۤ@��������P���ߡA�ӥb�q���ҶK��۵M�A��������S�窺�ֽ�a�I
			�V�x�L�D����޴֧���b�O�B�ѹ��B�q�s�W�ߵ��ɹԡB���Ŧе��E�Q���ȡB�𶢮�A�ڭ̳����z�ǳƦn�o�I',
           5,
           '2024-02-10',
           '2024-03-01'),

		    ('����P���S��Camp',
           '�S��',
           5000,
           '�ӪὬ�����P���S��h�I�[�P�ݮ��Y��C�P�檺��X�A�b����P���S��@������ݨ�I',
           5,
           '2024-02-10',
           '2024-03-01')
GO



--�x�謡�ʷӤ���ƪ�
INSERT INTO [dbo].[OfficialPhoto]
           ([ActivityID]
           ,[PhotoPath])
     VALUES
           (1, '/IMG/�v��u�y�D.JPG'),
		   (1, '/IMG/�v��u1.jpeg'),
		   (1, '/IMG/�v��u2.jpeg'),
		   (1, '/IMG/�v��u3.jpeg'),

		   (2 , '/IMG/�_�ܥ_.JPG'),
		   (2 , '/IMG/�_�ܥ_����.JPG'),
		   (2 , '/IMG/�_��3.jpeg'),
		   (2 , '/IMG/�_��4.jpeg'),

		   (3 , '/IMG/test1.JPG'),
		   (3 , '/IMG/�j��.JPG'),
		   (3 , '/IMG/�j��.jpg'),
		   (3 , '/IMG/�j��|.jpg'),

		   (4 , '/IMG/�ѱ�1.jpeg'),
		   (4 , '/IMG/�ѱ�2.jpeg'),
		   (4 , '/IMG/�ѱ�3.jpeg'),
		   (4 , '/IMG/�ѱ�4.jpeg'),

		   (5 , '/IMG/�C��1.jpeg'),
		   (5 , '/IMG/�C��2.jpg'),
		   (5 , '/IMG/�C��3.jpg'),
		   (5 , '/IMG/�C��4.jpg'),

		   (6 , '/IMG/�b��1.jpg'),
		   (6 , '/IMG/�b��2.jpeg'),
		   (6 , '/IMG/�b��3.jpeg'),
		   (6 , '/IMG/�b��4.jpeg'),

		   (7 , '/IMG/�X���f1.jpeg'),
		   (7 , '/IMG/�X���f2.jpeg'),
		   (7 , '/IMG/�X���f3.jpeg'),
		   (7 , '/IMG/�X���f4.png'),

		   (8 , '/IMG/�p�[�y1.jpg'),
		   (8 , '/IMG/�p�[�y2.jpg'),
		   (8 , '/IMG/�p�[�y3.jpg'),
		   (8 , '/IMG/�p�[�y4.jpg'),

		   (9 , '/IMG/��q1.jpeg'),
		   (9 , '/IMG/��q2.jpeg'),
		   (9 , '/IMG/��q3.jpeg'),
		   (9 , '/IMG/��q4.jpg'),

		   (10 , '/IMG/����1.jpeg'),
		   (10 , '/IMG/����2.jpeg'),
		   (10 , '/IMG/����3.jpeg'),
		   (10 , '/IMG/����4.jpg'),

		   (11 , '/IMG/�b�q1.jpeg'),
		   (11 , '/IMG/�b�q2.jpeg'),
		   (11 , '/IMG/�b�q3.jpeg'),
		   (11 , '/IMG/�b�q4.jpeg'),

		   (12 , '/IMG/���1.jpeg'),
		   (12 , '/IMG/���2.jpeg'),
		   (12 , '/IMG/���3.jpeg'),
		   (12 , '/IMG/���4.jpeg')
		   
GO


--�|����ƪ�
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
	 ('�d�j��', '00000', '00000', '00000@gmail.com', 0911111111, NULL, NULL,1),
	 ('���p��', '00001', '000011', 'chenxiaoming@email.com', 0911111111, NULL, NULL, 1),
	 ('�L����', '00002', '00002', 'linmeiling@email.com', 0911111111, NULL, NULL, 1),
	 ('�����X', '00003', '00003', 'wangyaqi@email.com', 0911111111, NULL, NULL, 1),
	 ('���Ӧ�', '00004', '00004', 'huangzhicheng@email.com', 0911111111, NULL, NULL, 1)


GO

--
