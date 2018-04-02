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
    public partial class CheckBoxView : ContentView
    {
        public CheckBoxView(string title,string message, params string[] options)
        {
            InitializeComponent();
            this.BindingContext = new { Title = title, Message = message };
            for (int i = 0; i < options.Length; i++)
                slContent.Children.Add(new Checkbox(options[i],i));
        }

        public CheckBoxView(string title,string message,Dictionary<int,string> keyValues)
        {
            InitializeComponent();
            this.BindingContext = new { Title = title, Message = message };
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
        public IList<string> SelectedValues { get => GetSelectedValues(); }
        public IList<int> GetSelectedKeys()
        {
            List<int> selectedKeys = new List<int>();
            foreach (var item in slContent.Children)
                if (item is Checkbox)
                    if ((item as Checkbox).IsChecked)
                        selectedKeys.Add((item as Checkbox).Key);

            return selectedKeys;
        }
        public void UpdateSelectedKeys(IList<int> keys)
        {
            foreach (var item in slContent.Children)
            {
                if (item is Checkbox)
                    (item as Checkbox).IsChecked = keys.Contains((item as Checkbox).Key);
            }
        }
        public IList<string> GetSelectedValues()
        {
            List<string> selectedValues = new List<string>();
            foreach (var item in slContent.Children)
                if (item is Checkbox)
                    if ((item as Checkbox).IsChecked)
                        selectedValues.Add((item as Checkbox).Value);

            return selectedValues;
        }

    }

    public class Checkbox : StackLayout
    {
        BoxView boxBackground = new BoxView { HeightRequest = 40, WidthRequest = 40, BackgroundColor = Color.LightGray };
        BoxView boxSelected = new BoxView { IsVisible = false, HeightRequest = 25, WidthRequest = 25, BackgroundColor = Color.Accent, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.Center };
        Label lblOption = new Label { VerticalOptions = LayoutOptions.CenterAndExpand };
        public Checkbox(string optionName,int key)
        {
            Key = key;
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
        public int Key { get; set; }
        public string Value { get => lblOption.Text; set => lblOption.Text = value; }
        public bool IsChecked { get => boxSelected.IsVisible; set => boxSelected.IsVisible = value; }
        public Color Color { get => boxSelected.BackgroundColor; set => boxSelected.BackgroundColor = value; }
        public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(Checkbox), Color.Accent, propertyChanged: (bo, ov, nv) => (bo as Checkbox).Color = (Color)nv);
        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(Checkbox), false, propertyChanged: (bo, ov, nv) => (bo as Checkbox).IsChecked = (bool)nv);
        public static readonly BindableProperty KeyProperty = BindableProperty.Create(nameof(Key), typeof(int), typeof(Checkbox), 0, propertyChanged: (bo, ov, nv) => (bo as Checkbox).Key = (int)nv);
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value),typeof(string),typeof(Checkbox),"",propertyChanged:(bo,ov,nv)=>(bo as Checkbox).Value = (string)nv);


    }
}