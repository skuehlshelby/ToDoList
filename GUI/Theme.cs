using Application;
using Avalonia.Media;

namespace GUI
{
    internal sealed class Theme
    {
        private static readonly IBrush Navy = SolidColorBrush.Parse("#16213E");
        private static readonly IBrush Blue = SolidColorBrush.Parse("#0F3460");
        private static readonly IBrush Purple = SolidColorBrush.Parse("#533483");
        private static readonly IBrush Red = SolidColorBrush.Parse("#E94560");
        private static readonly IBrush White = SolidColorBrush.Parse("#EEEEEE");

        public IBrush TextColor => White;

        public IBrush ListBackgroundColor => Navy;

        public IBrush ToDoItemColor => Purple;

        public IBrush ToDoItemHighlightColor => Red;

        public IBrush MenuBackgroundColor => Blue;

        public IBrush ButtonColor => Purple;

        public IBrush ButtonHighlightColor => Red;

        public IBrush CheckboxColor => Purple;

        public IBrush CheckboxHighlightColor => Red;
    }
}
