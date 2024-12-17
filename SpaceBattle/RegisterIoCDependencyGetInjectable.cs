using App;

namespace SpaceBattle
{
    public class RegisterIoCDependencyGetInjectable : ICommand
    {
        public void Execute()
        {
            Ioc.Resolve<App.ICommand>(
                "IoC.Register",
                "Game.Object.GetInjectable",
                (object[] args) =>
                {
                    var objectId = (string)args[0];
                    var commandName = (string)args[1];

                    var objects = Ioc.Resolve<IDictionary<string, IDictionary<string, object>>>("Game.Objects");
                    return (ICommandInjectable)objects[objectId][commandName];
                }
            ).Execute();
        }
    }
}
