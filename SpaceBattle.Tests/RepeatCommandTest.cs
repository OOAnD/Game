using Moq;

namespace SpaceBattle.Tests
{
    public class RepeatCommandTest
    {
        [Fact]
        public void Execute_ShouldRepeatCommand()
        {
            // Arrange
            var command = new Mock<ICommand>();
            var queue = new Mock<ICommandQueue>();
            var repeatCommand = new RepeatCommand(queue.Object, command.Object);

            // Act
            repeatCommand.Execute();

            // Assert
            queue.Verify(q => q.Enqueue(command.Object), Times.Once);
        }

        [Fact]
        public void Define_ShouldThrow_WhenCommandIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new RepeatCommand(new Mock<ICommandQueue>().Object, null!));
        }

        [Fact]
        public void Define_ShouldThrow_WhenQueueIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new RepeatCommand(null!, new Mock<ICommand>().Object));
        }

        [Fact]
        public void Execute_ShouldThrow_WhenEnqueueThrow()
        {
            // Arrange
            var command = new Mock<ICommand>();
            var queue = new Mock<ICommandQueue>();
            queue.Setup(q => q.Enqueue(command.Object)).Throws(new Exception());
            var repeatCommand = new RepeatCommand(queue.Object, command.Object);

            // Act & Assert
            Assert.Throws<Exception>(() => repeatCommand.Execute());
        }
    }
}
