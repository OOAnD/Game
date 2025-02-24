using App;
using Moq;

namespace SpaceBattle.Tests
{
    public class GameTests
    {
        [Fact]
        public void Execute_ShouldProcessCommandsInQueue()
        {
            // Arrange
            var mockCommandQueue = new Mock<IQueue<ICommand>>();
            var mockCommand = new Mock<ICommand>();

            mockCommandQueue.Setup(q => q.Take()).Returns(mockCommand.Object);
            var game = new Game(mockCommandQueue.Object);

            // Act
            Task.Run(() => game.Execute());
            Thread.Sleep(100);
            game.Stop();

            // Assert
            mockCommand.Verify(c => c.Execute(), Times.AtLeastOnce);
        }

        [Fact]
        public void Execute_ShouldHandleCommandExecutionFailure()
        {
            // Arrange
            var mockCommandQueue = new Mock<IQueue<ICommand>>();
            var mockCommand = new Mock<ICommand>();

            mockCommand.Setup(c => c.Execute()).Throws(new Exception("Command failed"));
            mockCommandQueue.Setup(q => q.Take()).Returns(mockCommand.Object);
            var game = new Game(mockCommandQueue.Object);

            // Act
            Task.Run(() => game.Execute());
            Thread.Sleep(100);
            game.Stop();

            // Assert
            mockCommand.Verify(c => c.Execute(), Times.AtLeastOnce);
        }
    }
}
