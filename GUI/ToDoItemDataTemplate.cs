using Application;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using System;
using System.Globalization;
using ToDo;

namespace GUI
{
    internal sealed class ToDoItemDataTemplate : IDataTemplate
    {
        private readonly IApplication application;
        private readonly Theme theme;

        public ToDoItemDataTemplate(IApplication application, Theme theme)
        {
            this.application = application;
            this.theme = theme;
        }

        public bool Match(object data) => data is not null && data is IToDo;

        public IControl Build(object param) 
        {
            IToDo todo = param as IToDo;

            CheckBox checkBox = new CheckBox()
            {
                BorderBrush = theme.TextColor,
                BorderThickness = new Thickness(2.0),
                CornerRadius = new CornerRadius(5.0),
                Foreground = theme.TextColor,
                IsChecked = todo.Completed,
                Margin = new Thickness(2.0),
                Name = "Completed",
                [!ToggleButton.IsCheckedProperty] = new Binding()
                {
                    Source = todo,
                    Mode = BindingMode.TwoWay,
                    Path = nameof(IToDo.Completed),
                }
            };

            TextBlock displayText = new TextBlock()
            {
                Foreground = theme.TextColor,
                Name = "Display Text",
                VerticalAlignment = VerticalAlignment.Center,
                Text = todo.Summary.ToString(),
                [!TextBlock.TextDecorationsProperty] = new Binding()
                { 
                    Source = todo,
                    Mode = BindingMode.OneWay,
                    Path = nameof(IToDo.Completed),
                    Converter = new FuncValueConverter<bool, TextDecorationCollection>(v => v ? TextDecorations.Strikethrough : new TextDecorationCollection())
                }
            };

            displayText.DoubleTapped += (sender, e) =>
            {
                if (sender is TextBlock txtblk)
                {
                    txtblk.IsVisible = false;
                }

                e.Handled = true;
            };

            TextBox editableText = new TextBox()
            {
                AcceptsReturn = false,
                Name = "Editable Text",
                Text = (string)todo.Summary,
                [!Visual.IsVisibleProperty] = new Binding()
                {
                    Source = displayText,
                    Mode = BindingMode.OneWay,
                    Path = nameof(TextBlock.IsVisible),
                    Converter = new FuncValueConverter<bool, bool>(v => !v)
                },
            };

            editableText.KeyUp += (sender, e) =>
            {
                if (sender is TextBox txtbx)
                {
                    if (e.Key == Key.Escape || e.Key == Key.Enter)
                    {
                        txtbx.Parent.Focus();
                    }
                    else if (e.Key == Key.Space)
                    {
                        txtbx.Text = txtbx.Text.Insert(txtbx.CaretIndex, " ");
                        txtbx.CaretIndex++;
                    }
                }

                e.Handled = true;
            };
            
            editableText.LostFocus += (sender, e) =>
            {
                displayText.IsVisible = true;
                todo.Summary = editableText.Text;
                e.Handled = true;
            };

            Button deleteButton = new Button()
            {
                BorderThickness = new Thickness(0.0),
                Background = theme.ToDoItemHighlightColor,
                MaxHeight = 22,
                MaxWidth = 22,
                Padding = new Thickness(0.0),
                Content = new Image() 
                {
                    Margin = new Thickness(0.0),
                    Source = (IImage)(Avalonia.Application.Current.Resources.TryGetValue("Trash-Icon", out object trash) ? trash : null),
                },
                Cursor = new Cursor(StandardCursorType.Hand),
                Name = "Delete",
            };

            deleteButton.Click += (sender, e) =>
            {
                application.RemoveToDo(todo);
                e.Handled = true;
            };

            Grid headerGrid = new Grid()
            {
                ColumnDefinitions = new ColumnDefinitions("5*,90*,5*"),
                Name = "Header Grid",
                RowDefinitions = new RowDefinitions("100*")
            };

            headerGrid.Add(checkBox, 0, 0);
            headerGrid.Add(displayText, 1, 0);
            headerGrid.Add(editableText, 1, 0);
            headerGrid.Add(deleteButton, 2, 0);

            TextBox detailsBox = new TextBox()
            {
                CornerRadius = new CornerRadius(5.0),
                Name = "Details",
                Text = todo.Details.ToString(),
            };

            detailsBox.LostFocus += (sender, e) =>
            {
                todo.Details = detailsBox.Text;
                e.Handled = true;
            };

            Expander expander = new Expander()
            {
                BorderThickness = new Thickness(0.0),
                Content = detailsBox,
                CornerRadius = new CornerRadius(5.0),
                ExpandDirection = ExpandDirection.Down,
                Header = headerGrid,
                Name = "Expander",
            };

            Border border = new Border()
            {
                Background = theme.ToDoItemColor,
                BorderBrush = theme.ToDoItemColor,
                BorderThickness = new Thickness(2.0),
                Child = expander,
                CornerRadius = new CornerRadius(5.0),
                Margin = new Thickness(4.0),
                Name = "Border"
            };

            deleteButton.Bind(Button.IsVisibleProperty, new Binding()
            {
                Source = border,
                Mode = BindingMode.OneWay,
                Path = nameof(Border.IsPointerOver)
            });

            todo.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(IToDo.Summary))
                {
                    displayText.Text = todo.Summary.ToString();
                }
            };

            border.AddHandler(InputElement.PointerEnterEvent, HandleMouseover);
            border.AddHandler(InputElement.PointerLeaveEvent, HandleMouseover);

            return border;
        }

        private sealed class SummaryConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return value.ToString();
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return new Summary(value.ToString());
            }
        }

        private sealed class DetailsConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return value.ToString();
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return new Details(value.ToString());
            }
        }

        private void HandleMouseover(object sender, PointerEventArgs e)
        {
            if (e.Source is Border border)
            {
                if (border.IsPointerOver)
                {
                    border.Background = theme.ToDoItemHighlightColor;
                    border.BorderBrush = theme.ToDoItemHighlightColor;
                }
                else
                {
                    border.Background = theme.ToDoItemColor;
                    border.BorderBrush = theme.ToDoItemColor;
                }

                e.Handled = true;
            }
            else
            {
                Console.WriteLine(e.Source.ToString());
                e.Handled = true;
            }
        }
    }
}
