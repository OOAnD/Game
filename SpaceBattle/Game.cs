using App;

namespace SpaceBattle
{
    public class Game : ICommand
    {
        private readonly GameState _gameState;
        private const int TimeQuantum = 100;

        public Game(GameState gameState)
        {
            _gameState = gameState;
        }

        public void Execute()
        {
            var timeLimitCommand = Ioc.Resolve<ICommand>("Commands.TimeLimit", _gameState, TimeQuantum);
            var nextCommand = Ioc.Resolve<Func<ICommand>>("Game.Scheduler.Next");

            while (_gameState.IsRunning())
            {
                timeLimitCommand.Execute();
                try
                {
                    var command = nextCommand();
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
