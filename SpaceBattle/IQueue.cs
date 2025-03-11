using App;

namespace SpaceBattle
{
    public interface IQueue
    {
        void Add(ICommand command);
        ICommand Take();
    }
}
