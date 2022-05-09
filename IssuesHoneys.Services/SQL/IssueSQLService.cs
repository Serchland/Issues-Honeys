using IssuesHoneys.Business;
using IssuesHoneys.Services.Interfaces;
using IssuesHoneys.Services.SQL.Converters;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace IssuesHoneys.Services.SQL
{
    public class IssueSQLService : BindableBase, IIssueService
    {
        private List<Label> _labels = null;
        private List<Milestone> _milestones = null;

        public void CreateLabel()
        {
            throw new NotImplementedException();
        }

        public List<Issue> GetIssues()
        {
            throw new NotImplementedException();
        }

        public List<Label> GetLabels()
        {
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
                            _labels.Add(SQLConverters.SQLLabelConverter(reader));
                        }
                    }
                };
            }
            
            return _labels;
        }

        public List<Milestone> GetMillestones()
        {
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
                            _milestones.Add(SQLConverters.SQLMilestoneConverter(reader));
                        }
                    }
                };
            }

            return _milestones;
        }
    }
}
