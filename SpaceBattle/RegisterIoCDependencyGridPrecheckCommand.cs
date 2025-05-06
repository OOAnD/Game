using App;

namespace SpaceBattle
{
    public class RegisterIoCDependencyGridPrecheckCommand
    {
        public static void Execute()
        {
            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Adapters.IMovingObject",
                (object[] args) => Ioc.Resolve<IMoving>("Adapters.Moving", args[0])
            ).Execute();

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Commands.GridPrecheck",
                (object[] args) => new GridPrecheckCommand(Ioc.Resolve<IMoving>("Adapters.Moving", args[0]))
            ).Execute();
        }
    }
}
