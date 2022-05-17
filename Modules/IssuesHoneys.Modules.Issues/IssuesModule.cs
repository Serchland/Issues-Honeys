using IssuesHoneys.Core.NameDefinition;
using IssuesHoneys.Modules.Issues.ViewModels;
using IssuesHoneys.Modules.Issues.Views;
using IssuesHoneys.Services;
using IssuesHoneys.Services.Dummies;
using IssuesHoneys.Services.Interfaces;
using IssuesHoneys.Services.SQL;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Configuration;

namespace Issues
{
    public class IssuesModule : IModule
    {
        private IRegionManager _regionManager;
        public IssuesModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.ButtonContentRegion, typeof(AppButton));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ViewModelLocationProvider.Register<AppButton, AppButtonViewModel>();
            ViewModelLocationProvider.Register<AppFooter, AppFooterViewModel>();
            ViewModelLocationProvider.Register<IssuesView, IssuesViewModel>();
            ViewModelLocationProvider.Register<IssueCommentsView, IssueCommentsViewModel>();
            ViewModelLocationProvider.Register<LabelsView, LabelsViewModel>();
            ViewModelLocationProvider.Register<MilestonesView, MilestonesViewModel>();
            ViewModelLocationProvider.Register<NewIssue, NewIssueViewModel>();
            ViewModelLocationProvider.Register<NewMilestone, NewMilestoneViewModel>();

            containerRegistry.RegisterForNavigation<AppFooter, AppFooterViewModel>(RegisterForNavigation.IssueFooter);
            containerRegistry.RegisterForNavigation<IssueCommentsView, IssueCommentsViewModel>(RegisterForNavigation.IssueComments);
            containerRegistry.RegisterForNavigation<IssuesView, IssuesViewModel>(RegisterForNavigation.IssuesMain);
            containerRegistry.RegisterForNavigation<LabelsView, LabelsViewModel>(RegisterForNavigation.LabelsMain);
            containerRegistry.RegisterForNavigation<MilestonesView, MilestonesViewModel>(RegisterForNavigation.MilestonesMain);
            containerRegistry.RegisterForNavigation<NewIssue, NewIssueViewModel>(RegisterForNavigation.NewIssue);
            containerRegistry.RegisterForNavigation<NewMilestone, NewMilestoneViewModel>(RegisterForNavigation.NewMilestone);

            var appSettings = ConfigurationManager.AppSettings;
            string result = appSettings[AppSettings.UseDummyService] ?? throw new ConfigurationErrorsException("Error reading app settings");
            
            if (Convert.ToBoolean(result))
                containerRegistry.RegisterSingleton<IIssueService, IssueDummyService>();
            else
                containerRegistry.RegisterSingleton<IIssueService, IssueSQLService>();
        }
    }
}