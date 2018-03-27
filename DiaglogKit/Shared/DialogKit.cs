
using Plugin.DialogKit.Views;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
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
            _dialogView.Picked += (s, o) => { cts.SetResult(o); PopupNavigation.PopAsync(); };
            PopupNavigation.PushAsync(new PopupPage { Content = _dialogView });
            return cts.Task;
        }
    }
}
