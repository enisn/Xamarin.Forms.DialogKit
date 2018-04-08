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
        public RadioButtonView(string title, string message, string[] options) : this()
        {
            this.BindingContext = new { Title = title, Message = message };

            for (int i = 0; i < options.Length; i++)
            {
                slContent.Children.Add(new RadioButton(options[i], i == 0));
            }
        }

        public event EventHandler Completed;
        public string SelectedOption
        {
            get 
            {
                foreach (var item in slContent.Children)
                {
                    if (item is RadioButton && (item as RadioButton).IsChecked)
                        return (item as RadioButton).Text;
                }
                return null;
            }
        }

        private void Confirm_Clicked()
        {
            Completed?.Invoke(this, new EventArgs());
        }
        private void Cancel_Clicked()
        {
            Completed?.Invoke(this, new EventArgs());
        }

    }
    //public class RadioButtonGroupView : StackLayout
    //{
    //    public RadioButtonGroupView()
    //    {

    //    }
    //}

    public class RadioButton : StackLayout
    {
        Label lblEmpty = new Label { TextColor = Color.Gray, Text = "○", FontSize = 45 , VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.Center };
        //Label lblFilled = new Label { TextColor = Color.Accent, Text = "⦿", FontSize = 40, IsVisible = false, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.Center  };
        Label lblText = new Label { Text = "", VerticalOptions = LayoutOptions.CenterAndExpand };

        public RadioButton()
        {
            Orientation = StackOrientation.Horizontal;
            this.Children.Add(new Grid
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children =
                {
                    lblEmpty,
                    //lblFilled
                }
            });
            this.Children.Add(lblText);
            this.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(Tapped) });
        }

        public RadioButton(string text, bool isChecked = false) : this()
        {
            lblText.Text = text;
            this.IsChecked = isChecked;
        }

        public event EventHandler Clicked;
        public ICommand ClickCommand { get; set; }
        public bool IsChecked { get => lblEmpty.Text == "⦿"; set => lblEmpty.Text = value ? "⦿" : "○"; }
        public string Text { get => lblText.Text; set => lblText.Text = value; }

        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(RadioButton), false, propertyChanged: (bo, ov, nv) => (bo as RadioButton).IsChecked = (bool)nv);
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(RadioButton), "", propertyChanged: (bo, ov, nv) => (bo as RadioButton).Text = (string)nv);


        void Tapped()
        {
            IsChecked = !IsChecked;
            Clicked?.Invoke(this, new EventArgs());
            ClickCommand?.Execute(this);
        }
    }
}