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
        private List<Issue> _iisues = null;
        private List<Label> _labels = null;
        private List<Milestone> _milestones = null;
        private List<User> _users = null;

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
                                        ,[CRTNUSER]
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
        /// Obtain ISSUES from the sql model. Implementation of IIssueService.GetIssues
        /// </summary>
        public List<Issue> GetIssues()
        {
            _iisues = new List<Issue>();
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
                        _iisues.Add(Converters.SQLIssueConverter(reader, ref _labels, ref _users, ref _milestones));
                    }
                }
            };

            return _iisues;
        }

        public int GetIssuesWithLabelId(int labelID)
        {
            int result = 0;
            var connectionString = ConfigurationManager.ConnectionStrings[Captions.AppSettings.HONEYSCONTEXT].ConnectionString;
            var queryString = "SELECT COUNT(ID) FROM issues.ISSUES WHERE ASSIGNEES LIKE '%" + labelID.ToString() + "%'";

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
            if (_labels == null)
            {
                _labels = new List<Label>();
                var connectionString = ConfigurationManager.ConnectionStrings["HONEYSCONTEXT"].ConnectionString;
                var queryString = "SELECT * FROM [HONEYS].[issues].[LABELS];";

                Converters.IssueService = this;

                using (var connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand(queryString, connection);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            _labels.Add(Converters.SQLLabelConverter(reader));
                        }
                    }
                };
            }

            Converters.IssueService = null;
            return _labels;
        }

        /// <summary>
        /// Obtain MILESTONES from the SQL model. Implementation of IIssueService.GetMillestones
        /// </summary>
        /// <returns>List<Milestone></returns>
        public List<Milestone> GetMillestones()
        {
            //SERCH00: Assess whether it is necessary to have the values in memory
            if (_milestones == null)
            {
                _milestones = new List<Milestone>();
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
                            _milestones.Add(Converters.SQLMilestoneConverter(reader));
                        }
                    }
                };
            }

            return _milestones;
        }

        /// <summary>
        /// Obtain USERS from the SQL model. Implementation of IIssueService.GetUsers
        /// </summary>
        /// <returns>List<User></returns>
        public List<User> GetUsers()
        {
            //SERCH00: Assess whether it is necessary to have the values in memory
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
    }
}
