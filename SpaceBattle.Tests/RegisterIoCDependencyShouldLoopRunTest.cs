using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencyShouldLoopRunTests
    {
        [Fact]
        public void Execute_RegistersShouldLoopRunDependency()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var mockQueueCount = new Mock<IQueueCount>();
            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Game.Queue",
                (object[] _) => mockQueueCount.Object).Execute();

            var registerShouldLoopRun = new RegisterIoCDependencyShouldLoopRun();

            // Act
            registerShouldLoopRun.Execute();

            // Assert
            var shouldLoopRun = Ioc.Resolve<bool>("Game.ShouldLoopRun");
            Assert.False(shouldLoopRun);
        }
    }
}
