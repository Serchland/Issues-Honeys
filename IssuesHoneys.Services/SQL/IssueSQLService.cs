using IssuesHoneys.Business;
using IssuesHoneys.Services.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace IssuesHoneys.Services.SQL
{
    public class IssueSQLService : BindableBase, IIssueService
    {
        protected List<Label> _labels = null;
        protected List<Milestone> _milestones = null;
        protected List<Issue> _iisues = null;
        protected List<User> _users = null;

        public void CreateLabel()
        {
            throw new NotImplementedException();
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
                            _users.Add(SQLUserConverter(reader));
                        }
                    }
                };
            }

            return _users;
        }

        private User SQLUserConverter(SqlDataReader reader)
        {
            User result = new User();

            //Id - Name - Surname - Gu - CtnDate
            result.Id = Convert.ToInt32(reader[0]);
            result.Name = Convert.ToString(reader[1]);
            result.SurName = Convert.ToString(reader[2]);
            result.Gu = Convert.ToString(reader[3]);
            result.CrtnDate = Convert.ToDateTime(reader[4]);

            return result;
        }

        public List<Issue> GetIssues()
        {
            _iisues = new List<Issue>();
            var connectionString = ConfigurationManager.ConnectionStrings["HONEYSCONTEXT"].ConnectionString;

            string queryString = "SELECT * FROM ISSUES;";
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        _iisues.Add(SQLIssueConverter(reader));
                    }
                }
            };

            return _iisues;
        }

        private Issue SQLIssueConverter(SqlDataReader reader)
        {
            Issue result = new Issue();

            //Id - Number - CrtnUser, Title, Body, ClosedBy, Labels, Assignees, Milestones, Comments, CrtnDate, ClosedDay, LastUpdate, Projects, State, TotalComments
            result.Id = Convert.ToInt32(reader[0]);
            result.Number = Convert.ToString(reader[1]);
            result.CrtnUser = Convert.ToString(reader[2]);
            result.Title = Convert.ToString(reader[3]);
            result.Body = Convert.ToString(reader[4]);
            //result.ClosedBy = Convert.ToInt32(reader[5]);
            result.Labels.FillOutIssueList<Label>(_labels, reader[6], ref result);
            result.Assignees.FillOutIssueList<User>(_users, reader[7], ref result);
            result.Milestones.FillOutIssueList<Milestone>(_milestones, reader[8], ref result);
            result.TotalComments = Convert.ToInt32(reader[9]);
            result.CrtnDate = Convert.ToDateTime(reader[10]);
            //SERCH00: Check null value
            //result.ClosedDate = Convert.ToDateTime(reader[11]);
            //SERCH00: Check null value
            //result.LastUpdDate = Convert.ToDateTime(reader[12]);
            //SERCH00: Pending to implemet
            //result.Projects
            result.State = (State)Convert.ToInt32(reader[14]);
            result.TotalComments = Convert.ToInt32(reader[15]);

            return result;
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
                            _labels.Add(SQLLabelConverter(reader));
                        }
                    }
                };
            }
            
            return _labels;
        }

        private Label SQLLabelConverter(SqlDataReader reader)
        {
            Label result = new Label();

            //Id - Name - Color - Description - CrtnUser - CtnDate
            result.Id = Convert.ToInt32(reader[0]);
            result.Name = Convert.ToString(reader[1]);
            result.Color = Convert.ToString(reader[2]).ToBrush();
            result.Description = Convert.ToString(reader[3]);
            result.CrtnUser = Convert.ToString(reader[4]);
            result.CrtnDate = Convert.ToDateTime(reader[5]);

            return result;
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
                            _milestones.Add(SQLMilestoneConverter(reader));
                        }
                    }
                };
            }

            return _milestones;
        }

        private static Milestone SQLMilestoneConverter(SqlDataReader reader)
        {
            Milestone result = new Milestone();

            //Id - Number - State - Title - Description - CrtnUser - CrtnDate
            result.Id = Convert.ToInt32(reader[0]);
            result.Number = Convert.ToString(reader[1]);
            result.State = (State)Enum.Parse(typeof(State), reader[2].ToString());
            result.Title = Convert.ToString(reader[3]);
            result.Description = Convert.ToString(reader[4]);
            result.CrtnUser = Convert.ToString(reader[5]);
            result.CrtnDate = Convert.ToDateTime(reader[6]);

            return result;
        }
    }
}
