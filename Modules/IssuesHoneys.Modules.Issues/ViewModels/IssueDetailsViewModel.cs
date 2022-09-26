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
        IMainProperties _mainProperties;
        private IIssueService _issuesService;
        public IssueDetailsViewModel(IMainProperties mainProperties, IIssueService issueService, IRegionManager regionManager, IApplicationCommands applicationCommands) : base(regionManager, applicationCommands)
        {
            _mainProperties = mainProperties;
            _issuesService = issueService;

          

            Initialize();
        }

        private void Initialize()
        {
            if (_mainProperties.Labels == null)
                Labels = new ObservableCollection<Label>(_issuesService.GetLabels(LabelType.Issue));

            if (_mainProperties.Issues == null)
                Issues = new ObservableCollection<Issue>(_issuesService.GetIssues());

            if (_mainProperties.Milestones == null)
                Milestones = new ObservableCollection<Milestone>(_issuesService.GetMilestones());

            if (_mainProperties.Users == null)
                Users = new ObservableCollection<User>(_issuesService.GetUsers());
        }

        #region "Properties"
        private ObservableCollection<Milestone> _milestones;
        public ObservableCollection<Milestone> Milestones
        {
            get
            {
                return _mainProperties.Milestones;
            }
            set
            {
                _mainProperties.Milestones = value;
                SetProperty(ref _milestones, value);
            }
        }

        private ObservableCollection<Label> _labels;
        public ObservableCollection<Label> Labels
        {
            get
            {
                return _mainProperties.Labels;
            }
            set
            {
                _mainProperties.Labels = value;
                SetProperty(ref _labels, value);
            }
        }

        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get
            {
                return _mainProperties.Users;
            }
            set
            {
                _mainProperties.Users = value;
                SetProperty(ref _users, value);
            }
        }

        private ObservableCollection<Issue> _issues;
        public ObservableCollection<Issue> Issues
        {
            get
            {
                return _mainProperties.Issues;
            }
            set
            {
                _mainProperties.Issues = value;
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
