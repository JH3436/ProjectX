create database ProjectX
go

use ProjectX
go

--���ʸ�ƪ�
CREATE TABLE Activity (
    ActivityID INT identity(1,1) PRIMARY KEY,
    ActivityName NVARCHAR(255) UNIQUE, -- �ߤ@�����A�T�O���ʦW�٪��ߤ@��
    Category NVARCHAR(255),
    Map NVARCHAR(255),
    SuggestedAmount MONEY,
    ActivityContent NVARCHAR(MAX),
    MinAttendee INT,
    VoteDate DATETIME,
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
    LoginMethod NVARCHAR(50)      
);
go

--���θ�ƪ�
CREATE TABLE ActivityAdded (
    ActivityID INT identity(1,1) PRIMARY KEY,
    ActivityName NVARCHAR(255) UNIQUE, -- �ߤ@�����A�T�O���ʦW�٪��ߤ@��
    Category NVARCHAR(50),
    ActivityContent NVARCHAR(MAX),
    MinAttendee INT,
    MaxAttendee INT,
    StartDate DATETIME,
    EndDate DATETIME,
    Organizer INT FOREIGN KEY REFERENCES Member(UserID)
);
go

--�벼�ɶ���ƪ�
--�]�w�~�����(FK_VoteTime_Activity)�ѦҨ쬡�ʸ�ƪ�����ID
CREATE TABLE VoteTime (
	VoteID INT PRIMARY KEY,        
    ActivityID INT,               
    StartDate DATE,               
    VoteCount INT,                   
    CONSTRAINT FK_VoteTime_Activity FOREIGN KEY (ActivityID) REFERENCES Activity(ActivityID)
);
go

--�벼������ƪ�
CREATE TABLE VoteRecord (
    RecordID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
    ActivityID INT,
    VoteResult DATE,
    CONSTRAINT FK_VoteRecord_UserID FOREIGN KEY (UserID) REFERENCES Member(UserID),
    CONSTRAINT FK_VoteRecord_ActivityID FOREIGN KEY (ActivityID) REFERENCES Activity(ActivityID)
);
go





