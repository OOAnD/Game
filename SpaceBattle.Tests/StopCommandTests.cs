using Moq;
namespace SpaceBattle.Tests
{
    public class StopCommandTests
    {
        [Fact]
        public void Execute_ShouldSetEmptyCommand_WhenCalled()
        {
            // Arrange
            var mockCommandBox = new Mock<ICommandBox>();
            var stopCommand = new StopCommand(mockCommandBox.Object);

            // Act
            stopCommand.Execute();

            // Assert
            mockCommandBox.Verify(cb => cb.Set(It.IsAny<EmptyCommand>()), Times.Once);
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_WhenCommandBoxIsNull()
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new StopCommand(null));
            Assert.Equal("cancellableCommand", exception.ParamName);
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenCommandBoxSetFails()
        {
            // Arrange
            var mockCommandBox = new Mock<ICommandBox>();
            var stopCommand = new StopCommand(mockCommandBox.Object);
            mockCommandBox.Setup(cb => cb.Set(It.IsAny<EmptyCommand>())).Throws(new Exception("Failed to set command in CommandBox"));

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => stopCommand.Execute());
            Assert.Equal("Failed to set command in CommandBox", exception.Message);
        }
    }
}
