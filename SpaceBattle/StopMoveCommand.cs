namespace SpaceBattle
{
    public class StopMoveCommand : ICommand
    {
        private readonly IDictionary<string, object> _gameObject;

        public StopMoveCommand(IDictionary<string, object> gameObject)
        {
            _gameObject = gameObject ?? throw new ArgumentNullException(nameof(gameObject));
        }

        public void Execute()
        {
            var cancelableCommand = (ICommandBox)_gameObject["Movement"];
            cancelableCommand.Set(new EmptyCommand());
        }
    }
}
