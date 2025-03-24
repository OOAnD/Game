using App;

namespace SpaceBattle
{
    public class Game : ICommand
    {
        private readonly IQueue<ICommand> _commandQueue;
        private readonly GameState _gameState;
        private const int TimeQuantum = 100;

        public Game(IQueue<ICommand> commandQueue)
        {
            _commandQueue = commandQueue;
            _gameState = new GameState();
        }

        public void Execute()
        {
            var timeLimitCommand = Ioc.Resolve<ICommand>("Commands.TimeLimit", _gameState, TimeQuantum);

            while (_gameState.IsRunning())
            {
                timeLimitCommand.Execute();
                var command = _commandQueue.Take();
                try
                {
                    command.Execute();
                }
                catch (Exception ex)
                {
                    Ioc.Resolve<ICommand>("ErrorHandler", ex).Execute();
                }
            }
        }
    }
}
