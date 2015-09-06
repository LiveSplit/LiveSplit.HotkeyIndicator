using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LiveSplit
{
    public class HotkeyIndicator : IComponent
    {
        public float PaddingTop => 0f;
        public float PaddingLeft => 0f;
        public float PaddingBottom => 0f;
        public float PaddingRight => 0f;

        protected LineComponent Line { get; set; }

        public Color CurrentColor { get; set; }

        public HotkeyIndicatorSettings Settings { get; set; }

        public GraphicsCache Cache { get; set; }

        public float VerticalHeight => 3f;

        public float MinimumWidth => 0f;

        public float HorizontalWidth => 3f;

        public float MinimumHeight => 0f;

        public HotkeyIndicator()
        {
            Line = new LineComponent(3, Color.White);
            Cache = new GraphicsCache();
            Settings = new HotkeyIndicatorSettings();
        }

        public void DrawVertical(Graphics g, LiveSplitState state, float width, Region clipRegion)
        {
            var oldClip = g.Clip;
            var oldMatrix = g.Transform;
            var oldMode = g.SmoothingMode;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            g.Clip = new Region();
            Line.LineColor = CurrentColor;
            var scale = g.Transform.Elements.First();
            var newHeight = Math.Max((int)(3f * scale + 0.5f), 1) / scale;
            Line.VerticalHeight = newHeight;
            g.TranslateTransform(0, (3f - newHeight) / 2f);
            Line.DrawVertical(g, state, width, clipRegion);
            g.Clip = oldClip;
            g.Transform = oldMatrix;
            g.SmoothingMode = oldMode;
        }

        public void DrawHorizontal(Graphics g, LiveSplitState state, float height, Region clipRegion)
        {
            var oldClip = g.Clip;
            var oldMatrix = g.Transform;
            var oldMode = g.SmoothingMode;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            g.Clip = new Region();
            Line.LineColor = CurrentColor;
            var scale = g.Transform.Elements.First();
            var newWidth = Math.Max((int)(3f * scale + 0.5f), 1) / scale;
            g.TranslateTransform((3f - newWidth) / 2f, 0);
            Line.HorizontalWidth = newWidth;
            Line.DrawHorizontal(g, state, height, clipRegion);
            g.Clip = oldClip;
            g.Transform = oldMatrix;
            g.SmoothingMode = oldMode;
        }

        public string ComponentName => "Hotkey Indicator";

        public Control GetSettingsControl(LayoutMode mode)
        {
            return Settings;
        }

        public void SetSettings(System.Xml.XmlNode settings)
        {
            Settings.SetSettings(settings);
        }


        public System.Xml.XmlNode GetSettings(System.Xml.XmlDocument document)
        {
            return Settings.GetSettings(document);
        }

        
        public IDictionary<string, Action> ContextMenuControls => null;

        public void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            CurrentColor = state.Settings.GlobalHotkeysEnabled ? Settings.HotkeysOnColor : Settings.HotkeysOffColor;

            Cache.Restart();
            Cache["IndicatorColor"] = CurrentColor.ToArgb();

            if (invalidator != null && Cache.HasChanged)
                invalidator.Invalidate(0, 0, width, height);
        }

        public void Dispose()
        {
        }

        public int GetSettingsHashCode() => Settings.GetSettingsHashCode();
    }
}
