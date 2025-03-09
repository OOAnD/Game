namespace SpaceBattle
{
    public class SaveGameObjectStrategy(IDictionary<string, object> gameObjects)
    {
        private readonly IDictionary<string, object> _gameObjects = gameObjects ?? throw new ArgumentNullException(nameof(gameObjects));

        public void Execute(string objectId, object gameObject)
        {
            _gameObjects[objectId ?? throw new ArgumentNullException(nameof(objectId), "Идентификатор объекта не может быть null.")] = gameObject ?? throw new ArgumentNullException(nameof(gameObject), "Игровой объект не может быть null.");
        }
    }
}
