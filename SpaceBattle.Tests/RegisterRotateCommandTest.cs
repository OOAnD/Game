using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class RegisterRotateCommandTest
    {
        [Fact]
        public void Execute_ShouldResolve()
        {
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var registrator = new RegisterRotateCommand();
            var rotatingObject = new Mock<IRotating>();

            registrator.Execute();

            var dependency = Ioc.Resolve<ICommand>("Commands.Rotate", rotatingObject.Object);
            Assert.IsType<RotateCommand>(dependency);
        }
    }
}
