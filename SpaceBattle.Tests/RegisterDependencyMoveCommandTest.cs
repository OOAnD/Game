using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class RegisterDependencyMoveCommandTest
    {
        [Fact]
        public void Execute_ShouldRegisterDependencyMoveCommand()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var movingObject = new Mock<IMoving>();
            var moving = new Mock<IMoving>();
            var gameObject = new object();
            var registrator = new RegisterIoCDependencyMoveCommand();
            Ioc.Resolve<ICommand>("IoC.Register", "Adapters.Moving", (object[] args) => moving.Object).Execute();

            // Act
            registrator.Execute();

            // Assert
            var dependency = Ioc.Resolve<ICommand>("Commands.Move", gameObject);
            Assert.IsType<MoveCommand>(dependency);
        }
    }
}
