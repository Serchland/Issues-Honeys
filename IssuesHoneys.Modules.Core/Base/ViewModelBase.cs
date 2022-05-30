using IssuesHoneys.Core.Types.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows;

namespace IssuesHoneys.Core.Base
{
    public class ViewModelBase : BindableBase
    {
        private IApplicationCommands _applicationCommands;
        public ViewModelBase(IApplicationCommands applicationCommands)
        {
            _applicationCommands = applicationCommands;
            _argumentExceptionMessage = Application.Current.Resources["AppMessageArgumentException"].ToString();
        }

        #region "Properties"
        private string _argumentExceptionMessage;
        public string  ArgumentExceptionMessage
        {
            get { return _argumentExceptionMessage; }
        }
        #endregion

        #region "Commands"
        private DelegateCommand<string> _navigateCommand;
        public DelegateCommand<string> NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(ExecuteNavigateCommand));

        void ExecuteNavigateCommand(string parameter)
        {
            if (string.IsNullOrEmpty(parameter))
                throw new ArgumentNullException(ArgumentExceptionMessage);

            _applicationCommands.NavigateCommand.Execute(parameter);
        }
        #endregion
    }
}