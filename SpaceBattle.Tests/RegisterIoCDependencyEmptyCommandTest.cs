using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencyEmptyCommandTests
    {
        [Fact]
        public void Execute_ShouldRegisterEmptyCommandDependency()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var registerCommand = new RegisterIoCDependencyEmptyCommand();

            // Act
            registerCommand.Execute();
            var resolvedCommand = Ioc.Resolve<ICommand>("Commands.Empty");

            // Assert
            Assert.IsType<EmptyCommand>(resolvedCommand);
        }
    }
}
