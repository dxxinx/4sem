using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace FlowerShop.Controls
{
    public partial class PriceBox : UserControl
    {
        private bool _isSyncingFromPrice;
        private bool _preserveUserText;

        public PriceBox()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty PriceProperty =
            DependencyProperty.Register(
                nameof(Price),
                typeof(decimal),
                typeof(PriceBox),
                new FrameworkPropertyMetadata(
                    0m,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnPriceChanged,
                    CoercePrice),
                ValidatePrice);

        public decimal Price
        {
            get => (decimal)GetValue(PriceProperty);
            set => SetValue(PriceProperty, value);
        }

        public static readonly DependencyProperty DisplayPriceProperty =
            DependencyProperty.Register(
                nameof(DisplayPrice),
                typeof(string),
                typeof(PriceBox),
                new FrameworkPropertyMetadata("0", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDisplayPriceChanged));

        public string DisplayPrice
        {
            get => (string)GetValue(DisplayPriceProperty);
            set => SetValue(DisplayPriceProperty, value);
        }

        private static bool ValidatePrice(object value)
        {
            decimal v = (decimal)value;
            return v >= 0 && v <= 10000;
        }

        private static object CoercePrice(DependencyObject d, object baseValue)
        {
            decimal v = (decimal)baseValue;
            if (v < 0) return 0m;
            if (v > 10000) return 10000m;
            return v;
        }

        public static readonly RoutedEvent PriceChangedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(PriceChanged),
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(PriceBox));

        public event RoutedEventHandler PriceChanged
        {
            add => AddHandler(PriceChangedEvent, value);
            remove => RemoveHandler(PriceChangedEvent, value);
        }

        public static readonly RoutedEvent PreviewPriceChangedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(PreviewPriceChanged),
                RoutingStrategy.Tunnel,
                typeof(RoutedEventHandler),
                typeof(PriceBox));

        public event RoutedEventHandler PreviewPriceChanged
        {
            add => AddHandler(PreviewPriceChangedEvent, value);
            remove => RemoveHandler(PreviewPriceChangedEvent, value);
        }

        private static void OnPriceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pb = (PriceBox)d;

            if (!(pb._preserveUserText && pb.PART_Text != null && pb.PART_Text.IsKeyboardFocused))
            {
                pb._isSyncingFromPrice = true;
                pb.DisplayPrice = pb.Price.ToString(CultureInfo.CurrentCulture);
                pb._isSyncingFromPrice = false;
            }

            pb.RaiseEvent(new RoutedEventArgs(PreviewPriceChangedEvent));
            pb.RaiseEvent(new RoutedEventArgs(PriceChangedEvent));
        }

        private static void OnDisplayPriceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pb = (PriceBox)d;

            if (pb._isSyncingFromPrice)
                return;

            string text = (e.NewValue as string ?? string.Empty).Trim();

            if (string.IsNullOrEmpty(text))
            {
                pb._preserveUserText = true;
                pb.SetCurrentValue(PriceProperty, 0m);
                return;
            }

            if (decimal.TryParse(text, NumberStyles.Number, CultureInfo.CurrentCulture, out decimal price))
            {
                pb._preserveUserText = false;
                pb.SetCurrentValue(PriceProperty, price);
            }
        }

        private void PART_Text_LostFocus(object sender, RoutedEventArgs e)
        {
            _preserveUserText = false;
            _isSyncingFromPrice = true;
            DisplayPrice = Price.ToString(CultureInfo.CurrentCulture);
            _isSyncingFromPrice = false;
        }
    }
}
