using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencyActionsStartTest
    {
        [Fact]
        public void Execute_ShouldRegisterDependencyActionStart()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var setCommand = new Mock<ICommand>();
            Ioc.Resolve<App.ICommand>("IoC.Register", "Game.Object.Actions.Set", (object[] args) => setCommand.Object).Execute();

            // Act
            var registerIoCDependencyActionsStart = new RegisterIoCDependencyActionsStart();
            registerIoCDependencyActionsStart.Execute();
            var res = Ioc.Resolve<ICommand>("Actions.Start", "TestCommand", "testObjectId", new object[] { });

            // Assert
            Assert.IsType<StartCommand>(res);
        }
    }
}
