using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using project01.Views;
using System.Windows;

namespace project01
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ZipFileModule.ZipFileModule>();
        }
    }
}
