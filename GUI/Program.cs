using System;
using Avalonia;

namespace GUI
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            AppBuilder.Configure(() => new ToDoListApp(new Application.Application()))
                .UseWin32()
                .UseSkia()
                .StartWithClassicDesktopLifetime(args);
        }
    }
}