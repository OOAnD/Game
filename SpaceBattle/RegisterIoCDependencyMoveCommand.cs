using App;

namespace SpaceBattle
{
    public class RegisterIoCDependencyMoveCommand : ICommand
    {

        public void Execute()
        {
            Ioc.Resolve<App.ICommand>(
                "IoC.Register",
                "Commands.Move",
                (object[] args) => new MoveCommand((IMoving)args[0])
            ).Execute();
        }
    }
}
