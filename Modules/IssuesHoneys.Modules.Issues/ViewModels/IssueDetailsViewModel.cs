using IssuesHoneys.Business.Types;
using IssuesHoneys.Core.Base;
using IssuesHoneys.Core.NameDefinition;
using IssuesHoneys.Core.Types.Interfaces;
using IssuesHoneys.Services.Interfaces;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class IssueDetailsViewModel : ViewModelBase<Issue>, INavigationAware
    {
        IMainProperties _mainProperties;
        private IIssueService _issuesService;

        public IssueDetailsViewModel(IMainProperties mainProperties, IIssueService issueService, IRegionManager regionManager, IApplicationCommands applicationCommands, IEventAggregator eventAggregator) : base(regionManager, applicationCommands, eventAggregator)
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

        #region "Commands"
        //SERCH00: This command has incorrect name: change
        private DelegateCommand<object> _filterIssuesCommand;
        public DelegateCommand<object> FilterIssuesCommand =>
            _filterIssuesCommand ?? (_filterIssuesCommand = new DelegateCommand<object>(ExecuteFilterIssuesCommand));

        void ExecuteFilterIssuesCommand(object param)
        {
            if (param == null)
                throw new ArgumentNullException(ArgumentExceptionMessage);

            var parameters = (object[])param;
            var issuesFilterEnum = (IssuesFilterEnum)Convert.ToInt32(parameters[0]);
            var itemId = (int)parameters[1];

            switch (issuesFilterEnum)
            {
                case IssuesFilterEnum.Assignee:
                    var user = (object[])param;
                    _issuesService.AddUserToIssue(SelectedItem.Id.GetValueOrDefault(), itemId);
                    break;

                case IssuesFilterEnum.Labels:
                    _issuesService.AddLabelToIssue(SelectedItem.Id.GetValueOrDefault(), itemId);
                    break;

                case IssuesFilterEnum.Millestones:
                    _issuesService.AddMilestoneToIssue (SelectedItem.Id.GetValueOrDefault(), itemId);
                    break;

                default:
                    break;
            
            }

            SelectedItem = _issuesService.GetIssueById(SelectedItem.Id.GetValueOrDefault());

        }

        private DelegateCommand<object> _unAssignCommand;
        public DelegateCommand<object> UnAssignCommand =>
            _unAssignCommand ?? (_unAssignCommand = new DelegateCommand<object>(ExecuteUnAssignCommand));

        void ExecuteUnAssignCommand(object param)
        {
            if (param == null)
                throw new ArgumentNullException(ArgumentExceptionMessage);

            var parameters = (object[])param;
            var issuesFilterEnum = (IssuesFilterEnum)Convert.ToInt32(parameters[0]);
            var itemId = (int)parameters[1];


            switch (issuesFilterEnum)
            {
                case IssuesFilterEnum.Assignee:
                    _issuesService.DeleteUserToIssue(SelectedItem.Id.GetValueOrDefault(), itemId);
                    break;

                case IssuesFilterEnum.Labels:
                    _issuesService.DeleteLabelToIssue(SelectedItem.Id.GetValueOrDefault(), itemId);
                    break;

                case IssuesFilterEnum.Millestones:
                    _issuesService.DeleteMilestoneToIssue(SelectedItem.Id.GetValueOrDefault(), itemId);
                    break;

                case IssuesFilterEnum.Projects:
                    //SERCH00: Under construction
                    break;

                default:
                    break;

            }

            SelectedItem = _issuesService.GetIssueById(SelectedItem.Id.GetValueOrDefault());
        }
        #endregion

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

        private string _crtnIssueStamp;
        public string CrtnIssueStamp
        {
            get
            {
                return _crtnIssueStamp;
            }

            set
            {
                SetProperty(ref _crtnIssueStamp, value);
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

            SelectedItem = _issuesService.GetIssueById(Convert.ToInt32(detailId));

            var _crtnUser = Users.Where(u => u.Id == SelectedItem.Id).SingleOrDefault();
            var _crtnOpenText = Application.Current.FindResource(LabelsResources.TextBlockOpenedTheIssueOn).ToString();
            CrtnIssueStamp = string.Format("{0} {1} {2}", _crtnUser.Name, _crtnOpenText, SelectedItem.CrtnDate);
            
        }
        #endregion
    }
}
