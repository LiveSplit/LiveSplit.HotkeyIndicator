using Fetze.WinFormsColor;
using LiveSplit.TimeFormatters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.UI.Components
{
    public partial class HotkeyIndicatorSettings : UserControl
    {
        public Color HotkeysOnColor { get; set; }
        public Color HotkeysOffColor { get; set; }
        public GradientType BackgroundGradient { get; set; }
        public String GradientString
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
            parent.AppendChild(SettingsHelper.ToElement(document, HotkeysOnColor, "HotkeysOnColor"));
            parent.AppendChild(SettingsHelper.ToElement(document, HotkeysOffColor, "HotkeysOffColor"));
            return parent;
        }

        private void ColorButtonClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var picker = new ColorPickerDialog();
            picker.SelectedColor = picker.OldColor = button.BackColor;
            picker.SelectedColorChanged += (s, x) => button.BackColor = picker.SelectedColor;
            picker.ShowDialog(this);
            button.BackColor = picker.SelectedColor;
        }

    }
}
