using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class StopCommandTest
    {
        [Fact]
        public void Execute_ShouldInjectEmptyCommand()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var mockInjectable = new Mock<ICommandInjectable>();
            var mockEmptyCommand = new Mock<ICommand>();

            Ioc.Resolve<ICommand>("IoC.Register", "Game.Object.GetInjectable", (object[] args) => mockInjectable.Object).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Commands.Empty", (object[] args) => mockEmptyCommand.Object).Execute();

            var stopCommand = new StopCommand("testObjectId", "testCommandName");

            // Act
            stopCommand.Execute();

            // Assert
            mockInjectable.Verify(i => i.InjectCommand(mockEmptyCommand.Object), Times.Once);
        }
    }
}
