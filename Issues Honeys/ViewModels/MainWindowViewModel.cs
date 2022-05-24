using IssuesHoneys.Core.Base;
using IssuesHoneys.Core.NameDefinition;
using IssuesHoneys.Core.Types.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace Issues_Honeys.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        IRegionManager _regionManager;

        public MainWindowViewModel(IApplicationCommands applicationCommands, IRegionManager regionManager) : base(applicationCommands)
        {
            _regionManager = regionManager;
            applicationCommands.NavigateCommand.RegisterCommand(NavigateCommand);

            _buttonContentRegionHeight = 73;
        }

        private double _buttonContentRegionHeight;
        public double ButtonContentRegionHeight
        {
            get { return _buttonContentRegionHeight; }
            set { SetProperty(ref _buttonContentRegionHeight, value); }
        }

        private DelegateCommand<string> _navigateCommand;
        public DelegateCommand<string> NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(ExecuteNavigateCommand));

        void ExecuteNavigateCommand(string parameter)
        {
            //SERCH00: only for test pourpoposes:
            //string res = Application.Current.FindResource("WaterMarkSearchLabelsCaption").ToString();
            if (string.IsNullOrEmpty(parameter))
                throw new ArgumentNullException(ArgumentExceptionMessage);

            switch (parameter)
            {
                case CommandParameters.Comments:
                    _regionManager.RequestNavigate(RegionNames.FooterContentRegion, RegisterForNavigation.IssueFooter);
                    _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegisterForNavigation.IssueComments);

                    ButtonContentRegionHeight = 0;
                    break;

                case CommandParameters.Issues:
                    _regionManager.RequestNavigate(RegionNames.FooterContentRegion, RegisterForNavigation.IssueFooter);
                    _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegisterForNavigation.IssuesMain);

                    ButtonContentRegionHeight = 73;
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
    }
}
