using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencyGetInjectableTests
    {
        [Fact]
        public void Execute_ShouldRegisterGetInjectableDependency()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var mockInjectable = new Mock<ICommandInjectable>();
            var mockObjects = new Mock<IDictionary<string, IDictionary<string, object>>>();
            var mockObject = new Mock<IDictionary<string, object>>();

            mockObjects.Setup(o => o["testObjectId"]).Returns(mockObject.Object);
            mockObject.Setup(o => o["testCommandName"]).Returns(mockInjectable.Object);

            Ioc.Resolve<App.ICommand>("IoC.Register", "Game.Objects", (object[] args) => mockObjects.Object).Execute();

            var registerCommand = new RegisterIoCDependencyGetInjectable();

            // Act
            registerCommand.Execute();
            var resolvedInjectable = Ioc.Resolve<ICommandInjectable>("Game.Object.GetInjectable", "testObjectId", "testCommandName");

            // Assert
            Assert.Equal(mockInjectable.Object, resolvedInjectable);
        }
    }
}
