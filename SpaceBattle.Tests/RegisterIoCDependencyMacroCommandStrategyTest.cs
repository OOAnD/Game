using App;
using App.Scopes;
using Moq;
using Xunit;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencyMacroMoveRotateTests
    {
        [Fact]
        public void Execute_ShouldRegisterMacroMoveDependency()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var mockMacroCommand = new Mock<ICommand>();
            var mockMoveCommand = new Mock<ICommand>();

            Ioc.Resolve<ICommand>("IoC.Register", "Commands.Macro", (object[] args) => mockMacroCommand.Object).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Specs.Move", (object[] args) => new[] { "MoveCommand" }).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Commands.MoveCommand", (object[] args) => mockMoveCommand.Object).Execute();

            var registerCommand = new RegisterIoCDependencyMacroMoveRotate();

            // Act
            registerCommand.Execute();
            var resolvedMacroMove = Ioc.Resolve<ICommand>("Macro.Move", new object[] { new object() });

            // Assert
            Assert.Equal(mockMacroCommand.Object, resolvedMacroMove);
        }

        [Fact]
        public void Execute_ShouldRegisterMacroRotateDependency()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var mockMacroCommand = new Mock<ICommand>();
            var mockRotateCommand = new Mock<ICommand>();

            Ioc.Resolve<ICommand>("IoC.Register", "Commands.Macro", (object[] args) => mockMacroCommand.Object).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Specs.Rotate", (object[] args) => new[] { "RotateCommand" }).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Commands.RotateCommand", (object[] args) => mockRotateCommand.Object).Execute();

            var registerCommand = new RegisterIoCDependencyMacroMoveRotate();

            // Act
            registerCommand.Execute();
            var resolvedMacroRotate = Ioc.Resolve<ICommand>("Macro.Rotate", new object[] { new object() });

            // Assert
            Assert.Equal(mockMacroCommand.Object, resolvedMacroRotate);
        }

        [Fact]
        public void Execute_ShouldResolveMacroCommandsAsExpected()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var mockMacroCommand = new Mock<ICommand>();
            var mockMoveCommand = new Mock<ICommand>();
            var mockRotateCommand = new Mock<ICommand>();

            Ioc.Resolve<ICommand>("IoC.Register", "Commands.Macro", (object[] args) => mockMacroCommand.Object).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Specs.Move", (object[] args) => new[] { "MoveCommand" }).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Commands.MoveCommand", (object[] args) => mockMoveCommand.Object).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Specs.Rotate", (object[] args) => new[] { "RotateCommand" }).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Commands.RotateCommand", (object[] args) => mockRotateCommand.Object).Execute();

            var registerCommand = new RegisterIoCDependencyMacroMoveRotate();

            // Act
            registerCommand.Execute();
            var resolvedMacroMove = Ioc.Resolve<ICommand>("Macro.Move", new object[] { new object() });
            var resolvedMacroRotate = Ioc.Resolve<ICommand>("Macro.Rotate", new object[] { new object() });

            // Assert
            Assert.Equal(mockMacroCommand.Object, resolvedMacroMove);
            Assert.Equal(mockMacroCommand.Object, resolvedMacroRotate);
        }
    }
}
