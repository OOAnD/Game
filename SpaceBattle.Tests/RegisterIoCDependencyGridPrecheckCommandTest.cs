using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencyGridPrecheckCommandTest
    {
        [Fact]
        public void Execute_ShouldRegisterIMovingObjectAdapter()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var movingMock = new Mock<IMoving>();
            var gameObject = new object();

            Ioc.Resolve<ICommand>("IoC.Register", "Adapters.Moving", (object[] args) => movingMock.Object).Execute();

            // Act
            RegisterIoCDependencyGridPrecheckCommand.Execute();

            // Assert
            var adapter = Ioc.Resolve<IMoving>("Adapters.IMovingObject", gameObject);
            Assert.NotNull(adapter);
        }

        [Fact]
        public void Execute_ShouldRegisterGridPrecheckCommand()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var movingMock = new Mock<IMoving>();
            var gameObject = new object();

            Ioc.Resolve<ICommand>("IoC.Register", "Adapters.Moving", (object[] args) => movingMock.Object).Execute();

            // Act
            RegisterIoCDependencyGridPrecheckCommand.Execute();

            // Assert
            var command = Ioc.Resolve<ICommand>("Commands.GridPrecheck", gameObject);
            Assert.NotNull(command);
            Assert.IsAssignableFrom<ICommand>(command);
        }

        [Fact]
        public void GridPrecheckCommand_ShouldImplementICommand()
        {
            // Arrange & Act
            var command = new GridPrecheckCommand(Mock.Of<IMoving>());

            // Assert
            Assert.IsAssignableFrom<ICommand>(command);
        }
    }
}
