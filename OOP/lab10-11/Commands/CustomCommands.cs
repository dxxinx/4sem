using System.Windows.Input;

namespace FlowerShop.Commands
{
    public static class CustomCommands
    {
        public static readonly RoutedUICommand ResetPrice =
            new RoutedUICommand(
                "Reset Price",
                "ResetPrice",
                typeof(CustomCommands));
    }
}
