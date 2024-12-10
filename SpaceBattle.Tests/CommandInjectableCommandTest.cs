using Moq;

namespace SpaceBattle.Tests
{
    public class CommandInjectableCommandTest
    {
        [Fact]
        public void Execute_ShouldExecuteInnerCommand()
        {
            // Arrange
            var command = new Mock<ICommand>();
            var commandInjectableCommand = new CommandInjectableCommand();

            // Act
            commandInjectableCommand.InjectCommand(command.Object);
            commandInjectableCommand.Execute();

            // Assert
            command.Verify(c => c.Execute(), Times.Once);
        }

        [Fact]
        public void Execute_ShouldThrow_WhenInnerCommandCouldntInject()
        {
            // Arrange
            var command = new Mock<ICommand>();
            var commandInjectableCommand = new CommandInjectableCommand();

            // Act & Assert
            Assert.Throws<Exception>(() => commandInjectableCommand.Execute());
        }
    }
}
