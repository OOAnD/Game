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
                    var moving = Ioc.Resolve<IMoving>("Adapters.Moving", args[0]);

                    return new MoveCommand(moving);
                }
            ).Execute();
        }
    }
}
