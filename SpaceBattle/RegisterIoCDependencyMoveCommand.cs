using App;

namespace SpaceBattle
{
    public class RegisterIoCDependencyMoveCommand : ICommand
    {

        public void Execute()
        {
            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Commands.Move",
                (object[] args) =>
                {
                    var rotating = Ioc.Resolve<IMoving>("Adapters.Moving", args[0]);

                    return new MoveCommand(rotating);
                }
            ).Execute();
        }
    }
}
