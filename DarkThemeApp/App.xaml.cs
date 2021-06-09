using DarkThemeApp.Helpers;
using DarkThemeApp.PageModels;
using FreshMvvm;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DarkThemeApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ThemeHelper.SetTheme();
            var page = FreshPageModelResolver.ResolvePageModel<MainPageModel>();
            var basicNavContainer = new FreshNavigationContainer(page);
            MainPage = basicNavContainer;
        }

        protected override void OnStart()
        {
            OnResume();
        }

        protected override void OnSleep()
        {
            ThemeHelper.SetTheme();
            RequestedThemeChanged -= App_RequestedThemeChanged;
        }

        protected override void OnResume()
        {
            ThemeHelper.SetTheme();
            RequestedThemeChanged += App_RequestedThemeChanged;
        }

        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                ThemeHelper.SetTheme();
            })
        }
    }
}
