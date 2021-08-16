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

        bool isSelected { get; set; }
            
        private ButtonModel selectedButton;

        public ButtonModel SelectedButton
        {
            get => selectedButton;
            set
            {
                isSelected = true;
                if (selectedButton != value)
                {
                    selectedButton = value;

                    //Sets theme when pressed
                    Theme = selectedButton.Theme;
                    ThemeHelper.SetTheme();
                    
                    //Handles image changes
                    if(isSelected == true && (selectedButton.Image != ThemeHelper.ImageName))
                    {
                        selectedButton.Image = ThemeHelper.ImageName;
                        isSelected = false; 
                    }
                }
            }
        }

        const int theme = 0;

        public static int Theme
        {
            get => Preferences.Get(nameof(Theme), theme);
            set => Preferences.Set(nameof(Theme), value);
        }

        //Color rainbowColour;
        //public Color RainbowColour
        //{
        //    get => rainbowColour;
        //    set
        //    {
        //        rainbowColour = value;
        //    }

        //}

        //Commands
        //ICommand rainbowCommand;
        //public ICommand RainbowCommand =>
        //    rainbowCommand ?? (rainbowCommand = new Command( async () => ExecuteRainbowCommand(), () => canExecuteRainbowCommand));

        //private bool canExecuteRainbowCommand { get; set; } = true;

        //private void SetCanExecuteRainbowCommand(bool val)
        //{
        //    canExecuteRainbowCommand = val;
        //    (RainbowCommand as Command).ChangeCanExecute();
        //}


        //Methods

        public void SetUpButtons()
        {
            ObservableCollection<ButtonModel> buttonsList = new ObservableCollection<ButtonModel>();

            buttonsList.Add(new ButtonModel() { Name = AppResources.System, Theme = 0, Image = "settingsicon",});
            buttonsList.Add(new ButtonModel() { Name = AppResources.Light, Theme = 1, Image = "lighticon" });
            buttonsList.Add(new ButtonModel() { Name = AppResources.Dark, Theme = 2, Image = "darkicon" });

            Buttons = buttonsList;
        }

        //private Color ExecuteRainbowCommand()
        //{
        //    SetCanExecuteRainbowCommand(false);
        //    var random = new Random();
        //    var colour1 = random.Next(0, 255);
        //    var colour2 = random.Next(0, 255);
        //    var colour3 = random.Next(0, 255);

        //    var newColour = Color.FromRgb(colour1, colour2, colour3);

        //    SetCanExecuteRainbowCommand(true);
        //    return newColour;
            
        //}

        //Text
        //public string Random => AppResources.Random;
    }
}
