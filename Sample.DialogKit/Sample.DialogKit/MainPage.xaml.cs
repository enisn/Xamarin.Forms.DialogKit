
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
        private void ChooseColor_Clicked(object sender,EventArgs e)
        {
            Plugin.DialogKit.CrossDiaglogKit.Current.GetColorAsync("Choose a color", "Pick one:");
        }
        private void InputText_Clicked(object sender, EventArgs e)
        {
            Plugin.DialogKit.CrossDiaglogKit.Current.GetInputTextAsync("New Title","Type something:");
        }

    }
}
