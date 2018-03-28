using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plugin.DialogKit.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InputView : ContentView
	{
        public InputView(string title, string message,Keyboard keyboard = null)
        {
            InitializeComponent();
            this.BindingContext = new { Title = title, Message = message };
            txtInput.Keyboard = keyboard;
            txtInput.Focus();
        }

        public event EventHandler<string> Picked;
        public void Confirm_Clicked(object sender, EventArgs e)
        {
            Picked?.Invoke(this, txtInput.Text);
        }
        public void Cancel_Clicked(object sender, EventArgs e)
        {
            Picked?.Invoke(this, null);
        }
    }
}