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
                (object[] args) =>
                {
                    var rotating = Ioc.Resolve<IRotating>("Adapters.Rotating", args[0]);

                    return new RotateCommand(rotating);
                }
            ).Execute();
        }
    }
}
