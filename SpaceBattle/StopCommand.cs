namespace SpaceBattle
{
    public class StopCommand : ICommand
    {

        private readonly ICommandBox _cancellableCommand;
        private readonly IEmptyCommandFactory _emptyCommandFactory;

        public StopCommand(ICommandBox cancellableCommand, IEmptyCommandFactory emptyCommandFactory)
        {
            _cancellableCommand = cancellableCommand ?? throw new ArgumentNullException(nameof(cancellableCommand));
            _emptyCommandFactory = emptyCommandFactory ?? throw new ArgumentNullException(nameof(emptyCommandFactory));
        }

        public void Execute()
        {
            var emptyCommand = _emptyCommandFactory.CreateEmptyCommand() ?? throw new InvalidOperationException();
            _cancellableCommand.Set(emptyCommand);
        }
    }
}
