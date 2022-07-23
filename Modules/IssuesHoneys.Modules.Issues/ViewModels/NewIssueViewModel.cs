using IssuesHoneys.Business.Types;
using IssuesHoneys.Core.Base;
using IssuesHoneys.Core.Types.Interfaces;
using IssuesHoneys.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class NewIssueViewModel : ViewModelBase<Issue>
    {
        IIssueService _issuesService;
        public NewIssueViewModel(IApplicationCommands applicationCommands, IIssueService issuesService) : base(applicationCommands)
        {
            _issuesService = issuesService;
            _labels = new ObservableCollection<Label>(_issuesService.GetLabels());
            _milestones = new ObservableCollection<Milestone>(_issuesService.GetMilestones());
            _users = new ObservableCollection<User>(_issuesService.GetUsers());

            _issueTextView = @"**Describe the bug**
A clear and concise description of what the bug is.

**To Reproduce**
Steps to reproduce the behavior:
1. Go to '...'
2. Click on '....'
3. Scroll down to '....'
4. See error

**Expected behavior**
A clear and concise description of what you expected to happen.

**Screenshots**
If applicable, add screenshots to help explain your problem.

**Desktop (please complete the following information):**
 - OS: [e.g. iOS]
 - Browser [e.g. chrome, safari]
 - Version [e.g. 22]

**Additional context**
Add any other context about the problem here.
";

            _foregroundRed = 0x00;
            _foregroundGreen = 0x00;
            _foregroundBlue = 0x00;

            _backgroundRed = 0xFF;
            _backgroundGreen = 0xFF;
            _backgroundBlue = 0xFF;
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

        private string _issueTextView;
        public string IssueTextView
        {
            get { return _issueTextView; }
            set { SetProperty(ref _issueTextView, value); }
        }

        //private Task TextXamlChangeEvent;
        //public string _textXaml;
        //public string TextXaml
        //{
        //    get { return _textXaml; }
        //    set
        //    {
        //        if (_textXaml == value) return;
        //        _textXaml = value;
        //        if (TextXamlChangeEvent == null || TextXamlChangeEvent.Status >= TaskStatus.RanToCompletion)
        //        {
        //            TextXamlChangeEvent = Task.Run(() =>
        //            {
        //                Task.Delay(100);
        //            retry:
        //                var oldVal = _textXaml;

        //                Thread.MemoryBarrier();
        //                SetProperty(ref _issueTextView, value); 

        //                Thread.MemoryBarrier();
        //                if (oldVal != _textXaml) goto retry;
        //            });
        //        }
        //    }
        //}

        private readonly byte _foregroundRed;
        public byte ForegroundRed
        {
            get { return _foregroundRed; }
        }

        private readonly byte _foregroundGreen;
        public byte ForegroundGreen
        {
            get { return _foregroundGreen; }
        }

        private readonly byte _foregroundBlue;
        public byte ForegroundBlue
        {
            get { return _foregroundBlue; }
        }

        private readonly byte _backgroundRed;
        public byte BackgroundRed
        {
            get { return _backgroundRed; }
        }

        private readonly byte _backgroundGreen;
        public byte BackgroundGreen
        {
            get { return _backgroundGreen; }
        }

        private readonly byte _backgroundBlue;
        public byte BackgroundBlue
        {
            get { return _backgroundBlue; }
        }
        #endregion
    }
}
