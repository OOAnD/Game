using App;

namespace SpaceBattle
{
    public class RegisterIoCDependencySetCommand : ICommand
    {
        public void Execute()
        {
            Ioc.Resolve<App.ICommand>(
                "IoC.Register",
                "Game.Object.Actions.Set",
                (object[] args) => new SetCommand((string)args[0], (string)args[1], (ICommand)args[2])
            ).Execute();
        }
    }
}
