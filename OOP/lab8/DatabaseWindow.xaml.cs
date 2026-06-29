using System.Windows;
using System.Windows.Controls;
using FlowerShop.ViewModels;

namespace FlowerShop
{
    public partial class DatabaseWindow : Window
    {
        public DatabaseWindow()
        {
            InitializeComponent();
        }

        private async void AdminGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction != DataGridEditAction.Commit)
                return;

            await Dispatcher.InvokeAsync(() => { }, System.Windows.Threading.DispatcherPriority.Background);

            if (DataContext is MainViewModel viewModel)
                await viewModel.CommitDatabaseChangesAsync();
        }
    }
}
