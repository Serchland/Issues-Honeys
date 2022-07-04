using IssuesHoneys.Business.Types;
using System.Collections.Generic;

namespace IssuesHoneys.Services.Interfaces
{
    /// <summary>
    /// Contract used by the service layer
    /// </summary>
    public interface IIssueService 
    {
        /// <summary>
        /// Method used to create a new LABEL in the model
        /// </summary>
        void CreateLabel(Label newLabel);

        /// <summary>
        /// Method used to delete a LABEL in the model
        /// </summary>
        void DeleteLabel(int labelId);

        /// <summary>
        /// Gets the users assigned to the ISSUE
        /// </summary>
        /// <param name="issueId"></param>
        /// <returns></returns>
        List<User> GetAssignedUsersToIssue(int issueId);

        /// <summary>
        /// Gets the labels assigned to the ISSUE
        /// </summary>
        /// <param name="issueId"></param>
        /// <returns></returns>
        List<Label> GetAssignedLabelsToIssue(int issueId);
        

        /// <summary>
        /// Gets milestones assigned to the ISSUE
        /// </summary>
        /// <param name="issueId"></param>
        /// <returns></returns>
        List<Milestone> GetAssignedMilestonesToIssue(int issueId);

        /// <summary>
        /// Method used to obtain the ISSUES from the model
        /// </summary>
        /// <returns>List<Issue></returns>
        List<Issue> GetIssues();

        /// <summary>
        /// Method used to obtain the LABELS from the model
        /// </summary>
        /// <returns>List<Label></returns>
        List<Label> GetLabels();

        /// <summary>
        /// Method used to obtain the MILESTONES from the model
        /// </summary>
        /// <returns>List<Milestone></returns>
        List<Milestone> GetMilestones();

        /// <summary>
        /// Method used to obtain the USERS from the model
        /// </summary>
        /// <returns>List<User></returns>
        List<User> GetUsers();


        /// <summary>
        ///  Method used to obtain the total number of ISSUES containing the LABEL identifier passed as a parameter.
        /// </summary>
        /// <param name="labelID"></param>
        /// <returns></returns>
        int GetIssuesWithLabelId(int labelID);


        /// <summary>
        ///  Method used update the label.
        /// </summary>
        /// <param name="labelID"></param>
        /// <returns></returns>
        void UpdateLabel(Label label);
    }
}
