using IssuesHoneys.Business.Types;
using IssuesHoneys.Core.Base;
using IssuesHoneys.Core.Types.Interfaces;
using IssuesHoneys.Services.Interfaces;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class IssuesViewModel : ViewModelBase
    {
        private IIssueService _issuesService;
        public IssuesViewModel(IApplicationCommands applicationsCommands, IIssueService issueService) : base(applicationsCommands)
        {
            _issuesService = issueService;
            Initialize();
        }

        private void Initialize()
        {
            _totalLabels = _issuesService.GetLabels().Count.ToString();
            _totalMilestones = _issuesService.GetMillestones().Count.ToString();
            _users = new ObservableCollection<User>(_issuesService.GetUsers());
            _issues = new ObservableCollection<Issue>(_issuesService.GetIssues());
        }

        private DelegateCommand<SelectionChangedEventArgs> _testCommand;
        public DelegateCommand<SelectionChangedEventArgs> TestCommand =>
            _testCommand ?? (_testCommand = new DelegateCommand<SelectionChangedEventArgs>(ExecuteTestCommand));

        void ExecuteTestCommand(SelectionChangedEventArgs param)
        {
            object[] list = (object[])param.AddedItems;
            MessageBox.Show("Navigate to Milestone " + (list[0] as Milestone).Title);
        }
        #region "Properties"
        private string _totalLabels;
        public string TotalLabels
        {
            get { return _totalLabels; }
            set { SetProperty(ref _totalLabels, value); }
        }

        private string _totalMilestones;
        public string TotalMilestones
        {
            get { return _totalMilestones; }
            set { SetProperty(ref _totalMilestones, value); }
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
            get { return _issues; }
            set { SetProperty(ref _issues, value); }
        }

        private Issue _selectedItem;
        public Issue SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }
        #endregion
    }
}
