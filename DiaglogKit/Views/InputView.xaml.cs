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
        public InputView(string title, string message,string text = null,Keyboard keyboard = null)
        {
            InitializeComponent();
            txtInput.Text = text;
            BindTexts(title, message);
            txtInput.Keyboard = keyboard;
        }

        private void BindTexts(string title, string message)
        {
            this.BindingContext = new
            {
                Title = title,
                Message = message,
                OK = CrossDiaglogKit.GlobalSettings.DialogAffirmative,
                Cancel = CrossDiaglogKit.GlobalSettings.DialogNegative,
            };
        }
        public event EventHandler<string> Picked;
        public void FocusEntry()
        {
            txtInput.Focus();
        }
        private void Confirm_Clicked(object sender, EventArgs e)
        {
            Picked?.Invoke(this, txtInput.Text);
        }
        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Picked?.Invoke(this, null);
        }
    }
}