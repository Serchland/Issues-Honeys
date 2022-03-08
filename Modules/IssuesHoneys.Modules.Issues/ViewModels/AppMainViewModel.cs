using IssuesHoneys.Business;
using IssuesHoneys.Core.NameDefinition;
using IssuesHoneys.Core.Types;
using IssuesHoneys.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class AppMainViewModel : BindableBase
    {
        private IIssueService _isuesService;
        public AppMainViewModel(IIssueService issueService)
        {
            _isuesService = issueService;
            Initialize();
        }

        private void Initialize()
        {
            NewLabelViewVisibilitity = Visibility.Collapsed;
            Issues = _isuesService.GetIssues();
        }

        #region "Properties"

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

        private DelegateCommand<string> _newLabelVisibilityCommand;
        public DelegateCommand<string> NewLabelVisibilityCommand =>
            _newLabelVisibilityCommand ?? (_newLabelVisibilityCommand = new DelegateCommand<string>(ExecuteNewLabelVisibilityCommand));

        void ExecuteNewLabelVisibilityCommand(string param)
        {
            if (string.IsNullOrEmpty(param))
                throw new ArgumentException("param cant be null");

            switch (param)
            {
                case Captions.Cancel:
                    NewLabelViewVisibilitity = Visibility.Collapsed;
                    break;

                case Captions.CreateLabel:
                    NewLabelViewVisibilitity = Visibility.Collapsed;
                    break;

                case Captions.NewLabel:
                    NewLabelViewVisibilitity = Visibility.Visible;
                    break;
            }
        }
        #endregion
    }
}
