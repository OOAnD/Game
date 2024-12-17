using App;

namespace SpaceBattle
{
    public class RegisterRotateCommand : ICommand
    {
        public void Execute()
        {
            Ioc.Resolve<App.ICommand>(
                "IoC.Register",
                "Commands.Rotate",
                (object[] args) => new RotateCommand((IRotating)args[0])
            ).Execute();
        }
    }
}
