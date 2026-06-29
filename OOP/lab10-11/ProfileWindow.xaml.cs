using System.Windows;

namespace FlowerShop
{
    public partial class ProfileWindow : Window
    {
        public ProfileWindow()
        {
            InitializeComponent();
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
