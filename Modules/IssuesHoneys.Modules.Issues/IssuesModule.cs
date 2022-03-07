using IssuesHoneys.Core.NameDefinition;
using IssuesHoneys.Modules.Issues.ViewModels;
using IssuesHoneys.Modules.Issues.Views;
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
            _regionManager.RegisterViewWithRegion(RegionNames.RibbonTabButtonContentRegion, typeof(AppRibbonTabButton));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ViewModelLocationProvider.Register<AppFooter, AppFooterViewModel>();
            ViewModelLocationProvider.Register<AppMain, AppMainViewModel>();
            ViewModelLocationProvider.Register<AppRibbonTabButton, AppRibbonTabViewModel>();

            containerRegistry.RegisterForNavigation<AppFooter, AppFooterViewModel>(RegisterForNavigation.IssueFooter);
            containerRegistry.RegisterForNavigation<AppMain, AppMainViewModel>(RegisterForNavigation.IssueMain);

        }
    }
}