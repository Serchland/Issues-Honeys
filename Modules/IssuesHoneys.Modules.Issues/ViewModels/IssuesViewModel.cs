using IssuesHoneys.Business.Types;
using IssuesHoneys.Core.Base;
using IssuesHoneys.Core.Types.Interfaces;
using IssuesHoneys.Services.Interfaces;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Label = IssuesHoneys.Business.Types.Label;

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
            _labels = new ObservableCollection<Label>(_issuesService.GetLabels());
            _issues = new ObservableCollection<Issue>(_issuesService.GetIssues());
            _milestones = new ObservableCollection<Milestone>(_issuesService.GetMilestones());
            _users = new ObservableCollection<User>(_issuesService.GetUsers());
            _issuesView = CollectionViewSource.GetDefaultView(_issues);
            _totalLabels = _labels.Count.ToString();
            _totalMilestones = _milestones.Count.ToString();
         
            _issuesView.Filter = IssuesFilter;
            _issuesView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));

            _milestones.Insert(0, new Milestone() { Title = "Issues with no millestones" });
            _labels.Insert(0, new Label() { Name = "Unlabeled", Color = Brushes.Transparent });
        }

        private bool IssuesFilter(object item)
        {
            Issue issue = item as Issue;
            if (!String.IsNullOrEmpty(FilterText))
            {
                return issue.Title.ToLower().Contains(FilterText.ToLower());
            }
            else
            {
                return true;
            }
        }

        private DelegateCommand<SelectionChangedEventArgs> _testCommand;
        public DelegateCommand<SelectionChangedEventArgs> TestCommand =>
            _testCommand ?? (_testCommand = new DelegateCommand<SelectionChangedEventArgs>(ExecuteTestCommand));

        void ExecuteTestCommand(SelectionChangedEventArgs param)
        {
            object[] list = (object[])param.AddedItems;
            MessageBox.Show("Navigate to Milestone " + (list[0] as Milestone).Title);
        }

        private DelegateCommand<object> _filterIssuesCommand;
        public DelegateCommand<object> FilterIssuesCommand =>
            _filterIssuesCommand ?? (_filterIssuesCommand = new DelegateCommand<object>(ExecuteFilterIssuesCommand));

        void ExecuteFilterIssuesCommand(object param)
        {
            //FilterText = param;
            //CollectionViewSource.GetDefaultView(Issues).Refresh();

            //FilterText = string.Empty;
        }

        #region "Properties"
        private string _filterText;
        public string FilterText
        {
            get
            {
                return _filterText;
            }
            set
            {
                SetProperty(ref _filterText, value);
            }
        }

        private ICollectionView _issuesView;
        public ICollectionView IssuesView
        {
            get
            {
                return _issuesView;
            }
            set
            {
                SetProperty(ref _issuesView, value);
            }
        }

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
