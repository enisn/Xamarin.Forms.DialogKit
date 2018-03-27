using System.Threading.Tasks;
using Xamarin.Forms;

namespace Plugin.DialogKit.Shared
{
    public interface IDialogKit
    {
        Task<Color?> GetColorAsync(string title, string message, params Color[] colors);
        Task<string> GetInputTextAsync(string title, string message, Keyboard keyboard = null);
    }
}