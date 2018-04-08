
using Plugin.DialogKit.Views;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Plugin.DialogKit.Shared
{
    public class DialogKit : IDialogKit
    {
        static readonly Color[] defaultColors = { Color.Blue, Color.Green, Color.Red, Color.Yellow, Color.Orange, Color.Purple, Color.Brown };

        public Task<Color?> GetColorAsync(string title, string message, params Color[] colors)
        {
            var cts = new TaskCompletionSource<Color?>();

            if (colors == null || colors.Length == 0) colors = defaultColors;
            var _dialogView = new ColorPickerView(title, message, colors);
            _dialogView.Picked += (s, e) => { cts.SetResult(_dialogView.CurrentColor); PopupNavigation.PopAsync(); };
            PopupNavigation.PushAsync(new Rg.Plugins.Popup.Pages.PopupPage
            {
                Content = _dialogView
            });

            return cts.Task;
        }

        public Task<string> GetInputTextAsync(string title, string message, Keyboard keyboard = null)
        {
           
            if (keyboard == null) keyboard = Keyboard.Default;
            var cts = new TaskCompletionSource<string>();
            var _dialogView = new Plugin.DialogKit.Views.InputView(title, message);
            _dialogView.FocusEntry();
            _dialogView.Picked += (s, o) => { cts.SetResult(o); PopupNavigation.PopAsync(); };
            PopupNavigation.PushAsync(new PopupPage { Content = _dialogView });
            return cts.Task;
        }
        public Task<string[]> GetCheckboxResultAsync(string title,string message, params string[] options)
        {
            var tcs = new TaskCompletionSource<string[]>();
            var _dialogView = new Plugin.DialogKit.Views.CheckBoxView(title, message, options);
            _dialogView.Completed += (s, e) => { tcs.SetResult(_dialogView.SelectedValues.ToArray()); PopupNavigation.PopAsync(); };
            PopupNavigation.PushAsync(new PopupPage { Content = _dialogView });

            return tcs.Task;
        }
        public Task<int[]> GetCheckboxResultAsync(string title, string message, Dictionary<int,string> options)
        {
            var tcs = new TaskCompletionSource<int[]>();
            var _dialogView = new Plugin.DialogKit.Views.CheckBoxView(title, message, options);
            _dialogView.Completed += (s, e) => { tcs.SetResult(_dialogView.SelectedKeys.ToArray()); PopupNavigation.PopAsync(); };
            PopupNavigation.PushAsync(new PopupPage { Content = _dialogView });

            return tcs.Task;
        }

        public Task<string> GetRadioButtonResultAsync(string title,string message,params string[] options)
        {
            var tcs = new TaskCompletionSource<string>();
            var _dialogView = new Plugin.DialogKit.Views.RadioButtonView(title,message,options);
            _dialogView.Completed += (s, e) => { tcs.SetResult(_dialogView.SelectedOption); PopupNavigation.PopAsync(); };
            PopupNavigation.PushAsync(new PopupPage { Content = _dialogView });
            return tcs.Task;
        }
    }
}
