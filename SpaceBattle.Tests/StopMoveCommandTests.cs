using Moq;

namespace SpaceBattle.Tests
{
    public class StopMoveCommandTests
    {
        [Fact]
        public void Execute_ShouldSetEmptyCommand_WhenCalled()
        {
            // Arrange
            var mockCommandBox = new Mock<ICommandBox>();
            var gameObject = new Dictionary<string, object>
            {
                { "Movement", mockCommandBox.Object }
            };
            var stopMoveCommand = new StopMoveCommand(gameObject);

            // Act
            stopMoveCommand.Execute();

            // Assert
            mockCommandBox.Verify(cb => cb.Set(It.IsAny<EmptyCommand>()), Times.Once);
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_WhenGameObjectIsNull()
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new StopMoveCommand(null!));
            Assert.Equal("gameObject", exception.ParamName);
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenMovementCommandBoxNotFound()
        {
            // Arrange
            var gameObject = new Dictionary<string, object>();
            var stopMoveCommand = new StopMoveCommand(gameObject);

            // Act & Assert
            var exception = Assert.Throws<KeyNotFoundException>(() => stopMoveCommand.Execute());
            Assert.Equal("The given key 'Movement' was not present in the dictionary.", exception.Message);
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenCommandBoxSetFails()
        {
            // Arrange
            var mockCommandBox = new Mock<ICommandBox>();
            mockCommandBox.Setup(cb => cb.Set(It.IsAny<EmptyCommand>())).Throws(new Exception("Failed to set command in CommandBox"));

            var gameObject = new Dictionary<string, object>
            {
                { "Movement", mockCommandBox.Object }
            };

            var stopMoveCommand = new StopMoveCommand(gameObject);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => stopMoveCommand.Execute());
            Assert.Equal("Failed to set command in CommandBox", exception.Message);
        }
    }
}
