namespace FlowerShop.Services
{
    public interface IAction
    {
        void Do();
        void Undo();
    }
}
