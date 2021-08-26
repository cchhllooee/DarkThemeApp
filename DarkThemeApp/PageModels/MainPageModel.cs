using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DarkThemeApp.Helpers;
using DarkThemeApp.Models;
using DarkThemeApp.Resx;
using FreshMvvm;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DarkThemeApp.PageModels
{
    public class MainPageModel : FreshBasePageModel
    {
        //Ctor
        public MainPageModel()
        {
            SetUpButtons();
        }

        //Properties
        ObservableCollection<ButtonModel> buttons;
        public ObservableCollection<ButtonModel> Buttons
        {
            get => buttons;
            set
            {
                buttons = value;
                RaisePropertyChanged(nameof(Buttons));
            }
        }
            
        private ButtonModel selectedButton;

        public ButtonModel SelectedButton
        {
            get => selectedButton;
            set
            {
                if (selectedButton != value)
                {
                    selectedButton = value;

                    //Sets theme when pressed
                    Theme = selectedButton.Theme;
                    ThemeHelper.SetTheme();
                   
                }
            }
        }

        const int theme = 0;

        public static int Theme
        {
            get => Preferences.Get(nameof(Theme), theme);
            set => Preferences.Set(nameof(Theme), value);
        }

        //Methods
        public void SetUpButtons()
        {
            ObservableCollection<ButtonModel> buttonsList = new ObservableCollection<ButtonModel>();

            buttonsList.Add(new ButtonModel() { Name = AppResources.System, Theme = 0, Image = "settingsicon_filled",});
            buttonsList.Add(new ButtonModel() { Name = AppResources.Light, Theme = 1, Image = "lighticon_filled" });
            buttonsList.Add(new ButtonModel() { Name = AppResources.Dark, Theme = 2, Image = "darkicon_filled" });

            Buttons = buttonsList;
        }

    }
}
