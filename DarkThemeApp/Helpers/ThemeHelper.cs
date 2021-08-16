using DarkThemeApp.Models;
using DarkThemeApp.PageModels;
using Xamarin.Forms;

namespace DarkThemeApp.Helpers
{
    public static class ThemeHelper
    {
        public static string ImageName { get; set; }
        public static void SetTheme()
        {
           
            switch(MainPageModel.Theme)
            {
                case 0:
                    App.Current.UserAppTheme = OSAppTheme.Unspecified;
                    ImageName = "settingsicon_filled.png";
                    break;

                case 1:
                    App.Current.UserAppTheme = OSAppTheme.Light;
                    ImageName = "lighticon_filled.png";
                    break;

                case 2:
                    App.Current.UserAppTheme = OSAppTheme.Dark;
                    ImageName = "darkicon_filled.png";
                    break;
            }
        }
    }
}
