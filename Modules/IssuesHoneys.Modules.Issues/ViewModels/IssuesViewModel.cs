using IssuesHoneys.BusinessTypes;
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
        private IIssueService _isuesService;
        public IssuesViewModel(IApplicationCommands applicationsCommands, IIssueService issueService) : base(applicationsCommands)
        {
            _isuesService = issueService;
            Initialize();
        }

        private void Initialize()
        {
            _totalLabels = _isuesService.GetLabels().Count.ToString();
            _totalMilestones = _isuesService.GetMillestones().Count.ToString();
            Issues = new ObservableCollection<Issue>(_isuesService.GetIssues());
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
