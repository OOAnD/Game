using Moq;

namespace SpaceBattle.Tests
{
    public class GameLoopTests
    {
        [Fact]
        public void ShouldLoopRun_ReturnsTrue_WhenQueueIsNotEmpty()
        {
            // Arrange
            var mockQueueCount = new Mock<IQueueCount>();
            mockQueueCount.Setup(q => q.Count()).Returns(1);

            var gameLoop = new GameLoop(mockQueueCount.Object);

            // Act
            var result = gameLoop.ShouldLoopRun();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ShouldLoopRun_ReturnsFalse_WhenQueueIsEmpty()
        {
            // Arrange
            var mockQueueCount = new Mock<IQueueCount>();
            mockQueueCount.Setup(q => q.Count()).Returns(0);

            var gameLoop = new GameLoop(mockQueueCount.Object);

            // Act
            var result = gameLoop.ShouldLoopRun();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ShouldLoopRun_ReturnsCorrectValue_WhenQueueCountChanges()
        {
            // Arrange
            var mockQueueCount = new Mock<IQueueCount>();
            mockQueueCount.SetupSequence(q => q.Count())
                          .Returns(1)
                          .Returns(0);

            var gameLoop = new GameLoop(mockQueueCount.Object);

            // Act
            var result1 = gameLoop.ShouldLoopRun();
            var result2 = gameLoop.ShouldLoopRun();

            // Assert
            Assert.True(result1);
            Assert.False(result2);
        }
    }
}
