namespace SpaceBattle
{
    public interface IUtilCommandFactory
    {
        ICommand CreateQueueSetter(ICommand command);
        ICommandBox CreateCommandBox();
        IMacroCommand CreateMacroCommand();
    }
}