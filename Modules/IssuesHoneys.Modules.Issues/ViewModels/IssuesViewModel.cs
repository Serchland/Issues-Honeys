using IssuesHoneys.Business;
using IssuesHoneys.Core.NameDefinition;
using IssuesHoneys.Core.Types.Interfaces;
using IssuesHoneys.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class IssuesViewModel : BindableBase
    {
        private IApplicationCommands _applicationCommands;
        private IIssueService _isuesService;
        public IssuesViewModel(IApplicationCommands applicationsCommands, IIssueService issueService) 
        {
            _applicationCommands = applicationsCommands;
            _isuesService = issueService;
            Initialize();
        }

        private void Initialize()
        {
            _totalLabels = _isuesService.GetLabels().Count.ToString();
            _totalMilestones = _isuesService.GetMillestones().Count.ToString();
            Issues = new ObservableCollection<Issue>(_isuesService.GetIssues());
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

        #region "Commands"
        private DelegateCommand<SelectionChangedEventArgs> _testCommand;
        public DelegateCommand<SelectionChangedEventArgs> TestCommand =>
            _testCommand ?? (_testCommand = new DelegateCommand<SelectionChangedEventArgs>(ExecuteTestCommand));

        void ExecuteTestCommand(SelectionChangedEventArgs args)
        {
            //SERCH00: I don't know how I'm going to navigate yet
            if (args == null)
                throw new ArgumentNullException("args cant be null");

            _applicationCommands.NavigateCommand.Execute(((Milestone)args.AddedItems[0]).Name);
        }

        private DelegateCommand<string> _navigateCommand;
        public DelegateCommand<string> NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(ExecuteNavigateCommand));

        void ExecuteNavigateCommand(string parameter)
        {
            if (string.IsNullOrEmpty(parameter))
                throw new ArgumentNullException("parameter cant be null");

            _applicationCommands.NavigateCommand.Execute(parameter);
        }

       
        #endregion
    }
}
