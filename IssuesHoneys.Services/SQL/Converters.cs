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
            Issue result = new Issue();

            //Id - Number - CrtnUser, Title, Body, ClosedBy, Labels, Assignees, Milestones, Comments, CrtnDate, ClosedDay, LastUpdate, Projects, State, TotalComments
            result.Id = Convert.ToInt32(reader[0]);
            result.Number = Convert.ToString(reader[1]);
            result.CrtnUser = Convert.ToString(reader[2]);
            result.Title = Convert.ToString(reader[3]);
            result.Body = Convert.ToString(reader[4]);
            //result.ClosedBy = Convert.ToInt32(reader[5]);
            result.Labels.FillOutIssueList<Label>(labels, reader[6], ref result);
            result.Assignees.FillOutIssueList<User>(users, reader[7], ref result);
            result.Milestones.FillOutIssueList<Milestone>(milestones, reader[8], ref result);
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

        /// <summary>
        /// Converts the results obtained from the model to the type of data expected by the application.
        /// </summary>
        /// <param name="reader">Reader with datas</param>
        /// <returns>Label</returns>
        internal static Label SQLLabelConverter(SqlDataReader reader)
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

        /// <summary>
        /// Converts the results obtained from the model to the type of data expected by the application.
        /// </summary>
        /// <param name="reader">Reader with datas</param>
        /// <returns>Milestone</returns>
        internal static Milestone SQLMilestoneConverter(SqlDataReader reader)
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

        /// <summary>
        /// Converts the results obtained from the model to the type of data expected by the application.
        /// </summary>
        /// <param name="reader">Reader with datas</param>
        /// <returns>User</returns>
        internal static User SQLUserConverter(SqlDataReader reader)
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
    }
}
