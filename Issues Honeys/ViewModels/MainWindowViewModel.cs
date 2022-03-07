﻿using IssuesHoneys.Core.Interfaces;
using IssuesHoneys.Core.NameDefinition;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Issues_Honeys.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        IRegionManager _regionManager;

        public MainWindowViewModel(IApplicationCommands applicationCommand, IRegionManager regionManager)
        {
            _regionManager = regionManager;
            applicationCommand.NavigationNavigateCommand.RegisterCommand(NavigateCommand);
        }

        private DelegateCommand<string> _navigateCommand;
        public DelegateCommand<string> NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(ExecuteNavigateCommand));

        void ExecuteNavigateCommand(string parameter)
        {
            if (string.IsNullOrEmpty(parameter))
                _regionManager.RequestNavigate(RegionNames.FooterContentRegion, RegisterForNavigation.IssueFooter);
                _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegisterForNavigation.IssueMain);
        }
    }
}