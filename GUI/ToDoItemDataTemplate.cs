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
using GUI.Styles;
using System;
using System.Globalization;
using ToDo;

namespace GUI
{
    internal sealed class ToDoItemDataTemplate : IDataTemplate
    {
        private readonly IApplication application;

        public ToDoItemDataTemplate(IApplication application)
        {
            this.application = application;
        }

        public bool Match(object data) => data is not null && data is IToDo;

        public IControl Build(object param) 
        {
            IToDo todo = param as IToDo;

            CheckBox checkBox = new CheckBox()
            {
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

            checkBox.Classes.AddRange(new string[] { WhiteBorder.Name, Rounded.Name });

            TextBlock displayText = new TextBlock()
            {
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

            displayText.Classes.Add(WhiteText.Name);

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
                MaxHeight = 22,
                MaxWidth = 22,
                Padding = new Thickness(0.0),
                Margin = new Thickness(0.0),
                BorderBrush = new SolidColorBrush(Colors.White),
                Content = new Image() 
                {
                    Margin = new Thickness(0.0),
                    Source = (IImage)(Avalonia.Application.Current.Resources.TryGetValue("Trash-Icon", out object trash) ? trash : null),
                },
                Cursor = new Cursor(StandardCursorType.Hand),
                Name = "Delete",
            };

            deleteButton.Classes.Add(Red.Name);

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
                Child = expander,
                Name = "Border"
            };

            border.Classes.AddRange(new string[] { PurpleBackgroundRedHighlight.Name, Rounded.Name, SmallMargin.Name });

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
    }
}
