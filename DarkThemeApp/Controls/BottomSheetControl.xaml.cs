using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DarkThemeApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BottomSheetControl : ContentView
    {
        public BottomSheetControl()
        {
            InitializeComponent();
        }

        //when the sheet opens - set the height to be plus 60 so it can be seen - CAn be binded to implementation to be TranslationY = {Binding SheetHeight} + 60 but can also be used in pagemodel
        protected override void OnBindingContextChanged()
        {
            try
            {
                base.OnBindingContextChanged();
                PanContainerRef.Content.TranslationY = SheetHeight + 60;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        #region Properties

        //sets the height of the sheet - SheetHeight
        public static BindableProperty SheetHeightProperty = BindableProperty.Create(
            nameof(SheetHeight),
            typeof(double),
            typeof(BottomSheetControl),
            defaultValue: default(double),
            defaultBindingMode: BindingMode.TwoWay);

        //sets value of sheetheight property to be the same value of sheetheight 
        public double SheetHeight
        {
            get { return (double)GetValue(SheetHeightProperty); }
            set { SetValue(SheetHeightProperty, value); OnPropertyChanged(); }
        }

        //sets what is going to be in the frame however this can be ignored to be honest it just requires a bit of rejigging - does not need to be binded to as you could easily put a stacj layout but I guess binding makes it reusable

        public static BindableProperty SheetContentProperty = BindableProperty.Create(
            nameof(SheetContent),
            typeof(View),
            typeof(BottomSheetControl),
            defaultValue: default(View),
            defaultBindingMode: BindingMode.TwoWay);

        public View SheetContent
        {
            get { return (View)GetValue(SheetContentProperty); }
            set { SetValue(SheetContentProperty, value); OnPropertyChanged(); }
        }
        #endregion

        //long int that later on helps with the transition - maybe the transition should be dealt with in a service?
        uint duration = 250;

        //change position depending on platform (this statement is horrible oh my days)
        double openPosition = (DeviceInfo.Platform == DevicePlatform.Android) ? 20 : 60;
        //default position is 0
        double currentPosition = 0;

        public async void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            try
            {
                if (e.StatusType == GestureStatus.Running)
                {
                    //if you tap outside of the box
                    currentPosition = e.TotalY;
                    if (e.TotalY > 0)
                    {
                        PanContainerRef.Content.TranslationY = openPosition + e.TotalY;
                    }
                }
                else if (e.StatusType == GestureStatus.Completed)
                {
                    //if the box has been opened or it isn't active then it will either open the sheet or close the sheet
                    var threshold = SheetHeight * 0.55;

                    if (currentPosition < threshold)
                    {
                        await OpenSheet();
                    }
                    else
                    {
                        await CloseSheet();
                    }
                }
            }
            catch (Exception ex)
            {
                //gotta catch em all
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task OpenSheet()
        {
            try
            {
                IsVisible = true;
                //change to async this task.whenall thing is weird 
                await Task.WhenAll
                (
                    //control animation but this might have to stay here but we will see 
                    Backdrop.FadeTo(0.4, length: duration),
                    //opens up the box so that it is bigger
                    Sheet.TranslateTo(0, openPosition, length: duration, easing: Easing.SinIn)
                );

                //oh god this statement is anightmare but i guess you need to make sure it's all good

                BottomSheetRef.InputTransparent = Backdrop.InputTransparent = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task CloseSheet()
        {
            try
            {
                IsVisible = false;
                await Task.WhenAll
                (
                    //the fading has been complete and now needs to leave
                    Backdrop.FadeTo(0, length: duration),
                    //there is no sheet height
                    PanContainerRef.Content.TranslateTo(x: 0, y: SheetHeight + 60, length: duration, easing: Easing.SinIn)
                );

                //it becomes transparent as the sheet closes
                BottomSheetRef.InputTransparent = Backdrop.InputTransparent = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                //closes the sheet when tapped out of box`
                await CloseSheet();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

    }
}