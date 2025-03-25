using App;

namespace SpaceBattle
{
    public class RegisterIocDependencyFindGameObjectStrategy : ICommand
    {
        public void Execute()
        {
            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Game.Objects.Get",
                (object[] args) => new FindGameObjectStrategy((string)args[0]).Find()
            ).Execute();
        }
    }
}
