using Application;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using ToDo;

namespace GUI
{
    internal sealed class AppMainWindow : Window
    {
        private readonly ItemsControl toDoItems = new ItemsControl();

        public AppMainWindow(IApplication app)
        {
            DataContext = app;
            Title = app.GetApplicationName();
            Configure();
        }

        IApplication Application
        {
            get
            {
                return (IApplication)DataContext;
            }
        }

        private void Configure()
        {
            Theme theme = new Theme();

            StackPanel buttons = new StackPanel()
            {
                Background = theme.MenuBackgroundColor,
                Margin = new Thickness(0.0, 5.0)
            };

            buttons.Children.AddRange(new IControl[]
            {
                ButtonFactory.CreateButton(Application, theme, "Create To-Do Item", a => a.AddToDo()),
                ButtonFactory.CreateButton(Application, theme, "Remove Completed", a => a.RemoveCompletedToDos()),
                CheckboxFactory.CreateCheckbox(Application,
                                                    theme,
                                                    "Sort By Completed",
                                                    Application.IsSortingByCompleted(),
                                                    a => a.SortByCompleted(),
                                                    a => a.StopSortingByCompleted()),
                CheckboxFactory.CreateCheckbox(Application,
                                                    theme,
                                                    "Sort By Date Created",
                                                    Application.IsSortingByDateCreated(),
                                                    a => a.SortByDateCreated(),
                                                    a => a.StopSortingByDateCreated()),
                CheckboxFactory.CreateCheckbox(Application,
                                                    theme,
                                                    "Sort By Summary",
                                                    Application.IsSortingBySummary(),
                                                    a => a.SortBySummary(),
                                                    a => a.StopSortingBySummary())
            });

            toDoItems.Background = theme.ListBackgroundColor;
            ((AvaloniaList<object>)toDoItems.Items).AddRange(Application.GetToDos());
            toDoItems.DataTemplates.Add(new ToDoItemDataTemplate(Application, theme));
            toDoItems.Padding = new Thickness(5.0);
            toDoItems.CornerRadius = new CornerRadius(10.0, 0.0, 0.0, 10.0);

            Application.ToDosChanged += Application_ToDosChanged;

            Grid mainGrid = new Grid()
            {
                Background = theme.MenuBackgroundColor,
                Name = "Main Grid",
                ColumnDefinitions = new ColumnDefinitions("20*,80*"),
                RowDefinitions = new RowDefinitions("100*")
            };

            mainGrid.Add(buttons, 0, 0);
            mainGrid.Add(toDoItems, 1, 0);

            Content = mainGrid;
        }

        private void Application_ToDosChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ((AvaloniaList<object>)toDoItems.Items).Clear();
            ((AvaloniaList<object>)toDoItems.Items).AddRange(Application.GetToDos());
        }
    }
}
