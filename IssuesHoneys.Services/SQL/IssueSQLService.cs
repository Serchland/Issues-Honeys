using IssuesHoneys.BusinessTypes;
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
            List<User> _assigneeUsers = new List<User>();

            var connectionString = ConfigurationManager.ConnectionStrings[Captions.AppSettings.HONEYSCONTEXT].ConnectionString;
            var queryString = "SELECT * FROM [HONEYS].[issues].[USERS] WHERE ID IN " +
                "(SELECT Fk_USERASSIGNEE FROM [HONEYS].[issues].[USERSTOISSUES] WHERE Fk_ISSUE = " +  issueId + ")";

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        _assigneeUsers.Add(Converters.SQLUserConverter(reader));
                    }
                }
            };

            return _assigneeUsers;
        }

        /// <summary>
        /// Gets users assigned labels to ISSUE. Implementation of IIssueService.GetLabelsToIssue
        /// </summary>
        public List<Label> GetAssignedLabelsToIssue(int issueId)
        {
            List<Label> _labels = new List<Label>();

            var connectionString = ConfigurationManager.ConnectionStrings[Captions.AppSettings.HONEYSCONTEXT].ConnectionString;
            var queryString = "SELECT * FROM [HONEYS].[issues].[LABELS] WHERE ID IN " +
                "(SELECT Fk_LABEL FROM [HONEYS].[issues].[LABELSTOISSUES] WHERE Fk_ISSUE = " + issueId + ")";

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        _labels.Add(Converters.SQLLabelConverter(reader, this));
                    }
                }
            };

            return _labels;
        }

        /// <summary>
        /// Gets users assigned to ISSUE. Implementation of IIssueService.GetIssues
        /// </summary>
        public List<Milestone> GetAssignedMilestonesToIssue(int issueId)
        {
            List<Milestone> _milestones = new List<Milestone>();

            var connectionString = ConfigurationManager.ConnectionStrings[Captions.AppSettings.HONEYSCONTEXT].ConnectionString;
            var queryString = "SELECT * FROM [HONEYS].[issues].[MILESTONES] WHERE ID IN " +
                "(SELECT Fk_MILESTONE FROM [HONEYS].[issues].[MILESTONESTOISSUES] WHERE Fk_ISSUE = " + issueId + ")";

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        _milestones.Add(Converters.SQLMilestoneConverter(reader));
                    }
                }
            };

            return _milestones;
        }

        /// <summary>
        /// Obtain ISSUES from the sql model. Implementation of IIssueService.GetIssues
        /// </summary>
        public List<Issue> GetIssues()
        {
            List<Issue> issues = new List<Issue>();

            var connectionString = ConfigurationManager.ConnectionStrings[Captions.AppSettings.HONEYSCONTEXT].ConnectionString;
            var queryString = "SELECT * FROM [HONEYS].[issues].[ISSUES];";

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
            var queryString = "SELECT COUNT(ID) FROM issues.LABELSTOISSUES WHERE FK_LABEL =" + labelID.ToString() + "";

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
            List<Label> labels = new List<Label>();

            var connectionString = ConfigurationManager.ConnectionStrings["HONEYSCONTEXT"].ConnectionString;
            var queryString = "SELECT * FROM [HONEYS].[issues].[LABELS] WHERE ISACTIVE = 1;";

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
        public List<Milestone> GetMillestones()
        {
            //SERCH00: Assess whether it is necessary to have the values in memory
            List<Milestone> milestones = new List<Milestone>();

            var connectionString = ConfigurationManager.ConnectionStrings["HONEYSCONTEXT"].ConnectionString;
            var queryString = "SELECT * FROM [HONEYS].[issues].[MILESTONES];";

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
            //SERCH00: Assess whether it is necessary to have the values in memory
            List<User> _users = new List<User>(); 
            if (_users == null)
            {
                _users = new List<User>();
                var connectionString = ConfigurationManager.ConnectionStrings["HONEYSCONTEXT"].ConnectionString;
                var queryString = "SELECT * FROM [HONEYS].[issues].[USERS];";

                using (var connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand(queryString, connection);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            _users.Add(Converters.SQLUserConverter(reader));
                        }
                    }
                };
            }

            return _users;
        }

        void IIssueService.UpdateLabel(Label label)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[Captions.AppSettings.HONEYSCONTEXT].ConnectionString;

            var id = label.Id;
            var color = label.Color;
            var crtnDate = DateTime.Now;
            var crtnUser = 1;
            var description = label.Description;
            var name = label.Name;

            var queryString = $@"                                    
                                    UPDATE [issues].[LABELS] SET
                                        [DESCRIPTION] = '{description}'
                                        ,[COLOR] = '{color}'
                                        ,[CRTNDATE] = '{crtnDate}'
                                        ,[Fk_CRTNUSER] = {crtnUser}
                                        ,[NAME] = '{name}'
                                    WHERE ID = '{id}'
                               ";


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
                                    UPDATE [issues].[LABELS] SET
                                        [ISACTIVE] = {0}
                                    WHERE ID = {labelId}
                               ";


            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteScalar();
            };
        }
    }
}
