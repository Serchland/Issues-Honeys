USE [HONEYS]
GO

-- SERCH00: UNDER REVISON
---- ************************************** [FUNCTION_TOTALCOMMENTS]
--IF OBJECT_ID('FUNCTION_TOTALCOMMENTS') IS NOT NULL 
--BEGIN  
--    DECLARE @Count int;
--    SELECT @Count = COUNT(@Id)
--    FROM ISSUEDETAILS
--    WHERE FK_IssueTracking_Id = @Id AND ACTION=1; 
--    RETURN @Count;
--END;
--ELSE BEGIN
--SELECT 'FUNCTION FUNCTION_TOTALCOMMENTS NOT EXISTS'
--END;

-- ************************************** [USERS]
IF OBJECT_ID('USERS') IS NOT NULL 
BEGIN
DROP TABLE USERS;
CREATE TABLE [USERS]
(
 [PK_UserTracking_Id] int IDENTITY (1, 1) NOT NULL ,
 [NAME]               varchar(50) NOT NULL ,
 [SURNAME]            varchar(50) NULL ,
 [GU]                 varchar(50) NOT NULL ,
 [CRTNDATE]           datetime NOT NULL ,


 CONSTRAINT [PK_UserTracking_Id] PRIMARY KEY CLUSTERED ([PK_UserTracking_Id] ASC)
);

END
ELSE BEGIN
SELECT 'USERS NOT EXISTS'
END;

-- ************************************** [LABELS]
IF OBJECT_ID('LABELS') IS NOT NULL 
BEGIN
DROP TABLE [LABELS];
CREATE TABLE [LABELS]
(
 [PK_LabelTracking_Id] int IDENTITY (1, 1) NOT NULL ,
 [NAME]                      varchar(50) NOT NULL ,
 [COLOR]                     varchar(50) NOT NULL ,
 [DESCRIPTION]               varchar(50) NOT NULL ,
 [CRTNUSER]                  int NOT NULL ,
 [CRTNDATE]                  varchar(50) NOT NULL ,


 CONSTRAINT [PK_LabelTracking_Id] PRIMARY KEY CLUSTERED ([PK_LabelTracking_Id] ASC)
);

END
ELSE BEGIN
SELECT 'TABLE [LABELS] NOT EXISTS'
END;

-- ************************************** [MILESTONES]
IF OBJECT_ID('MILESTONES') IS NOT NULL 
BEGIN
DROP TABLE [MILESTONES];
CREATE TABLE [MILESTONES]
(
 [PK_MilestoneTracking_Id] int IDENTITY NOT NULL ,
 [NUMBER]                  int NOT NULL ,
 [STATE]                   int NOT NULL ,
 [TITLE]                   varchar(50) NOT NULL ,
 [DESCRIPTION]             varchar(50) NOT NULL ,
 [CRTNUSER]                int NOT NULL ,
 [CRTNDATE]                varchar(50) NOT NULL ,


 CONSTRAINT [PK_MilestoneTracking_Id] PRIMARY KEY CLUSTERED ([PK_MilestoneTracking_Id] ASC)
);

END
ELSE BEGIN
SELECT 'TABLE [MILESTONES] NOT EXISTS'
END;


-- ************************************** [ISSUES]
IF OBJECT_ID('ISSUES') IS NOT NULL 
BEGIN

ALTER TABLE [ISSUEDETAILS] DROP CONSTRAINT [FK_IssueTracking_Id]

DROP TABLE [ISSUES]
CREATE TABLE [ISSUES]
(
 [PK_IssueTracking_Id]       int IDENTITY (1, 1) NOT NULL ,
 [NUMBER]                    int NOT NULL ,
 [CTRNUSER]                  int NOT NULL ,
 [TITLE]                     varchar(50) NOT NULL ,
 [BODY]                      varchar(max) NOT NULL ,
 [CLOSEDBY]                  int NULL ,
 [LABELS]                    varchar(50) NOT NULL ,
 [ASSIGNEES]                 varchar(50) NULL ,
 [MILESTONES]                varchar(50) NULL ,
 [COMMENTS]                  int NULL ,
 [CRTNDATE]                  datetime NOT NULL ,
 [CLOSEDDAY]                 datetime NULL ,
 [LASTUPDDATE]               datetime NULL ,
 [PROJECTS]                  varchar(50) NULL ,
 [STATE]                     int NOT NULL CONSTRAINT [DF_ISSUES_STATE] DEFAULT 0 ,
 [TOTALCOMMENTS]             AS [dbo].[FUNCTION_TOTALCOMMENTS]([PK_IssueTracking_Id]),


 CONSTRAINT [PK_IssueDetailsTracking_Id] PRIMARY KEY CLUSTERED ([PK_IssueTracking_Id] ASC)
);
END
ELSE BEGIN
SELECT '[ISSUES] TABLE NOT EXIST'
END;

--************************************** [ISSUEDETAILS]
IF OBJECT_ID('ISSUEDETAILS') IS NOT NULL
BEGIN

ALTER TABLE USERCOMMENTS
DROP CONSTRAINT FK_IssueDetailTracking_Id;

DROP TABLE [ISSUEDETAILS];
CREATE TABLE [ISSUEDETAILS]
(
 [PK_IssueDetailTracking_Id] int IDENTITY (1, 1) NOT NULL ,
 [FK_IssueTracking_Id]   int NOT NULL ,
 [USERID]                    int NOT NULL ,
 [ACTION]                    int NOT NULL ,
 [CRTNDATE]                  datetime NOT NULL ,

 CONSTRAINT [PK_IssueDetailTracking_Id] PRIMARY KEY CLUSTERED ([PK_IssueDetailTracking_Id] ASC),
 CONSTRAINT [FK_IssueTracking_Id] FOREIGN KEY ([FK_Issuetracking_Id]) REFERENCES [ISSUES]([PK_IssueTracking_Id])
);

END 
ELSE BEGIN 
SELECT 'TABLE [ISSUEDETAILS] NOT EXIST'
END;

-- ************************************** [USERCOMMENTS]
IF OBJECT_ID('USERCOMMENTS') IS NOT NULL
BEGIN
DROP TABLE [USERCOMMENTS];
CREATE TABLE [USERCOMMENTS]
(
 [PK_CommentTracking_Id]     int IDENTITY (1, 1) NOT NULL ,
 [comment]                   varchar(max) NOT NULL ,
 [FK_IssueDetailTracking_Id] int NOT NULL ,


 CONSTRAINT [PK_CommentTracking_Id] PRIMARY KEY CLUSTERED ([PK_CommentTracking_Id] ASC),
 CONSTRAINT [FK_IssueDetailTracking_Id] FOREIGN KEY ([FK_IssueDetailTracking_Id])  REFERENCES [ISSUEDETAILS]([PK_IssueDetailTracking_Id])
);
END

ELSE BEGIN
SELECT 'TABLE [USERCOMMENTS] NOT EXIST'
END

--************************************** 
--************************************** 
--************************************** 

-- POPULATE SECTION

--************************************** 
--************************************** 
--************************************** 


--************************************** POPULATE USERS
INSERT INTO [USERS]
           ([NAME]
           ,[SURNAME]
           ,[GU]
           ,[CRTNDATE])
     VALUES
           ('Brian',
		    'J. Kidwell',
			'222-70',
			GETDATE())
GO

INSERT INTO [USERS]
           ([NAME]
           ,[SURNAME]
           ,[GU]
           ,[CRTNDATE])
     VALUES
           ('Cora',
		    'L. Richards',
			'756-01',
			GETDATE())
GO

INSERT INTO [USERS]
           ([NAME]
           ,[SURNAME]
           ,[GU]
           ,[CRTNDATE])
     VALUES
           ('Richard',
		    'L. Swink',
			'574-10',
			GETDATE())
GO

INSERT INTO [USERS]
           ([NAME]
           ,[SURNAME]
           ,[GU]
           ,[CRTNDATE])
     VALUES
           ('Karen',
		    'A. McNally',
			'618-54',
			GETDATE())
GO

INSERT INTO [USERS]
           ([NAME]
           ,[SURNAME]
           ,[GU]
           ,[CRTNDATE])
     VALUES
           ('Ricky',
		    'S. Morey',
			'314-03',
			GETDATE())
GO

INSERT INTO [USERS]
           ([NAME]
           ,[SURNAME]
           ,[GU]
           ,[CRTNDATE])
     VALUES
           ('Eleanor',
		    'K. Olmstead',
			'155-58',
			GETDATE())
GO


--************************************** POPULATE LABELS
INSERT INTO [LABELS]
           ([NAME]
           ,[COLOR]
           ,[DESCRIPTION]
           ,[CRTNUSER]
           ,[CRTNDATE])
     VALUES
           ('bug',
		    '#FFFF0000',
			'Something isnt working',
			1,
			GETDATE())
GO
INSERT INTO [LABELS]
           ([NAME]
           ,[COLOR]
           ,[DESCRIPTION]
           ,[CRTNUSER]
           ,[CRTNDATE])
     VALUES
           ('enhancement',
		    '#FFADD8E6',
			'New feature or request',
			1,
			GETDATE())
GO

INSERT INTO [LABELS]
           ([NAME]
           ,[COLOR]
           ,[DESCRIPTION]
           ,[CRTNUSER]
           ,[CRTNDATE])
     VALUES
           ('question',
		    '#FFFFC0CB',
			'Further information is requested',
			1,
			GETDATE())
GO

INSERT INTO [LABELS]
           ([NAME]
           ,[COLOR]
           ,[DESCRIPTION]
           ,[CRTNUSER]
           ,[CRTNDATE])
     VALUES
           ('documentation',
		    '#FF08000',
			'Improvements or additions to documentation',
			1,
			GETDATE())
GO

INSERT INTO [LABELS]
           ([NAME]
           ,[COLOR]
           ,[DESCRIPTION]
           ,[CRTNUSER]
           ,[CRTNDATE])
     VALUES
           ('duplicate',
		    '#FFFFD700',
			'This issue already exists',
			1,
			GETDATE())
GO

INSERT INTO [LABELS]
           ([NAME]
           ,[COLOR]
           ,[DESCRIPTION]
           ,[CRTNUSER]
           ,[CRTNDATE])
     VALUES
           ('help wanted',
		    '#FF008000',
			'Extra attention is needed',
			1,
			GETDATE())
GO

INSERT INTO [LABELS]
           ([NAME]
           ,[COLOR]
           ,[DESCRIPTION]
           ,[CRTNUSER]
           ,[CRTNDATE])
     VALUES
           ('invalid',
		    '#FFF5F5DC',
			'This doesnt seem right',
			1,
			GETDATE())
GO

--************************************** POPULATE MILESTONES
INSERT INTO [MILESTONES]
           ([NUMBER]
           ,[STATE]
           ,[TITLE]
           ,[DESCRIPTION]
           ,[CRTNUSER]
           ,[CRTNDATE])
     VALUES
           (1,
			0,
            'SEND PRO V1.12',
            'SENDPRO',
            3,
            GETDATE())
GO

INSERT INTO [dbo].[MILESTONES]
           ([NUMBER]
           ,[STATE]
           ,[TITLE]
           ,[DESCRIPTION]
           ,[CRTNUSER]
           ,[CRTNDATE])
     VALUES
           (1,
			0,
            'SEND PRE V1.12',
            'SEND PRE',
            3,
            GETDATE())
GO

INSERT INTO [dbo].[MILESTONES]
           ([NUMBER]
           ,[STATE]
           ,[TITLE]
           ,[DESCRIPTION]
           ,[CRTNUSER]
           ,[CRTNDATE])
     VALUES
           (1,
			0,
            'SEND FOR V1.12',
            'SEND FOR',
            3,
            GETDATE())
GO

--************************************** POPULATE ISSUES
INSERT INTO [ISSUES]
           ([NUMBER]
           ,[CTRNUSER]
           ,[TITLE]
           ,[BODY]
           ,[CLOSEDBY]
           ,[LABELS]
           ,[ASSIGNEES]
           ,[MILESTONES]
           ,[COMMENTS]
           ,[CRTNDATE]
           ,[CLOSEDDAY]
           ,[LASTUPDDATE]
           ,[PROJECTS])
     VALUES
           (1,
            3,
            'IS51742',
            'Allow adding xml files',
            NULL,
            '1;2',
            '4',
            NULL,
            2,
            GETDATE(),
            NULL,
            NULL,
            1)
GO


INSERT INTO [ISSUES]
           ([NUMBER]
           ,[CTRNUSER]
           ,[TITLE]
           ,[BODY]
           ,[CLOSEDBY]
           ,[LABELS]
           ,[ASSIGNEES]
           ,[MILESTONES]
           ,[COMMENTS]
           ,[CRTNDATE]
           ,[CLOSEDDAY]
           ,[LASTUPDDATE]
           ,[PROJECTS])
     VALUES
           (2,
            5,
            'IS98893',
            'Create DARK Theme',
            NULL,
            '3',
            '2',
            NULL,
            1,
            GETDATE(),
            NULL,
            NULL,
            1)
GO

INSERT INTO [ISSUES]
           ([NUMBER]
           ,[CTRNUSER]
           ,[TITLE]
           ,[BODY]
           ,[CLOSEDBY]
           ,[LABELS]
           ,[ASSIGNEES]
           ,[MILESTONES]
           ,[COMMENTS]
           ,[CRTNDATE]
           ,[CLOSEDDAY]
           ,[LASTUPDDATE]
           ,[PROJECTS])
     VALUES
           (2,
            5,
            'IS88876',
            'Create LIGHT Theme',
            NULL,
            '2',
            '4',
            NULL,
            1,
            GETDATE(),
            NULL,
            NULL,
            1)
GO

INSERT INTO [ISSUES]
           ([NUMBER]
           ,[CTRNUSER]
           ,[TITLE]
           ,[BODY]
           ,[CLOSEDBY]
           ,[LABELS]
           ,[ASSIGNEES]
           ,[MILESTONES]
           ,[COMMENTS]
           ,[CRTNDATE]
           ,[CLOSEDDAY]
           ,[LASTUPDDATE]
           ,[PROJECTS])
     VALUES
           (2,
            5,
            'IS12453',
            'Allow only one instance of the application',
            NULL,
            '2',
            '2',
            NULL,
            1,
            GETDATE(),
            NULL,
            NULL,
            1)
GO

--************************************** POPULATE ISSUEDETAILS
INSERT INTO [dbo].[ISSUEDETAILS]
           ([FK_IssueTracking_Id]
           ,[USERID]
           ,[ACTION]
           ,[CRTNDATE])
     VALUES
           (1,
            1,
            1,
            GETDATE())
GO
