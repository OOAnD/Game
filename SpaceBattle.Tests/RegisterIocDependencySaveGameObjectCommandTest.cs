using App;
using App.Scopes;

namespace SpaceBattle.Tests
{
    public class RegisterIocDependencySaveGameObjectCommandTest
    {
        [Fact]
        public void Execute_ShouldRegisterAllDependencies()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var command = new RegisterIocDependencySaveGameObjectCommand();

            // Act
            command.Execute();

            // Assert
            var gameObject = new Dictionary<string, object> { ["id"] = "test" };
            var saveCommand = Ioc.Resolve<ICommand>("Game.Objects.Save", gameObject);
            Assert.IsType<SaveGameObjectCommand>(saveCommand);
        }
    }
}
