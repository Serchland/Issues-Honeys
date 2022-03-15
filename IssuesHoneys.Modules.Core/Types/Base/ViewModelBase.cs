using IssuesHoneys.Business;
using IssuesHoneys.Core.NameDefinition;
using IssuesHoneys.Core.Types.Interfaces;
using IssuesHoneys.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace IssuesHoneys.Core.Types.Base
{
    public class ViewModelBase : BindableBase
    {
        private static bool _isInitialized;
        private IApplicationCommands _applicationCommands;
        private IIssueService _issueService;


        public ViewModelBase(IApplicationCommands applicationCommands, IIssueService issueService)
        {
            _applicationCommands = applicationCommands;
            _issueService = issueService;

            Init();
        }

        private void Init()
        {
            if (!_isInitialized)
            {
                _labels = new ObservableCollection<Label>(_issueService.GetLabels());
                _issues = new ObservableCollection<Issue>(_issueService.GetIssues());
                _millestones = new ObservableCollection<Millestone>(_issueService.GetMillestones());

                _isInitialized = true;
            }   
        }

        #region "Properties"

        private ObservableCollection<Label> _labels;
        public ObservableCollection<Label> Labels
        {
            get { return _labels; }
            set { SetProperty(ref _labels, value); }
        }

        private ObservableCollection<Issue> _issues;
        public ObservableCollection<Issue> Issues
        {
            get { return _issues; }
            set { SetProperty(ref _issues, value); }
        }

        private ObservableCollection<Millestone> _millestones;
        public ObservableCollection<Millestone> Millestones
        {
            get { return _millestones; }
            set { SetProperty(ref _millestones, value); }
        }

        private string _totalLabels;
        public string TotalLabels
        {
            get { return _labels.Count.ToString(); }
            set { SetProperty(ref _totalLabels, value); }
        }

        private string _totalMillestones;
        public string TotalMillestones
        {
            get { return _millestones.Count.ToString(); }
            set { SetProperty(ref _totalMillestones, value); }
        }

        private string _totalIssues;
        public string TotalIssues
        {
            get { return _issues.Count.ToString(); }
            set { SetProperty(ref _totalIssues, value); }
        }
        #endregion

        #region "Commands"
        private DelegateCommand<string> _navigateCommand;
        public DelegateCommand<string> NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(ExecuteNavigateCommand));

        void ExecuteNavigateCommand(string param)
        {
            if (string.IsNullOrEmpty(param))
                param = CommandParameters.Defautl;

            _applicationCommands.NavigateCommand.Execute(param);
        }
        #endregion

    }
}
