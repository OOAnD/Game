namespace SpaceBattle.Test
{
    public class CreateGameObjectStrategyTest
    {
        [Fact]
        public void Execute_ShouldAddGameObject_WithUniqueId()
        {
            // Arrange
            var strategy = new CreateGameObjectStrategy();
            var gameObject = new object();

            // Act
            var objectId = strategy.Execute(gameObject);

            // Assert
            var gameObjects = strategy.GetGameObjects();
            Assert.Single(gameObjects);
            Assert.Equal(gameObject, gameObjects[objectId]);
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenGameObjectIsNull()
        {
            // Arrange
            var strategy = new CreateGameObjectStrategy();

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => strategy.Execute(null!));
            Assert.Equal("gameObject", exception.ParamName);
            Assert.Contains("Игровой объект не может быть null.", exception.Message);
        }

        [Fact]
        public void Execute_ShouldGenerateUniqueIds_ForMultipleObjects()
        {
            // Arrange
            var strategy = new CreateGameObjectStrategy();
            var gameObject1 = new object();
            var gameObject2 = new object();

            // Act
            var objectId1 = strategy.Execute(gameObject1);
            var objectId2 = strategy.Execute(gameObject2);

            // Assert
            Assert.NotEqual(objectId1, objectId2);
        }
    }
}
