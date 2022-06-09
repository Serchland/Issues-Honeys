using IssuesHoneys.BusinessTypes;
using IssuesHoneys.Services.Interfaces;
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
        //private T GetValue<T>(SqlDataReader sqlDataReader, object type)
        //{

        //    switch (type.GetType())
        //    {
                
        //    }

        //}
        internal static IIssueService IssueService { get; set; }

        /// <summary>
        /// Converts the results obtained from the model to the type of data expected by the application.
        /// </summary>
        /// <param name="reader">Reader with datas</param>
        /// <param name="labels">Labels obtained from the model to cross-reference data</param>
        /// <param name="users">Users obtained from the model to cross-reference data</param>
        /// <param name="milestones">Milestones obtained from the model to cross-reference data</param>
        /// <returns>Issue</returns>
        internal static Issue SQLIssueConverter(SqlDataReader reader, ref List<Label> labels, ref List<User> users, ref List<Milestone> milestones, IIssueService issueService)
        {
            //[BODY] [varchar](max)NOT NULL,
	        //[Fk_CLOSEDBY] [int] NULL,
	        //[CLOSEDDAY] [datetime] NULL,
	        //[CRTNDATE] [datetime] NOT NULL,
            //[Fk_CRTNUSER] [int] NOT NULL,
            //[ID] [int] IDENTITY(1, 1) NOT NULL,
            //[LASTUPD] [datetime] NULL,
	        //[MILESTONES] [varchar](50) NULL,
	        //[NUMBER] [int] NOT NULL,
            //[PROJECTS] [varchar](50) NULL,
	        //[STATE] [int] NOT NULL,
            //[TITLE] [varchar](50) NOT NULL,
            //[TOTALCOMMENTS]  AS([issues].[Function_TotalComments]([ID])),

            Issue result = new Issue();
            int idx = 0;
            int issueId = 5;

            result.Assignees.FillOutAssigneeList<User>(ref result, issueService, Convert.ToInt32(reader[issueId]));
            result.Body = Convert.ToString(reader[idx++]);
            result.ClosedBy = Convert.ToInt32(reader[idx++]);
            result.ClosedDate = Convert.ToDateTime(reader[idx++]);
            result.CrtnDate = Convert.ToDateTime(reader[idx++]);
            result.CrtnUser = Convert.ToString(reader[idx++]);
            result.Id = Convert.ToInt32(reader[idx++]);
            result.LastUpdDate = Convert.ToDateTime(reader[idx++]);
            //result.Milestones.FillOutIssueList<Milestone>(milestones, reader[idx++], ref result);
            idx++;
            result.Number = Convert.ToString(reader[idx++]);
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
            //[DESCRIPTION] [varchar](50) NOT NULL,
            //[COLOR] [varchar](50) NOT NULL,
            //[CRTNDATE] [datetime] NOT NULL,
            //[Fk_CRTNUSER] [int] NOT NULL,
            //[ID] [int] IDENTITY(1, 1) NOT NULL,
            //[NAME] [varchar](50) NOT NULL,

            int labelId = 0;
            Label result = new Label();
            int idx = 0;

            result.Description = Convert.ToString(reader[idx++]);
            result.Color = Convert.ToString(reader[idx++]).ToBrush();
            result.CrtnDate = Convert.ToDateTime(reader[idx++]);
            result.CrtnUser = Convert.ToString(reader[idx++]);
            labelId = Convert.ToInt32(reader[idx++]);
            result.Name = Convert.ToString(reader[idx++]);
            result.TotalIssuesWithLabel = result.GetTotalIssuesWithLabel(labelId, IssueService);
            return result;
        }
        
        /// <summary>
        /// Converts the results obtained from the model to the type of data expected by the application.
        /// </summary>
        /// <param name="reader">Reader with datas</param>
        /// <returns>Milestone</returns>
        internal static Milestone SQLMilestoneConverter(SqlDataReader reader)
        {
            //[CRTNDATE] [datetime] NOT NULL,
            //[DESCRIPTION] [varchar](50) NOT NULL,
            //[Fk_CRTNUSER] [int] NOT NULL,
            //[ID] [int] IDENTITY(1, 1) NOT NULL,
            //[NUMBER] [int] NOT NULL,
            //[STATE] [int] NOT NULL,
            //[TITLE] [varchar](50) NOT NULL,

             Milestone result = new Milestone();
            int idx = 0;

            result.CrtnDate = Convert.ToDateTime(reader[idx++]);
            result.Description = Convert.ToString(reader[idx++]);
            result.CrtnUser = Convert.ToString(reader[idx++]);
            result.Id = Convert.ToInt32(reader[idx++]);
            result.Number = Convert.ToString(reader[idx++]);
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
