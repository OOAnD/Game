
using App;

namespace SpaceBattle
{
    public class ShootOperationIocRegisterCommand : ICommand
    {
        public void Execute()
        {
            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Operations.Shoot",
                (object[] args) =>
                {
                    var authCommand = Ioc.Resolve<ICommand>("Commands.ShootAuth", args[0], args[1]);
                    var shootCommand = Ioc.Resolve<ICommand>("Commands.Shoot", args[1]);
                    return new MacroCommand(authCommand, shootCommand);
                }
            ).Execute();
        }
    }
}
