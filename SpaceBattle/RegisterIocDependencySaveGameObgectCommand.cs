using App;

namespace SpaceBattle
{
    public class RegisterIocDependencySaveGameObjectCommand : ICommand
    {
        public void Execute()
        {
            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Game.Objects.Save",
                (object[] args) => new SaveGameObjectCommand((IDictionary<string, object>)args[0])
            ).Execute();
        }
    }
}
