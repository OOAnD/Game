using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class GameTests
    {
        [Fact]
        public void ProcessOrder_ShouldExecuteCommand()
        {
            // Arrange
            var mockIoC = new Mock<IIoCContainer>();
            var mockCommand = new Mock<ICommand>();

            mockIoC.Setup(ioc => ioc.Resolve<ICommand>("Commands.Shoot", It.IsAny<IDictionary<string, object>>()))
                   .Returns(mockCommand.Object);

            var game = new Game(mockIoC.Object);
            var order = new Dictionary<string, object>();

            // Act
            game.ProcessOrder(order);

            // Assert
            mockCommand.Verify(cmd => cmd.Execute(), Times.Once);
        }

        [Fact]
        public void ProcessOrder_ShouldHandleException()
        {
            // Arrange
            var mockIoC = new Mock<IIoCContainer>();
            var mockCommand = new Mock<ICommand>();

            mockCommand.Setup(cmd => cmd.Execute()).Throws(new Exception("Test exception"));
            mockIoC.Setup(ioc => ioc.Resolve<ICommand>("Commands.Shoot", It.IsAny<IDictionary<string, object>>()))
                   .Returns(mockCommand.Object);

            var game = new Game(mockIoC.Object);
            var order = new Dictionary<string, object>();

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => game.ProcessOrder(order));
            Assert.Equal("Test exception", ex.Message);
        }
    }
}
