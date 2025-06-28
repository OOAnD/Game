using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class RegisterIocDependencyFindGameObjectStrategyTest
    {
        [Fact]
        public void Execute_ShouldRegisterAllDependencies()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
            var testObject = new Mock<object>();
            var testObjects = new Dictionary<string, object> { ["test_id"] = testObject.Object };
            Ioc.Resolve<ICommand>("IoC.Register", "Game.Object.GetAll", (object[] args) => testObjects).Execute();

            var command = new RegisterIocDependencyFindGameObjectStrategy();

            // Act
            command.Execute();

            // Assert
            var expected = Ioc.Resolve<object>("Game.Objects.Get", "test_id");
            Assert.Same(testObject.Object, expected);
        }
    }
}
