using IssuesHoneys.Business;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace IssuesHoneys.Services.SQL.Converters
{
    public static class SQLConverters
    {
        public static Label SQLLabelConverter(SqlDataReader reader)
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

        public static Milestone SQLMilestoneConverter(SqlDataReader reader)
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
