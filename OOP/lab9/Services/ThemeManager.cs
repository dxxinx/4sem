using System;
using System.Linq;
using System.Windows;

namespace FlowerShop.Services
{
    public static class ThemeManager
    {
        public static void ApplyTheme(string themeName)
        {
            var appResources = Application.Current.Resources.MergedDictionaries;

            var oldTheme = appResources
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Theme."));

            if (oldTheme != null)
                appResources.Remove(oldTheme);

            if (themeName == "Default")
            {
                var purple = new ResourceDictionary
                {
                    Source = new Uri("/Resources/Theme.Purple.xaml", UriKind.Relative)
                };

                appResources.Insert(0, purple);
                return;
            }
            var newTheme = new ResourceDictionary
            {
                Source = new Uri($"/Resources/Theme.{themeName}.xaml", UriKind.Relative)
            };

            appResources.Insert(0, newTheme);
        }

    }
}
