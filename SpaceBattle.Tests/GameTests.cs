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
        public void Game_Execute_ShouldRunLoopUntilQueueIsEmpty()
        {
            // Arrange
            var mockQueue = new Mock<IQueue>();
            var mockCmd = new Mock<ICommand>();
            var mockCountable = mockQueue.As<IQueueCount>();

            mockCountable.Setup(m => m.Count()).Returns(1);
            mockQueue.Setup(q => q.Take())
                     .Returns(mockCmd.Object)
                     .Callback(() =>
                     {
                         mockCountable.Setup(m => m.Count()).Returns(0);
                     });

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Game.Queue",
                (object[] _) => mockQueue.Object).Execute();

            var regShouldLoopRun = new RegisterIoCDependencyShouldLoopRun();
            regShouldLoopRun.Execute();

            var gameScope = Ioc.Resolve<object>("IoC.Scope.Create");
            var game = new Game(gameScope);

            // Act
            game.Execute();

            // Assert
            mockCmd.Verify(c => c.Execute(), Times.Once);
            mockQueue.Verify(q => q.Take(), Times.Once);
        }

        [Fact]
        public void Game_Execute_ShouldHandleExceptions()
        {
            // Arrange
            var mockQueue = new Mock<IQueue>();
            var mockCmd = new Mock<ICommand>();
            var mockCountable = mockQueue.As<IQueueCount>();

            mockCmd.Setup(cmd => cmd.Execute()).Throws(new Exception("Command execution failed"));
            mockCountable.Setup(m => m.Count()).Returns(1);
            mockQueue.Setup(q => q.Take())
                     .Returns(mockCmd.Object)
                     .Callback(() =>
                     {
                         mockCountable.Setup(m => m.Count()).Returns(0);
                     });

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Game.Queue",
                (object[] _) => mockQueue.Object).Execute();

            var regShouldLoopRun = new RegisterIoCDependencyShouldLoopRun();
            regShouldLoopRun.Execute();

            var gameScope = Ioc.Resolve<object>("IoC.Scope.Create");
            var game = new Game(gameScope);

            // Act
            game.Execute();

            // Assert
            mockCmd.Verify(c => c.Execute(), Times.Once);
            mockQueue.Verify(q => q.Take(), Times.Once);
        }

        [Fact]
        public void Game_Execute_ShouldRestoreOldScope()
        {
            // Arrange
            var mockQueue = new Mock<IQueue>();
            var mockCmd = new Mock<ICommand>();
            var mockCountable = mockQueue.As<IQueueCount>();

            mockCountable.Setup(m => m.Count()).Returns(1);
            mockQueue.Setup(q => q.Take())
                     .Returns(mockCmd.Object)
                     .Callback(() =>
                     {
                         mockCountable.Setup(m => m.Count()).Returns(0);
                     });

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Game.Queue",
                (object[] _) => mockQueue.Object).Execute();

            var regShouldLoopRun = new RegisterIoCDependencyShouldLoopRun();
            regShouldLoopRun.Execute();

            var oldScope = Ioc.Resolve<object>("IoC.Scope.Current");
            var gameScope = Ioc.Resolve<object>("IoC.Scope.Create");
            var game = new Game(gameScope);

            // Act
            game.Execute();

            // Assert
            var currentScope = Ioc.Resolve<object>("IoC.Scope.Current");
            Assert.Equal(oldScope, currentScope);
        }
    }
}
