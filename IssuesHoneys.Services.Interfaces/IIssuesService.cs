using IssuesHoneys.Business;
using System.Collections.Generic;

namespace IssuesHoneys.Services.Interfaces
{
    /// <summary>
    /// Contract used by the service layer
    /// </summary>
    public interface IIssueService 
    {
        /// <summary>
        /// Method used to create a new label in the model
        /// </summary>
        void CreateLabel();

        /// <summary>
        /// Method used to obtain the issues from the model
        /// </summary>
        /// <returns></returns>
        List<Issue> GetIssues();

        /// <summary>
        /// Method used to obtain the labels from the model
        /// </summary>
        /// <returns></returns>
        List<Label> GetLabels();

        /// <summary>
        /// Method used to obtain the milestones from the model
        /// </summary>
        /// <returns></returns>
        List<Milestone> GetMillestones();

        /// <summary>
        /// Method used to obtain the users from the model
        /// </summary>
        /// <returns></returns>
        List<User> GetUsers();
    }
}
