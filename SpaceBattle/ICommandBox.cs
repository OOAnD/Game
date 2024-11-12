namespace SpaceBattle
{
    public interface ICommandBox : ICommand
    {
        void Set(ICommand command);
    }
}