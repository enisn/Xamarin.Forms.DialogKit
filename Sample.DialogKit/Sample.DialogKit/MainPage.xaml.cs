
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sample.DialogKit
{
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

    }
}
