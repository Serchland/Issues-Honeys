using IssuesHoneys.Business.Types;
using IssuesHoneys.Core.NameDefinition;
using IssuesHoneys.Core.Types.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuesHoneys.Core.Base
{
    public class ViewModelBase<T> : BindableBase
    {
        private IApplicationCommands _applicationCommands;
        public ViewModelBase(IApplicationCommands applicationCommands)
        {
            _applicationCommands = applicationCommands;
           
        }

        #region "Properties"       
        private string _argumentExceptionMessage;
        public string ArgumentExceptionMessage
        {
            get { return _argumentExceptionMessage; }
        }

        public T _selectedItem;
        public virtual T SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }
        #endregion
        #region "Commands"

        private DelegateCommand<string> _navigateCommand;
        public DelegateCommand<string> NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(ExecuteNavigateCommand));

        void ExecuteNavigateCommand(string param)
        {
            if (string.IsNullOrEmpty(param))
                throw new ArgumentNullException(ArgumentExceptionMessage);

            if (param == CommandParameters.Details)
            {
                var current = _selectedItem as Issue;
                param += string.Format("{0}{1}", ";", current.Id.ToString());
            }


            _applicationCommands.NavigateCommand.Execute(param);
        }
        #endregion
    }
}
