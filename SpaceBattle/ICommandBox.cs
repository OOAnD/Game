namespace SpaceBattle
{
    public interface ICommandBox
    {
        void Set(ICommand command);
        ICommand GetCurrentCommand();
    }
}
