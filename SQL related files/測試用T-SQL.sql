ALTER TABLE Notification
ADD NotificationToWhichActivityID INT;

ALTER TABLE [Notification]
ADD NotificationType nvarchar(25);

INSERT INTO [dbo].[VoteTime]
           ([ActivityID]
           ,[StartDate])
     VALUES
           (4, '2023-11-04'),
           (4, '2023-11-11'),
           (4, '2023-11-18'),
           (4, '2023-11-25')
GO

-- 通知資料表
INSERT INTO [dbo].[Notification]
           ([UserID]
           ,[NotificationContent], [NotificationType], [NotificationToWhichActivityID])
     VALUES
		   (1,'活動連接測試', 'Vote', 1)
GO

UPDATE Chat
SET UserID = 3
WHERE ChatID = 22;

UPDATE Notification
SET NotificationToWhichActivityID = 1
--WHERE NotificationID <= 1045;

UPDATE Notification
SET NotificationType = 'Vote';

update Myactivity
set ActivityName = '10/17 T-SQ建立測試' where ActivityID = 4

delete from dbo.Member

delete from dbo.voterecord

INSERT INTO [dbo].[VoteRecord]
           ([UserID]
           ,[ActivityID]
           ,[VoteResult])
     VALUES
           (1, 4, '2023-11-04'),
		   (2, 4, '2023-11-04'),
		   (3, 4, '2023-11-04'),
		   (4, 4, '2023-11-11'),
		   (5, 4, '2023-11-11'),
		   (6, 4, '2023-11-25')

INSERT INTO [dbo].[LikeRecord]
           ([UserID]
           ,[ActivityID])
     VALUES
           (2, 1)
		   

UPDATE dbo.LikeRecord
SET UserID=1004 where LikeRecordID=2010;

delete from LikeRecord 