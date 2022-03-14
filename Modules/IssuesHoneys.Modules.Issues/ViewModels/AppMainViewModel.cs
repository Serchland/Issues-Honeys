using IssuesHoneys.Business;
using IssuesHoneys.Core.NameDefinition;
using IssuesHoneys.Core.Types;
using IssuesHoneys.Core.Types.Interfaces;
using IssuesHoneys.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class AppMainViewModel : BindableBase
    {
        private IIssueService _isuesService;
        private IApplicationCommands _applicationCommands;
        public AppMainViewModel(IIssueService issueService, IApplicationCommands applicationCommands)
        {
            _applicationCommands = applicationCommands;
            _isuesService = issueService;
            Initialize();
        }

        private void Initialize()
        {
            _newLabelViewVisibility = Visibility.Collapsed;
            _brush = Brushes.Gray;
            Issues = _isuesService.GetIssues();
        }

        #region "Properties"
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

        private List<Issue> _issues;
        public List<Issue> Issues
        {
            get { return _issues; }
            set { SetProperty(ref _issues, value); }
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

        private DelegateCommand<string> _newLabelVisibilityCommand;
        public DelegateCommand<string> NewLabelVisibilityCommand =>
            _newLabelVisibilityCommand ?? (_newLabelVisibilityCommand = new DelegateCommand<string>(ExecuteNewLabelVisibilityCommand));

        void ExecuteNewLabelVisibilityCommand(string param)
        {
            if (string.IsNullOrEmpty(param))
                throw new ArgumentException("param cant be null");

            _applicationCommands.NavigationNavigateCommand.Execute(param);

            //switch (param)
            //{
            //    case Captions.Cancel:
            //        NewLabelViewVisibilitity = Visibility.Collapsed;
            //        break;

            //    case Captions.CreateLabel:
            //        NewLabelViewVisibilitity = Visibility.Collapsed;
            //        break;

            //    case Captions.NewIssue:
            //        NewLabelViewVisibilitity = Visibility.Visible;
            //        break;
            //}
        }
        #endregion
    }
}
