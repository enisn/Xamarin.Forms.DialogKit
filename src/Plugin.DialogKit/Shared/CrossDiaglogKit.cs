
using Plugin.DiaglogKit.Shared;
using Plugin.DialogKit.Shared;
using System;

namespace Plugin.DialogKit
{
    /// <summary>
    /// Cross DiaglogKit
    /// </summary>
    public static class CrossDiaglogKit
    {
        static Lazy<IDialogKit> implementation = new Lazy<IDialogKit>(() => CreateDiaglogKit(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Gets if the plugin is supported on the current platform.
        /// </summary>
        public static bool IsSupported => implementation.Value == null ? false : true;

        static IDialogKit _current;
        /// <summary>
        /// Current plugin implementation to use
        /// </summary>
        public static IDialogKit Current
        {
            get
            {
                if (_current == null)
                    _current = new DialogKit.Shared.DialogKit();
                return _current;
            }
        }
        public static GlobalSetting GlobalSettings { get; private set; } = new GlobalSetting();
        static IDialogKit CreateDiaglogKit()
        {
            return new DialogKit.Shared.DialogKit();
        }

        internal static Exception NotImplementedInReferenceAssembly() =>
            new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");

    }
}
