using App;

namespace SpaceBattle
{
    public class RegisterIoCDependencyRotateCommand : ICommand
    {
        public void Execute()
        {
            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Commands.Rotate",
                (object[] args) => new RotateCommand((IRotating)args[0])
            ).Execute();
        }
    }
}
