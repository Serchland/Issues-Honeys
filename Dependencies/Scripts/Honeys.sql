USE [HONEYS]

go

-- **************************************
-- **************************************
--				[CREATE SHEMA]
-- **************************************
-- **************************************
IF NOT EXISTS (SELECT * FROM   sys.schemas WHERE  NAME = 'issues')
  BEGIN
      SELECT 'SCHEMA [issues] not EXITS. Creating'
      EXEC('CREATE SCHEMA issues')
  END
ELSE
  BEGIN
      SELECT 'SCHEMA [issues] EXITS'
  END;

GO

---- SERCH00: UNDER REVISON
------ ************************************** [FUNCTION_TOTALCOMMENTS]
--IF OBJECT_ID('FUNCTION_TOTALCOMMENTS') IS NOT NULL 
--CREATE FUNCTION [issues].[FUNCTION_TOTALCOMMENTS] (@Id int)  
--RETURNS int
--AS  
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
-- **************************************
-- **************************************
--				[USERS]
-- **************************************
-- **************************************
IF Object_id('[issues].[USERS]') IS NOT NULL
  BEGIN
      DROP TABLE [issues].[USERS];
      CREATE TABLE [issues].[USERS]
        (
           [CRTNDATE]           DATETIME NOT NULL,
           [GU]                 VARCHAR(50) NOT NULL,
           [NAME]               VARCHAR(50) NOT NULL,
           [PK_Usertracking_Id] INT IDENTITY (1, 1) NOT NULL,
           [SURNAME]            VARCHAR(50) NULL,
           CONSTRAINT [PK_UserTracking_Id] PRIMARY KEY CLUSTERED (
           [PK_Usertracking_Id]
           ASC)
        );

      SELECT 'TABLE [issues].[USERS] REGENERATED'
  END
ELSE
  BEGIN
      SELECT 'USERS NOT EXISTS... CREATING TABLE'
      CREATE TABLE [issues].[USERS]
        (
           [CRTNDATE]           DATETIME NOT NULL,
           [GU]                 VARCHAR(50) NOT NULL,
           [NAME]               VARCHAR(50) NOT NULL,
           [PK_Usertracking_Id] INT IDENTITY (1, 1) NOT NULL,
           [SURNAME]            VARCHAR(50) NULL,
           CONSTRAINT [PK_UserTracking_Id] PRIMARY KEY CLUSTERED (
           [PK_Usertracking_Id]
           ASC)
        );

      SELECT 'TABLE [issues].[USERS] CREATED'
  END;

-- **************************************
-- **************************************
--				[LABELS]
-- **************************************
-- **************************************
IF Object_id('[issues].[LABELS]') IS NOT NULL
  BEGIN
      DROP TABLE [issues].[LABELS];
      CREATE TABLE [issues].[LABELS]
        (
           [DESCRIPTION]         VARCHAR(50) NOT NULL,
           [COLOR]               VARCHAR(50) NOT NULL,
           [CRTNDATE]            DATETIME NOT NULL,
           [CRTNUSER]            INT NOT NULL,
           [Pk_LabelTracking_Id] INT IDENTITY (1, 1) NOT NULL,
           [NAME]                VARCHAR(50) NOT NULL,
           CONSTRAINT [PK_LabelTracking_Id] PRIMARY KEY CLUSTERED (
           [Pk_LabelTracking_Id] 
		   ASC)
        );

      SELECT 'TABLE [issues].[LABELS] REGENERATED'
  END
ELSE
  BEGIN
      SELECT 'TABLE [issues].[LABELS] NOT EXISTS... CREATING TABLE'
      CREATE TABLE [issues].[LABELS]
        (
           [DESCRIPTION]         VARCHAR(50) NOT NULL,
           [COLOR]               VARCHAR(50) NOT NULL,
           [CRTNDATE]            DATETIME NOT NULL,
           [CRTNUSER]            INT NOT NULL,
           [Pk_LabelTracking_Id] INT IDENTITY (1, 1) NOT NULL,
           [NAME]                VARCHAR(50) NOT NULL,
           CONSTRAINT [PK_LabelTracking_Id] PRIMARY KEY CLUSTERED (
           [Pk_LabelTracking_Id] 
		   ASC)
        );

      SELECT 'TABLE [issues].[LABELS] CREATED'
  END;

-- **************************************
-- **************************************
--				[MILESTONES]
-- **************************************
-- **************************************
IF Object_id('[issues].[MILESTONES]') IS NOT NULL
  BEGIN
      DROP TABLE [issues].[MILESTONES];
      CREATE TABLE [issues].[MILESTONES]
        (
           [CRTNDATE]                DATETIME NOT NULL,
           [CRTNUSER]                INT NOT NULL,
           [DESCRIPTION]             VARCHAR(50) NOT NULL,
           [NUMBER]                  INT NOT NULL,
           [Pk_MilestoneTracking_Id] INT IDENTITY NOT NULL,
           [STATE]                   INT NOT NULL,
           [TITLE]                   VARCHAR(50) NOT NULL,
           CONSTRAINT [PK_MilestoneTracking_Id] PRIMARY KEY CLUSTERED (
           [PK_MilestoneTracking_Id] 
		   ASC)
        );

      SELECT 'TABLE [issues].[MILESTONES] REGENERATED'
  END
ELSE
  BEGIN
      SELECT 'TABLE [issues].[MILESTONES] NOT EXISTS... CREATING TABLE'
      CREATE TABLE [issues].[MILESTONES]
        (
           [CRTNDATE]                DATETIME NOT NULL,
           [CRTNUSER]                INT NOT NULL,
           [DESCRIPTION]             VARCHAR(50) NOT NULL,
           [NUMBER]                  INT NOT NULL,
           [Pk_MilestoneTracking_Id] INT IDENTITY NOT NULL,
           [STATE]                   INT NOT NULL,
           [TITLE]                   VARCHAR(50) NOT NULL,
           CONSTRAINT [PK_MilestoneTracking_Id] PRIMARY KEY CLUSTERED (
           [PK_MilestoneTracking_Id] 
		   ASC)
        );

      SELECT 'TABLE [issues].[MILESTONES] CREATED'
  END;

-- **************************************
-- **************************************
--				[ISSUES]
-- **************************************
-- **************************************
IF Object_id('[issues].[ISSUES]') IS NOT NULL
  BEGIN
      ALTER TABLE [issues].[issuedetails]
      DROP CONSTRAINT [FK_IssueTracking_Id]
      DROP TABLE [issues].[issues]

      CREATE TABLE [issues].[ISSUES]
        (
           [ASSIGNEES]           VARCHAR(50) NULL,
           [BODY]                VARCHAR(max) NOT NULL,
           [CLOSEDBY]            INT NULL,
           [CLOSEDDAY]           DATETIME NULL,
           [CRTNDATE]            DATETIME NOT NULL,
           [CRTNUSER]            INT NOT NULL,
           [LABELS]              VARCHAR(50) NOT NULL,
           [LASTUPD]	         DATETIME NULL,
           [MILESTONES]          VARCHAR(50) NULL,
           [NUMBER]              INT NOT NULL,
           [Pk_IssueTracking_Id] INT IDENTITY (1, 1) NOT NULL,
           [PROJECTS]            VARCHAR(50) NULL,
           [STATE]               INT NOT NULL CONSTRAINT [DF_ISSUES_STATE] DEFAULT 0,
           [TITLE]               VARCHAR(50) NOT NULL,
           [TOTALCOMMENTS] AS
           [issues].[Function_TotalComments]([Pk_IssueTracking_Id]),
           CONSTRAINT [PK_IssueDetailsTracking_Id] PRIMARY KEY CLUSTERED (
           [Pk_IssueTracking_Id] ASC)
        );

      SELECT 'TABLE [issues].[ISSUES] REGENERATED'
  END
ELSE
  BEGIN
      SELECT '[issues].[ISSUES] TABLE NOT EXIST... CREATING TABLE'

      CREATE TABLE [issues].[ISSUES]
        (
           [ASSIGNEES]           VARCHAR(50) NULL,
           [BODY]                VARCHAR(max) NOT NULL,
           [CLOSEDBY]            INT NULL,
           [CLOSEDDAY]           DATETIME NULL,
           [CRTNDATE]            DATETIME NOT NULL,
           [CRTNUSER]            INT NOT NULL,
           [LABELS]              VARCHAR(50) NOT NULL,
           [LASTUPD]	         DATETIME NULL,
           [MILESTONES]          VARCHAR(50) NULL,
           [NUMBER]              INT NOT NULL,
           [Pk_IssueTracking_Id] INT IDENTITY (1, 1) NOT NULL,
           [PROJECTS]            VARCHAR(50) NULL,
           [STATE]               INT NOT NULL CONSTRAINT [DF_ISSUES_STATE] DEFAULT 0,
           [TITLE]               VARCHAR(50) NOT NULL,
           [TOTALCOMMENTS] AS
           [issues].[Function_TotalComments]([Pk_IssueTracking_Id]),
           CONSTRAINT [PK_IssueDetailsTracking_Id] PRIMARY KEY CLUSTERED (
           [Pk_IssueTracking_Id] ASC)
        );

      SELECT 'TABLE [issues].[ISSUES] CREATED'
  END;

-- **************************************
-- **************************************
--				[ISSUESDETAILS]
-- **************************************
-- **************************************
IF Object_id('[issues].[ISSUEDETAILS]') IS NOT NULL
  BEGIN
      ALTER TABLE [issues].[USERCOMMENTS]
      DROP CONSTRAINT Fk_IssueDetailTracking_Id;
      DROP TABLE [issues].[ISSUEDETAILS];

      CREATE TABLE [issues].[ISSUEDETAILS]
        (
           [ACTION]                    INT NOT NULL,
           [CRTNDATE]                  DATETIME NOT NULL,
           [Fk_IssueTracking_Id]       INT NOT NULL,
           [Pk_IssueDetailTracking_Id] INT IDENTITY (1, 1) NOT NULL,
           [USERID]                    INT NOT NULL,
           
		   CONSTRAINT [PK_IssueDetailTracking_Id] PRIMARY KEY CLUSTERED (
		   [PK_IssueDetailTracking_Id] ASC),
           
		   CONSTRAINT [FK_IssueTracking_Id] FOREIGN KEY ([FK_IssueTracking_Id])
           REFERENCES [issues].[ISSUES]([Pk_IssueTracking_Id])
        );

      SELECT 'TABLE [issues].[ISSUEDETAILS] REGENERATED'
  END
ELSE
  BEGIN
      SELECT 'TABLE [issues].[ISSUEDETAILS] NOT EXIST... CREATING TABLE'

      CREATE TABLE [issues].[ISSUEDETAILS]
        (
            [ACTION]                    INT NOT NULL,
			[CRTNDATE]                  DATETIME NOT NULL,
			[Fk_IssueTracking_Id]       INT NOT NULL,
			[Pk_IssueDetailTracking_Id] INT IDENTITY (1, 1) NOT NULL,
			[USERID]                    INT NOT NULL,
           
		   CONSTRAINT [PK_IssueDetailTracking_Id] PRIMARY KEY CLUSTERED (
		   [PK_IssueDetailTracking_Id] ASC),
           
		   CONSTRAINT [FK_IssueTracking_Id] FOREIGN KEY ([FK_IssueTracking_Id])
           REFERENCES [issues].[ISSUES]([Pk_IssueTracking_Id])
        );

      SELECT 'TABLE [issues].[ISSUEDETAILS] CREATED'
  END

-- **************************************
-- **************************************
--				[USERCOMMENTS]
-- **************************************
-- **************************************
IF Object_id('[issues].[USERCOMMENTS]') IS NOT NULL
  BEGIN
      DROP TABLE [issues].[USERCOMMENTS];

      CREATE TABLE [issues].[USERCOMMENTS]
        (
           [COMMENT]                   VARCHAR(max) NOT NULL,
           [Fk_IssueDetailTracking_Id] INT NOT NULL,
           [Pk_CommentTracking_Id]     INT IDENTITY (1, 1) NOT NULL,

           CONSTRAINT [PK_CommentTracking_Id] PRIMARY KEY CLUSTERED (
           [Pk_CommentTracking_Id] ASC),
           CONSTRAINT [FK_IssueDetailTracking_Id] FOREIGN KEY (
           [Fk_IssueDetailTracking_Id]) REFERENCES
           [issues].[ISSUEDETAILS]([Pk_IssueDetailTracking_Id])
        );

      SELECT 'TABLE [issues].[USERCOMMENTS] REGENERATED'
  END
ELSE
  BEGIN
      SELECT 'TABLE [issues].[USERCOMMENTS] NOT EXIST... CREATING TABLE'

      CREATE TABLE [issues].[USERCOMMENTS]
        (
           [COMMENT]                   VARCHAR(max) NOT NULL,
           [Fk_IssueDetailTracking_Id] INT NOT NULL,
           [Pk_CommentTracking_Id]     INT IDENTITY (1, 1) NOT NULL,

           CONSTRAINT [PK_CommentTracking_Id] PRIMARY KEY CLUSTERED (
           [Pk_CommentTracking_Id] ASC),
           CONSTRAINT [FK_IssueDetailTracking_Id] FOREIGN KEY (
           [Fk_IssueDetailTracking_Id]) REFERENCES
           [issues].[ISSUEDETAILS]([Pk_IssueDetailTracking_Id])
        );

      SELECT 'TABLE [issues].[USERCOMMENTS] CREATED'
  END

--************************************** 
--************************************** 
--************************************** 
--			POPULATE SECTION
--************************************** 
--************************************** 
--************************************** 
--************************************** POPULATE USERS
INSERT INTO [issues].[USERS]
            (
				[CRTNDATE],
				[GU],
				[NAME],
				[SURNAME])

VALUES      (
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				'99GU9889', 'Brian', 'J. Kidwell'
			)

go


INSERT INTO [issues].[USERS]
            (
				[CRTNDATE],
				[GU],
				[NAME],
				[SURNAME])
VALUES      (
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				'99GU9889', 'Cora', 'L. Richards'
			)

GO

INSERT INTO [issues].[USERS]
            (
				[CRTNDATE],
				[GU],
				[NAME],
				[SURNAME])
VALUES      (
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				'99GU9889', 'Richard', 'L. Swink'
			)

GO

INSERT INTO [issues].[USERS]
            (
				[CRTNDATE],
				[GU],
				[NAME],
				[SURNAME])
VALUES      (
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				'99GU9889', 'Karen', 'A. McNally'
			)

GO

INSERT INTO [issues].[USERS]
            (
				[CRTNDATE],
				[GU],
				[NAME],
				[SURNAME])
VALUES      
			(
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				'Ricky', 'S. Morey', 'A. McNally'
			)
GO

INSERT INTO [issues].[USERS]
            (
				[CRTNDATE],
				[GU],
				[NAME],
				[SURNAME])
VALUES      
			(
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				'Eleanor', 'K. Olmstead', 'A. McNally'
			)

GO

SELECT 'DUMMY USERS ADDED'

GO

--************************************** POPULATE LABELS
INSERT INTO [issues].[LABELS]
            (
				[DESCRIPTION],
				[COLOR],
				[CRTNDATE],
				[CRTNUSER],
				[NAME])
VALUES      (
				'Something isnt working', '#FFFF0000', 
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				1, 'bug'
			)

GO

INSERT INTO [issues].[LABELS]
            (
				[DESCRIPTION],
				[COLOR],
				[CRTNDATE],
				[CRTNUSER],
				[NAME])
VALUES      (
				'New feature or request', '#FFADD8E6',
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				1, 'enhancement'
			)

GO

INSERT INTO [issues].[LABELS]
            (
				[DESCRIPTION],
				[COLOR],
				[CRTNDATE],
				[CRTNUSER],
				[NAME])
VALUES      (
				'Further information is requested', '#FFFFC0CB',
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				1, 'question'
			)

GO

INSERT INTO [issues].[LABELS]
            (
				[DESCRIPTION],
				[COLOR],
				[CRTNDATE],
				[CRTNUSER],
				[NAME])
VALUES      (
				'This issue already exists', '#FFFFD700',
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				1, 'duplicate'
			)

GO

INSERT INTO [issues].[LABELS]
            (
				[DESCRIPTION],
				[COLOR],
				[CRTNDATE],
				[CRTNUSER],
				[NAME])
VALUES      (
				'Extra attention is needed', '#FF008000',
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				1, 'help wanted'
			)

GO

SELECT 'DUMMY LABELS ADDED'

GO

--************************************** POPULATE MILESTONES
INSERT INTO [issues].[MILESTONES]
            (
				[CRTNDATE],
				[CRTNUSER],
				[DESCRIPTION],
				[NUMBER],
				[STATE],
				[TITLE])
VALUES      (
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				2, 'SEND PRO V1.12', 0, 0, 'SENDPRO'
            )

GO

INSERT INTO [issues].[MILESTONES]
            (
				[CRTNDATE],
				[CRTNUSER],
				[DESCRIPTION],
				[NUMBER],
				[STATE],
				[TITLE])
VALUES      (
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				3, 'SEND PRE V1.12', 0, 0, 'SEND PRE'
            )

GO

INSERT INTO [issues].[MILESTONES]
            (
				[CRTNDATE],
				[CRTNUSER],
				[DESCRIPTION],
				[NUMBER],
				[STATE],
				[TITLE])
VALUES      (
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				4, 'SEND FOR V1.12', 0, 0, 'SEND FOR'
            )

GO

SELECT 'DUMMY MILESTONES ADDED'

--************************************** POPULATE ISSUES
INSERT INTO [issues].[issues]
            (
				[ASSIGNEES],
				[BODY],
				[CLOSEDBY],
				[CLOSEDDAY],
				[CRTNDATE],
				[CRTNUSER],
				[LABELS],
				[LASTUPD],
				[MILESTONES],
				[NUMBER],
				[PROJECTS],
				[STATE],
				[TITLE])
VALUES      (
				'1;2', 'Allow adding xml files', NULL, NULL,
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				1, '1', GETDATE(), NULL, 1, NULL, 0, 'SART899876'
			)
           
GO

INSERT INTO [issues].[issues]
            (
				[ASSIGNEES],
				[BODY],
				[CLOSEDBY],
				[CLOSEDDAY],
				[CRTNDATE],
				[CRTNUSER],
				[LABELS],
				[LASTUPD],
				[MILESTONES],
				[NUMBER],
				[PROJECTS],
				[STATE],
				[TITLE])
VALUES      (
				'3', 'Create DARK Theme', NULL, NULL,
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				1, '1', GETDATE(), NULL, 1, NULL, 0, 'INTG876432'
			)

GO

INSERT INTO [issues].[issues]
            (
				[ASSIGNEES],
				[BODY],
				[CLOSEDBY],
				[CLOSEDDAY],
				[CRTNDATE],
				[CRTNUSER],
				[LABELS],
				[LASTUPD],
				[MILESTONES],
				[NUMBER],
				[PROJECTS],
				[STATE],
				[TITLE])
VALUES      (
				'5', 'Allow only one instance of the application', NULL, NULL,
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				1, '1', GETDATE(), NULL, 1, NULL, 0, 'FSDI118764'
			)

GO

SELECT 'DUMMY ISSUES ADDED'

--************************************** POPULATE ISSUEDETAILS
INSERT INTO [issues].[issuedetails]
            (
				[ACTION],
				[CRTNDATE],
				[Fk_IssueTracking_Id],
				[USERID])
VALUES      (
				1, GETDATE(), 1, 1
			)

GO

SELECT 'DUMMY ISSUES DETAILS ADDED'