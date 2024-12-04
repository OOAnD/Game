namespace SpaceBattle
{
    public class StartMoveCommand(
        ICommandQueue queue,
        IMoving movingObject,
        IDictionary<string, object> gameObject,
        Func<string, object[], object> provider
    ) : ICommand
    {
        private readonly IMoving _movingObject = movingObject
            ?? throw new ArgumentNullException(nameof(movingObject));
        private readonly Func<string, object[], object> _provider = provider
            ?? throw new ArgumentNullException(nameof(provider));
        private readonly ICommandQueue _queue = queue
            ?? throw new ArgumentNullException(nameof(queue));
        private readonly IDictionary<string, object> _gameObject = gameObject
            ?? throw new ArgumentNullException(nameof(gameObject));

        public void Execute()
        {
            var moveCommand = _provider("Commands.Move", [_movingObject]);
            var boxCommand = (ICommandBox)_provider("Commands.Box", []);
            var repeatCommand = _provider("Commands.Repeat", [_queue, boxCommand]);
            var macroCommand = (ICommand)_provider("Commands.Macro", [moveCommand, repeatCommand]);

            boxCommand.Set(macroCommand);

            _queue.Enqueue(boxCommand);

            _gameObject.Add("Movement", boxCommand);
        }
    }
}
