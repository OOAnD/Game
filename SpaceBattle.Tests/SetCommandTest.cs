using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class SetCommandTest
    {
        [Fact]
        public void Execute_ShouldSetProperty()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope);

            var command = new Mock<ICommand>();
            var gameObject = new Mock<IDictionary<string, object>>();
            var gameObjects = new Mock<IDictionary<string, IDictionary<string, object>>>();
            gameObjects.Setup(g => g["testObjectId"]).Returns(gameObject.Object);

            Ioc.Resolve<App.ICommand>("IoC.Register", "Game.Objects", (object[] args) => gameObjects.Object).Execute();

            var setCommand = new SetCommand("testObjectId", "testCommandName", command.Object);

            // Act
            setCommand.Execute();

            // Assert
            gameObject.Verify(g => g.Add("testCommandName", command.Object), Times.Once);
        }
    }
}
