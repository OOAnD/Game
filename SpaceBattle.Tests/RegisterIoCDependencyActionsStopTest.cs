using App;
using App.Scopes;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencyActionsStopTests
    {
        [Fact]
        public void Execute_ShouldRegisterStopCommandDependency()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var registerCommand = new RegisterIoCDependencyActionsStop();

            // Act
            registerCommand.Execute();
            var stopCommand = Ioc.Resolve<ICommand>("Actions.Stop", "testObjectId", "testCommandName");

            // Assert
            Assert.IsType<StopCommand>(stopCommand);
        }
    }
}
