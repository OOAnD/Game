using App;

namespace SpaceBattle
{
    public class RegisterIoCDependencySendCommand : ICommand
    {
        public void Execute()
        {
            Ioc.Resolve<App.ICommand>(
                "IoC.Register",
                "Commands.Send",
                (object[] args) => new SendCommand((ICommandReciever)args[0], (ICommand)args[1])
            ).Execute();
        }
    }
}
