using App;

namespace SpaceBattle
{
    public class StartCommand(string commandName, string objectId, object[] commandArgs) : ICommand
    {
        public void Execute()
        {
            var macro = Ioc.Resolve<ICommand>("Macro." + commandName, commandArgs);
            var injectable = Ioc.Resolve<ICommandInjectable>("Commands.CommadInjectable");
            injectable.InjectCommand(macro);
            var queue = Ioc.Resolve<ICommandReciever>("Game.Queue");
            var sendCommand = Ioc.Resolve<ICommand>("Commands.Send", queue, injectable);
            sendCommand.Execute();
            var setCommand = Ioc.Resolve<ICommand>("Game.Object.Actions.Set", objectId, commandName, injectable);
            setCommand.Execute();
        }
    }
}
