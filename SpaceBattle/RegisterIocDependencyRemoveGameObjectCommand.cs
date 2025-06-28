using App;

namespace SpaceBattle
{
    public class RegisterIocDependencyRemoveGameObjectCommand : ICommand
    {
        public void Execute()
        {
            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Game.Objects.Remove",
                (object[] args) => new RemoveGameObjectCommand((IDictionary<string, object>)args[0])
            ).Execute();
        }
    }
}
