using App;
using App.Scopes;

namespace SpaceBattle
{
    public class Game : ICommand
    {
        private readonly IQueue<ICommand> _commandQueue;
        private bool _isRunning;

        public Game(IQueue<ICommand> commandQueue)
        {
            _commandQueue = commandQueue;
            _isRunning = true;
        }

        public void Execute()
        {
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            while (_isRunning)
            {
                var command = _commandQueue.Take();
                try
                {
                    command.Execute();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Command execution failed: {ex.Message}");
                }
            }
        }
        public void Stop()
        {
            _isRunning = false;
        }
    }
}
