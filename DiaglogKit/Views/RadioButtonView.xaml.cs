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
        public RadioButtonView(string title, string message, string[] options) : this()
        {
            BindTexts(title, message);
            for (int i = 0; i < options.Length; i++)
            {
                slContent.Children.Add(new RadioButton(options[i], i == 0));
            }
        }

        public RadioButtonView(string title,string message,IEnumerable<object> values,object selected, string displayMember)
        {
            BindTexts(title, message);
            foreach (var item in values)
            {
                slContent.Children.Add(new RadioButton(item,displayMember, item == selected));
            }
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
}