namespace SpaceBattle
{
    public class BridgeCommand : ICommandBox
    {
        private ICommand _innerCommand = new EmptyCommand();

        public void Execute()
        {
            _innerCommand.Execute();
        }

        public void Set(ICommand command)
        {
            _innerCommand = command
                ?? throw new ArgumentNullException(nameof(command));
        }
    }
}

