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

        public RadioButtonView(string title,string message,IEnumerable<object> values, string displayMember)
        {
            this.BindingContext = new { Title = title, Message = message };
            foreach (var item in values)
            {
                slContent.Children.Add(new RadioButton(item, displayMember));
            }
        }

        public event EventHandler Completed;
        public string SelectedText
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
        public int SelectedIndex { get => slContent.SelectedIndex; }
        public object SelectecItem { get => slContent.SelectedItem; }
        private void Confirm_Clicked()
        {
            Completed?.Invoke(this, new EventArgs());
        }
        private void Cancel_Clicked()
        {
            Completed?.Invoke(this, new EventArgs());
        }

    }

    public class RadioButtonGroupView : StackLayout
    {
        public RadioButtonGroupView()
        {
            this.ChildAdded += RadioButtonGroupView_ChildAdded;
            this.ChildrenReordered += RadioButtonGroupView_ChildrenReordered;
            //Task.Run(async()=> 
            //{
            //    await Task.Delay(1000);
            //    UpdateAllEvent();
            //});
        }

        private void RadioButtonGroupView_ChildrenReordered(object sender, EventArgs e)
        {
            UpdateAllEvent();
        }
        private void UpdateAllEvent()
        {
            foreach (var item in this.Children)
            {
                if (item is RadioButton)
                {
                    (item as RadioButton).Clicked -= UpdateSelected;
                    (item as RadioButton).Clicked += UpdateSelected;
                }
            }
        }

        private void RadioButtonGroupView_ChildAdded(object sender, ElementEventArgs e)
        {
            if (e.Element is RadioButton)
            {
                (e.Element as RadioButton).Clicked -= UpdateSelected;
                (e.Element as RadioButton).Clicked += UpdateSelected;
            }
        }

        void UpdateSelected(object selected, EventArgs e)
        {
            foreach (var item in this.Children)
            {
                if (item is RadioButton)
                    (item as RadioButton).IsChecked = item == selected;
            }
        }
        public int SelectedIndex
        {
            get
            {
                int index = 0;
                foreach (var item in this.Children)
                {
                    if(item is RadioButton)
                    {
                        if ((item as RadioButton).IsChecked)
                            return index;
                        index++;
                    }
                }
                return -1;
            }
            set
            {
                int index = 0;
                foreach (var item in this.Children)
                {
                    if (item is RadioButton)
                    {
                        (item as RadioButton).IsChecked = index == value;
                        index++;
                    }
                }
            }
        }
        public object SelectedItem
        {
            get
            {
                foreach (var item in this.Children)
                {
                    if (item is RadioButton && (item as RadioButton).IsChecked)
                        return item;
                }
                return null;
            }
            set
            {
                foreach (var item in this.Children)
                {
                    if (item is RadioButton)
                        (item as RadioButton).IsChecked = item == value;
                }
            }
        }
    }

    public class RadioButton : StackLayout
    {
        Label lblEmpty = new Label { TextColor = Color.Gray, Text = "◯", HorizontalTextAlignment = TextAlignment.Center };
        Label lblFilled = new Label { TextColor = Color.Accent, Text = "●", IsVisible = false, Scale = 0.9, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center };
        Label lblText = new Label { Text = "", VerticalTextAlignment = TextAlignment.Center, VerticalOptions = LayoutOptions.CenterAndExpand };

        public RadioButton()
        {
            lblEmpty.FontSize = lblText.FontSize * 1.3;
            lblFilled.FontSize = lblText.FontSize * 1.3;
            Orientation = StackOrientation.Horizontal;
            this.Children.Add(new Grid
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children =
                {
                    lblEmpty,
                    lblFilled
                }
            });
            this.Children.Add(lblText);
            this.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(Tapped) });
        }
        public RadioButton(object value, string displayMember) : this()
        {
            this.Value = value;
            var text = value.GetType().GetProperty(displayMember)?.GetValue(value).ToString();
            lblText.Text = text ?? " ";
        }

        public RadioButton(string text, bool isChecked = false) : this()
        {
            Value = text;
            lblText.Text = text;
            this.IsChecked = isChecked;
        }

        public event EventHandler Clicked;
        public ICommand ClickCommand { get; set; }
        public object Value { get; set; }
        public bool IsChecked { get => lblFilled.IsVisible; set => lblFilled.IsVisible = value; }
        public string Text { get => lblText.Text; set => lblText.Text = value; }
        public double FontSize { get => lblText.FontSize; set { lblText.FontSize = value; lblFilled.FontSize = value * 1.3; lblEmpty.FontSize = value * 1.3; } }

        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(RadioButton), false, propertyChanged: (bo, ov, nv) => (bo as RadioButton).IsChecked = (bool)nv);
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(RadioButton), "", propertyChanged: (bo, ov, nv) => (bo as RadioButton).Text = (string)nv);
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(RadioButton), 20.0, propertyChanged: (bo, ov, nv) => (bo as RadioButton).FontSize = (double)nv);

        void Tapped()
        {
            IsChecked = !IsChecked;
            Clicked?.Invoke(this, new EventArgs());
            ClickCommand?.Execute(this);
        }
    }
}