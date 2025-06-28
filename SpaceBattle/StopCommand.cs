using App;

namespace SpaceBattle
{
    public class StopCommand(string objectId, string commandName) : ICommand
    {
        public void Execute()
        {
            var injectable = Ioc.Resolve<ICommandInjectable>("Game.Object.GetInjectable", objectId, commandName);

            var emptyCommand = Ioc.Resolve<ICommand>("Commands.Empty");

            injectable.InjectCommand(emptyCommand);
        }
    }
}
