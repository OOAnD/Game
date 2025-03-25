using App;
using App.Scopes;

namespace SpaceBattle.Tests
{
    public class RegisterIocDependencyRemoveGameObjectCommandTest
    {
        [Fact]
        public void Execute_ShouldRegisterRemoveGameObjectDependency()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var command = new RegisterIocDependencyRemoveGameObjectCommand();

            // Act
            command.Execute();

            // Assert
            var gameObject = new Dictionary<string, object> { ["id"] = "test" };
            var removeCommand = Ioc.Resolve<ICommand>("Game.Objects.Remove", gameObject);
            Assert.IsType<RemoveGameObjectCommand>(removeCommand);
        }
    }
}
