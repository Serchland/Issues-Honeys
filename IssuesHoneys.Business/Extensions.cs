using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace IssuesHoneys.Business
{
    public static class Extensions
    {
        public static Brush ToBrush(this string HexColorString)
        {
            return (Brush)new BrushConverter().ConvertFromString(HexColorString);   
        }

        public static void FillOutIssueList<T>(this List<T> listObject, List<T> loadedObject, object reader, ref Issue issue)
        {
            string ids = Convert.ToString(reader);

            if (!string.IsNullOrEmpty(ids))
            {
                if (loadedObject is List<Label>)
                {
                    issue.Labels = new List<Label>();
                    List<Label> tempList = loadedObject.Cast<Label>().ToList();
                    var listIds = ids.Split(';').ToList();
                    var labelsFound = tempList.Where(element => listIds.Any(s => Convert.ToInt32(s) == element.Id)).ToList();

                    issue.Labels.AddRange(labelsFound);
                }

                if (loadedObject is List<Milestone>)
                {
                    issue.Milestones = new List<Milestone>();
                    List<Milestone> tempList = loadedObject.Cast<Milestone>().ToList();
                    var listIds = ids.Split(';').ToList();
                    var milestonesFound = tempList.Where(element => listIds.Any(s => Convert.ToInt32(s) == element.Id)).ToList();

                    issue.Milestones.AddRange(milestonesFound);
                }

                if (loadedObject is List<User>)
                {
                    issue.Assignees = new List<User>();
                    List<User> tempList = loadedObject.Cast<User>().ToList();
                    var listIds = ids.Split(';').ToList();
                    var usersFound = tempList.Where(element => listIds.Any(s => Convert.ToInt32(s) == element.Id)).ToList();

                    issue.Assignees.AddRange(usersFound);
                }

            }
        }
    }
}