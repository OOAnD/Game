using App.Scopes;
using App;
using Moq;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencyCreateGameTest
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
            Assert.NotNull(nextCommand);
            Assert.Equal(mockCommand.Object, nextCommand);
        }
    }
}
