using App;

namespace SpaceBattle
{
    public class RemoveGameObjectCommand(IDictionary<string, object> gameObject) : ICommand
    {
        public void Execute()
        {
            var objects = Ioc.Resolve<IDictionary<string, IDictionary<string, object>>>("Game.Objects.GetAll");
            objects.Remove((string)gameObject["id"]);
        }
    }
}
