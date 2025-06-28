using App;

namespace SpaceBattle
{
    public class FindGameObjectStrategy(string objectId)
    {
        public object Find()
        {
            var objects = Ioc.Resolve<IDictionary<string, object>>("Game.Object.GetAll");
            return objects[objectId];
        }
    }
}
