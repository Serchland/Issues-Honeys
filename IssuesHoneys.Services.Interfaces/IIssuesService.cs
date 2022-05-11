﻿using IssuesHoneys.BusinessTypes;
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
        List<Milestone> GetMillestones();

        /// <summary>
        /// Method used to obtain the USERS from the model
        /// </summary>
        /// <returns>List<User></returns>
        List<User> GetUsers();
    }
}
