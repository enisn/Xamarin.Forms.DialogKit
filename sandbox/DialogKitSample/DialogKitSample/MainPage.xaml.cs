using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DialogKitSample
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void ChooseColor_Clicked(object sender, EventArgs e)
        {
            var result = await Plugin.DialogKit.CrossDiaglogKit.Current.GetColorAsync("Choose a color", "Pick one:");
            if (result != null)
            {
                (sender as Button).BackgroundColor = result.Value;
            }
        }
        private async void InputText_Clicked(object sender, EventArgs e)
        {

            var result = await Plugin.DialogKit.CrossDiaglogKit.Current.GetInputTextAsync("New Title", "Type something:");
            if (result != null)
            {
                (sender as Button).Text = result;
            }
        }
        private async void InputCheckBox_Clicked(object sender, EventArgs e)
        {

            var result = await Plugin.DialogKit.CrossDiaglogKit.Current.GetCheckboxResultAsync("Title", "Choose some", "Option 1", "Option 2");
        }
        private async void InputRadioButton_Clicked(object sender, EventArgs e)
        {
            var result = await Plugin.DialogKit.CrossDiaglogKit.Current.GetRadioButtonResultAsync("Başlık", "Choose some", "Option 1", "Option 2", "Option 3", "Option 4");
        }
    }
}
