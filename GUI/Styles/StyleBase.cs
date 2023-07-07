using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Styling;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GUI.Styles
{
    internal abstract class StyleBase : IStyle
    {
        protected static readonly IBrush Navy = SolidColorBrush.Parse("#16213E");
        protected static readonly IBrush Blue = SolidColorBrush.Parse("#0F3460");
        protected static readonly IBrush Purple = SolidColorBrush.Parse("#533483");
        protected static readonly IBrush Red = SolidColorBrush.Parse("#E94560");
        protected static readonly IBrush White = SolidColorBrush.Parse("#EEEEEE");
        protected static readonly Thickness Thickness = new Thickness(2.0);
        protected static readonly Thickness Margin = new Thickness(2.0);
        protected static readonly Thickness Padding = new Thickness(2.0);
        protected static readonly CornerRadius CornerRadius = new CornerRadius(5.0);

        public abstract string Class();

        public IReadOnlyList<IStyle> Children => Array.Empty<IStyle>();

        public SelectorMatchResult TryAttach(IStyleable target, IStyleHost host)
        {
            if (target.Classes.Contains(Class()))
            {
                try
                {
                    Attach(target, host);
                    return SelectorMatchResult.AlwaysThisInstance;
                }
                catch (Exception)
                {
                }
            }

            return SelectorMatchResult.NeverThisInstance;
        }

        protected abstract void Attach(IStyleable target, IStyleHost host);
    }

    internal sealed class WhiteText : StyleBase
    {
        public static string Name => nameof(WhiteText);

        public override string Class() => Name;

        protected override void Attach(IStyleable target, IStyleHost host)
        {
            target.SetValue(TemplatedControl.ForegroundProperty, White);
        }
    }

    internal sealed class PurpleBackgroundRedHighlight : StyleBase
    {
        public static string Name => nameof(PurpleBackgroundRedHighlight);

        public override string Class() => Name;

        protected override void Attach(IStyleable target, IStyleHost host)
        {
            target.Bind(TemplatedControl.BackgroundProperty, new Binding()
            {
                Source = target,
                Path = nameof(InputElement.IsPointerOver),
                Mode = BindingMode.OneWay,
                Converter = new FuncValueConverter<bool, IBrush>(v => v ? Red : Purple)
            });
        }
    }

    internal sealed class Red : StyleBase
    {
        public static string Name => nameof(Red);

        public override string Class() => Name;

        protected override void Attach(IStyleable target, IStyleHost host)
        {
            target.SetValue(TemplatedControl.BackgroundProperty, Red);
        }
    }

    internal sealed class WhiteBorder : StyleBase
    {
        public static string Name => nameof(WhiteBorder);

        public override string Class() => Name;

        protected override void Attach(IStyleable target, IStyleHost host)
        {
            target.SetValue(TemplatedControl.BorderBrushProperty, White);
            target.SetValue(TemplatedControl.BorderThicknessProperty, Thickness);
        }
    }

    internal sealed class Rounded : StyleBase
    {
        public static string Name => nameof(Rounded);

        public override string Class() => Name;

        protected override void Attach(IStyleable target, IStyleHost host)
        {
            target.SetValue(TemplatedControl.CornerRadiusProperty, CornerRadius);
        }
    }

    internal sealed class Padded : StyleBase
    {
        public static string Name => nameof(Padded);

        public override string Class() => Name;

        protected override void Attach(IStyleable target, IStyleHost host)
        {
            target.SetValue(TemplatedControl.PaddingProperty, Padding);
        }
    }

    internal sealed class SmallMargin : StyleBase
    {
        public static string Name => nameof(SmallMargin);

        public override string Class() => Name;

        protected override void Attach(IStyleable target, IStyleHost host)
        {
            target.SetValue(Layoutable.MarginProperty, Margin);
        }
    }

    internal sealed class MediumPadding : StyleBase
    {
        public static string Name => nameof(MediumPadding);

        public override string Class() => Name;

        protected override void Attach(IStyleable target, IStyleHost host)
        {
            target.SetValue(TemplatedControl.PaddingProperty, Padding * 2);
        }
    }

    internal sealed class NoBorder : StyleBase
    {
        public static string Name => nameof(NoBorder);

        public override string Class() => Name;

        protected override void Attach(IStyleable target, IStyleHost host)
        {
            target.SetValue(TemplatedControl.BorderThicknessProperty, new Thickness(0.0));
        }
    }
}
