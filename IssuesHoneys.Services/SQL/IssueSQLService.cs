using IssuesHoneys.Business.Types;
using IssuesHoneys.Services.Interfaces;
using IssuesHoneys.Services.NameDefinition;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

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
        public void CreateLabel(Label newLabel)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[Captions.AppSettings.HONEYSCONTEXT].ConnectionString;
            var description = newLabel.Description;
            var color = newLabel.Color;
            var crtnDate = DateTime.Now;
            var crtnUser = 1;
            var name = newLabel.Name;

            var queryString = $@"                                    
                                    INSERT INTO [issues].[LABELS]
                                        ([DESCRIPTION]
                                        ,[COLOR]
                                        ,[CRTNDATE]
                                        ,[Fk_CRTNUSER]
                                        ,[NAME])
                                    VALUES
                                        ('{description}'
                                        ,'{color}'
                                        ,'{crtnDate}'
                                        ,{crtnUser}
                                        ,'{name}')
                               ";


            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteScalar();
            };

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
        public List<Label> GetLabels()
        {
            //SERCH00: Assess whether it is necessary to have the values in memory
            var labels = new List<Label>();
            var connectionString = ConfigurationManager.ConnectionStrings["HONEYSCONTEXT"].ConnectionString;
            var queryString = $@"SELECT * FROM [HONEYS].[issues].[LABELS] WHERE ISACTIVE = 1;";

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
            var id = updateLabel.Id;
            var name = updateLabel.Name;

            var queryString = $@"                                    
                                    UPDATE [issues].[LABELS]
                                    SET [DESCRIPTION] = '{description}'
                                        ,[COLOR] = '{color}'
                                        ,[CRTNDATE] = '{crtnDate}'
                                        ,[Fk_CRTNUSER] = {crtnUser}
                                        ,[NAME] = '{name}'
                                    WHERE [ID] = {id}";


            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteScalar();
            };
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

        public Issue GetIssuesById(int issueId)
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
    }
}
