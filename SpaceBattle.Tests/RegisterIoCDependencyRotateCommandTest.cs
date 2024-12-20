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

            var registrator = new RegisterIoCDependencyRotateCommand();
            var rotatingObject = new Mock<IRotating>();

            // Act
            registrator.Execute();

            // Assert
            var dependency = Ioc.Resolve<ICommand>("Commands.Rotate", rotatingObject.Object);
            Assert.IsType<RotateCommand>(dependency);
        }
    }
}
