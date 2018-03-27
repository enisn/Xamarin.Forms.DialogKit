using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.DialogKit.Shared
{
    public class CrossDialogKit
    {
        private static IDialogKit _current;
        public static IDialogKit Current
        {
            get
            {
                if (_current == null)
                    _current = new DialogKit();
                
                return _current;
            }
        }
    }
}
