using App;
using App.Scopes;

namespace SpaceBattle.Tests
{
    public class RegisterIocDependencyCreateGameObjectStrategyTest
    {
        [Fact]
        public void Execute_ShouldRegisterAllDependencies()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var command = new RegisterIocDependencyCreateGameObjectStrategy();

            // Act
            command.Execute();

            // Assert
            var newObject = Ioc.Resolve<object>("Game.Objects.Create");
            Assert.NotNull(newObject);
            Assert.IsType<Dictionary<string, object>>(newObject);
        }
    }
}
