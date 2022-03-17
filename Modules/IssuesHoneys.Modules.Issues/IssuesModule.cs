using IssuesHoneys.Core.NameDefinition;
using IssuesHoneys.Modules.Issues.ViewModels;
using IssuesHoneys.Modules.Issues.Views;
using IssuesHoneys.Services;
using IssuesHoneys.Services.Interfaces;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;

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
            ViewModelLocationProvider.Register<AppFooter, AppFooterViewModel>();
            ViewModelLocationProvider.Register<IssuesView, IssuesViewModel>();
            ViewModelLocationProvider.Register<AppButton, AppButtonViewModel>();
            //ViewModelLocationProvider.Register<AppLabel, AppLabelViewModel>();

            containerRegistry.RegisterForNavigation<AppFooter, AppFooterViewModel>(RegisterForNavigation.IssueFooter);
            containerRegistry.RegisterForNavigation<IssuesView, IssuesViewModel>(RegisterForNavigation.IssueMain);
            //containerRegistry.RegisterForNavigation<AppLabel, AppLabelViewModel>(RegisterForNavigation.IssueMain);
            containerRegistry.RegisterForNavigation<NewIssue, NewIssueViewModel>(RegisterForNavigation.NewIssue);

            containerRegistry.RegisterSingleton<IIssueService, IssueService>();
        }
    }
}