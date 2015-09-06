using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.UI.Components
{
    public partial class HotkeyIndicatorSettings : UserControl
    {
        public Color HotkeysOnColor { get; set; }
        public Color HotkeysOffColor { get; set; }
        public GradientType BackgroundGradient { get; set; }
        public string GradientString
        {
            get { return BackgroundGradient.ToString(); }
            set { BackgroundGradient = (GradientType)Enum.Parse(typeof(GradientType), value); }
        }

        public HotkeyIndicatorSettings()
        {
            InitializeComponent();

            HotkeysOnColor = Color.FromArgb(41, 204, 84);
            HotkeysOffColor = Color.FromArgb(204, 55, 41);

            btnColor1.DataBindings.Add("BackColor", this, "HotkeysOnColor", false, DataSourceUpdateMode.OnPropertyChanged);
            btnColor2.DataBindings.Add("BackColor", this, "HotkeysOffColor", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        public void SetSettings(XmlNode node)
        {
            var element = (XmlElement)node;
            HotkeysOnColor = SettingsHelper.ParseColor(element["HotkeysOnColor"]);
            HotkeysOffColor = SettingsHelper.ParseColor(element["HotkeysOffColor"]);
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            var parent = document.CreateElement("Settings");
            CreateSettingsNode(document, parent);
            return parent;
        }

        public int GetSettingsHashCode()
        {
            return CreateSettingsNode(null, null);
        }

        private int CreateSettingsNode(XmlDocument document, XmlElement parent)
        {
            return SettingsHelper.CreateSetting(document, parent, "HotkeysOnColor", HotkeysOnColor) ^
            SettingsHelper.CreateSetting(document, parent, "HotkeysOffColor", HotkeysOffColor);
        }

        private void ColorButtonClick(object sender, EventArgs e)
        {
            SettingsHelper.ColorButtonClick((Button)sender, this);
        }
    }
}
