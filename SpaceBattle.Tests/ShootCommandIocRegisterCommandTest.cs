using App;
using App.Scopes;

namespace SpaceBattle.Tests
{
    public class ShootCommandIocRegisterCommandTest
    {
        [Fact]
        public void Execute_ShouldRegisterShootCommandDependency()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var playerId = new object();
            var gameObjectId = new object();

            // Act
            var registerCommand = new ShootCommandIocRegisterCommand();
            registerCommand.Execute();
            
            var resolvedCommand = Ioc.Resolve<ICommand>("Commands.Shoot", playerId, gameObjectId);

            // Assert
            Assert.IsType<ShootCommand>(resolvedCommand);
        }
    }
}
