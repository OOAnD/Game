namespace SpaceBattle
{
    public class CreateGameObjectStrategy
    {
        public static object Create()
        {
            var newObject = new Dictionary<string, object>();
            var id = Guid.NewGuid().ToString();
            newObject["id"] = id;

            return newObject;
        }
    }
}
