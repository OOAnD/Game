using Moq;

namespace SpaceBattle.Test
{
    public class StopCommandTest
    {
        [Fact]
        public void Execute_ShouldCancelCommand()
        {
            // Arrange
            var cancelable = new Mock<ICommandBox>();
            var emptyCommand = new Mock<ICommand>();

            var utilCommandFactory = new Mock<IEmptyCommandFactory>();

            utilCommandFactory.Setup(f => f.CreateEmptyCommand()).Returns(emptyCommand.Object);

            var stopCommand = new StopCommand(cancelable.Object, utilCommandFactory.Object);

            // Act
            stopCommand.Execute();

            // Assert
            cancelable.Verify(c => c.Set(emptyCommand.Object), Times.Once);
        }

        [Fact]
        public void Constructor_NullCommandBox_ThrowsArgumentNullException()
        {
            // Arrange
            var emptyCommandFactory = new Mock<IEmptyCommandFactory>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new StopCommand(null, emptyCommandFactory.Object));
        }

        [Fact]
        public void Constructor_NullEmptyCommandFactory_ThrowsArgumentNullException()
        {
            // Arrange
            var cancelable = new Mock<ICommandBox>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new StopCommand(cancelable.Object, null));
        }
    }
}
