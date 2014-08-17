using LiveSplit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveSplit.UI.Components
{
    public class HotkeyIndicatorFactory : IComponentFactory
    {
        public string ComponentName
        {
            get { return "Hotkey Indicator"; }
        }

        public string Description
        {
            get { return "Shows whether global hotkeys are on or off. Green indicates that global hotkeys are on, while red indicates off."; }
        }

        public ComponentCategory Category
        {
            get { return ComponentCategory.Other; }
        }

        public IComponent Create(LiveSplitState state)
        {
            return new HotkeyIndicator();
        }

        public string UpdateName
        {
            get { return ComponentName; }
        }

        public string XMLURL
        {
#if RELEASE_CANDIDATE
            get { return "http://livesplit.org/update_rc_sdhjdop/Components/update.LiveSplit.HotkeyIndicator.xml"; }
#else
            get { return "http://livesplit.org/update/Components/update.LiveSplit.HotkeyIndicator.xml"; }
#endif
        }

        public string UpdateURL
        {
#if RELEASE_CANDIDATE
            get { return "http://livesplit.org/update_rc_sdhjdop/"; }
#else
            get { return "http://livesplit.org/update/"; }
#endif
        }

        public Version Version
        {
            get { return Version.Parse("1.1.0"); }
        }
    }
}
