using IssuesHoneys.Core.NameDefinition;
using IssuesHoneys.Modules.Projects.ViewModels;
using IssuesHoneys.Modules.Projects.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;

namespace IssuesHoneys.Modules.Projects
{
    public class ProjectsModule : IModule
    {
        IRegionManager _regionManager;
        public ProjectsModule(IRegionManager regionManager)
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
            ViewModelLocationProvider.Register<AppMain, AppMainViewModel>();
            ViewModelLocationProvider.Register<AppButton, AppButtonViewModel>();

            containerRegistry.RegisterForNavigation<AppFooter, AppFooterViewModel>(RegisterForNavigation.ProjectFooter);
            containerRegistry.RegisterForNavigation<AppMain, AppMainViewModel>(RegisterForNavigation.ProjectMain);
        }
    }
}