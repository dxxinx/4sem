using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace FlowerShop.Controls
{
    public partial class QuantityStepper : UserControl
    {
        private bool _isSyncing;

        public QuantityStepper()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty QuantityProperty =
            DependencyProperty.Register(
                nameof(Quantity),
                typeof(int),
                typeof(QuantityStepper),
                new FrameworkPropertyMetadata(
                    0,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnQuantityChanged,
                    CoerceQuantity),
                ValidateQuantity);

        public int Quantity
        {
            get => (int)GetValue(QuantityProperty);
            set => SetValue(QuantityProperty, value);
        }

        public static readonly DependencyProperty StepProperty =
            DependencyProperty.Register(
                nameof(Step),
                typeof(int),
                typeof(QuantityStepper),
                new FrameworkPropertyMetadata(1, null, CoerceStep),
                ValidateStep);

        public int Step
        {
            get => (int)GetValue(StepProperty);
            set => SetValue(StepProperty, value);
        }

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(
                nameof(Maximum),
                typeof(int),
                typeof(QuantityStepper),
                new FrameworkPropertyMetadata(1000, OnMaximumChanged, CoerceMaximum),
                ValidateMaximum);

        public int Maximum
        {
            get => (int)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        public static readonly DependencyProperty DisplayQuantityProperty =
            DependencyProperty.Register(
                nameof(DisplayQuantity),
                typeof(string),
                typeof(QuantityStepper),
                new FrameworkPropertyMetadata("0", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDisplayQuantityChanged));

        public string DisplayQuantity
        {
            get => (string)GetValue(DisplayQuantityProperty);
            set => SetValue(DisplayQuantityProperty, value);
        }

        public static readonly RoutedEvent PreviewQuantityChangedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(PreviewQuantityChanged),
                RoutingStrategy.Tunnel,
                typeof(RoutedEventHandler),
                typeof(QuantityStepper));

        public event RoutedEventHandler PreviewQuantityChanged
        {
            add => AddHandler(PreviewQuantityChangedEvent, value);
            remove => RemoveHandler(PreviewQuantityChangedEvent, value);
        }

        public static readonly RoutedEvent QuantityChangedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(QuantityChanged),
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(QuantityStepper));

        public event RoutedEventHandler QuantityChanged
        {
            add => AddHandler(QuantityChangedEvent, value);
            remove => RemoveHandler(QuantityChangedEvent, value);
        }

        private static bool ValidateQuantity(object value)
        {
            int quantity = (int)value;
            return quantity >= 0 && quantity <= 1000;
        }

        private static object CoerceQuantity(DependencyObject d, object baseValue)
        {
            var stepper = (QuantityStepper)d;
            int quantity = (int)baseValue;

            if (quantity < 0)
                return 0;

            if (quantity > stepper.Maximum)
                return stepper.Maximum;

            return quantity;
        }

        private static bool ValidateStep(object value)
        {
            int step = (int)value;
            return step >= 1 && step <= 20;
        }

        private static object CoerceStep(DependencyObject d, object baseValue)
        {
            int step = (int)baseValue;
            if (step < 1)
                return 1;
            if (step > 20)
                return 20;
            return step;
        }

        private static bool ValidateMaximum(object value)
        {
            int maximum = (int)value;
            return maximum >= 1 && maximum <= 1000;
        }

        private static object CoerceMaximum(DependencyObject d, object baseValue)
        {
            int maximum = (int)baseValue;
            if (maximum < 1)
                return 1;
            if (maximum > 1000)
                return 1000;
            return maximum;
        }

        private static void OnMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var stepper = (QuantityStepper)d;
            stepper.CoerceValue(QuantityProperty);
        }

        private static void OnQuantityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var stepper = (QuantityStepper)d;

            stepper._isSyncing = true;
            stepper.DisplayQuantity = stepper.Quantity.ToString(CultureInfo.CurrentCulture);
            stepper._isSyncing = false;

            stepper.RaiseEvent(new RoutedEventArgs(PreviewQuantityChangedEvent, stepper));
            stepper.RaiseEvent(new RoutedEventArgs(QuantityChangedEvent, stepper));
        }

        private static void OnDisplayQuantityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var stepper = (QuantityStepper)d;

            if (stepper._isSyncing)
                return;

            string text = (e.NewValue as string ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(text))
            {
                stepper.Quantity = 0;
                return;
            }

            if (int.TryParse(text, out int quantity))
            {
                stepper.Quantity = quantity;
            }
        }

        private void DecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            Quantity -= Step;
        }

        private void IncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            Quantity += Step;
        }

        private void PART_Text_LostFocus(object sender, RoutedEventArgs e)
        {
            _isSyncing = true;
            DisplayQuantity = Quantity.ToString(CultureInfo.CurrentCulture);
            _isSyncing = false;
        }
    }
}
