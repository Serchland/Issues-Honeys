using IssuesHoneys.Business.Types;
using IssuesHoneys.Core.Base;
using IssuesHoneys.Core.Types.Interfaces;
using IssuesHoneys.Services.Interfaces;
using System.Collections.ObjectModel;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class MilestonesViewModel : PartViewModelBase<Milestone>
    {
        IIssueService _issueService;
        public MilestonesViewModel(IApplicationCommands applicationCommands, IIssueService issueService) : base(applicationCommands)
        {
            _issueService = issueService;
            Initialize();
        }

        private void Initialize()
        {
            _milestones = new ObservableCollection<Milestone>(_issueService.GetMilestones());
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
    }
}
