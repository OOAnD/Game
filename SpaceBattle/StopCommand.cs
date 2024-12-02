namespace SpaceBattle
{
    public class StopCommand : ICommand
    {
        private readonly ICommandBox _cancellableCommand;

        public StopCommand(ICommandBox cancellableCommand)
        {
            _cancellableCommand = cancellableCommand ?? throw new ArgumentNullException(nameof(cancellableCommand));
        }

        public void Execute()
        {
            var emptyCommand = new EmptyCommand();
            _cancellableCommand.Set(emptyCommand);
        }
    }
}
