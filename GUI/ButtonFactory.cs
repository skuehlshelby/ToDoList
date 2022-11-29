using Application;
using Avalonia;
using Avalonia.Controls;
using System;

namespace GUI
{
    internal static class ButtonFactory
    {
        public static IControl CreateButton(IApplication application, Theme theme, string label, Action<IApplication> onClick)
        {
            Button button = new Button()
            {
                Background = theme.ButtonColor,
                BorderThickness = new Thickness(0.0),
                Content = label,
                CornerRadius = new CornerRadius(5.0),
                Foreground = theme.TextColor,
            };

            button.Click += (sender, e) =>
            {
                onClick(application);

                e.Handled = true;
            };

            Border border = new Border()
            {
                Background = theme.ButtonColor,
                BorderBrush = theme.ButtonColor,
                BorderThickness = new Thickness(2.0),
                Child = button,
                CornerRadius = new CornerRadius(5.0),
                Margin = new Thickness(4.0)
            };

            border.PointerEnter += (sender, e) =>
            {
                if (sender is Border bdr)
                {
                    bdr.Background = theme.ButtonHighlightColor;
                    bdr.BorderBrush = theme.ButtonHighlightColor;

                    if (bdr.Child is Button btn)
                    {
                        btn.Background = theme.ButtonHighlightColor;
                        btn.BorderBrush = theme.ButtonHighlightColor;
                    }
                }

                e.Handled = true;
            };

            border.PointerLeave += (sender, e) =>
            {
                if (sender is Border bdr)
                {
                    bdr.Background = theme.ButtonColor;
                    bdr.BorderBrush = theme.ButtonColor;

                    if (bdr.Child is Button btn)
                    {
                        btn.Background = theme.ButtonColor;
                        btn.BorderBrush = theme.ButtonColor;
                    }
                }

                e.Handled = true;
            };

            return border;
        }
    }
}
