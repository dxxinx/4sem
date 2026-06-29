using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace FlowerShop.Converters
{
    public class FileImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string relativePath)
            {
                // Путь к exe
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;

                // Полный путь
                string fullPath = Path.Combine(baseDir, relativePath);

                if (File.Exists(fullPath))
                {
                    return new BitmapImage(new Uri(fullPath, UriKind.Absolute));
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
