using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencyRotateCommandTest
    {
        [Fact]
        public void Execute_ShouldResolve()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var rotating = new Mock<IRotating>();
            var gameObject = new object();
            var registrator = new RegisterIoCDependencyRotateCommand();
            Ioc.Resolve<ICommand>("IoC.Register", "Adapters.Rotating", (object[] args) => rotating.Object).Execute();

            // Act
            registrator.Execute();

            // Assert
            var dependency = Ioc.Resolve<ICommand>("Commands.Rotate", gameObject);
            Assert.IsType<RotateCommand>(dependency);
        }
    }
}
