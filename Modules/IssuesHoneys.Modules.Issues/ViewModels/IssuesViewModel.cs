using IssuesHoneys.Business.Types;
using IssuesHoneys.Core.Base;
using IssuesHoneys.Core.Types.Interfaces;
using IssuesHoneys.Services.Interfaces;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Label = IssuesHoneys.Business.Types.Label;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class IssuesViewModel : ViewModelBase<Issue>
    {
        private IssuesFilterEnum? issuesFilterEnum;
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
            _issuesView.SortDescriptions.Add(new SortDescription("CrtnDate", ListSortDirection.Descending));

            _milestones.Insert(0, new Milestone() { Title = Application.Current.Resources["LabelNoMilestone"].ToString() });
            _labels.Insert(0, new Label() { Name = Application.Current.Resources["LabelUnlabeled"].ToString(), Color = Brushes.Transparent });
            
            _sortItems = GetSortItems();
            IsFiltered = false;
        }

        private bool IssuesFilter(object item)
        {
            bool result = false;
            Issue issue = item as Issue;

            switch (issuesFilterEnum)
            {
                case IssuesFilterEnum.Assignee:
                    User userFinder = null;
                    userFinder = issue.Assignees.Where(i => i.Name.ToLower() == (FilterText.ToLower())).FirstOrDefault();

                    result = userFinder != null;
                    break;

                case IssuesFilterEnum.Authors:
                    result = _issuesService.GetUserById(issue.CrtnUser).Name.ToLower() == FilterText.ToLower();
                    break;

                case IssuesFilterEnum.Labels:
                    Label labelFinder = null;
                    labelFinder = issue.Labels.Where(i => i.Name.ToLower() == (FilterText.ToLower())).FirstOrDefault();

                    result = labelFinder != null;

                    break;

                case IssuesFilterEnum.Millestones:
                    Milestone milestoneFinder = null;
                    milestoneFinder = issue.Milestones.Where(i => i.Title.ToLower() == (FilterText.ToLower())).FirstOrDefault();

                    result = milestoneFinder != null;

                    break;

                case IssuesFilterEnum.Projects:
                    //SERCH00: Under construction
                    result = true;
                    break;

                default:
                    return true;

            }
            
            return result;
        }

        private ObservableCollection<IssuesSortDto> GetSortItems()
        {
            ObservableCollection<IssuesSortDto> result = new ObservableCollection<IssuesSortDto>()
                {
                    new IssuesSortDto(){StringValue =  Application.Current.Resources["SortNewest"].ToString(), EnumValue = IssuesSortEnum.Newest},
                    new IssuesSortDto(){StringValue =  Application.Current.Resources["SortOldest"].ToString(), EnumValue = IssuesSortEnum.Oldest}
                };

            return result;
        }

        #region "Commands"

        private DelegateCommand<IssuesSortEnum?> _selectedSortItemCommand;
        public DelegateCommand<IssuesSortEnum?> SelectedSortItemCommand =>
            _selectedSortItemCommand ?? (_selectedSortItemCommand = new DelegateCommand<IssuesSortEnum?>(ExecuteSelectedSortItemCommand));

        void ExecuteSelectedSortItemCommand(IssuesSortEnum? param)
        {
            _issuesView.SortDescriptions.Clear();
            switch (param)
            {
                case IssuesSortEnum.Newest:
                    _issuesView.SortDescriptions.Add(new SortDescription("CrtnDate", ListSortDirection.Descending));
                    break;

                case IssuesSortEnum.Oldest:
                    _issuesView.SortDescriptions.Add(new SortDescription("CrtnDate", ListSortDirection.Ascending));
                    break;
            }
        }

        private DelegateCommand _isFilteredCommand;
        public DelegateCommand IsFilteredCommand =>
            _isFilteredCommand ?? (_isFilteredCommand = new DelegateCommand(ExecuteIsFilteredCommand));

        void ExecuteIsFilteredCommand()
        {
            IsFiltered = false;
            issuesFilterEnum = null;
            FilterText = String.Empty;

            CollectionViewSource.GetDefaultView(Issues).Refresh();
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
            if (param == null)
                throw new ArgumentNullException(ArgumentExceptionMessage);

            var parameters = (object[])param;
            issuesFilterEnum = (IssuesFilterEnum)Convert.ToInt32(parameters[0]);
            FilterText = parameters[1].ToString();

            CollectionViewSource.GetDefaultView(Issues).Refresh();

            FilterText = string.Empty;
            issuesFilterEnum = null;
            IsFiltered = true;
        }
        #endregion

        #region "Properties"
        private ObservableCollection<IssuesSortDto> _sortItems;
        public ObservableCollection<IssuesSortDto> SortItems
        {
            get
            {
                return _sortItems;
            }
        }

        private bool _isFiltered;
        public bool IsFiltered
        {
            get
            {
                return _isFiltered;
            }
            set
            {
                SetProperty(ref _isFiltered, value);
            }
        }

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

        //private Issue _selectedItem;
        //public Issue SelectedItem
        //{
        //    get { return _selectedItem; }
        //    set { SetProperty(ref _selectedItem, value); }
        //}
        #endregion
    }
}
