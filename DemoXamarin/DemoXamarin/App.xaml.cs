using System;
using DemoXamarin.View;
using DemoXamarin.ViewModel;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoXamarin
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null):base(initializer)
        {
            MainPage = new HomePage();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainPage,MainPageViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<QLCTPage, QLCTPageViewModel>();
        }

        protected override void OnInitialized()
        {
            InitializeComponent();
        }
    }
}
