
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
            _dialogView.Picked += (s, e) => { cts.SetResult(e); PopupNavigation.PopAsync(); };
            PopupNavigation.PushAsync(new Rg.Plugins.Popup.Pages.PopupPage
            {
                Content = _dialogView
            });

            return cts.Task;
        }
        public Task<string> GetInputTextAsync(string title, string message,string currentText = null, Keyboard keyboard = null)
        {
           
            if (keyboard == null) keyboard = Keyboard.Default;
            var cts = new TaskCompletionSource<string>();
            var _dialogView = new Plugin.DialogKit.Views.InputView(title, message,currentText,keyboard);
            _dialogView.FocusEntry();
            _dialogView.Picked += (s, o) => { cts.SetResult(o); PopupNavigation.PopAsync(); };
            PopupNavigation.PushAsync(new PopupPage { Content = _dialogView });
            return cts.Task;
        }
        public Task<string[]> GetCheckboxResultAsync(string title,string message, params string[] options)
        {
            var tcs = new TaskCompletionSource<string[]>();
            var _dialogView = new Plugin.DialogKit.Views.CheckBoxView(title, message, options);
            _dialogView.Completed += (s, e) => { tcs.SetResult(e == null ? null : _dialogView.SelectedValues.ToArray()); PopupNavigation.PopAsync(); };
            PopupNavigation.PushAsync(new PopupPage { Content = _dialogView });

            return tcs.Task;
        }
        public Task<int[]> GetCheckboxResultAsync(string title, string message, Dictionary<int,string> options)
        {
            var tcs = new TaskCompletionSource<int[]>();
            var _dialogView = new Plugin.DialogKit.Views.CheckBoxView(title, message, options);
            _dialogView.Completed += (s, e) => { tcs.SetResult(e?.ToArray()); PopupNavigation.PopAsync(); };
            PopupNavigation.PushAsync(new PopupPage { Content = _dialogView });

            return tcs.Task;
        }
        public Task<IEnumerable<T>> GetCheckboxResultAsync<T>(string title, string message, IList<T> source, IList<T> selecteds = null)
        {
            TaskCompletionSource<IEnumerable<T>> tcs = new TaskCompletionSource<IEnumerable<T>>();

            var _dialogView = new Plugin.DialogKit.Views.CheckBoxView(title, message, source, selecteds.AsEnumerable());
            _dialogView.Completed += (s, e) => { tcs.TrySetResult((s as CheckBoxView).GetSelectedValues().Cast<T>());  PopupNavigation.PopAsync(); };
            PopupNavigation.PushAsync(new PopupPage { Content = _dialogView });
            return tcs.Task;
        } 
        public Task<string> GetRadioButtonResultAsync(string title,string message,params string[] options)
        {
            var tcs = new TaskCompletionSource<string>();
            var _dialogView = new Plugin.DialogKit.Views.RadioButtonView(title,message,options);
            _dialogView.Completed += (s, e) => { tcs.SetResult(_dialogView.SelectedText); PopupNavigation.PopAsync(); };
            PopupNavigation.PushAsync(new PopupPage { Content = _dialogView });
            return tcs.Task;
        }
        /// <summary>
        /// Pushes a pop-up page which includes radio buttons for selection
        /// </summary>
        /// <param name="title">Title to be shown to user</param>
        /// <param name="message">Message to be shown to user</param>
        /// <param name="selectionSource">Ask options from a Collection</param>
        /// <param name="displayMember">Which property will be shown of object in collection</param>
        public Task<T> GetRadioButtonResultAsync<T>(string title, string message, IEnumerable<T> selectionSource,T selected = default(T), string displayMember = null)
        {
            var tcs = new TaskCompletionSource<T>();
            var _dialogView = new Plugin.DialogKit.Views.RadioButtonView(title,message, (IEnumerable<object>)selectionSource,selected, displayMember);
            _dialogView.Completed += (s, e) => { tcs.SetResult((T)_dialogView.SelectecItem); PopupNavigation.PopAsync(); };
            PopupNavigation.PushAsync(new PopupPage { Content = _dialogView });
            return tcs.Task;
        }
    }
}
