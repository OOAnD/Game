using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class CreateMacroCommandStrategyTests
    {
        [Fact]
        public void Resolve_CreatesMacroCommand_WhenAllDependenciesAreResolved()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var command1Mock = new Mock<ICommand>();
            var command2Mock = new Mock<ICommand>();
            var macroCommandMock = new Mock<ICommand>();

            Ioc.Resolve<ICommand>("IoC.Register", "Specs.Move", (object[] args) => new string[] { "Command1", "Command2" }).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Commands.Command1", (object[] args) => command1Mock.Object).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Commands.Command2", (object[] args) => command2Mock.Object).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Commands.Macro", (object[] args) =>
            {
                Assert.Equal(2, args.Length);
                Assert.Equal(command1Mock.Object, args[0]);
                Assert.Equal(command2Mock.Object, args[1]);
                return macroCommandMock.Object;
            }).Execute();

            // Act
            var strategy = new CreateMacroCommandStrategy("Move");
            var macroCommand = strategy.Resolve(new object[] { new object() });

            // Assert
            Assert.Equal(macroCommandMock.Object, macroCommand);
        }

        [Fact]
        public void Resolve_ThrowsException_WhenCommandDependenciesAreNotResolved()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            Ioc.Resolve<ICommand>("IoC.Register", "Specs.Move", (object[] args) => new string[] { "Command1", "Command2" }).Execute();

            // Act & Assert
            var strategy = new CreateMacroCommandStrategy("Move");
            Assert.Throws<Exception>(() => strategy.Resolve(new object[] { new object() }));
        }

        [Fact]
        public void Resolve_ThrowsException_WhenMacroCommandDependencyIsNotResolved()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var command1Mock = new Mock<ICommand>();
            var command2Mock = new Mock<ICommand>();

            Ioc.Resolve<ICommand>("IoC.Register", "Specs.Move", (object[] args) => new string[] { "Command1", "Command2" }).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Commands.Command1", (object[] args) => command1Mock.Object).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Commands.Command2", (object[] args) => command2Mock.Object).Execute();

            // Act & Assert
            var strategy = new CreateMacroCommandStrategy("Move");
            Assert.Throws<Exception>(() => strategy.Resolve(new object[] { new object() }));
        }

        [Fact]
        public void Resolve_ThrowsException_WhenSpecDependenciesAreNotResolved()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            // Act & Assert
            var strategy = new CreateMacroCommandStrategy("Move");
            Assert.Throws<Exception>(() => strategy.Resolve(new object[] { new object() }));
        }
    }
}
