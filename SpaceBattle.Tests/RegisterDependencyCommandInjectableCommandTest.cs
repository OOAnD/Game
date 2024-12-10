using App;
using App.Scopes;

namespace SpaceBattle.Tests
{
    public class RegisterDependencyCommandInjectableCommandTest
    {
        [Fact]
        public void Execute_ShouldRegisterDependencyCommandInjectableCommand()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            // Act
            var registerDependencyCommandInjectableCommand = new RegisterDependencyCommandInjectableCommand();
            registerDependencyCommandInjectableCommand.Execute();
            var res = Ioc.Resolve<ICommand>("Commands.CommadInjectable");

            // Assert
            Assert.IsType<CommandInjectableCommand>(res);
        }

        [Fact]
        public void Execute_ShouldResolveCatiousForm()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            // Act
            var registerDependencyCommandInjectableCommand = new RegisterDependencyCommandInjectableCommand();
            registerDependencyCommandInjectableCommand.Execute();

            // Assert
            Ioc.Resolve<ICommand>("Commands.CommadInjectable");
            Ioc.Resolve<ICommandInjectable>("Commands.CommadInjectable");
            Ioc.Resolve<CommandInjectableCommand>("Commands.CommadInjectable");

        }
    }
}
