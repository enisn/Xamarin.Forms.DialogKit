using Plugin.InputKit.Shared.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plugin.DialogKit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RadioButtonView : ContentView
    {
        public RadioButtonView()
        {
            InitializeComponent();
        }

        public RadioButtonView(string title, string message, IEnumerable<object> values, object selected, string displayMember) : this()
        {
            BindTexts(title, message);

            if (!string.IsNullOrEmpty(displayMember))
                selectionView.ItemDisplayBinding = new Binding(displayMember);
            selectionView.SelectedItem = selected;
            selectionView.ItemsSource = values.ToList();
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

        public event EventHandler Completed;

        public int SelectedIndex { get => selectionView.SelectedIndex; }
        public object SelectecItem { get => selectionView.SelectedItem; }

        private void Confirm_Clicked(object sender, EventArgs e)
        {
            Completed?.Invoke(this, new EventArgs());
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Completed?.Invoke(this, new EventArgs());
        }
    }
}