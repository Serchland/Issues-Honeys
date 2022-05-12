using IssuesHoneys.BusinessTypes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace IssuesHoneys.Services.SQL
{
    /// <summary>
    /// Converts the results obtained from the model to the type of data expected by the application.
    /// </summary>
    internal static class Converters
    {
        /// <summary>
        /// Converts the results obtained from the model to the type of data expected by the application.
        /// </summary>
        /// <param name="reader">Reader with datas</param>
        /// <param name="labels">Labels obtained from the model to cross-reference data</param>
        /// <param name="users">Users obtained from the model to cross-reference data</param>
        /// <param name="milestones">Milestones obtained from the model to cross-reference data</param>
        /// <returns>Issue</returns>
        internal static Issue SQLIssueConverter(SqlDataReader reader, ref List<Label> labels, ref List<User> users, ref List<Milestone> milestones)
        {
            //[ASSIGNEES]           VARCHAR(50) NULL,
            //[BODY]                VARCHAR(max) NOT NULL,
            //[CLOSEDBY]            INT NULL,
            //[CLOSEDDAY]           DATETIME NULL,
            //[CRTNDATE]            DATETIME NOT NULL,
            //[CRTNUSER]            INT NOT NULL,
            //[LABELS]              VARCHAR(50) NOT NULL,
            //[LASTUPD]             DATETIME NULL,
            //[MILESTONES]          VARCHAR(50) NULL,
            //[NUMBER]              INT NOT NULL,
            //[Pk_IssueTracking_Id] INT IDENTITY(1, 1) NOT NULL,
            //[PROJECTS]            VARCHAR(50) NULL,
            //[STATE]               INT NOT NULL CONSTRAINT[DF_ISSUES_STATE] DEFAULT 0,
            //[TITLE]               VARCHAR(50) NOT NULL,
            //[TOTALCOMMENTS] AS [issues].[Function_TotalComments]([Pk_IssueTracking_Id]),
            //CONSTRAINT[PK_IssueDetailsTracking_Id] PRIMARY KEY CLUSTERED([Pk_IssueTracking_Id] ASC)

            Issue result = new Issue();
            int idx = 0;

            result.Assignees.FillOutIssueList<User>(users, reader[idx++], ref result);
            result.Body = Convert.ToString(reader[idx++]);
            //result.ClosedBy = Convert.ToInt32(index++]);
            idx++;
            //result.ClosedDate = Convert.ToDateTime(reader[3]);
            idx++;
            result.CrtnDate = Convert.ToDateTime(reader[idx++]);
            result.CrtnUser = Convert.ToString(reader[idx++]);
            result.Labels.FillOutIssueList<Label>(labels, reader[idx++], ref result);
            //result.LastUpdDate = Convert.ToDateTime(reader[7]);
            idx++;
            result.Milestones.FillOutIssueList<Milestone>(milestones, reader[idx++], ref result);
            result.Number = Convert.ToString(reader[idx++]);
            result.Id= Convert.ToInt32(reader[idx++]);
            //result.Projects.FillOutIssueList<Project>(labels, reader[11], ref result);
            idx++;
            result.State = (State)Convert.ToInt32(reader[idx++]);
            result.Title = Convert.ToString(reader[idx++]);
            result.TotalComments = Convert.ToInt32(reader[idx++]);
            
            return result;
        }

        /// <summary>
        /// Converts the results obtained from the model to the type of data expected by the application.
        /// </summary>
        /// <param name="reader">Reader with datas</param>
        /// <returns>Label</returns>
        internal static Label SQLLabelConverter(SqlDataReader reader)
        {
            //[DESCRIPTION]         VARCHAR(50) NOT NULL,
            //[COLOR]               VARCHAR(50) NOT NULL,
            //[CRTNDATE]            VARCHAR(50) NOT NULL,
            //[CRTNUSER]            INT NOT NULL,
            //[Pk_LabelTracking_Id] INT IDENTITY(1, 1) NOT NULL,
            //[NAME]                VARCHAR(50) NOT NULL,
            //CONSTRAINT[PK_LabelTracking_Id] PRIMARY KEY CLUSTERED([Pk_LabelTracking_Id] ASC)
            
            Label result = new Label();
            int idx = 0;

            result.Description = Convert.ToString(reader[idx++]);
            result.Color = Convert.ToString(reader[idx++]).ToBrush();
            result.CrtnDate = Convert.ToDateTime(reader[idx++]);
            result.CrtnUser = Convert.ToString(reader[idx++]);
            result.Name = Convert.ToString(reader[idx++]);

            return result;
        }

        /// <summary>
        /// Converts the results obtained from the model to the type of data expected by the application.
        /// </summary>
        /// <param name="reader">Reader with datas</param>
        /// <returns>Milestone</returns>
        internal static Milestone SQLMilestoneConverter(SqlDataReader reader)
        {
            //[CRTNDATE]                   VARCHAR(50) NOT NULL,
            //[CRTNUSER]                   INT NOT NULL,
            //[DESCRIPTION]                VARCHAR(50) NOT NULL,
            //[NUMBER]                     INT NOT NULL,
            //[Pk_MilestoneTracking_Id]    INT IDENTITY NOT NULL,
            //[STATE]                      INT NOT NULL,
            //[TITLE]                      VARCHAR(50) NOT NULL,
            //CONSTRAINT[PK_MilestoneTracking_Id] PRIMARY KEY CLUSTERED([PK_MilestoneTracking_Id] ASC)
            
            Milestone result = new Milestone();
            int idx = 0;

            result.CrtnDate = Convert.ToDateTime(reader[idx++]);
            result.CrtnUser = Convert.ToString(reader[idx++]);
            result.Description = Convert.ToString(reader[idx++]);
            result.Number = Convert.ToString(reader[idx++]);
            result.Id = Convert.ToInt32(reader[idx++]);
            result.State = (State)Enum.Parse(typeof(State), reader[idx++].ToString());
            result.Title = Convert.ToString(reader[idx++]);
           
            return result;
        }

        /// <summary>
        /// Converts the results obtained from the model to the type of data expected by the application.
        /// </summary>
        /// <param name="reader">Reader with datas</param>
        /// <returns>User</returns>
        internal static User SQLUserConverter(SqlDataReader reader)
        {
            //[CRTNDATE]           DATETIME NOT NULL,
            //[GU]                 VARCHAR(50) NOT NULL,
            //[NAME]               VARCHAR(50) NOT NULL,
            //[PK_Usertracking_Id] INT IDENTITY(1, 1) NOT NULL,
            //[SURNAME]            VARCHAR(50) NULL,
            //CONSTRAINT[PK_UserTracking_Id] PRIMARY KEY CLUSTERED([PK_Usertracking_Id] ASC)

            User result = new User();
            int idx = 0;

            result.CrtnDate = Convert.ToDateTime(reader[idx++]);
            result.Gu = Convert.ToString(reader[idx++]);
            result.Name = Convert.ToString(reader[idx++]);
            result.SurName = Convert.ToString(reader[idx++]);
           
            return result;
        }
    }
}
