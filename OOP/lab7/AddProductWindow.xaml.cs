using FlowerShop.Models;
using System.Windows;

namespace FlowerShop
{
    public partial class AddProductWindow : Window
    {
        public Product product;

        public AddProductWindow()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            product = new Product
            {
                ShortName = NameBox.Text,
                Category = CategoryBox.Text,
                Price = double.Parse(PriceBox.Text),
                Quantity = int.Parse(QuantityBox.Text)
            };

            DialogResult = true;
        }
    }
}