using IssuesHoneys.Core.Types.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class NewIssueViewModel : BindableBase
    {
        IApplicationCommands _applicationCommand;
        public NewIssueViewModel(IApplicationCommands applicationCommands)
        {
            _applicationCommand = applicationCommands;
        }

        #region "Properties"
        private string _issueTextView;
        public string IssueTextView
        {
            get { return _issueTextView; }
            set { SetProperty(ref _issueTextView, value); }
        }

        private Task TextXamlChangeEvent;
        public string _textXaml;
        public string TextXaml
        {
            get { return _textXaml; }
            set
            {
                if (_textXaml == value) return;
                _textXaml = value;
                if (TextXamlChangeEvent == null || TextXamlChangeEvent.Status >= TaskStatus.RanToCompletion)
                {
                    TextXamlChangeEvent = Task.Run(() =>
                    {
                        Task.Delay(100);
                    retry:
                        var oldVal = _textXaml;

                        Thread.MemoryBarrier();
                        //FirePropertyChanged(nameof(TextXaml));
                        SetProperty(ref _issueTextView, value); 

                        Thread.MemoryBarrier();
                        if (oldVal != _textXaml) goto retry;
                    });
                }
            }
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

            _applicationCommand.NavigateCommand.Execute(parameter);
        }
        #endregion
    }
}
