using Application;
using Avalonia;
using Avalonia.Controls;
using GUI.Styles;
using System;
using System.Linq;

namespace GUI
{
    internal static class ButtonFactory
    {
        public static IControl CreateButton(IApplication application, string label, Action<IApplication> onClick)
        {
            Button button = new Button()
            {
                Content = label,
            };

            button.Classes.AddRange(new string[]
            {
                WhiteText.Name,
                PurpleBackgroundRedHighlight.Name,
                Rounded.Name,
                NoBorder.Name,
                SmallMargin.Name,
                MediumPadding.Name
            });

            button.Click += (sender, e) =>
            {
                onClick(application);

                e.Handled = true;
            };

            return button;
        }
    }
}
