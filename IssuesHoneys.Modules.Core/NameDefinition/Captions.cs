﻿namespace IssuesHoneys.Core.NameDefinition
{
    /// <summary>
    /// Avoid magic strings.
    /// </summary>
    public static class AppSettings
    {
        public const string UseDummyService = "UseDummyService";
    }

    public static class BookMark
    {
        public const string Id = "Id";
    }

    public static class CommandParameters
    {
        public const string Assignes = "Assignes";
        public const string Cancel = "Cancel";
        public const string Details = "Details";
        public const string Create = "Create";
        public const string CreateMilestone = "CreateMilestone";
        public const string Defautl = "Default";
        public const string Issues = "Issues";
        public const string Labels = "Labels";
        public const string Milestones = "Milestones";
        public const string NewIssue = "NewIssue";
        public const string NewLabel = "NewLabel";
        public const string NewMilestone = "NewMilestone";
        public const string Projects = "Projects";
        public const string Shell = "Shell";
        public const string Update = "Update";
    }

    public static class RegionNames
    {
        public const string ButtonContentRegion = "ButtonContentRegion";
        public const string FooterContentRegion = "FooterContentRegion";
        public const string MainContentRegion = "MainContentRegion";
    }

    public static class RegisterForNavigation
    {
        public const string IssueDetails = "IssueDetails";
        public const string IssueFooter = "IssueFooter";
        public const string IssuesMain = "IssuesMain";
        public const string LabelsMain = "LabelsMain";
        public const string MilestonesMain = "MilestonesMain";
        public const string NewIssue = "NewIssue";
        public const string NewMilestone = "NewMilestone";
        public const string ProjectFooter = "ProjectFooter";
        public const string ProjectMain = "ProjectMain";
    }

    public static class MessagesResources
    {
        public const string AppArgumentException = "AppMessageArgumentException";
        public const string LabelMessageArgumentException = "LabelTypeArgumentException";
    }

    public static class LabelsResources
    {
        public const string TextBlockOpenedTheIssueOn = "TextBlockOpenedTheIssueOn";
    }

}
