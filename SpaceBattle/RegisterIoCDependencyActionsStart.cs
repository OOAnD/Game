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
        }
    }
}
