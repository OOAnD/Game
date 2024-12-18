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
            var newScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", newScope).Execute();

            var movingObject = new Mock<IMoving>();

            // Act
            var registerDependencyMoveCommand = new RegisterIoCDependencyMoveCommand();
            registerDependencyMoveCommand.Execute();
            var res = Ioc.Resolve<ICommand>("Commands.Move", new object[] { movingObject.Object });

            // Assert
            Assert.IsType<MoveCommand>(res);
        }
    }
}
