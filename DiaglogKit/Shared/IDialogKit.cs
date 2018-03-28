using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Plugin.DialogKit.Shared
{
    public interface IDialogKit
    {
        /// <summary>
        /// To get color input from user
        /// </summary>
        /// <param name="title">Title will be shown top of dialog box</param>
        /// <param name="message">A message about input</param>
        /// <param name="colors">You can send custom colors to pick as available</param>
        /// <returns></returns>
        Task<Color?> GetColorAsync(string title, string message, params Color[] colors);
        /// <summary>
        /// Just quick get an Text input
        /// </summary>
        /// <param name="title">Title will be shown top of dialog box</param>
        /// <param name="message">A message about input</param>
        /// <param name="keyboard">You can set the keyboard for this input</param>
        Task<string> GetInputTextAsync(string title, string message, Keyboard keyboard = null);
        /// <summary>
        /// Checkbox group
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<string[]> GetCheckboxResultAsync(string title, string message, params string[] options);
        Task<int[]> GetCheckboxResultAsync(string title, string message, Dictionary<int, string> options);
    }
}