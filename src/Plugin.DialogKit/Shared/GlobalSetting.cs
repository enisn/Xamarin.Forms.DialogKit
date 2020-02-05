using Plugin.InputKit.Shared.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.DiaglogKit.Shared
{
    public class GlobalSetting
    {
        public CheckBox.CheckType CheckBoxType { get; set; } = CheckBox.CheckType.Material;
        public string DialogAffirmative { get; set; } = "OK";
        public string DialogNegative { get; set; } = "CANCEL";
    }
}
