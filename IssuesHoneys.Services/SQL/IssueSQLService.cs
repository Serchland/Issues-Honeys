using IssuesHoneys.Business;
using IssuesHoneys.Services.Interfaces;
using IssuesHoneys.Services.NameDefinition;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace IssuesHoneys.Services.SQL
{
    public class IssueSQLService : BindableBase, IIssueService
    {
        private List<Issue> _iisues = null;
        private List<Label> _labels = null;
        private List<Milestone> _milestones = null;
        private List<User> _users = null;

        public void CreateLabel()
        {
            throw new NotImplementedException();
        }

        public List<Issue> GetIssues()
        {
            _iisues = new List<Issue>();
            var connectionString = ConfigurationManager.ConnectionStrings[Captions.AppSettings.HONEYSCONTEXT].ConnectionString;

            string queryString = "SELECT * FROM ISSUES;";
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

        public List<Label> GetLabels()
        {
            //SERCH00: Assess whether it is necessary to have the values in memory
            if (_labels == null)
            {
                _labels = new List<Label>();
                var connectionString = ConfigurationManager.ConnectionStrings["HONEYSCONTEXT"].ConnectionString;

                string queryString = "SELECT * FROM LABELS;";
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
            
            return _labels;
        }

        public List<Milestone> GetMillestones()
        {
            //SERCH00: Assess whether it is necessary to have the values in memory
            if (_milestones == null)
            {
                _milestones = new List<Milestone>();
                var connectionString = ConfigurationManager.ConnectionStrings["HONEYSCONTEXT"].ConnectionString;

                string queryString = "SELECT * FROM MILESTONES;";
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

        public List<User> GetUsers()
        {
            //SERCH00: Assess whether it is necessary to have the values in memory
            if (_users == null)
            {
                _users = new List<User>();
                var connectionString = ConfigurationManager.ConnectionStrings["HONEYSCONTEXT"].ConnectionString;

                string queryString = "SELECT * FROM USERS;";
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
