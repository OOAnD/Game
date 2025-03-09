namespace SpaceBattle.Test
{
    public class SaveGameObjectStrategyTest
    {
        [Fact]
        public void Execute_ShouldSaveGameObject_WhenInputIsValid()
        {
            // Arrange
            var gameObjects = new Dictionary<string, object>();
            var saveStrategy = new SaveGameObjectStrategy(gameObjects);

            var objectId = "object1";
            var gameObject = new object();

            // Act
            saveStrategy.Execute(objectId, gameObject);

            // Assert
            Assert.Single(gameObjects);
            Assert.Equal(gameObject, gameObjects[objectId]);
        }

        [Fact]
        public void Execute_ShouldOverwriteGameObject_WhenObjectIdExists()
        {
            // Arrange
            var gameObjects = new Dictionary<string, object>
            {
                { "object1", new object() }
            };

            var saveStrategy = new SaveGameObjectStrategy(gameObjects);

            var objectId = "object1";
            var newGameObject = new object();

            // Act
            saveStrategy.Execute(objectId, newGameObject);

            // Assert
            Assert.Single(gameObjects);
            Assert.Equal(newGameObject, gameObjects[objectId]);
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenObjectIdIsNull()
        {
            // Arrange
            var gameObjects = new Dictionary<string, object>();
            var saveStrategy = new SaveGameObjectStrategy(gameObjects);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => saveStrategy.Execute(null!, new object()));
            Assert.Equal("objectId", exception.ParamName);
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenGameObjectIsNull()
        {
            // Arrange
            var gameObjects = new Dictionary<string, object>();
            var saveStrategy = new SaveGameObjectStrategy(gameObjects);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => saveStrategy.Execute("object1", null!));
            Assert.Equal("gameObject", exception.ParamName);
        }
    }
}
