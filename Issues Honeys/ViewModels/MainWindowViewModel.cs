using IssuesHoneys.Core.NameDefinition;
using IssuesHoneys.Core.Types.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Windows;

namespace Issues_Honeys.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        IRegionManager _regionManager;

        public MainWindowViewModel(IApplicationCommands applicationCommand, IRegionManager regionManager)
        {
            _regionManager = regionManager;
            applicationCommand.NavigateCommand.RegisterCommand(NavigateCommand);
        }

        private DelegateCommand<string> _navigateCommand;
        public DelegateCommand<string> NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(ExecuteNavigateCommand));

        void ExecuteNavigateCommand(string parameter)
        {
            //SERCH00: only for test pourpoposes: string res = Application.Current.FindResource("WaterMarkSearchLabelsCaption").ToString();
            if (string.IsNullOrEmpty(parameter))
                throw new ArgumentNullException("Parameter cant be null");

            switch (parameter)
            {
                case CommandParameters.Issues:
                    _regionManager.RequestNavigate(RegionNames.FooterContentRegion, RegisterForNavigation.IssueFooter);
                    _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegisterForNavigation.IssuesMain);

                    break;

                case CommandParameters.Projects:
                    _regionManager.RequestNavigate(RegionNames.FooterContentRegion, RegisterForNavigation.ProjectFooter);
                    _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegisterForNavigation.ProjectMain);

                    break;

                case CommandParameters.Labels:
                    _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegisterForNavigation.LabelsMain);

                    break;

                case CommandParameters.Milestones:
                    _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegisterForNavigation.MilestonesMain);

                    break;

                case CommandParameters.NewIssue:
                    _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegisterForNavigation.NewIssue);

                    break;

                case CommandParameters.NewMilestone:
                    _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegisterForNavigation.NewMilestone);

                    break;

                    
                default:
                    _regionManager.RequestNavigate(RegionNames.FooterContentRegion, RegisterForNavigation.IssueFooter);
                    _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegisterForNavigation.IssuesMain);

                    break;
            }
        }
    }
}
