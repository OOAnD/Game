using App;

namespace SpaceBattle
{
    public class ShootCommandIocRegisterCommand : ICommand
    {
        public void Execute()
        {
            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Commands.Shoot",
                (object[] args) => new ShootCommand(args[0])
            ).Execute();
        }
    }
}
