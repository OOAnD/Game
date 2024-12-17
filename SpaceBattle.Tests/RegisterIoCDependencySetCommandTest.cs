using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencySetCommandTest
    {
        [Fact]
        public void Execute_ShouldCorrectRegister()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var objectId = "testObjectId";
            var commandName = "testCommandName";
            var command = new Mock<ICommand>();

            // Act
            new RegisterIoCDependencySetCommand().Execute();
            var res = Ioc.Resolve<ICommand>("Game.Object.Actions.Set", objectId, commandName, command.Object);

            // Assert
            Assert.IsType<SetCommand>(res);
        }
    }
}
