namespace SpaceBattle
{
    public interface ICommandQueue
    {
        void Enqueue(ICommand command);
    }
}
