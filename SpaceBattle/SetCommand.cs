using App;

namespace SpaceBattle
{
    public class SetCommand(string objectId, string commandName, ICommand command) : ICommand
    {
        public void Execute()
        {
            var objects = Ioc.Resolve<IDictionary<string, IDictionary<string, object>>>("Game.Objects");
            objects[objectId].Add(commandName, command);
        }
    }
}
