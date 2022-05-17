using IssuesHoneys.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace IssuesHoneys.BusinessTypes
{
    /// <summary>
    /// Extension methods used by the application
    /// </summary>
    public static class Extensions
    {

        /// <summary>
        /// Allows to fill in the types list<T> in the ISSUE object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listObject"></param>
        /// <param name="loadedObject"></param>
        /// <param name="reader"></param>
        /// <param name="issue"></param>
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

        /// <summary>
        /// Converts a colour in hexadecimal format to Brush
        /// </summary>
        /// <param name="HexColorString"></param>
        /// <returns>Brush</returns>
        public static Brush ToBrush(this string HexColorString)
        {
            return (Brush)new BrushConverter().ConvertFromString(HexColorString);
        }

        /// <summary>
        /// Get the total number of ISSUES containing the LABEL identifier passed as a parameter
        /// </summary>
        /// <param name="label"></param>
        /// <param name="labelId"></param>
        /// <param name="issueService"></param>
        /// <returns></returns>
        public static int GetTotalIssuesWithLabel(this Label label, int labelId, IIssueService issueService)
        {
            return issueService.GetIssuesWithLabelId(labelId);
        }
    }
}