using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using DarkThemeApp.Helpers;
using DarkThemeApp.Models;
using DarkThemeApp.Resx;
using FreshMvvm;
using Xamarin.Essentials;

namespace DarkThemeApp.PageModels
{
    public class MainPageModel : FreshBasePageModel
    {

        public MainPageModel()
        {
            SetUpButtons();
        }

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
                if(selectedButton != value)
                {
                    selectedButton = value;
                    ChangeTheme();
                }
            }
        }

        const int theme = 0;

        public static int Theme
        {
            get => Preferences.Get(nameof(Theme), theme);
            set => Preferences.Get(nameof(Theme), value);
        }

        //Methods
        public void ChangeTheme()
        {
            ThemeHelper.SetTheme();
        }

        public void SetUpButtons()
        {
            ObservableCollection<ButtonModel> buttons = new ObservableCollection<ButtonModel>();

            buttons.Add(new ButtonModel() { Name = AppResources.System, Theme = 0 });
            buttons.Add(new ButtonModel() { Name = AppResources.Light, Theme = 1 });
            buttons.Add(new ButtonModel() { Name = AppResources.Dark, Theme = 2 });
            buttons.Add(new ButtonModel() { Name = AppResources.Random, Theme = 3 });

            Buttons = buttons;
        }

        //Text
       
    }
}
