using IssuesHoneys.Business.Types;
using IssuesHoneys.Core.Base;
using IssuesHoneys.Core.NameDefinition;
using IssuesHoneys.Core.Types.Interfaces;
using IssuesHoneys.Services.Interfaces;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class IssueDetailsViewModel : ViewModelBase<Issue>, INavigationAware
    {
        private IIssueService _issuesService;
        public IssueDetailsViewModel(IApplicationCommands applicationCommands, IIssueService issueService) : base(applicationCommands)
        {
            _issuesService = issueService;

            _labels = new ObservableCollection<Label>(_issuesService.GetLabels());
            _issues = new ObservableCollection<Issue>(_issuesService.GetIssues());
            _milestones = new ObservableCollection<Milestone>(_issuesService.GetMilestones());
            _users = new ObservableCollection<User>(_issuesService.GetUsers());
        }

        #region "Properties"
        private ObservableCollection<Milestone> _milestones;
        public ObservableCollection<Milestone> Milestones
        {
            get
            {
                return _milestones;
            }
            set
            {
                SetProperty(ref _milestones, value);
            }
        }

        private ObservableCollection<Label> _labels;
        public ObservableCollection<Label> Labels
        {
            get
            {
                return _labels;
            }
            set
            {
                SetProperty(ref _labels, value);
            }
        }

        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get
            {
                return _users;
            }
            set
            {
                SetProperty(ref _users, value);
            }
        }

        private ObservableCollection<Issue> _issues;
        public ObservableCollection<Issue> Issues
        {
            get
            {
                return _issues;
            }
            set
            {
                SetProperty(ref _issues, value);
            }
        }
        #endregion

        #region "INavigationAware implementation"
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            string detailId = (string)navigationContext.Parameters[BookMark.Id];

            SelectedItem = _issuesService.GetIssuesById(Convert.ToInt32(detailId));
        }
        #endregion
    }
}
