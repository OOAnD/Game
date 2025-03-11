using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencyNextCommandTest
    {
        [Fact]
        public void Execute_RegistersNextCommandDependency()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var mockQueue = new Mock<IQueue>();
            var mockCommand = new Mock<ICommand>();

            mockQueue.Setup(q => q.Take()).Returns(mockCommand.Object);

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Game.Queue",
                (object[] _) => mockQueue.Object).Execute();

            var registerNextCommand = new RegisterIoCDependencyNextCommand();

            // Act
            registerNextCommand.Execute();

            // Assert
            var nextCommand = Ioc.Resolve<ICommand>("Commands.GetNextCommand");
            Assert.Equal(mockCommand.Object, nextCommand);
        }

        [Fact]
        public void Execute_RegistersNextCommandDependency_WithCorrectQueue()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var mockQueue = new Mock<IQueue>();
            var mockCommand = new Mock<ICommand>();

            mockQueue.Setup(q => q.Take()).Returns(mockCommand.Object);

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Game.Queue",
                (object[] _) => mockQueue.Object).Execute();

            var registerNextCommand = new RegisterIoCDependencyNextCommand();

            // Act
            registerNextCommand.Execute();

            // Assert
            var nextCommand = Ioc.Resolve<ICommand>("Commands.GetNextCommand");
            Assert.Equal(mockCommand.Object, nextCommand);
        }
    }
}
