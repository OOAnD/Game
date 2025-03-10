using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class FindGameObjectStrategyTest
    {
        [Fact]
        public void Find_ShouldReturnGameObject_WhenObjectExists()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var objectId = "player";
            var expectedObject = new object();

            var mockObjects = new Mock<IDictionary<string, object>>();
            mockObjects.Setup(d => d[objectId]).Returns(expectedObject);

            Ioc.Resolve<ICommand>("IoC.Register", "Game.Object.GetAll", (object[] args) => mockObjects.Object).Execute();

            var strategy = new FindGameObjectStrategy(objectId);

            // Act
            var result = strategy.Find();

            // Assert
            Assert.Equal(expectedObject, result);
        }

        [Fact]
        public void Find_ShouldThrowException_WhenObjectDoesNotExist()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var objectId = "nonExistentObject";

            var mockObjects = new Mock<IDictionary<string, object>>();
            mockObjects.Setup(d => d[objectId]).Throws<KeyNotFoundException>();

            Ioc.Resolve<ICommand>("IoC.Register", "Game.Object.GetAll", (object[] args) => mockObjects.Object).Execute();

            var strategy = new FindGameObjectStrategy(objectId);

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => strategy.Find());
        }
    }
}
