--NOTES:
--ISACTIVE = 1 TRUE
--ISACTIVE = 0 FALSE

--ISSUEDETAILS ACTIONS:
--1 DELETELABEL

--ISSUEDETAILS ACTIONS:
-- 1 COMMENTADDED
-- 2 COMMENTDELETED
-- 3 COMMENTUPDATED
-- 4 LABELADDED
-- 5 LABELDELETED
-- 6 MILESTONEADDED
-- 7 MILESTONEDELETED
-- 8 USERADDED
-- 9 USERDELETED


USE [HONEYS]
GO 

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
--    WHERE Fk_ISSUE = @Id AND ACTION=1; 
--    RETURN @Count;
--END;
--ELSE BEGIN
--SELECT 'FUNCTION FUNCTION_TOTALCOMMENTS NOT EXISTS'
--END;


--SELECT FK_LABEL, COUNT(Fk_ISSUE) AS ISS FROM ISSUES.LABELSTOISSUES 
--GROUP BY FK_LABEL
--ORDER BY ISS DESC

---- ************************************** [FUNCTION_TOTALISSUES]
--IF OBJECT_ID('FUNCTION_TOTALISSUES') IS NOT NULL 
--CREATE FUNCTION [issues].[FUNCTION_TOTALISSUES] (@Id int)  
--RETURNS int
--AS  
--BEGIN  
--    DECLARE @Count int;
--    SELECT @Count = COUNT(@Id)
--    FROM LABELSTOISSUES 
--    WHERE FK_LABEL = @Id AND ISACTIVE=1; 
--    RETURN @Count;
--END;
--ELSE BEGIN
--SELECT 'FUNCTION FFUNCTION_TOTALISSUES NOT EXISTS'
--END;


IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[issues].[COUNTBY1]') AND type = 'SO')
	BEGIN
		DROP SEQUENCE [issues].[COUNTBY1];
		SELECT 'SEQUENCE COUNTBY1 DELETE'
	END;

CREATE SEQUENCE [issues].[COUNTBY1]
    AS [bigint]
    START WITH 1
    INCREMENT BY 1
    MINVALUE 1
    MAXVALUE 9223372036854775807
    CACHE  3;

	SELECT 'SEQUENCE COUNTBY1 CREATE'

-- **************************************
-- **************************************
--				[USERS]
-- **************************************
-- **************************************
IF Object_id('[issues].[USERS]') IS NOT NULL
  BEGIN
      ALTER TABLE [issues].[LABELS]
      DROP CONSTRAINT [FK_Labels_User_Id]

	  ALTER TABLE [issues].[MILESTONES]
      DROP CONSTRAINT [FK_Milestones_User_Id]

      ALTER TABLE [issues].[ISSUEDETAILS]
      DROP CONSTRAINT [Fk_IssueDetails_User_Id]

	  ALTER TABLE [issues].[ISSUEDETAILS]
      DROP CONSTRAINT [Fk_IssueDetails_Issue_Id]

	  ALTER TABLE [issues].[USERSTOISSUES]
	  DROP CONSTRAINT [FK_UsersToIssues_AssignedBy_User_Id]

	  ALTER TABLE [issues].[USERSTOISSUES]
	  DROP CONSTRAINT [FK_UsersToIssues_Assignee_User_Id]

	  ALTER TABLE [issues].[ISSUES]
	  DROP CONSTRAINT [Fk_issues_ClosedBy_User_Id]

	  ALTER TABLE [issues].[ISSUES]
	  DROP CONSTRAINT [Fk_issues_CrtnUser_User_Id]

      ALTER TABLE [issues].[LABELSTOISSUES]
	  DROP CONSTRAINT [Fk_LabelsToIssues_User_Id]

	  ALTER TABLE [issues].[MILESTONESTOISSUES]
	  DROP CONSTRAINT [Fk_MilestonesToIssues_User_Id]

      DROP TABLE [issues].[USERS];
      CREATE TABLE [issues].[USERS]
        (
           [CRTNDATE]           DATETIME NOT NULL,
           [GU]                 VARCHAR(50) NOT NULL,
		   [ID]					INT IDENTITY (1, 1) NOT NULL,
		   [ISACTIVE]			BIT NOT NULL,
           [NAME]               VARCHAR(50) NOT NULL,
           [SURNAME]            VARCHAR(50) NULL,

           CONSTRAINT [PK_Users_Id] PRIMARY KEY CLUSTERED (
           [ID]
           ASC)
        )
		WITH (DATA_COMPRESSION = PAGE);

      SELECT 'TABLE [issues].[USERS] REGENERATED'
  END
ELSE
  BEGIN
      SELECT 'USERS NOT EXISTS... CREATING TABLE'

       CREATE TABLE [issues].[USERS]
        (
           [CRTNDATE]           DATETIME NOT NULL,
           [GU]                 VARCHAR(50) NOT NULL,
		   [ID]					INT IDENTITY (1, 1) NOT NULL,
		   [ISACTIVE]			BIT NOT NULL,
           [NAME]               VARCHAR(50) NOT NULL,
           [SURNAME]            VARCHAR(50) NULL,

           CONSTRAINT [PK_Users_Id] PRIMARY KEY CLUSTERED (
           [ID]
           ASC)
        )
		WITH (DATA_COMPRESSION = PAGE);

      SELECT 'TABLE [issues].[USERS] CREATED'
  END;

-- **************************************
-- **************************************
--				[LABELS]
-- **************************************
-- **************************************
IF Object_id('[issues].[LABELS]') IS NOT NULL
  BEGIN
      ALTER TABLE [issues].[LABELSTOISSUES]
	  DROP CONSTRAINT [Fk_LabelsToIssues_Label_Id]

      DROP TABLE [issues].[LABELS];
      CREATE TABLE [issues].[LABELS]
        (
           [DESCRIPTION]         VARCHAR(50) NOT NULL,
           [COLOR]               VARCHAR(50) NOT NULL,
           [CRTNDATE]            DATETIME NOT NULL,
		   [Fk_CRTNUSER]		 INT NOT NULL,
           [ID]					 INT IDENTITY (1, 1) NOT NULL,
		   [ISACTIVE]			 BIT NOT NULL,
           [NAME]                VARCHAR(50) NOT NULL,
		   [TOTALISSUES] AS
           [issues].[Function_Totalissues]([ID]),

           CONSTRAINT [PK_Labels_Id] PRIMARY KEY CLUSTERED (
           [ID] 
		   ASC),

		   CONSTRAINT [Fk_Labels_User_Id] FOREIGN KEY ([Fk_CRTNUSER])
           REFERENCES [issues].[USERS]([ID]),
        )
		WITH (DATA_COMPRESSION = PAGE);

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
		   [Fk_CRTNUSER]		 INT NOT NULL,
           [ID]					 INT IDENTITY (1, 1) NOT NULL,
		   [ISACTIVE]			 BIT NOT NULL,
           [NAME]                VARCHAR(50) NOT NULL,
		   [TOTALISSUES] AS
           [issues].[Function_Totalissues]([ID]),

           CONSTRAINT [PK_Labels_Id] PRIMARY KEY CLUSTERED (
           [ID] 
		   ASC),

		   CONSTRAINT [Fk_Labels_User_Id] FOREIGN KEY ([Fk_CRTNUSER])
           REFERENCES [issues].[USERS]([ID]),
        )
		WITH (DATA_COMPRESSION = PAGE);

      SELECT 'TABLE [issues].[LABELS] CREATED'
  END;

-- **************************************
-- **************************************
--				[MILESTONES]
-- **************************************
-- **************************************
IF Object_id('[issues].[MILESTONES]') IS NOT NULL
  BEGIN
	ALTER TABLE [issues].[MILESTONESTOISSUES]
	DROP CONSTRAINT [Fk_MilestonesToIssues_Milestone_Id]

      DROP TABLE [issues].[MILESTONES];
      CREATE TABLE [issues].[MILESTONES]
        (
           [CRTNDATE]                DATETIME NOT NULL,
           [DESCRIPTION]             VARCHAR(50) NOT NULL,
		   [Fk_CRTNUSER]		     INT NOT NULL,
           [ID]						 INT IDENTITY NOT NULL,
		   [NUMBER]                  INT NOT NULL,
           [STATE]                   INT NOT NULL,
           [TITLE]                   VARCHAR(50) NOT NULL,

           CONSTRAINT [PK_Milestones_Id] PRIMARY KEY CLUSTERED (
           [ID] 
		   ASC),

		   CONSTRAINT [Fk_Milestones_User_Id] FOREIGN KEY ([Fk_CRTNUSER])
           REFERENCES [issues].[USERS]([ID])
        )
		WITH (DATA_COMPRESSION = PAGE);

      SELECT 'TABLE [issues].[MILESTONES] REGENERATED'
  END
ELSE
  BEGIN
      SELECT 'TABLE [issues].[MILESTONES] NOT EXISTS... CREATING TABLE'
       CREATE TABLE [issues].[MILESTONES]
        (
           [CRTNDATE]                DATETIME NOT NULL,
           [DESCRIPTION]             VARCHAR(50) NOT NULL,
		   [Fk_CRTNUSER]		     INT NOT NULL,
           [ID]						 INT IDENTITY NOT NULL,
		   [NUMBER]                  INT NOT NULL,
           [STATE]                   INT NOT NULL,
           [TITLE]                   VARCHAR(50) NOT NULL,

           CONSTRAINT [PK_Milestones_Id] PRIMARY KEY CLUSTERED (
           [ID] 
		   ASC),

		   CONSTRAINT [Fk_Milestones_User_Id] FOREIGN KEY ([Fk_CRTNUSER])
           REFERENCES [issues].[USERS]([ID])
        )
		WITH (DATA_COMPRESSION = PAGE);

      SELECT 'TABLE [issues].[MILESTONES] CREATED'
  END;

-- **************************************
-- **************************************
--				[ISSUES]
-- **************************************
-- **************************************
IF Object_id('[issues].[ISSUES]') IS NOT NULL
  BEGIN
      ALTER TABLE [issues].[LABELSTOISSUES]
      DROP CONSTRAINT [Fk_LabelsToIssues_Issues_Id]

	  ALTER TABLE [issues].[USERSTOISSUES]
	  DROP CONSTRAINT [FK_UsersToIssues_Issue_Id]

	  ALTER TABLE [issues].[MILESTONESTOISSUES]
	  DROP CONSTRAINT [Fk_MilestonesToIssues_Issues_Id]

      DROP TABLE [issues].[issues]

      CREATE TABLE [issues].[ISSUES]
        (
           [BODY]                VARCHAR(max) NOT NULL,
           [Fk_CLOSEDBY]         INT NULL,
           [CLOSEDDAY]           DATETIME NULL,
           [CRTNDATE]            DATETIME NOT NULL,
           [Fk_CRTNUSER]         INT NOT NULL,
           [ID]					 INT IDENTITY (1, 1) NOT NULL,
           [LASTUPD]	         DATETIME NULL,
           [NUMBER]              INT NOT NULL,
           [STATE]               INT NOT NULL CONSTRAINT [DF_ISSUES_STATE] DEFAULT 1,
           [TITLE]               VARCHAR(50) NOT NULL,
           [TOTALCOMMENTS] AS
           [issues].[Function_TotalComments]([ID]),

           CONSTRAINT [PK_Issues_Id] PRIMARY KEY CLUSTERED (
           [ID] 
		   ASC),

		   CONSTRAINT [Fk_issues_CrtnUser_User_Id] FOREIGN KEY ([Fk_CRTNUSER])
           REFERENCES [issues].[USERS]([ID]),

		   CONSTRAINT [Fk_issues_ClosedBy_User_Id] FOREIGN KEY ([Fk_CLOSEDBY])
           REFERENCES [issues].[USERS]([ID])
        )
		WITH (DATA_COMPRESSION = PAGE);

      SELECT 'TABLE [issues].[ISSUES] REGENERATED'
  END
ELSE
  BEGIN
      SELECT '[issues].[ISSUES] TABLE NOT EXIST... CREATING TABLE'

       CREATE TABLE [issues].[ISSUES]
        (
           [BODY]                VARCHAR(max) NOT NULL,
           [Fk_CLOSEDBY]         INT NULL,
           [CLOSEDDAY]           DATETIME NULL,
           [CRTNDATE]            DATETIME NOT NULL,
           [Fk_CRTNUSER]         INT NOT NULL,
           [ID]					 INT IDENTITY (1, 1) NOT NULL,
           [LASTUPD]	         DATETIME NULL,
           [NUMBER]              INT NOT NULL,
           [STATE]               INT NOT NULL CONSTRAINT [DF_ISSUES_STATE] DEFAULT 1,
           [TITLE]               VARCHAR(50) NOT NULL,
           [TOTALCOMMENTS] AS
           [issues].[Function_TotalComments]([ID]),

           CONSTRAINT [PK_Issues_Id] PRIMARY KEY CLUSTERED (
           [ID] 
		   ASC),

		   CONSTRAINT [Fk_issues_CrtnUser_User_Id] FOREIGN KEY ([Fk_CRTNUSER])
           REFERENCES [issues].[USERS]([ID]),

		   CONSTRAINT [Fk_issues_ClosedBy_User_Id] FOREIGN KEY ([Fk_CLOSEDBY])
           REFERENCES [issues].[USERS]([ID])
        )
		WITH (DATA_COMPRESSION = PAGE);

      SELECT 'TABLE [issues].[ISSUES] CREATED'
  END;

GO

-- **************************************
-- **************************************
--				[ISSUESDETAILS]
-- **************************************
-- **************************************
IF Object_id('[issues].[ISSUEDETAILS]') IS NOT NULL
  BEGIN
      ALTER TABLE [issues].[ISSUECOMMENTS]
      DROP CONSTRAINT FK_IssueComments_IssueDetails_Id;

      DROP TABLE [issues].[ISSUEDETAILS];

      CREATE TABLE [issues].[ISSUEDETAILS]
        (
            [ACTION]                    INT NOT NULL,
			[CRTNDATE]                  DATETIME NOT NULL,
			[Fk_CRTNUSER]				INT NOT NULL,
			[Fk_ISSUE]  				INT NOT NULL,
			[ID]						INT IDENTITY (1, 1) NOT NULL
           
		   CONSTRAINT [PK_IssueDetails_Id] PRIMARY KEY CLUSTERED (
		   [ID] 
		   ASC),
           		   
		   CONSTRAINT [FK_IssueDetails_Issue_Id] FOREIGN KEY ([Fk_ISSUE])
           REFERENCES [issues].[ISSUES]([ID]),

		   CONSTRAINT [FK_IssueDetails_User_Id] FOREIGN KEY ([Fk_CRTNUSER] )
           REFERENCES [issues].[USERS]([ID])
        )
		WITH (DATA_COMPRESSION = PAGE);

      SELECT 'TABLE [issues].[ISSUEDETAILS] REGENERATED'
  END
ELSE
  BEGIN
      SELECT 'TABLE [issues].[ISSUEDETAILS] NOT EXIST... CREATING TABLE'

       CREATE TABLE [issues].[ISSUEDETAILS]
        (
            [ACTION]                    INT NOT NULL,
			[CRTNDATE]                  DATETIME NOT NULL,
			[Fk_CRTNUSER]				INT NOT NULL,
			[Fk_ISSUE]  				INT NOT NULL,
			[ID]						INT IDENTITY (1, 1) NOT NULL
           
		   CONSTRAINT [PK_IssueDetails_Id] PRIMARY KEY CLUSTERED (
		   [ID] 
		   ASC),
           		   
		   CONSTRAINT [FK_IssueDetails_Issue_Id] FOREIGN KEY ([Fk_ISSUE])
           REFERENCES [issues].[ISSUES]([ID]),

		   CONSTRAINT [FK_IssueDetails_User_Id] FOREIGN KEY ([Fk_CRTNUSER] )
           REFERENCES [issues].[USERS]([ID])
        )
		WITH (DATA_COMPRESSION = PAGE);

      SELECT 'TABLE [issues].[ISSUEDETAILS] CREATED'
  END


-- **************************************
-- **************************************
--				[ISSUECOMMENTS]
-- **************************************
-- **************************************
IF Object_id('[issues].[ISSUECOMMENTS]') IS NOT NULL
  BEGIN
      DROP TABLE [issues].[ISSUECOMMENTS];

      CREATE TABLE [issues].[ISSUECOMMENTS]
        (
           [COMMENT]                   VARCHAR(max) NOT NULL,
   		   [CRTNDATE]				   DATETIME,
           [Fk_ISSUEDETAIL]			   INT NOT NULL,
           [ID]						   INT IDENTITY (1, 1) NOT NULL,
		   [ISACTIVE]				   BIT NOT NULL,

           CONSTRAINT [PK_Comment_Id] PRIMARY KEY CLUSTERED (
           [ID] 
		   ASC),

           CONSTRAINT [FK_IssueComments_IssueDetails_Id] FOREIGN KEY (
           [Fk_ISSUEDETAIL]) REFERENCES
           [issues].[ISSUEDETAILS]([ID])
        )
		WITH (DATA_COMPRESSION = PAGE);

      SELECT 'TABLE [issues].[ISSUECOMMENTS] REGENERATED'
  END
ELSE
  BEGIN
      SELECT 'TABLE [issues].[ISSUECOMMENTS] NOT EXIST... CREATING TABLE'

     CREATE TABLE [issues].[ISSUECOMMENTS]
        (
           [COMMENT]                   VARCHAR(max) NOT NULL,
		   [CRTNDATE]				   DATETIME,
           [Fk_ISSUEDETAIL]			   INT NOT NULL,
           [ID]						   INT IDENTITY (1, 1) NOT NULL,
		   [ISACTIVE]				   BIT NOT NULL,

           CONSTRAINT [PK_Comment_Id] PRIMARY KEY CLUSTERED (
           [ID] 
		   ASC),

           CONSTRAINT [FK_IssueComments_IssueDetails_Id] FOREIGN KEY (
           [Fk_ISSUEDETAIL]) REFERENCES
           [issues].[ISSUEDETAILS]([ID])
        )
		WITH (DATA_COMPRESSION = PAGE);

      SELECT 'TABLE [issues].[ISSUECOMMENTS] CREATED'
  END

  -- **************************************
-- **************************************
--				[USERSTOISSUES]
-- **************************************
-- **************************************
  IF Object_id('[issues].[USERSTOISSUES]') IS NOT NULL
  BEGIN
	  DROP TABLE [issues].[USERSTOISSUES];
      CREATE TABLE [issues].[USERSTOISSUES]
        (
			[CRTNDATE]				DATETIME,
			[Fk_CRTNUSER]			INT NOT NULL,
			[Fk_ISSUE]				INT NOT NULL,
			[Fk_USERASSIGNEE]		INT NOT NULL,
			[ID]					INT IDENTITY (1, 1) NOT NULL,
			[ISACTIVE]				BIT NOT NULL
           
		   CONSTRAINT [PK_UsersToIssues_Id] PRIMARY KEY CLUSTERED (
		   [ID] 
		   ASC),
           
		   CONSTRAINT [FK_UsersToIssues_Issue_Id] FOREIGN KEY ([Fk_ISSUE])
           REFERENCES [issues].[ISSUES]([ID]),

		   CONSTRAINT [FK_UsersToIssues_Assignee_User_Id] FOREIGN KEY ([Fk_USERASSIGNEE] )
           REFERENCES [issues].[USERS]([ID]),

		   CONSTRAINT [FK_UsersToIssues_AssignedBy_User_Id] FOREIGN KEY ([Fk_CRTNUSER] )
           REFERENCES [issues].[USERS]([ID])
        )
		WITH (DATA_COMPRESSION = PAGE);

      SELECT 'TABLE [issues].[USERSTOISSUES] REGENERATED'
  END
ELSE
  BEGIN
      SELECT 'TABLE [issues].[ISSUEDETAILS] NOT EXIST... CREATING TABLE'

        CREATE TABLE [issues].[USERSTOISSUES]
        (
			[CRTNDATE]				DATETIME,
			[Fk_CRTNUSER]			INT NOT NULL,
			[Fk_ISSUE]				INT NOT NULL,
			[Fk_USERASSIGNEE]		INT NOT NULL,
			[ID]					INT IDENTITY (1, 1) NOT NULL,
			[ISACTIVE]				BIT NOT NULL
           
		   CONSTRAINT [PK_UsersToIssues_Id] PRIMARY KEY CLUSTERED (
		   [ID] 
		   ASC),
           
		   CONSTRAINT [FK_UsersToIssues_Issue_Id] FOREIGN KEY ([Fk_ISSUE])
           REFERENCES [issues].[ISSUES]([ID]),

		   CONSTRAINT [FK_UsersToIssues_Assignee_User_Id] FOREIGN KEY ([Fk_USERASSIGNEE] )
           REFERENCES [issues].[USERS]([ID]),

		   CONSTRAINT [FK_UsersToIssues_AssignedBy_User_Id] FOREIGN KEY ([Fk_CRTNUSER] )
           REFERENCES [issues].[USERS]([ID])
        )
		WITH (DATA_COMPRESSION = PAGE);

      SELECT 'TABLE [issues].[USERSTOISSUES] CREATED'
  END

-- **************************************
-- **************************************
--				[LABELSTOISSUES]
-- **************************************
-- **************************************
  IF Object_id('[issues].[LABELSTOISSUES]') IS NOT NULL
  BEGIN
	  DROP TABLE [issues].[LABELSTOISSUES];
      CREATE TABLE [issues].[LABELSTOISSUES](
			[CRTNDATE] [datetime] NOT NULL,
			[Fk_CRTNUSER] [int] NOT NULL,
			[Fk_LABEL] [int] NOT NULL,
			[Fk_ISSUE] [int] NOT NULL,
			[ID] [int] IDENTITY(1,1) NOT NULL,
			[ISACTIVE] [bit] NOT NULL,

			CONSTRAINT [PK_LabelsToIsues_Id] PRIMARY KEY CLUSTERED (
			[ID] 
			ASC),

			CONSTRAINT [Fk_LabelsToIssues_User_Id] FOREIGN KEY ([Fk_CRTNUSER])
			REFERENCES [issues].[USERS]([ID]),

			CONSTRAINT [Fk_LabelsToIssues_Label_Id] FOREIGN KEY ([Fk_LABEL])
			REFERENCES [issues].[LABELS]([ID]),

			CONSTRAINT [Fk_LabelsToIssues_Issues_Id] FOREIGN KEY ([Fk_ISSUE])
			REFERENCES [issues].[ISSUES]([ID]),
	 )

	 WITH (DATA_COMPRESSION = PAGE);

      SELECT 'TABLE [issues].[LABELSTOISSUES] REGENERATED'
  END
ELSE
  BEGIN
      SELECT 'TABLE [issues].[LABELSTOISSUES] NOT EXIST... CREATING TABLE'

         CREATE TABLE [issues].[LABELSTOISSUES](
			[CRTNDATE] [datetime] NOT NULL,
			[Fk_CRTNUSER] [int] NOT NULL,
			[Fk_LABEL] [int] NOT NULL,
			[Fk_ISSUE] [int] NOT NULL,
			[ID] [int] IDENTITY(1,1) NOT NULL,
			[ISACTIVE] [bit] NOT NULL,

			CONSTRAINT [PK_LabelsToIsues_Id] PRIMARY KEY CLUSTERED (
			[ID] 
			ASC),

			CONSTRAINT [Fk_LabelsToIssues_User_Id] FOREIGN KEY ([Fk_CRTNUSER])
			REFERENCES [issues].[USERS]([ID]),

			CONSTRAINT [Fk_LabelsToIssues_Label_Id] FOREIGN KEY ([Fk_LABEL])
			REFERENCES [issues].[LABELS]([ID]),

			CONSTRAINT [Fk_LabelsToIssues_Issues_Id] FOREIGN KEY ([Fk_ISSUE])
			REFERENCES [issues].[ISSUES]([ID]),
	 )

	 WITH (DATA_COMPRESSION = PAGE);

      SELECT 'TABLE [issues].[LABELSTOISSUES] CREATED'
  END

-- **************************************
-- **************************************
--				[MILESTONESTOISSUES]
-- **************************************
-- **************************************
  IF Object_id('[issues].[MILESTONESTOISSUES]') IS NOT NULL
  BEGIN
	  DROP TABLE [issues].[MILESTONESTOISSUES];
      CREATE TABLE [issues].[MILESTONESTOISSUES](
			[CRTNDATE] [datetime] NOT NULL,
			[Fk_CRTNUSER] [int] NOT NULL,
			[Fk_MILESTONE] [int] NOT NULL,
			[Fk_ISSUE] [int] NOT NULL,
			[ID] [int] IDENTITY(1,1) NOT NULL,
			[ISACTIVE] [bit] NOT NULL,

			CONSTRAINT [PK_MilestonesToIsues_Id] PRIMARY KEY CLUSTERED (
			[ID] 
			ASC),

			CONSTRAINT [Fk_MilestonesToIssues_User_Id] FOREIGN KEY ([Fk_CRTNUSER])
			REFERENCES [issues].[USERS]([ID]),

			CONSTRAINT [Fk_MilestonesToIssues_Milestone_Id] FOREIGN KEY ([Fk_MILESTONE])
			REFERENCES [issues].[MILESTONES]([ID]),

			CONSTRAINT [Fk_MilestonesToIssues_Issues_Id] FOREIGN KEY ([Fk_ISSUE])
			REFERENCES [issues].[ISSUES]([ID]),
	 )

	 WITH (DATA_COMPRESSION = PAGE);

      SELECT 'TABLE [issues].[MILESTONESTOISSUES] REGENERATED'
  END
ELSE
  BEGIN
      SELECT 'TABLE [issues].[MILESTONESTOISSUES] NOT EXIST... CREATING TABLE'

          CREATE TABLE [issues].[MILESTONESTOISSUES](
			[CRTNDATE] [datetime] NOT NULL,
			[Fk_CRTNUSER] [int] NOT NULL,
			[Fk_MILESTONE] [int] NOT NULL,
			[Fk_ISSUE] [int] NOT NULL,
			[ID] [int] IDENTITY(1,1) NOT NULL,
			[ISACTIVE] [bit] NOT NULL,

			CONSTRAINT [PK_MilestonesToIsues_Id] PRIMARY KEY CLUSTERED (
			[ID] 
			ASC),

			CONSTRAINT [Fk_MilestonesToIssues_User_Id] FOREIGN KEY ([Fk_CRTNUSER])
			REFERENCES [issues].[USERS]([ID]),

			CONSTRAINT [Fk_MilestonesToIssues_Milestone_Id] FOREIGN KEY ([Fk_MILESTONE])
			REFERENCES [issues].[MILESTONES]([ID]),

			CONSTRAINT [Fk_MilestonesToIssues_Issues_Id] FOREIGN KEY ([Fk_ISSUE])
			REFERENCES [issues].[ISSUES]([ID]),
	 )

	 WITH (DATA_COMPRESSION = PAGE);

      SELECT 'TABLE [issues].[MILESTONESTOISSUES] CREATED'
  END

------ **************************************
------ **************************************
------				[PROJECTSTOISSUES]
------ **************************************
------ **************************************
----  IF Object_id('[issues].[PROJECTSTOISSUES]') IS NOT NULL
----  BEGIN
----	  DROP TABLE [issues].[PROJECTSTOISSUES];
----      CREATE TABLE [issues].[PROJECTSTOISSUES](
----			[CRTNDATE] [datetime] NOT NULL,
----			[Fk_CRTNUSER] [int] NOT NULL,
----			[Fk_PROJECTS] [int] NOT NULL,
----			[Fk_ISSUE] [int] NOT NULL,
----			[ID] [int] IDENTITY(1,1) NOT NULL,
----			[ISACTIVE] [bit] NOT NULL,

----			CONSTRAINT [PK_MilestonesToIsues_Id] PRIMARY KEY CLUSTERED (
----			[ID] 
----			ASC),

----			CONSTRAINT [Fk_ProjcetsToIssues_User_Id] FOREIGN KEY ([Fk_CRTNUSER])
----			REFERENCES [issues].[USERS]([ID]),

----			CONSTRAINT [Fk_ProjcetsToIssues_Projects_Id] FOREIGN KEY ([Fk_PROJECTS])
----			REFERENCES [issues].[MILESTONES]([ID]),

----			CONSTRAINT [Fk_ProjcetsToIssues_Issues_Id] FOREIGN KEY ([Fk_ISSUE])
----			REFERENCES [issues].[ISSUES]([ID]),
----	 )

----	 WITH (DATA_COMPRESSION = PAGE);

----      SELECT 'TABLE [issues].[PROJECTSTOISSUES] REGENERATED'
----  END
----ELSE
----  BEGIN
----      SELECT 'TABLE [issues].[PROJECTSTOISSUES] NOT EXIST... CREATING TABLE'

----          CREATE TABLE [issues].[PROJECTSTOISSUES](
----			[CRTNDATE] [datetime] NOT NULL,
----			[Fk_CRTNUSER] [int] NOT NULL,
----			[Fk_PROJECTS] [int] NOT NULL,
----			[Fk_ISSUE] [int] NOT NULL,
----			[ID] [int] IDENTITY(1,1) NOT NULL,
----			[ISACTIVE] [bit] NOT NULL,

----			CONSTRAINT [PK_MilestonesToIsues_Id] PRIMARY KEY CLUSTERED (
----			[ID] 
----			ASC),

----			CONSTRAINT [Fk_ProjcetsToIssues_User_Id] FOREIGN KEY ([Fk_CRTNUSER])
----			REFERENCES [issues].[USERS]([ID]),

----			CONSTRAINT [Fk_ProjcetsToIssues_Projects_Id] FOREIGN KEY ([Fk_PROJECTS])
----			REFERENCES [issues].[MILESTONES]([ID]),

----			CONSTRAINT [Fk_ProjcetsToIssues_Issues_Id] FOREIGN KEY ([Fk_ISSUE])
----			REFERENCES [issues].[ISSUES]([ID]),
----	 )

----	 WITH (DATA_COMPRESSION = PAGE);

----      SELECT 'TABLE [issues].[PROJECTSTOISSUES] CREATED'
----  END
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
				[ISACTIVE],
				[NAME],
				[SURNAME])
VALUES      
			(
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				'99GU9999', 1, 'SYSTEM', 'SYSTEM'
			)

INSERT INTO [issues].[USERS]
            (
				[CRTNDATE],
				[GU],
				[ISACTIVE],
				[NAME],
				[SURNAME])

VALUES      (
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				'99GU9889', 1, 'Brian', 'J. Kidwell'
			)



INSERT INTO [issues].[USERS]
            (
				[CRTNDATE],
				[GU],
				[ISACTIVE],
				[NAME],
				[SURNAME])
VALUES      (
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				'99GU9888', 1, 'Cora', 'L. Richards'
			)

GO

INSERT INTO [issues].[USERS]
            (
				[CRTNDATE],
				[GU],
				[ISACTIVE],
				[NAME],
				[SURNAME])
VALUES      (
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				'99GU6544', 1, 'Richard', 'L. Swink'
			)

GO

INSERT INTO [issues].[USERS]
            (
				[CRTNDATE],
				[GU],
				[ISACTIVE],
				[NAME],
				[SURNAME])
VALUES      (
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				'99GU9834', 1, 'Karen', 'A. McNally'
			)

GO

INSERT INTO [issues].[USERS]
            (
				[CRTNDATE],
				[GU],
				[ISACTIVE],
				[NAME],
				[SURNAME])
VALUES      
			(
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				'99GU5774', 1, 'S. Morey', 'A. McNally'
			)
GO

INSERT INTO [issues].[USERS]
            (
				[CRTNDATE],
				[GU],
				[ISACTIVE],
				[NAME],
				[SURNAME])
VALUES      
			(
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				'99GU5575', 1, 'K. Olmstead', 'A. McNally'
			)

GO

SELECT 'DUMMY USERS ADDED'

--GO

--************************************** POPULATE LABELS
INSERT INTO [issues].[LABELS]
            (
				[DESCRIPTION],
				[COLOR],
				[CRTNDATE],
				[Fk_CRTNUSER],
				[ISACTIVE],
				[NAME])
VALUES      (
				'Something isnt working', '#FFFF0000', 
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				7, 1, 'bug'
			)

GO

INSERT INTO [issues].[LABELS]
            (
				[DESCRIPTION],
				[COLOR],
				[CRTNDATE],
				[Fk_CRTNUSER],
				[ISACTIVE],
				[NAME])
VALUES      (
				'New feature or request', '#FFADD8E6',
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				7, 1, 'enhancement'
			)

GO

INSERT INTO [issues].[LABELS]
            (
				[DESCRIPTION],
				[COLOR],
				[CRTNDATE],
				[Fk_CRTNUSER],
				[ISACTIVE],
				[NAME])
VALUES      (
				'Further information is requested', '#FFFFC0CB',
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				7, 1, 'question'
			)

GO

INSERT INTO [issues].[LABELS]
            (
				[DESCRIPTION],
				[COLOR],
				[CRTNDATE],
				[Fk_CRTNUSER],
				[ISACTIVE],
				[NAME])
VALUES      (
				'This issue already exists', '#FFFFD700',
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				7, 1, 'duplicate'
			)

GO

INSERT INTO [issues].[LABELS]
            (
				[DESCRIPTION],
				[COLOR],
				[CRTNDATE],
				[ISACTIVE],
				[Fk_CRTNUSER],
				[NAME])
VALUES      (
				'Extra attention is needed', '#FF008000',
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				7, 1, 'help wanted'
			)

GO

SELECT 'DUMMY LABELS ADDED'

GO

--************************************** POPULATE MILESTONES
INSERT INTO [issues].[MILESTONES]
            (
				[CRTNDATE],
				[Fk_CRTNUSER],
				[DESCRIPTION],
				[NUMBER],
				[STATE],
				[TITLE])
VALUES      (
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				2, 'SEND PRO V1.12', NEXT VALUE FOR [issues].[COUNTBY1], 0, 'SENDPRO'
            )

GO

INSERT INTO [issues].[MILESTONES]
            (
				[CRTNDATE],
				[Fk_CRTNUSER],
				[DESCRIPTION],
				[NUMBER],
				[STATE],
				[TITLE])
VALUES      (
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				3, 'SEND PRE V1.12', NEXT VALUE FOR [issues].[COUNTBY1], 0, 'SEND PRE'
            )

GO

INSERT INTO [issues].[MILESTONES]
            (
				[CRTNDATE],
				[Fk_CRTNUSER],
				[DESCRIPTION],
				[NUMBER],
				[STATE],
				[TITLE])
VALUES      (
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				4, 'SEND FOR V1.12', NEXT VALUE FOR [issues].[COUNTBY1], 0, 'SEND FOR'
            )

GO

SELECT 'DUMMY MILESTONES ADDED'

--************************************** POPULATE ISSUES
INSERT INTO [issues].[issues]
            (
				[BODY],
				[Fk_CLOSEDBY],
				[CLOSEDDAY],
				[CRTNDATE],
				[Fk_CRTNUSER],
				[LASTUPD],
				[NUMBER],
				[STATE],
				[TITLE])
VALUES      (
				'Allow adding xml files', NULL, NULL,
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				1, NULL, NEXT VALUE FOR [issues].[COUNTBY1], 1, 'SART899876'
			)
           
GO

INSERT INTO [issues].[issues]
            (
				[BODY],
				[Fk_CLOSEDBY],
				[CLOSEDDAY],
				[CRTNDATE],
				[Fk_CRTNUSER],
				[LASTUPD],
				[NUMBER],				
				[STATE],
				[TITLE])
VALUES      (
				'Create dark theme', NULL, NULL,
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				1, NULL, NEXT VALUE FOR [issues].[COUNTBY1], 1, 'SART98354'
			)
           
GO

INSERT INTO [issues].[issues]
            (
				[BODY],
				[Fk_CLOSEDBY],
				[CLOSEDDAY],
				[CRTNDATE],
				[Fk_CRTNUSER],
				[LASTUPD],
				[NUMBER],
				[STATE],
				[TITLE])
VALUES      (
				'Call thrid party API REST', NULL, NULL,
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				1, NULL, NEXT VALUE FOR [issues].[COUNTBY1], 1, 'FSDI76673'
			)
           
GO

INSERT INTO [issues].[issues]
            (
				[BODY],
				[Fk_CLOSEDBY],
				[CLOSEDDAY],
				[CRTNDATE],
				[Fk_CRTNUSER],
				[LASTUPD],
				[NUMBER],
				[STATE],
				[TITLE])
VALUES      (
				'Create new caller', NULL, NULL,
				DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 3650) * -1, GETDATE()),
				1, NULL, NEXT VALUE FOR [issues].[COUNTBY1], 1, 'INTG33234'
			)

GO

SELECT 'DUMMY ISSUES ADDED'

--************************************** POPULATE ISSUEDETAILS
INSERT INTO [issues].[issuedetails]
            (
				[ACTION],
				[CRTNDATE],
				[Fk_CRTNUSER],
				[Fk_ISSUE])
VALUES      (
				1, GETDATE(), 1, 1
			)

GO

SELECT 'DUMMY ISSUES DETAILS ADDED'


----************************************** POPULATE USERSTOISSUES
INSERT INTO [issues].[USERSTOISSUES]
           ([CRTNDATE]
           ,[Fk_CRTNUSER]
           ,[Fk_ISSUE]
           ,[Fk_USERASSIGNEE]
           ,[ISACTIVE])
     VALUES
           (GETDATE(),
            2,
            1,
            6,
            1)

INSERT INTO [issues].[USERSTOISSUES]
           ([CRTNDATE]
           ,[Fk_CRTNUSER]
           ,[Fk_ISSUE]
           ,[Fk_USERASSIGNEE]
           ,[ISACTIVE])
     VALUES
           (GETDATE(),
            2,
            1,
            5,
            1)
GO

INSERT INTO [issues].[USERSTOISSUES]
           ([CRTNDATE]
           ,[Fk_CRTNUSER]
           ,[Fk_ISSUE]
           ,[Fk_USERASSIGNEE]
           ,[ISACTIVE])
     VALUES
           (GETDATE(),
            2,
            2,
            4,
            1)

GO

INSERT INTO [issues].[USERSTOISSUES]
           ([CRTNDATE]
           ,[Fk_CRTNUSER]
           ,[Fk_ISSUE]
           ,[Fk_USERASSIGNEE]
           ,[ISACTIVE])
     VALUES
           (GETDATE(),
            2,
            3,
            3,
            1)

GO

INSERT INTO [issues].[USERSTOISSUES]
           ([CRTNDATE]
           ,[Fk_CRTNUSER]
           ,[Fk_ISSUE]
           ,[Fk_USERASSIGNEE]
           ,[ISACTIVE])
     VALUES
           (GETDATE(),
            2,
            4,
            2,
            1)

GO

INSERT INTO [issues].[USERSTOISSUES]
           ([CRTNDATE]
           ,[Fk_CRTNUSER]
           ,[Fk_ISSUE]
           ,[Fk_USERASSIGNEE]
           ,[ISACTIVE])
     VALUES
           (GETDATE(),
            2,
            4,
            1,
            1)

SELECT 'DUMMY USERS TO ISSUES ADDED'

----************************************** POPULATE LABELSTOISSUES
INSERT INTO [issues].[LABELSTOISSUES]
           ([CRTNDATE]
           ,[Fk_CRTNUSER]
           ,[Fk_LABEL]
           ,[Fk_ISSUE]
           ,[ISACTIVE])
     VALUES
           (GETDATE()
           ,1
           ,1
           ,1
           ,1)
GO

INSERT INTO [issues].[LABELSTOISSUES]
           ([CRTNDATE]
           ,[Fk_CRTNUSER]
           ,[Fk_LABEL]
           ,[Fk_ISSUE]
           ,[ISACTIVE])
     VALUES
           (GETDATE()
           ,1
           ,2
           ,1
           ,1)
GO

INSERT INTO [issues].[LABELSTOISSUES]
           ([CRTNDATE]
           ,[Fk_CRTNUSER]
           ,[Fk_LABEL]
           ,[Fk_ISSUE]
           ,[ISACTIVE])
     VALUES
           (GETDATE()
           ,1
           ,3
           ,1
           ,1)
GO

INSERT INTO [issues].[LABELSTOISSUES]
           ([CRTNDATE]
           ,[Fk_CRTNUSER]
           ,[Fk_LABEL]
           ,[Fk_ISSUE]
           ,[ISACTIVE])
     VALUES
           (GETDATE()
           ,1
           ,2
           ,2
           ,1)
GO

INSERT INTO [issues].[LABELSTOISSUES]
           ([CRTNDATE]
           ,[Fk_CRTNUSER]
           ,[Fk_LABEL]
           ,[Fk_ISSUE]
           ,[ISACTIVE])
     VALUES
           (GETDATE()
           ,1
           ,3
           ,3
           ,1)
GO

INSERT INTO [issues].[LABELSTOISSUES]
           ([CRTNDATE]
           ,[Fk_CRTNUSER]
           ,[Fk_LABEL]
           ,[Fk_ISSUE]
           ,[ISACTIVE])
     VALUES
           (GETDATE()
           ,1
           ,4
           ,4
           ,1)
GO
SELECT 'DUMMY LABELS TO ISSUES ADDED'

----************************************** POPULATE LABELSTOISSUES
INSERT INTO [issues].[MILESTONESTOISSUES]
           ([CRTNDATE]
           ,[Fk_CRTNUSER]
           ,[Fk_MILESTONE]
           ,[Fk_ISSUE]
           ,[ISACTIVE])
     VALUES
           (GETDATE()
           ,1
           ,1
           ,1
           ,1)

INSERT INTO [issues].[MILESTONESTOISSUES]
           ([CRTNDATE]
           ,[Fk_CRTNUSER]
           ,[Fk_MILESTONE]
           ,[Fk_ISSUE]
           ,[ISACTIVE])
VALUES
           (GETDATE()
           ,1
           ,1
           ,2
           ,1)

INSERT INTO [issues].[MILESTONESTOISSUES]
           ([CRTNDATE]
           ,[Fk_CRTNUSER]
           ,[Fk_MILESTONE]
           ,[Fk_ISSUE]
           ,[ISACTIVE])
VALUES
           (GETDATE()
           ,1
           ,2
           ,3
           ,1)

INSERT INTO [issues].[MILESTONESTOISSUES]
           ([CRTNDATE]
           ,[Fk_CRTNUSER]
           ,[Fk_MILESTONE]
           ,[Fk_ISSUE]
           ,[ISACTIVE])
VALUES
           (GETDATE()
           ,1
           ,3
           ,4
           ,1)


SELECT 'DUMMY MILESTONES TO ISSUES ADDED'