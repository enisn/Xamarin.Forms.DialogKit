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
        ///---------------------------------------------------------------------------------------------
        /// <summary>
        /// Just quick get an Text input
        /// </summary>
        /// <param name="title">Title will be shown top of dialog box</param>
        /// <param name="message">A message about input</param>
        /// <param name="keyboard">You can set the keyboard for this input</param>
        Task<string> GetInputTextAsync(string title, string message, Keyboard keyboard = null);
        ///---------------------------------------------------------------------------------------------
        /// <summary>
        /// Checkbox group
        /// </summary>
        /// <returns></returns>
        Task<string[]> GetCheckboxResultAsync(string title, string message, params string[] options);
        ///---------------------------------------------------------------------------------------------
        /// <summary>
        /// Checkbox group. Returns values of your defination from dictionary
        /// </summary>
        /// <param name="title">Title to be shown to user</param>
        /// <param name="message">Message to be shown to user</param>
        /// <param name="options">Optins to ask to user</param>
        /// <returns></returns>
        Task<int[]> GetCheckboxResultAsync(string title, string message, Dictionary<int, string> options);
        ///---------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets Radio buttons result
        /// </summary>
        /// <returns></returns>
        Task<string> GetRadioButtonResultAsync(string title, string message, params string[] options);
        ///---------------------------------------------------------------------------------------------
        /// <summary>
        /// Pushes a pop-up page which includes radio buttons for selection
        /// </summary>
        /// <param name="title">Title to be shown to user</param>
        /// <param name="message">Message to be shown to user</param>
        /// <param name="selectionSource">Ask options from a Collection</param>
        /// <param name="displayMember">Which property will be shown of object in collection</param>
        Task<T> GetRadioButtonResultAsync<T>(string title, string message, IEnumerable<T> selectionSource, string displayMember);

    }
}