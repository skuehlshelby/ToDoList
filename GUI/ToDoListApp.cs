using System;
using System.Linq;
using System.Reflection;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media.Imaging;
using Avalonia.Styling;
using GUI.Styles;
using AvaloniaApplication = Avalonia.Application;
using IApplication = Application.IApplication;

namespace GUI
{
    internal sealed class ToDoListApp : AvaloniaApplication
    {
        public ToDoListApp(IApplication app)
        {
            DataContext = app;

            Styles.AddRange(new IStyle[] {
                new StyleInclude(baseUri: null) { Source = new Uri("resm:Avalonia.Themes.Default.DefaultTheme.xaml?assembly=Avalonia.Themes.Default") },
                new StyleInclude(baseUri: null) { Source = new Uri("resm:Avalonia.Themes.Default.Accents.BaseLight.xaml?assembly=Avalonia.Themes.Default"), },
                new WhiteText(),
                new WhiteBorder(),
                new Rounded(),
                new Padded(),
                new MediumPadding(),
                new SmallMargin(),
                new PurpleBackgroundRedHighlight(),
                new NoBorder(),
            });

            Resources.Add("Trash-Icon", ReadEmbeddedRessourceImage("trash-347.png"));
            Resources.Add("Planner-Icon", ReadEmbeddedRessourceImage("planner.png"));
        }

        public IApplication Application => (IApplication)DataContext;

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.Startup += (sender, e) => Application.Startup();
                desktop.Exit += (sender, e) => Application.Shutdown();
                desktop.MainWindow = new AppMainWindow(Application);               
            }
        }

        public static Bitmap ReadEmbeddedRessourceImage(string searchPattern)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            var resourceName = assembly.GetManifestResourceNames().FirstOrDefault(x => x.Contains(searchPattern));
            
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null) return Bitmap.DecodeToWidth(stream, 32);
            }

            return null;
        }
    }      
}
