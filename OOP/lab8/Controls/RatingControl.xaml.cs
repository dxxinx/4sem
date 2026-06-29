using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FlowerShop.Controls
{
    public partial class RatingControl : UserControl
    {
        public RatingControl()
        {
            InitializeComponent();
            Loaded += RatingControl_Loaded;
        }

        private void RatingControl_Loaded(object sender, RoutedEventArgs e)
        {
            RenderStars();
        }

        public static readonly DependencyProperty RatingProperty =
            DependencyProperty.Register(
                nameof(Rating),
                typeof(double),
                typeof(RatingControl),
                new FrameworkPropertyMetadata(
                    0d,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnRatingChanged,
                    CoerceRating),
                ValidateRating);

        public double Rating
        {
            get => (double)GetValue(RatingProperty);
            set => SetValue(RatingProperty, value);
        }

        public static readonly DependencyProperty MaxRatingProperty =
            DependencyProperty.Register(
                nameof(MaxRating),
                typeof(int),
                typeof(RatingControl),
                new FrameworkPropertyMetadata(5, OnMaxRatingChanged, CoerceMaxRating),
                ValidateMaxRating);

        public int MaxRating
        {
            get => (int)GetValue(MaxRatingProperty);
            set => SetValue(MaxRatingProperty, value);
        }

        public static readonly DependencyProperty StarSizeProperty =
            DependencyProperty.Register(
                nameof(StarSize),
                typeof(double),
                typeof(RatingControl),
                new FrameworkPropertyMetadata(20d, OnAppearanceChanged, CoerceStarSize),
                ValidateStarSize);

        public double StarSize
        {
            get => (double)GetValue(StarSizeProperty);
            set => SetValue(StarSizeProperty, value);
        }

        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register(
                nameof(IsReadOnly),
                typeof(bool),
                typeof(RatingControl),
                new FrameworkPropertyMetadata(false, OnAppearanceChanged));

        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        private static bool ValidateRating(object value)
        {
            double rating = (double)value;
            return rating >= 0 && rating <= 10;
        }

        private static object CoerceRating(DependencyObject d, object baseValue)
        {
            RatingControl control = (RatingControl)d;
            double rating = (double)baseValue;

            if (rating < 0)
                return 0d;

            if (rating > control.MaxRating)
                return (double)control.MaxRating;

            return rating;
        }

        private static bool ValidateMaxRating(object value)
        {
            int maxRating = (int)value;
            return maxRating >= 1 && maxRating <= 10;
        }

        private static object CoerceMaxRating(DependencyObject d, object baseValue)
        {
            int maxRating = (int)baseValue;
            if (maxRating < 1)
                return 1;
            if (maxRating > 10)
                return 10;
            return maxRating;
        }

        private static bool ValidateStarSize(object value)
        {
            double starSize = (double)value;
            return starSize >= 10 && starSize <= 40;
        }

        private static object CoerceStarSize(DependencyObject d, object baseValue)
        {
            double starSize = (double)baseValue;
            if (starSize < 10)
                return 10d;
            if (starSize > 40)
                return 40d;
            return starSize;
        }

        private static void OnRatingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((RatingControl)d).RenderStars();
        }

        private static void OnMaxRatingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RatingControl control = (RatingControl)d;
            control.CoerceValue(RatingProperty);
            control.RenderStars();
        }

        private static void OnAppearanceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((RatingControl)d).RenderStars();
        }

        private void RenderStars()
        {
            if (PART_Stars == null)
                return;

            PART_Stars.Items.Clear();
            int filledStars = (int)Math.Round(Rating, MidpointRounding.AwayFromZero);

            for (int i = 0; i < MaxRating; i++)
            {
                int index = i;
                TextBlock star = new TextBlock
                {
                    Text = index < filledStars ? "\u2605" : "\u2606",
                    FontSize = StarSize,
                    Margin = new Thickness(2),
                    Foreground = Brushes.Gold,
                    Cursor = IsReadOnly ? Cursors.Arrow : Cursors.Hand
                };

                star.MouseLeftButtonDown += (s, e) =>
                {
                    if (IsReadOnly)
                        return;

                    Rating = index + 1;
                    RaiseEvent(new RoutedEventArgs(RatingClickedEvent, this));
                };

                PART_Stars.Items.Add(star);
            }
        }

        public static readonly RoutedEvent RatingClickedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(RatingClicked),
                RoutingStrategy.Direct,
                typeof(RoutedEventHandler),
                typeof(RatingControl));

        public event RoutedEventHandler RatingClicked
        {
            add => AddHandler(RatingClickedEvent, value);
            remove => RemoveHandler(RatingClickedEvent, value);
        }
    }
}
