-- �Ыظ�Ʈw�]�p�G�|���s�b�^
CREATE DATABASE PhotoUploadDB;

-- �ϥέ��Ыت���Ʈw
USE PhotoUploadDB;

-- �Ы� Photos ��ƪ�
CREATE TABLE Photos
(
    PhotoId INT PRIMARY KEY IDENTITY(1,1),
    PhotoName NVARCHAR(MAX),
    ImageData VARBINARY(MAX)
);
