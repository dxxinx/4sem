using FlowerShop.Services;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FlowerShop
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
