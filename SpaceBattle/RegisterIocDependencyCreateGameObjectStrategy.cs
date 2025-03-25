using App;

namespace SpaceBattle
{
    public class RegisterIocDependencyCreateGameObjectStrategy : ICommand
    {
        public void Execute()
        {
            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Game.Objects.Create",
                (object[] _) => CreateGameObjectStrategy.Create()
            ).Execute();
        }
    }
}
