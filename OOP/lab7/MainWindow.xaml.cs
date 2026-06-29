using FlowerShop.Controls;
using FlowerShop.Services;
using FlowerShop.ViewModels;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace FlowerShop
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AddHandler(QuantityStepper.PreviewQuantityChangedEvent, new RoutedEventHandler(OnWindowPreviewQuantityChanged));
            AddHandler(QuantityStepper.QuantityChangedEvent, new RoutedEventHandler(OnWindowQuantityChanged));
            AddHandler(RatingControl.RatingClickedEvent, new RoutedEventHandler(OnWindowRatingClicked));
        }

        private void SwitchLanguage(string dictionaryPath)
        {
            var langDict = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Strings."));

            if (langDict != null)
            {
                var index = Application.Current.Resources.MergedDictionaries.IndexOf(langDict);
                Application.Current.Resources.MergedDictionaries.Remove(langDict);
                Application.Current.Resources.MergedDictionaries.Insert(index,
                    new ResourceDictionary { Source = new System.Uri(dictionaryPath, System.UriKind.Relative) });
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Add(
                    new ResourceDictionary { Source = new System.Uri(dictionaryPath, System.UriKind.Relative) });
            }
        }

        private void ResetPrice_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (DataContext is MainViewModel vm && vm.SelectedProduct != null)
            {
                vm.SelectedProduct.Price = 0;
            }
        }

        private void QuantityStepper_PreviewQuantityChanged(object sender, RoutedEventArgs e)
        {
            StatusBarTextBlock.Text = "Tunneling: PreviewQuantityChanged дошло до QuantityStepper.";
        }

        private void QuantityStepper_QuantityChanged(object sender, RoutedEventArgs e)
        {
            StatusBarTextBlock.Text = "Bubbling: QuantityChanged поднялось от QuantityStepper.";
        }

        private void RatingControl_RatingClicked(object sender, RoutedEventArgs e)
        {
            StatusBarTextBlock.Text = "Direct: RatingClicked обработано самим RatingControl.";
        }

        private void OnWindowPreviewQuantityChanged(object sender, RoutedEventArgs e)
        {
            StatusBarTextBlock.Text = "Tunneling: PreviewQuantityChanged прошло сверху вниз до окна.";
        }

        private void OnWindowQuantityChanged(object sender, RoutedEventArgs e)
        {
            StatusBarTextBlock.Text = "Bubbling: QuantityChanged поднялось снизу вверх до окна.";
        }

        private void OnWindowRatingClicked(object sender, RoutedEventArgs e)
        {
            StatusBarTextBlock.Text = "Direct: RatingClicked не маршрутизируется по дереву, событие остаётся на контроле.";
        }

        private void MenuItem_LangRu_Click(object sender, RoutedEventArgs e)
        {
            SwitchLanguage("Resources/Strings.ru.xaml");
        }

        private void MenuItem_LangEn_Click(object sender, RoutedEventArgs e)
        {
            SwitchLanguage("Resources/Strings.en.xaml");
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new ProfileWindow { Owner = this };
            win.ShowDialog();
        }

        private void ThemePurple_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("Default");
        }

        private void ThemePink_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("Pink");
        }
    }
}
