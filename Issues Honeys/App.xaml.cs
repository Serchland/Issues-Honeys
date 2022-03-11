using Issues;
using Issues_Honeys.Core.Prism;
using Issues_Honeys.Views;
using IssuesHoneys.Core.Types.Interfaces;
using IssuesHoneys.Modules.Projects;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Windows;
using System.Windows.Controls;

namespace Issues_Honeys
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommands>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);

            moduleCatalog.AddModule<IssuesModule>();
            moduleCatalog.AddModule<ProjectsModule>();
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);

            //regionAdapterMappings.RegisterMapping(typeof(RibbonGroupBox), Container.Resolve<RibbonGroupBoxRegionAdapter>());
            regionAdapterMappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdapter>());
            //regionAdapterMappings.RegisterMapping(typeof(Ribbon), Container.Resolve<RibbonRegionAdapter>());
        }
    }
}
