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
            _newLabelViewVisibility = Visibility.Collapsed;
            _brush = Brushes.Gray;

            Issues = new ObservableCollection<Issue>(_isuesService.GetIssues());
        }

        #region "Properties"

        private ObservableCollection<Issue> _issues;
        public ObservableCollection<Issue> Issues
        {
            get { return _issues; }
            set { SetProperty(ref _issues, value); }
        }

        private string _brushString;
        public string BrushString
        {
            get { return _brushString; }
            set { SetProperty(ref _brushString, value); }
        }

        private Brush _brush;
        public Brush Brush
        {
            get { return _brush; }
            set {
                    BrushString = _brush.ToString();
                    SetProperty(ref _brush, value); 
                }
        }

        private Visibility _newLabelViewVisibility;
        public Visibility NewLabelViewVisibilitity
        {
            get { return _newLabelViewVisibility; }
            set { SetProperty(ref _newLabelViewVisibility, value); }
        }
        #endregion

        #region "Commands"
        private DelegateCommand _randomColor;
        public DelegateCommand RandomColor =>
            _randomColor ?? (_randomColor = new DelegateCommand(ExecuteRandomColor));

        void ExecuteRandomColor()
        {
            Brush result = Brushes.Transparent;
            Random rnd = new Random();
            Type brushesType = typeof(Brushes);
            PropertyInfo[] properties = brushesType.GetProperties();
            int random = rnd.Next(properties.Length);
            Brush = (Brush)properties[random].GetValue(null, null);
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

        private DelegateCommand<string> _newLabelVisibilityCommand;
        public DelegateCommand<string> NewLabelVisibilityCommand =>
            _newLabelVisibilityCommand ?? (_newLabelVisibilityCommand = new DelegateCommand<string>(ExecuteNewLabelVisibilityCommand));

        void ExecuteNewLabelVisibilityCommand(string parameter)
        {
            if (string.IsNullOrEmpty(parameter))
                throw new ArgumentException("parameter cant be null");

            switch (parameter)
            {
                case CommandParameters.Cancel:
                    NewLabelViewVisibilitity = Visibility.Collapsed;
                    break;

                case CommandParameters.CreateLabel:
                    NewLabelViewVisibilitity = Visibility.Collapsed;
                    break;

                case CommandParameters.NewIssue:
                    NewLabelViewVisibilitity = Visibility.Visible;
                    break;
            }
        }
        #endregion
    }
}
