using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class GameTests
    {
        public GameTests()
        {
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
        }

        [Fact]
        public void Execute_ShouldProcessCommandsInQueue()
        {
            // Arrange
            var mockCommandQueue = new Mock<IQueue<ICommand>>();
            var mockCommand = new Mock<ICommand>();

            Ioc.Resolve<ICommand>("IoC.Register", "Commands.TimeLimit", (object[] args) =>
            {
                return new TimeLimitCommand((GameState)args[0], (int)args[1]);
            }).Execute();

            mockCommandQueue.Setup(q => q.Take()).Returns(mockCommand.Object);
            var game = new Game(mockCommandQueue.Object);

            // Act
            game.Execute();

            // Assert
            mockCommandQueue.Verify(q => q.Take(), Times.AtLeastOnce);
            mockCommand.Verify(c => c.Execute(), Times.AtLeastOnce);
        }

        [Fact]
        public void Execute_ShouldThrows_WhenCommandThrowException()
        {
            // Arrange
            var mockCommandQueue = new Mock<IQueue<ICommand>>();
            var mockCommand = new Mock<ICommand>();
            var errorHandlerCommand = new Mock<ICommand>();

            Ioc.Resolve<ICommand>("IoC.Register", "ErrorHandler", (object[] args) =>
            {
                return errorHandlerCommand.Object;
            }).Execute();

            mockCommandQueue.Setup(q => q.Take()).Returns(mockCommand.Object);
            mockCommand.Setup(c => c.Execute()).Throws(new Exception());
            var game = new Game(mockCommandQueue.Object);

            // Act
            game.Execute();

            // Assert
            errorHandlerCommand.Verify(c => c.Execute(), Times.AtLeastOnce);
        }

        [Fact]
        public void Execute_ShouldStop_WhenTimeQuantumReached()
        {
            // Arrange
            var mockCommandQueue = new Mock<IQueue<ICommand>>();
            var mockCommand = new Mock<ICommand>();

            mockCommandQueue.Setup(q => q.Take()).Returns(mockCommand.Object);
            var game = new Game(mockCommandQueue.Object);

            // Act
            game.Execute();

            // Assert
            mockCommandQueue.Verify(q => q.Take(), Times.AtLeastOnce);
        }
    }
}
