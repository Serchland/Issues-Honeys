using IssuesHoneys.Business.Types;
using IssuesHoneys.Core.NameDefinition;
using IssuesHoneys.Services.Interfaces;
using IssuesHoneys.Services.NameDefinition;
using IssuesHoneys.Services.Types;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace IssuesHoneys.Services.SQL
{
    /// <summary>
    /// Redirects the implemented functionality to the SQL model
    /// </summary>
    public class IssueSQLService : BindableBase, IIssueService
    {

        //private List<User> _assigneeUsers = null;
        //private List<Issue> _iisues = null;
        //private List<Label> _labels = null;
        //private List<Milestone> _milestones = null;
        //private List<User> _users = null;

        /// <summary>
        /// Create a LABEL in the sql model. Implementation of IIssueService.CreateLabel
        /// </summary>
        /// <param name="newLabel"></param>
        public void AddLabel(Label newLabel)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[Captions.AppSettings.HONEYSCONTEXT].ConnectionString;
            var description = newLabel.Description;
            var color = newLabel.Color;
            var crtnDate = DateTime.Now;
            var crtnUser = 1;
            var name = newLabel.Name;
            int labelId;

            var queryString = $@"                                    
                                    INSERT INTO [issues].[LABELS]
                                        ([DESCRIPTION]
                                        ,[COLOR]
                                        ,[CRTNDATE]
                                        ,[Fk_CRTNUSER]
                                        ,[ISACTIVE]
                                        ,[LABELTYPE]
                                        ,[NAME])
                                    VALUES
                                        ('{description}'
                                        ,'{color}'
                                        ,'{crtnDate}'
                                        , {crtnUser}
                                        , 1
                                        , 1
                                        ,'{name}'); SELECT SCOPE_IDENTITY();
                               ";


            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                labelId = Convert.ToInt32(command.ExecuteScalar());
            };


            AddHistorical(HistoricalEnum.ADDLABEL, null, labelId);
        }

        /// <summary>
        /// Gets users assigned to ISSUE. Implementation of IIssueService.GetIssues
        /// </summary>
        public List<User> GetAssignedUsersToIssue(int issueId)
        {
            var assigneeUsers = new List<User>();
            var connectionString = ConfigurationManager.ConnectionStrings[Captions.AppSettings.HONEYSCONTEXT].ConnectionString;
            var queryString = $@"SELECT * FROM [HONEYS].[issues].[USERS] WHERE ID IN " +
                "(SELECT Fk_USERASSIGNEE FROM [HONEYS].[issues].[USERSTOISSUES] WHERE Fk_ISSUE = " +  issueId + ")";

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        assigneeUsers.Add(Converters.SQLUserConverter(reader));
                    }
                }
            };

            return assigneeUsers;
        }

        /// <summary>
        /// Gets users assigned labels to ISSUE. Implementation of IIssueService.GetLabelsToIssue
        /// </summary>
        public List<Label> GetAssignedLabelsToIssue(int issueId)
        {
            var labels = new List<Label>();
            var connectionString = ConfigurationManager.ConnectionStrings[Captions.AppSettings.HONEYSCONTEXT].ConnectionString;
            var queryString = $@"SELECT * FROM [HONEYS].[issues].[LABELS] WHERE ID IN " +
                "(SELECT Fk_LABEL FROM [HONEYS].[issues].[LABELSTOISSUES] WHERE Fk_ISSUE = " + issueId + ")";

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        labels.Add(Converters.SQLLabelConverter(reader, this));
                    }
                }
            };

            return labels;
        }

        /// <summary>
        /// Gets users assigned to ISSUE. Implementation of IIssueService.GetIssues
        /// </summary>
        public List<Milestone> GetAssignedMilestonesToIssue(int issueId)
        {
            var milestones = new List<Milestone>();
            var connectionString = ConfigurationManager.ConnectionStrings[Captions.AppSettings.HONEYSCONTEXT].ConnectionString;
            var queryString = $@"SELECT * FROM [HONEYS].[issues].[MILESTONES] WHERE ID IN " +
                "(SELECT Fk_MILESTONE FROM [HONEYS].[issues].[MILESTONESTOISSUES] WHERE Fk_ISSUE = " + issueId + ")";

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        milestones.Add(Converters.SQLMilestoneConverter(reader));
                    }
                }
            };

            return milestones;
        }

        /// <summary>
        /// Obtain ISSUES from the sql model. Implementation of IIssueService.GetIssues
        /// </summary>
        public List<Issue> GetIssues()
        {
            var issues = new List<Issue>();
            var connectionString = ConfigurationManager.ConnectionStrings[Captions.AppSettings.HONEYSCONTEXT].ConnectionString;
            var queryString = $@"SELECT * FROM [HONEYS].[issues].[ISSUES];";

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        issues.Add(Converters.SQLIssueConverter(reader, this));
                    }
                }
            };

            return issues;
        }

        public int GetIssuesWithLabelId(int labelID)
        {
            int result = 0;
            var connectionString = ConfigurationManager.ConnectionStrings[Captions.AppSettings.HONEYSCONTEXT].ConnectionString;
            var queryString = $@"SELECT COUNT(ID) FROM issues.LABELSTOISSUES WHERE FK_LABEL =" + labelID.ToString() + "";

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                result = Convert.ToInt32(command.ExecuteScalar());
            };

            return result;
        }

        /// <summary>
        /// Obtain LABELS from the SQL model. Implementation of IIssueService.GetLabels
        /// </summary>
        /// <returns>List<Label></returns>
        public List<Label> GetLabels(LabelType labelType)
        {
            //SERCH00: Assess whether it is necessary to have the values in memory
            var labels = new List<Label>();
            var connectionString = ConfigurationManager.ConnectionStrings["HONEYSCONTEXT"].ConnectionString;
            var queryString = $@"SELECT * FROM [HONEYS].[issues].[LABELS] WHERE ISACTIVE = 1;";
            switch (labelType)
            {
                case LabelType.Issue:
                    queryString = $@"SELECT * FROM [HONEYS].[issues].[LABELS] WHERE ISACTIVE = 1 AND LABELTYPE = 1";
                    break;

                case LabelType.Project:
                    queryString = $@"SELECT * FROM [HONEYS].[issues].[LABELS] WHERE ISACTIVE = 1 AND LABELTYPE = 2";
                    break;

                default:
                    throw new ArgumentException(Application.Current.FindResource(MessagesResources.AppArgumentException).ToString());
            }
                
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        labels.Add(Converters.SQLLabelConverter(reader, this));
                    }
                }
            };
            
            return labels;
        }

        /// <summary>
        /// Obtain MILESTONES from the SQL model. Implementation of IIssueService.GetMillestones
        /// </summary>
        /// <returns>List<Milestone></returns>
        public List<Milestone> GetMilestones()
        {
            //SERCH00: Assess whether it is necessary to have the values in memory
            var milestones = new List<Milestone>();
            var connectionString = ConfigurationManager.ConnectionStrings["HONEYSCONTEXT"].ConnectionString;
            var queryString = $@"SELECT * FROM [HONEYS].[issues].[MILESTONES];";

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        milestones.Add(Converters.SQLMilestoneConverter(reader));
                    }
                }
            };

            return milestones;
        }

        /// <summary>
        /// Obtain USERS from the SQL model. Implementation of IIssueService.GetUsers
        /// </summary>
        /// <returns>List<User></returns>
        public List<User> GetUsers()
        {

            var users = new List<User>();
            var connectionString = ConfigurationManager.ConnectionStrings["HONEYSCONTEXT"].ConnectionString;
            var queryString = $@"SELECT * FROM [HONEYS].[issues].[USERS];";

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(Converters.SQLUserConverter(reader));
                    }
                }
            };


            return users;
        }

        public void UpdateLabel(Label updateLabel)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[Captions.AppSettings.HONEYSCONTEXT].ConnectionString;

            var description = updateLabel.Description;
            var color = updateLabel.Color;
            var crtnDate = DateTime.Now;
            var crtnUser = 1;
            var labelId = updateLabel.Id;
            var name = updateLabel.Name;

            var queryString = $@"                                    
                                    UPDATE [issues].[LABELS]
                                    SET [DESCRIPTION] = '{description}'
                                        ,[COLOR] = '{color}'
                                        ,[CRTNDATE] = '{crtnDate}'
                                        ,[Fk_CRTNUSER] = {crtnUser}
                                        ,[NAME] = '{name}'
                                    WHERE [ID] = {labelId}";


            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteScalar();
            };

            AddHistorical(HistoricalEnum.UPDATELABEL, null, labelId);
        }

        public void DeleteLabel(int labelId)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[Captions.AppSettings.HONEYSCONTEXT].ConnectionString;


            var queryString = $@"                                    
                                    UPDATE [issues].[LABELS]
                                    SET [ISACTIVE] = 0
                                    WHERE [ID] = {labelId}";


            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteScalar();
            };

            AddHistorical(HistoricalEnum.DELETELABEL, null, labelId);
        }

        public User GetUserById(int? userId)
        {
            var user = new User();
            var connectionString = ConfigurationManager.ConnectionStrings["HONEYSCONTEXT"].ConnectionString;
            var queryString = $@"SELECT * FROM [HONEYS].[issues].[USERS] WHERE ID = {userId}";

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = Converters.SQLUserConverter(reader);
                    }
                }
            };


            return user;
        }

        public Issue GetIssueById(int issueId)
        {
            var issue = new Issue();
            var connectionString = ConfigurationManager.ConnectionStrings["HONEYSCONTEXT"].ConnectionString;
            var queryString = $@"SELECT * FROM [HONEYS].[issues].[ISSUES] WHERE ID = {issueId}";

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        issue = Converters.SQLIssueConverter(reader, this);
                    }
                }
            };


            return issue;
        }

        public void AddUserToIssue(int issueId, int userId)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["HONEYSCONTEXT"].ConnectionString;
            var crtnUser = 1;
            var crtnDate = DateTime.Now;
            var queryString = $@"                                    
                                    INSERT INTO [issues].[USERSTOISSUES]
                                        ([CRTNDATE]
                                        ,[Fk_CRTNUSER]
                                        ,[Fk_ISSUE]
                                        ,[Fk_USERASSIGNEE]
                                        ,[ISACTIVE])
                                    VALUES
                                        ('{crtnDate}'
                                        ,{crtnUser}
                                        ,{issueId} 
                                        ,{userId}
                                        ,{1})
                               ";

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteScalar();
            };

            AddHistorical(HistoricalEnum.ADDUSERTOIISUE, issueId, userId);
        }

        public void DeleteUserToIssue(int issueId, int userId)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["HONEYSCONTEXT"].ConnectionString;
            var queryString = $@"                                    
                                    DELETE FROM [issues].[USERSTOISSUES] 
                                    WHERE Fk_ISSUE = {issueId} AND Fk_USERASSIGNEE = {userId}";

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteScalar();
            };

            AddHistorical(HistoricalEnum.DELETEUSERTOIISUE, issueId, userId);
        }

        public void AddLabelToIssue(int issueId, int labelId)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["HONEYSCONTEXT"].ConnectionString;
            var crtnUser = 1;
            var crtnDate = DateTime.Now;
            var queryString = $@"                                    
                                    INSERT INTO [issues].[LABELSTOISSUES]
                                        ([CRTNDATE]
                                        ,[Fk_CRTNUSER]
                                        ,[Fk_LABEL]
                                        ,[Fk_ISSUE]
                                        ,[ISACTIVE])
                                    VALUES
                                        ('{crtnDate}'
                                        ,{crtnUser}
                                        ,{labelId}
                                        ,{issueId} 
                                        ,{1})
                               ";

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteScalar();
            };

            AddHistorical(HistoricalEnum.ADDLABELTOIISUE, issueId, labelId);
        }

        public void DeleteLabelToIssue(int issueId, int labelId)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["HONEYSCONTEXT"].ConnectionString;
            var queryString = $@"                                    
                                    DELETE FROM [issues].[LABELSTOISSUES] 
                                    WHERE Fk_ISSUE = {issueId} AND Fk_LABEL = {labelId}";

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteScalar();
            };

            AddHistorical(HistoricalEnum.DELETELABELTOIISUE, issueId, labelId);
        }

        public void DeleteMilestoneToIssue(int issueId, int milestoneId)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["HONEYSCONTEXT"].ConnectionString;
            var queryString = $@"                                    
                                    DELETE FROM [issues].[MILESTONESTOISSUES]
                                    WHERE Fk_ISSUE = {issueId} AND Fk_MILESTONE = {milestoneId}";

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteScalar();
            };

            AddHistorical(HistoricalEnum.DELETEMILESTONETOIISUE, issueId, milestoneId);
        }

        public void AddMilestoneToIssue(int issueId, int milestoneId)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["HONEYSCONTEXT"].ConnectionString;
            var crtnUser = 1;
            var crtnDate = DateTime.Now;
            var queryString = $@"                                    
                                    INSERT INTO [issues].[MILESTONESTOISSUES]
                                        ([CRTNDATE]
                                        ,[Fk_CRTNUSER]
                                        ,[Fk_MILESTONE]
                                        ,[Fk_ISSUE]
                                        ,[ISACTIVE])
                                    VALUES
                                        ('{crtnDate}'
                                        ,{crtnUser}
                                        ,{milestoneId}
                                        ,{issueId} 
                                        ,{1})
                               ";

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteScalar();
            };

            AddHistorical(HistoricalEnum.ADDMILESTONETOIISUE, issueId, milestoneId);
        }

        async void AddHistorical (HistoricalEnum historicalEnum, int? issueId, int? toId)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["HONEYSCONTEXT"].ConnectionString;
            var crtnUser = 1;
            var crtnDate = DateTime.Now;
            var queryString = string.Empty;

            var histValue = (int)historicalEnum;

            if (issueId == null)
            {
                queryString = $@"                                    
                                    INSERT INTO [issues].[ISSUEHISTORICAL]
                                        ([ACTION],
                                         [CRTNDATE],
                                         [Fk_CRTNUSER],
                                         [TOID])
                                    VALUES
                                        ({histValue}
                                        ,'{crtnDate}'
                                        ,{crtnUser}
                                        ,{toId})
                               ";
            }

            if (toId == null)
            {
                queryString = $@"                                    
                                    INSERT INTO [issues].[ISSUEHISTORICAL]
                                        ([ACTION],
                                         [CRTNDATE],
                                         [Fk_CRTNUSER],
                                         [Fk_ISSUE])
                                    VALUES
                                        ({histValue}
                                        ,'{crtnDate}'
                                        ,{crtnUser}
                                        ,{issueId})
                               ";
            }

            if (issueId != null && toId != null)
            {
                queryString = $@"                                    
                                    INSERT INTO [issues].[ISSUEHISTORICAL]
                                        ([ACTION],
                                         [CRTNDATE],
                                         [Fk_CRTNUSER],
                                         [Fk_ISSUE],
                                         [TOID])
                                    VALUES
                                        ({histValue}
                                        ,'{crtnDate}'
                                        ,{crtnUser}
                                        ,{issueId}
                                        ,{toId})
                               ";
            }

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                var res = await command.ExecuteScalarAsync();
            };
        }
    }
}
