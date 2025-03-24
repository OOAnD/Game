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
        public void Execute_ShouldProcessCommands()
        {
            // Arrange
            var gameState = new GameState();
            var mockCommand = new Mock<ICommand>();
            mockCommand.Setup(c => c.Execute()).Callback(() => gameState.Stop());

            Ioc.Resolve<ICommand>("IoC.Register", "Commands.TimeLimit", (object[] args) =>
            {
                return new Mock<ICommand>().Object;
            }).Execute();

            Ioc.Resolve<ICommand>("IoC.Register", "Game.Scheduler.Next", (object[] args) =>
            {
                return (Func<ICommand>)(() => mockCommand.Object);
            }).Execute();

            var game = new Game(gameState);

            // Act
            game.Execute();

            // Assert
            mockCommand.Verify(c => c.Execute(), Times.Once);
        }

        [Fact]
        public void Execute_ShouldHandleException_WhenCommandThrowsException()
        {
            // Arrange
            var gameState = new GameState();
            var mockCommand = new Mock<ICommand>();
            var mockErrorHandler = new Mock<ICommand>();

            mockCommand.Setup(c => c.Execute()).Throws(new Exception("Test exception"));
            mockErrorHandler.Setup(e => e.Execute()).Callback(() => gameState.Stop());

            Ioc.Resolve<ICommand>("IoC.Register", "ErrorHandler", (object[] args) =>
            {
                return mockErrorHandler.Object;
            }).Execute();

            Ioc.Resolve<ICommand>("IoC.Register", "Game.Scheduler.Next", (object[] args) =>
            {
                return (Func<ICommand>)(() => mockCommand.Object);
            }).Execute();

            Ioc.Resolve<ICommand>("IoC.Register", "Commands.TimeLimit", (object[] args) =>
            {
                return new Mock<ICommand>().Object;
            }).Execute();

            var game = new Game(gameState);

            // Act
            game.Execute();

            // Assert
            mockErrorHandler.Verify(e => e.Execute(), Times.Once);
        }

        [Fact]
        public void Execute_ShouldStop_WhenTimeQuantumReached()
        {
            // Arrange
            var gameState = new GameState();
            var mockTimeLimitCommand = new Mock<ICommand>();
            mockTimeLimitCommand.Setup(c => c.Execute()).Callback(() => gameState.Stop());

            Ioc.Resolve<ICommand>("IoC.Register", "Commands.TimeLimit", (object[] args) =>
            {
                return mockTimeLimitCommand.Object;
            }).Execute();

            Ioc.Resolve<ICommand>("IoC.Register", "Game.Scheduler.Next", (object[] args) =>
            {
                return (Func<ICommand>)(() => new Mock<ICommand>().Object);
            }).Execute();

            var game = new Game(gameState);

            // Act
            game.Execute();

            // Assert
            mockTimeLimitCommand.Verify(c => c.Execute(), Times.AtLeastOnce);
            Assert.False(gameState.IsRunning());
        }
    }
}
