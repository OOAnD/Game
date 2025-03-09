using Moq;

namespace SpaceBattle.Test
{
    public class FindGameObjectStrategyTest
    {
        [Fact]
        public void Execute_ShouldReturnGameObject_WhenObjectExists()
        {
            // Arrange
            var objectId = "object1";
            var expectedGameObject = new object();

            var gameObjectsMock = new Mock<IDictionary<string, object>>();
            gameObjectsMock.Setup(dict => dict.TryGetValue(objectId, out expectedGameObject))
                          .Returns(true);

            var strategy = new FindGameObjectStrategy(gameObjectsMock.Object);

            // Act
            var result = strategy.Execute(objectId);

            // Assert
            Assert.Equal(expectedGameObject, result);
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenObjectDoesNotExist()
        {
            // Arrange
            var objectId = "nonExistentObject";
            object? nullGameObject = null;

            var gameObjectsMock = new Mock<IDictionary<string, object>>();
            gameObjectsMock.Setup(dict => dict.TryGetValue(objectId, out nullGameObject))
                          .Returns(false);

            var strategy = new FindGameObjectStrategy(gameObjectsMock.Object);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => strategy.Execute(objectId));
            Assert.Equal("Object not found", exception.Message);
        }
    }
}
