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
            var command3 = new Mock<ICommand>();
            var macroCommand = new MacroCommand(command1.Object, command2.Object, command3.Object);

            // Act
            macroCommand.Execute();

            // Assert
            command1.Verify(c => c.Execute(), Times.Once);
            command2.Verify(c => c.Execute(), Times.Once);
            command3.Verify(c => c.Execute(), Times.Once);
        }

        [Fact]
        public void Execute_ShouldThrow_WhenAnySubcommandThrows()
        {
            // Arrange
            var command1 = new Mock<ICommand>();
            var command2 = new Mock<ICommand>();
            var command3 = new Mock<ICommand>();
            command2.Setup(c => c.Execute()).Throws(new Exception());

            var macroCommand = new MacroCommand(command1.Object, command2.Object, command3.Object);

            // Act & Assert
            Assert.Throws<Exception>(() => macroCommand.Execute());
            command1.Verify(c => c.Execute(), Times.Once);
            command2.Verify(c => c.Execute(), Times.Once);
            command3.Verify(c => c.Execute(), Times.Never);
        }
    }
}
