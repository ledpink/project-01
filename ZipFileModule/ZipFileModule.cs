using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using ZipFileModule.MVVM.Views;

namespace ZipFileModule
{
    public class ZipFileModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("RegionModule", typeof(MainView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
