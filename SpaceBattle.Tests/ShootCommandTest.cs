using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class ShootCommandTest
    {
        [Fact]
        public void Execute_ShoulCorrectShoot()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var authCommand = new Mock<ICommand>();
            var gameObject = new Mock<object>();
            var torpedo = new Mock<object>();
            var saveGameObjectCommand = new Mock<ICommand>();
            var startMoveCommand = new Mock<ICommand>();
            var confMovingObjectCommand = new Mock<ICommand>();

            var playerId = new object();
            var gameObjectId = new object();

            // Настроить все зависимости айока
            Ioc.Resolve<ICommand>("IoC.Register", "Commands.ShootAuth", (object[] args) => authCommand.Object).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.Get", (object[] args) => gameObject.Object).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.Create", (object[] args) => torpedo.Object).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.Save", (object[] args) => saveGameObjectCommand.Object).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Actions.Start", (object[] args) => startMoveCommand.Object).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Configuration.MovingObject.ByMoveConfProvider", (object[] args) => confMovingObjectCommand.Object).Execute();

            var shootCommand = new ShootCommand(playerId, gameObjectId);

            // Act
            shootCommand.Execute();

            // Assert
            authCommand.Verify(c => c.Execute(), Times.Once);
            confMovingObjectCommand.Verify(m => m.Execute(), Times.Once);
            saveGameObjectCommand.Verify(s => s.Execute(), Times.Once);
            startMoveCommand.Verify(s => s.Execute(), Times.Once);
        }

        [Fact]
        public void Execute_ShouldThrowWhenCreateTorpedoFails()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var authCommand = new Mock<ICommand>();
            var gameObject = new Mock<object>();
            var torpedo = new Mock<object>();
            torpedo.Setup(t => t.ToString()).Throws<InvalidOperationException>();

            var playerId = new object();
            var gameObjectId = new object();

            Ioc.Resolve<ICommand>("IoC.Register", "Commands.ShootAuth", (object[] args) => authCommand.Object).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.Get", (object[] args) => gameObject.Object).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.Create", (object[] args) => torpedo.Object).Execute();

            var shootCommand = new ShootCommand(playerId, gameObjectId);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => shootCommand.Execute());
        }

        [Fact]
        public void Execute_ShouldThrowWhenConfMovingObjectFails()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var authCommand = new Mock<ICommand>();
            var gameObject = new Mock<object>();
            var torpedo = new Mock<object>();
            var confMovingObjectCommand = new Mock<ICommand>();
            confMovingObjectCommand.Setup(c => c.Execute()).Throws<InvalidOperationException>();

            var playerId = new object();
            var gameObjectId = new object();

            Ioc.Resolve<ICommand>("IoC.Register", "Commands.ShootAuth", (object[] args) => authCommand.Object).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.Get", (object[] args) => gameObject.Object).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.Create", (object[] args) => torpedo.Object).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Configuration.MovingObject.ByMoveConfProvider", (object[] args) => confMovingObjectCommand.Object).Execute();

            var shootCommand = new ShootCommand(playerId, gameObjectId);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => shootCommand.Execute());
        }

        [Fact]
        public void Execute_ShouldThrowWhenSaveGameObjectFails()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var authCommand = new Mock<ICommand>();
            var gameObject = new Mock<object>();
            var torpedo = new Mock<object>();
            var saveGameObjectCommand = new Mock<ICommand>();
            saveGameObjectCommand.Setup(c => c.Execute()).Throws<InvalidOperationException>();

            var playerId = new object();
            var gameObjectId = new object();

            Ioc.Resolve<ICommand>("IoC.Register", "Commands.ShootAuth", (object[] args) => authCommand.Object).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.Get", (object[] args) => gameObject.Object).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.Create", (object[] args) => torpedo.Object).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Configuration.MovingObject.ByMoveConfProvider", (object[] args) => new Mock<ICommand>().Object).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.Save", (object[] args) => saveGameObjectCommand.Object).Execute();

            var shootCommand = new ShootCommand(playerId, gameObjectId);

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

            var authCommand = new Mock<ICommand>();
            var gameObject = new Mock<object>();
            var torpedo = new Mock<object>();
            var startMoveCommand = new Mock<ICommand>();
            startMoveCommand.Setup(c => c.Execute()).Throws<InvalidOperationException>();

            var playerId = new object();
            var gameObjectId = new object();

            Ioc.Resolve<ICommand>("IoC.Register", "Commands.ShootAuth", (object[] args) => authCommand.Object).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.Get", (object[] args) => gameObject.Object).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.Create", (object[] args) => torpedo.Object).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Configuration.MovingObject.ByMoveConfProvider", (object[] args) => new Mock<ICommand>().Object).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.Save", (object[] args) => new Mock<ICommand>().Object).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Actions.Start", (object[] args) => startMoveCommand.Object).Execute();

            var shootCommand = new ShootCommand(playerId, gameObjectId);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => shootCommand.Execute());
        }
    }
}
