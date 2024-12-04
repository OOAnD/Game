using Moq;

namespace SpaceBattle.Tests
{
    public class BridgeCommandTest
    {
        [Fact]
        public void Execute_ShouldExecuteInnerCommand()
        {
            // Arrange
            var innerCommand = new Mock<ICommand>();
            var bridgeCommand = new BridgeCommand();
            bridgeCommand.Set(innerCommand.Object);

            // Act
            bridgeCommand.Execute();

            // Assert
            innerCommand.Verify(c => c.Execute(), Times.Once);
        }

        [Fact]
        public void Set_ShouldThrow_WhenInnerCommandIsNull()
        {
            // Arrange 
            var bridgeCommand = new BridgeCommand();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => bridgeCommand.Set(null!));
        }

        [Fact]
        public void Execute_ShouldThrow_WhenCommandThrow()
        {
            // Arrange
            var innerCommand = new Mock<ICommand>();
            innerCommand.Setup(c => c.Execute()).Throws(new Exception());
            var bridgeCommand = new BridgeCommand();
            bridgeCommand.Set(innerCommand.Object);

            // Act & Assert
            Assert.Throws<Exception>(() => bridgeCommand.Execute());
        }
    }
}
