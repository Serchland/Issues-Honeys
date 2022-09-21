using IssuesHoneys.Business.Types;
using IssuesHoneys.Core.Base;
using IssuesHoneys.Core.Types.Interfaces;
using IssuesHoneys.Services.Interfaces;
using System.Collections.ObjectModel;

namespace IssuesHoneys.Modules.Issues.ViewModels
{

    public class NewIssueViewModel : ViewModelBase<Issue>
    {
        IMainProperties _mainProperties;
        IIssueService _issuesService;
        public NewIssueViewModel(IMainProperties mainProperties, IIssueService issuesService, IApplicationCommands applicationCommands) : base(applicationCommands)
        {
            _mainProperties = mainProperties;
            _issuesService = issuesService;

            _foregroundRed = 0x00;
            _foregroundGreen = 0x00;
            _foregroundBlue = 0x00;

            _backgroundRed = 0xFF;
            _backgroundGreen = 0xFF;
            _backgroundBlue = 0xFF;

            Initialize();
        }

        private void Initialize()
        {
            if (_mainProperties.Labels == null)
                Labels = new ObservableCollection<Label>(_issuesService.GetLabels());

            if (_mainProperties.Issues == null)
                Issues = new ObservableCollection<Issue>(_issuesService.GetIssues());

            if (_mainProperties.Users == null)
                Users = new ObservableCollection<User>(_issuesService.GetUsers());

            IssueTextView = @"**Describe the bug**
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
        }

        #region "Properties"
        private ObservableCollection<Label> _labels;
        public ObservableCollection<Label> Labels
        {
            get
            {
                return _mainProperties.Labels;
            }
            set
            {
                _mainProperties.Labels = value;
                SetProperty(ref _labels, value);
            }
        }

        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get
            {
                return _mainProperties.Users;
            }
            set
            {
                _mainProperties.Users = value;
                SetProperty(ref _users, value);
            }
        }

        private ObservableCollection<Issue> _issues;
        public ObservableCollection<Issue> Issues
        {
            get
            {
                return _mainProperties.Issues;
            }
            set
            {
                _mainProperties.Issues = value;
                SetProperty(ref _issues, value);
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
