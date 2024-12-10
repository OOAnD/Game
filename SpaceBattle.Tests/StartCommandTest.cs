using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class StartCommandTest
    {
        [Fact]
        public void Execute_ShouldCorrectStartAction()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var macro = new Mock<ICommand>();
            var injectable = new Mock<ICommandInjectable>();
            var setCommand = new Mock<ICommand>();
            var queue = new Mock<ICommandReciever>();
            var sendCommand = new Mock<ICommand>();

            Ioc.Resolve<App.ICommand>("IoC.Register", "Macro.TestCommand", (object[] args) => macro.Object).Execute();
            Ioc.Resolve<App.ICommand>("IoC.Register", "Commands.CommadInjectable", (object[] args) => injectable.Object).Execute();
            Ioc.Resolve<App.ICommand>("IoC.Register", "Game.Object.Actions.Set", (object[] args) => setCommand.Object).Execute();
            Ioc.Resolve<App.ICommand>("IoC.Register", "Game.Queue", (object[] args) => queue.Object).Execute();
            Ioc.Resolve<App.ICommand>("IoC.Register", "Commands.Send", (object[] args) => sendCommand.Object).Execute();

            var startCommand = new StartCommand("TestCommand", "testObjectId", []);

            // Act
            startCommand.Execute();

            //Assert
            injectable.Verify(c => c.InjectCommand(macro.Object), Times.Once);
            sendCommand.Verify(c => c.Execute(), Times.Once);
            setCommand.Verify(c => c.Execute(), Times.Once);

        }
    }
}
