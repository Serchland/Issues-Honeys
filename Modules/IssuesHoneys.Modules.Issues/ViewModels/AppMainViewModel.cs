using IssuesHoneys.Core.NameDefinition;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class AppMainViewModel : BindableBase
    {
        public AppMainViewModel()
        {
            NewLabelViewVisibilitity = Visibility.Collapsed;
        }

        #region "Properties"
        
        private Visibility _newLabelViewVisibility;
        public Visibility NewLabelViewVisibilitity
        {
            get { return _newLabelViewVisibility; }
            set { SetProperty(ref _newLabelViewVisibility, value); }
        }
        #endregion

        #region "Commands"

        private DelegateCommand<string> _newLabelVisibilityCommand;
        public DelegateCommand<string> NewLabelVisibilityCommand =>
            _newLabelVisibilityCommand ?? (_newLabelVisibilityCommand = new DelegateCommand<string>(ExecuteNewLabelVisibilityCommand));

        void ExecuteNewLabelVisibilityCommand(string param)
        {
            if (string.IsNullOrEmpty(param))
                throw new ArgumentException("param cant be null");

            switch (param)
            {
                case Captions.Cancel:
                    NewLabelViewVisibilitity = Visibility.Collapsed;
                    break;

                case Captions.CreateLabel:
                    NewLabelViewVisibilitity = Visibility.Collapsed;
                    break;

                case Captions.NewLabel:
                    NewLabelViewVisibilitity = Visibility.Visible;
                    break;
            }
        }
        #endregion
    }
}
