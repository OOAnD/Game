using App;

namespace SpaceBattle
{
    public class RegisterDependencyCommandInjectableCommand : ICommand
    {
        public void Execute()
        {
            Ioc.Resolve<App.ICommand>(
                "IoC.Register",
                "Commands.CommadInjectable",
                (object[] args) => new CommandInjectableCommand()
            ).Execute();
        }
    }
}
