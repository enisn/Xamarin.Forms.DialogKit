using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plugin.DiaglogKit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckBoxView : ContentView
    {
        public CheckBoxView(params string[] options)
        {
            InitializeComponent();
            for (int i = 0; i < options.Length; i++)
                slContent.Children.Add(new Checkbox(options[i],i));
        }

        public CheckBoxView(Dictionary<int,string> keyValues)
        {
            InitializeComponent();
            foreach (var item in keyValues)
                slContent.Children.Add(new Checkbox(item.Value,item.Key));
        }
        public event EventHandler<IList<int>> Completed;

        private void Confirm_Clicked(object sender, EventArgs e)
        {
            Completed?.Invoke(this, GetSelectedKeys());
        }
        private void Cancel_Clicked(object sender, EventArgs e)
        {

        }
        public static readonly BindableProperty SelectedKeysProperty = BindableProperty.Create(nameof(SelectedKeys), typeof(IList<int>), typeof(CheckBoxView), new[] { 0 }, propertyChanged: (bo, ov, nv) => (bo as CheckBoxView).SelectedKeys = (IList<int>)nv);
        public IList<int> SelectedKeys { get => GetSelectedKeys(); set => UpdateSelectedKeys(value); }
        public IList<int> GetSelectedKeys()
        {
            List<int> selectedValues = new List<int>();
            foreach (var item in slContent.Children)
                if (item is Checkbox)
                    if ((item as Checkbox).IsChecked)
                        selectedValues.Add((item as Checkbox).Value);

            return selectedValues;
        }
        public void UpdateSelectedKeys(IList<int> keys)
        {
            foreach (var item in slContent.Children)
            {
                if (item is Checkbox)
                    (item as Checkbox).IsChecked = keys.Contains((item as Checkbox).Value);
            }
        }
    }

    public class Checkbox : StackLayout
    {
        BoxView boxBackground = new BoxView { HeightRequest = 40, WidthRequest = 40, BackgroundColor = Color.LightGray };
        BoxView boxSelected = new BoxView { IsVisible = false, HeightRequest = 25, WidthRequest = 25, BackgroundColor = Color.Accent, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.Center };
        Label lblOption = new Label { VerticalOptions = LayoutOptions.CenterAndExpand };
        public Checkbox(string optionName,int value)
        {
            Value = value;
            lblOption.Text = optionName;

            this.Orientation = StackOrientation.Horizontal;
            this.Margin = new Thickness(10, 0);
            this.Padding = new Thickness(10);
            this.Spacing = 10;
            this.Children.Add(new Grid { Children = { boxBackground, boxSelected } });
            this.Children.Add(lblOption);
            this.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(()=> { IsChecked = !IsChecked; }),
            });
        }
        public int Value { get; set; }
        public bool IsChecked { get => boxSelected.IsVisible; set => boxSelected.IsVisible = value; }
        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(Checkbox), false, propertyChanged: (bo, ov, nv) => (bo as Checkbox).IsChecked = (bool)nv);




    }
}