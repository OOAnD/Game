using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencyMacroCommandTest
    {
        [Fact]
        public void Execute_ShouldCorrectRegister()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var command1 = new Mock<ICommand>();
            var command2 = new Mock<ICommand>();

            // Act
            new RegisterIoCDependencyMacroCommand().Execute();

            var macroCommand = Ioc.Resolve<ICommand>("Commands.Macro", command1.Object, command2.Object);

            // Assert
            Assert.IsType<MacroCommand>(macroCommand);
        }
    }
}
