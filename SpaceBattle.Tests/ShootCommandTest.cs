using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class ShootCommandTest
    {
        [Fact]
        public void Execute_ShouldPerformShootSequenceCorrectly()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var gameObjectId = new object();
            var shootingObject = new Mock<object>();
            var torpedo = new Mock<object>();
            var moveConfigCommand = new Mock<ICommand>();
            var startMoveCommand = new Mock<ICommand>();

            // Register dependencies
            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.Get",
                (Func<object[], object>)(args => shootingObject.Object)).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.Torpedo.Resolve",
                (Func<object[], object>)(args => torpedo.Object)).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Configuration.MovingObject.ByMoveConfProvider",
                (Func<object[], ICommand>)(args => moveConfigCommand.Object)).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Actions.Start",
                (Func<object[], ICommand>)(args => startMoveCommand.Object)).Execute();

            var shootCommand = new ShootCommand(gameObjectId);

            // Act
            shootCommand.Execute();

            // Assert
            moveConfigCommand.Verify(cmd => cmd.Execute(), Times.Once);
            startMoveCommand.Verify(cmd => cmd.Execute(), Times.Once);
        }

        [Fact]
        public void Execute_ShouldThrowWhenGetGameObjectFails()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var gameObjectId = new object();

            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.Get",
                (Func<object[], object>)(args => throw new InvalidOperationException())).Execute();

            var shootCommand = new ShootCommand(gameObjectId);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => shootCommand.Execute());
        }

        [Fact]
        public void Execute_ShouldThrowWhenTorpedoResolveFails()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var gameObjectId = new object();

            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.Get",
                (Func<object[], object>)(args => new object())).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.Torpedo.Resolve",
                (Func<object[], object>)(args => throw new InvalidOperationException())).Execute();

            var shootCommand = new ShootCommand(gameObjectId);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => shootCommand.Execute());
        }

        [Fact]
        public void Execute_ShouldThrowWhenMoveConfigFails()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var gameObjectId = new object();

            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.Get",
                (Func<object[], object>)(args => new object())).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.Torpedo.Resolve",
                (Func<object[], object>)(args => new object())).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Configuration.MovingObject.ByMoveConfProvider",
                (Func<object[], ICommand>)(args => throw new InvalidOperationException())).Execute();

            var shootCommand = new ShootCommand(gameObjectId);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => shootCommand.Execute());
        }

        [Fact]
        public void Execute_ShouldThrowWhenStartMoveFails()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var gameObjectId = new object();

            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.Get",
                (Func<object[], object>)(args => new object())).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.Torpedo.Resolve",
                (Func<object[], object>)(args => new object())).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Configuration.MovingObject.ByMoveConfProvider",
                (Func<object[], ICommand>)(args => new Mock<ICommand>().Object)).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Actions.Start",
                (Func<object[], ICommand>)(args => throw new InvalidOperationException())).Execute();

            var shootCommand = new ShootCommand(gameObjectId);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => shootCommand.Execute());
        }
    }
}
