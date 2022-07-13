using IssuesHoneys.Business.Types;
using IssuesHoneys.Core.NameDefinition;
using IssuesHoneys.Core.Types.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace IssuesHoneys.Core.Base
{
    public class PartViewModelBase : ViewModelBase
    {
        private IApplicationCommands _applicationCommands;
        public PartViewModelBase(IApplicationCommands applicationCommands) : base(applicationCommands)
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

        //public T _selectedItem;
        //public virtual T SelectedItem
        //{
        //    get { return _selectedItem; }
        //    set { SetProperty(ref _selectedItem, value); }
        //}
        #endregion

       
    }
}