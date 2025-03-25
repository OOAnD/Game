using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class ShootOperationIocRegisterCommandTest
    {
        [Fact]
        public void Execute_ShouldRegisterShootOperationDependency()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var playerId = new object();
            var gameObjectId = new object();

            // Register dependencies that will be resolved inside the operation
            var mockAuthCommand = new Mock<ICommand>();
            var mockShootCommand = new Mock<ICommand>();

            Ioc.Resolve<ICommand>("IoC.Register", "Commands.ShootAuth",
                (Func<object[], ICommand>)(args => mockAuthCommand.Object)).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "Commands.Shoot",
                (Func<object[], ICommand>)(args => mockShootCommand.Object)).Execute();

            // Act
            var registerCommand = new ShootOperationIocRegisterCommand();
            registerCommand.Execute();

            var operation = Ioc.Resolve<ICommand>("Operations.Shoot", playerId, gameObjectId);

            // Assert
            Assert.IsType<MacroCommand>(operation);
        }
    }
}
