using IssuesHoneys.BusinessTypes;
using IssuesHoneys.Core.Types.Interfaces;
using IssuesHoneys.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class MilestonesViewModel : BindableBase
    {
        IApplicationCommands _applicationCommands;
        IIssueService _issueService;
        public MilestonesViewModel(IApplicationCommands applicationCommands, IIssueService issueService)
        {
            _applicationCommands = applicationCommands;
            _issueService = issueService;
            Initialize();
        }

        private void Initialize()
        {
            _milestones = new ObservableCollection<Milestone>(_issueService.GetMillestones());
            _totalMilestones = _milestones.Count.ToString();
        }

        #region "Properties"
        private ObservableCollection<Milestone> _milestones;
        public ObservableCollection<Milestone> Milestones
        {
            get { return _milestones; }
            set { SetProperty(ref _milestones, value); }
        }

        private string _totalMilestones;
        public string TotalMilestones
        {
            get { return _totalMilestones; }
            set { SetProperty(ref _totalMilestones, value); }
        }
        #endregion

        #region "Commands"
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
