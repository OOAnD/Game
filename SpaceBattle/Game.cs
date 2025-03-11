using App;

namespace SpaceBattle
{
    public class Game : ICommand
    {
        private readonly object _gameScope;

        public Game(object gameScope)
        {
            var oldScope = Ioc.Resolve<object>("IoC.Scope.Current");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", gameScope).Execute();
            var regNextCommand = new RegisterIoCDependencyNextCommand();
            regNextCommand.Execute();
            var regShouldLoopRun = new RegisterIoCDependencyShouldLoopRun();
            regShouldLoopRun.Execute();

            _gameScope = gameScope;
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", oldScope).Execute();
        }

        public void Execute()
        {
            var oldScope = Ioc.Resolve<object>("IoC.Scope.Current");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", _gameScope).Execute();

            while (Ioc.Resolve<bool>("Game.ShouldLoopRun"))
            {
                var cmd = Ioc.Resolve<ICommand>("Commands.GetNextCommand");
                try
                {
                    cmd.Execute();
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Exception: {exception.Message}");
                }
            }

            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", oldScope).Execute();
        }
    }
}
