using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plugin.DialogKit.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColorPickerView : ContentView
    {
        public ColorPickerView(string title, string message, params Color[] colors)
        {
            InitializeComponent();
            this.BindingContext = new { Title = title, Message = message };
            foreach (var item in colors)
            {
                var colorView = new ColorView(item) { Margin = new Thickness(slColors.Children.Count > 0 ? -40 : 0, 0, 0, 0) };
                colorView.Picked += ColorView_Picked;
                slColors.Children.Add(colorView);
            }
        }
        public Color CurrentColor { get; set; }
        public event EventHandler<Color?> Picked;

        private void ColorView_Picked(object sender, EventArgs e)
        {
            CurrentColor = (sender as ColorView).OwnColor;
            foreach (var item in slColors.Children)
            {
                if (item is ColorView)
                    (item as ColorView).Raised = item == sender;
            }
        }

        public void Confirm_Clicked(object sender, EventArgs e)
        {
            Picked?.Invoke(this, CurrentColor);
        }

        public void Cancel_Clicked(object sender, EventArgs e)
        {
            Picked?.Invoke(this, null);
        }
    }

    public class ColorView : BoxView
    {
        public event EventHandler Picked;
        public Color OwnColor { get => this.BackgroundColor; set => this.BackgroundColor = value; }

        private bool _raised;
        public bool Raised { get => _raised; set => SetRaised(value); }

        private void SetRaised(bool value)
        {
            this.TranslateTo(0, Convert.ToDouble(value) * -25, 300, Easing.SpringOut);
            _raised = value;
        }

        public ColorView(Color color)
        {
            this.BackgroundColor = color;
            this.HeightRequest = 100;
            this.WidthRequest = 100;
            this.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => Picked?.Invoke(this, new EventArgs())),
            });
        }
    }
}