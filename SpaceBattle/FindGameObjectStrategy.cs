namespace SpaceBattle
{
    public class FindGameObjectStrategy(IDictionary<string, object> gameObjects)
    {
        private readonly IDictionary<string, object> _gameObjects = gameObjects ?? throw new ArgumentNullException(nameof(gameObjects));

        public object Execute(string objectId)
        {
            if (_gameObjects.TryGetValue(objectId, out var gameObject))
            {
                return gameObject;
            }

            throw new Exception("Object not found");
        }
    }
}
