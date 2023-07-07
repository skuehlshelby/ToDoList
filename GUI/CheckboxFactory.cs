using Application;
using Avalonia.Controls;
using System;
using GUI.Styles;

namespace GUI
{
    internal static class CheckboxFactory
    {
        public static IControl CreateCheckbox(IApplication application, string label, bool isChecked, Action<IApplication> onChecked, Action<IApplication> onUnchecked)
        {
            CheckBox checkbox = new CheckBox()
            {
                Content = label,
                IsChecked = isChecked,
            };

            checkbox.Classes.AddRange(new string[] { WhiteText.Name, WhiteBorder.Name, Rounded.Name, SmallMargin.Name });

            checkbox.Checked += (sender, e) => 
            {
                onChecked(application);

                e.Handled = true;
            };
            
            checkbox.Unchecked += (sender, e) =>
            {
                onUnchecked(application);

                e.Handled = true;
            };

            Border border = new Border()
            {
                Child = checkbox,
            };

            border.Classes.AddRange(new string[] { PurpleBackgroundRedHighlight.Name, Rounded.Name, Padded.Name, SmallMargin.Name });

            return border;
        }
    }
}
