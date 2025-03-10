using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class PlayersShipFindStrategyTest
    {
        [Fact]
        public void Find_ShouldReturnPlayerShips_WhenPlayerExists()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var playerId = "player1";
            var expectedShips = new List<object> { new object(), new object() };

            var mockPlayer = new Mock<IDictionary<string, object>>();
            mockPlayer.Setup(p => p["ownShips"]).Returns(expectedShips);

            var mockPlayers = new Mock<IDictionary<string, IDictionary<string, object>>>();
            mockPlayers.Setup(p => p[playerId]).Returns(mockPlayer.Object);

            Ioc.Resolve<ICommand>("IoC.Register", "Game.Players.GetAll", (object[] args) => mockPlayers.Object).Execute();

            var strategy = new PlayersShipFindStrategy(playerId);

            // Act
            var result = strategy.Find();

            // Assert
            Assert.Equal(expectedShips, result);
        }

        [Fact]
        public void Find_ShouldThrowException_WhenPlayerDoesNotExist()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var playerId = "nonExistentPlayer";

            var mockPlayers = new Mock<IDictionary<string, IDictionary<string, object>>>();
            mockPlayers.Setup(p => p[playerId]).Throws<KeyNotFoundException>();

            Ioc.Resolve<ICommand>("IoC.Register", "Game.Players.GetAll", (object[] args) => mockPlayers.Object).Execute();

            var strategy = new PlayersShipFindStrategy(playerId);

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => strategy.Find());
        }

        [Fact]
        public void Find_ShouldThrowException_WhenPlayerHasNoShips()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var playerId = "player1";

            var mockPlayer = new Mock<IDictionary<string, object>>();
            mockPlayer.Setup(p => p["ownShips"]).Throws<KeyNotFoundException>();

            var mockPlayers = new Mock<IDictionary<string, IDictionary<string, object>>>();
            mockPlayers.Setup(p => p[playerId]).Returns(mockPlayer.Object);

            Ioc.Resolve<ICommand>("IoC.Register", "Game.Players.GetAll", (object[] args) => mockPlayers.Object).Execute();

            var strategy = new PlayersShipFindStrategy(playerId);

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => strategy.Find());
        }
    }
}
