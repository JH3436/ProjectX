ALTER TABLE Notification
ADD NotificationToWhichActivityID INT;



-- �q����ƪ�
INSERT INTO [dbo].[Notification]
           ([UserID]
           ,[NotificationContent], [NotificationType], [NotificationToWhichActivityID])
     VALUES
		   (1,'���ʳs������', 'Vote', 1)
GO

UPDATE Chat
SET UserID = 3
WHERE ChatID = 22;

UPDATE Notification
SET NotificationToWhichActivityID = 1
WHERE NotificationID <= 1045;

delete from dbo.Notification where NotificationID>53

delete from dbo.voterecord

INSERT INTO [dbo].[VoteRecord]
           ([UserID]
           ,[ActivityID]
           ,[VoteResult])
     VALUES
           (1, 1, '2023-10-07')

