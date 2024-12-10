using App;

namespace SpaceBattle
{
    public class RegisterIoCDependencyActionsStart : ICommand
    {
        public void Execute()
        {

            Ioc.Resolve<App.ICommand>(
                "IoC.Register",
                "Actions.Start",
                (object[] args) => new StartCommand((string)args[0], (string)args[1], (object[])args[2])
            ).Execute();

            Ioc.Resolve<App.ICommand>(
                "IoC.Register",
                "Game.Object.Actions.Set",
                (object[] args) => new SetCommand((string)args[0], (string)args[1], (ICommand)args[2])
            ).Execute();
        }
    }
}
