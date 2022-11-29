using Avalonia.Controls;

namespace GUI
{
    internal static class Extensions
    {
        public static void Add(this Grid grid, IControl control, byte column, byte row)
        {
            control.SetValue(Grid.ColumnProperty, column);
            control.SetValue(Grid.RowProperty, row);

            grid.Children.Add(control);
        }
    }
}
