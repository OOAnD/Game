namespace SpaceBattle
{
    public class CreateGameObjectStrategy
    {
        private readonly Dictionary<string, object> _gameObjects;

        public CreateGameObjectStrategy()
        {
            _gameObjects = [];
        }
        public string Execute(object? gameObject)
        {
            var objectId = Guid.NewGuid().ToString();
            _gameObjects.Add(objectId, gameObject ?? throw new ArgumentNullException(nameof(gameObject), "Игровой объект не может быть null."));
            return objectId;
        }

        public IDictionary<string, object> GetGameObjects()
        {
            return _gameObjects;
        }
    }
}
