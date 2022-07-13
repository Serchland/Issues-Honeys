using IssuesHoneys.Core.Types.Interfaces;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuesHoneys.Core.Base
{
    public class MainDataModelBase<T> : PartViewModelBase
    {
        IApplicationCommands _applicationCommands;
        public MainDataModelBase(IApplicationCommands applicationCommands) : base(applicationCommands)
        {
            _applicationCommands = applicationCommands;
        }

        #region "Properties"
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

            _applicationCommands.NavigateCommand.Execute(param);
        }
        #endregion
    }
}
