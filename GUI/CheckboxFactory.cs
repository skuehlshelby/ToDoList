using Application;
using Avalonia.Controls;
using Avalonia;
using System;

namespace GUI
{
    internal static class CheckboxFactory
    {
        public static IControl CreateCheckbox(IApplication application, Theme theme, string label, bool isChecked, Action<IApplication> onChecked, Action<IApplication> onUnchecked)
        {
            CheckBox checkbox = new CheckBox()
            {
                BorderBrush = theme.TextColor,
                BorderThickness = new Thickness(2.0),
                Content = label,
                CornerRadius = new CornerRadius(5.0),
                Foreground = theme.TextColor,
                IsChecked = isChecked,
                Margin = new Thickness(2.0)
            };

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
                Background = theme.CheckboxColor,
                BorderBrush = theme.CheckboxColor,
                BorderThickness = new Thickness(2.0),
                Child = checkbox,
                CornerRadius = new CornerRadius(5.0),
                Margin = new Thickness(4.0)
            };

            border.PointerEnter += (sender, e) =>
            {
                if (sender is Border bdr)
                {
                    bdr.Background = theme.CheckboxHighlightColor;
                    bdr.BorderBrush = theme.CheckboxHighlightColor;
                }

                e.Handled = true;
            };

            border.PointerLeave += (sender, e) =>
            {
                if (sender is Border bdr)
                {
                    bdr.Background = theme.CheckboxColor;
                    bdr.BorderBrush = theme.CheckboxColor;
                }

                e.Handled = true;
            };

            return border;
        }
    }
}
