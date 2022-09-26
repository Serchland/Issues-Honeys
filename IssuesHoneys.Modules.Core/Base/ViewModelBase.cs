using IssuesHoneys.Business.Types;
using IssuesHoneys.Core.NameDefinition;
using IssuesHoneys.Core.Types.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Linq;
using System.Windows;

namespace IssuesHoneys.Core.Base
{
    public class ViewModelBase<T> : BindableBase
    {
        private IApplicationCommands _applicationCommands;
        private IRegionManager _regionManager;

        public ViewModelBase(IRegionManager regionManager, IApplicationCommands applicationCommands)
        {
            _applicationCommands = applicationCommands;
            ArgumentExceptionMessage = Application.Current.FindResource(MessagesResources.AppArgumentException).ToString();

            _regionManager = regionManager;
            applicationCommands.NavigateCommand.RegisterCommand(CompositeNavigateCommand);
        }

        #region "Properties"       
        public string ArgumentExceptionMessage { get; }

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


        private DelegateCommand<string> _compositeNavigateCommand;
        public DelegateCommand<string> CompositeNavigateCommand =>
            _compositeNavigateCommand ?? (_compositeNavigateCommand = new DelegateCommand<string>(ExecuteCompositeNavigateCommand));

        void ExecuteCompositeNavigateCommand(string param)
        {
            //SERCH00: only for test pourpoposes:
            //string res = Application.Current.FindResource("WaterMarkSearchLabelsCaption").ToString();
            if (string.IsNullOrEmpty(param))
                throw new ArgumentNullException(ArgumentExceptionMessage);

            switch (param.Split(';').FirstOrDefault())
            {
                case CommandParameters.Details:
                    var navParam = new NavigationParameters();
                    navParam.Add(BookMark.Id, param.Split(';').LastOrDefault());

                    _regionManager.RequestNavigate(RegionNames.FooterContentRegion, RegisterForNavigation.IssueFooter);
                    _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegisterForNavigation.IssueDetails, navParam);

                    break;

                case CommandParameters.Issues:
                    _regionManager.RequestNavigate(RegionNames.FooterContentRegion, RegisterForNavigation.IssueFooter);
                    _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegisterForNavigation.IssuesMain);

                    break;

                case CommandParameters.Labels:
                    _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegisterForNavigation.LabelsMain);

                    break;

                case CommandParameters.Milestones:
                case CommandParameters.CreateMilestone:
                    _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegisterForNavigation.MilestonesMain);

                    break;

                case CommandParameters.NewIssue:
                    _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegisterForNavigation.NewIssue);

                    break;

                case CommandParameters.NewMilestone:
                    _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegisterForNavigation.NewMilestone);

                    break;

                case CommandParameters.Projects:
                    _regionManager.RequestNavigate(RegionNames.FooterContentRegion, RegisterForNavigation.ProjectFooter);
                    _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegisterForNavigation.ProjectMain);

                    break;

                default:
                    _regionManager.RequestNavigate(RegionNames.FooterContentRegion, RegisterForNavigation.IssueFooter);
                    _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegisterForNavigation.IssuesMain);

                    break;
            }
        }
        #endregion
    }
}
