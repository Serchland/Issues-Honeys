using IssuesHoneys.Business.Types;
using IssuesHoneys.Core.Base;
using IssuesHoneys.Core.Types.Interfaces;
using IssuesHoneys.Services.Interfaces;
using System.Collections.ObjectModel;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class MilestonesViewModel : ViewModelBase<Milestone>
    {
        IMainProperties _mainProperties;
        IIssueService _issuesService;
        public MilestonesViewModel(IMainProperties mainProperties, IIssueService issuesService, IApplicationCommands applicationCommands) : base(applicationCommands)
        {
            _mainProperties = mainProperties;
            _issuesService = issuesService;
            Initialize();
        }

        private void Initialize()
        {
            if (_mainProperties.Milestones == null)
                Milestones = new ObservableCollection<Milestone>(_issuesService.GetMilestones());

            _totalMilestones = Milestones.Count.ToString();
        }

        #region "Properties"
        private ObservableCollection<Milestone> _milestones;
        public ObservableCollection<Milestone> Milestones
        {
            get
            {
                return _milestones;
            }
            set
            {
                _mainProperties.Milestones = value;
                SetProperty(ref _milestones, value);
            }
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
