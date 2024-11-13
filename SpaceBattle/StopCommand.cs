namespace SpaceBattle
{
    public class StopCommand : ICommand
    {

        private readonly ICommandBox _cancellableCommand;
        private readonly IEmptyCommandFactory _emptyCommandFactory;

        public StopCommand(ICommandBox cancellableCommand, IEmptyCommandFactory emptyCommandFactory)
        {
            _cancellableCommand = cancellableCommand ?? throw new ArgumentNullException(nameof(cancellableCommand));
            _emptyCommandFactory = emptyCommandFactory;
        }

        public void Execute()
        {
            var emptyCommand = _emptyCommandFactory.CreateEmptyCommand();
            _cancellableCommand.Set(emptyCommand);
        }
    }
}
