using Moq;

namespace SpaceBattle.Tests
{
    public class MacroCommandTest
    {
        [Fact]
        public void Execute_ShouldExecuteAllSubcommands()
        {
            // Arrange
            var command1 = new Mock<ICommand>();
            var command2 = new Mock<ICommand>();
            var macroCommand = new MacroCommand(command1.Object, command2.Object);

            // Act
            macroCommand.Execute();

            // Assert
            command1.Verify(c => c.Execute(), Times.Once);
            command2.Verify(c => c.Execute(), Times.Once);
        }

        [Fact]
        public void Execute_ShouldThrow_WhenCommandThrow()
        {
            // Arrange
            var command1 = new Mock<ICommand>();
            var command2 = new Mock<ICommand>();

            command1.Setup(c => c.Execute()).Throws(new Exception());

            var macroCommand = new MacroCommand(command1.Object, command2.Object);

            // Act & Assert
            Assert.Throws<Exception>(() => macroCommand.Execute());
        }

        [Fact]
        public void Define_ShouldThrow_WhenCommandsIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new MacroCommand(null!));
        }

        [Fact]
        public void Define_ShouldThrow_WhenAnyCommandIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new MacroCommand(new Mock<ICommand>().Object, null!));
        }
    }
}
