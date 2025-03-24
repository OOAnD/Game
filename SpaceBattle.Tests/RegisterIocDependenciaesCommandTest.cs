using App;
using App.Scopes;

namespace SpaceBattle.Tests
{
    public class RegisterGameDependenciesCommandTest
    {
        [Fact]
        public void Execute_ShouldRegisterAllDependencies()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var command = new RegisterGameDependenciesCommand();

            // Act
            command.Execute();

            // Assert
            var torpedo = Ioc.Resolve<object>("Strategies.Create.Torpedo");
            Assert.NotNull(torpedo);
            Assert.IsType<Dictionary<string, object>>(torpedo);

            var gameObject = new Dictionary<string, object> { ["id"] = "test" };
            var saveCommand = Ioc.Resolve<ICommand>("Commands.GameObject.Save", gameObject);
            Assert.IsType<SaveGameObjectCommand>(saveCommand);

            var removeCommand = Ioc.Resolve<ICommand>("Commands.GameObject.Remove", gameObject);
            Assert.IsType<RemoveGameObjectCommand>(removeCommand);

            var findStrategy = Ioc.Resolve<object>("Strategies.Find.GameObject", "obj1");
            Assert.IsType<FindGameObjectStrategy>(findStrategy);

            var shipsStrategy = Ioc.Resolve<object>("Strategies.Find.PlayerShips", "player1");
            Assert.IsType<PlayersShipFindStrategy>(shipsStrategy);
        }
    }
}
