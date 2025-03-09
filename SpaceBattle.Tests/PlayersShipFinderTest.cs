namespace SpaceBattle.Test
{
    public class PlayersShipFinderTests
    {
        [Fact]
        public void Execute_ShouldReturnShips_WhenUserHasShips()
        {
            // Arrange
            var ships = new Dictionary<string, string>
            {
                { "ship1", "user1" },
                { "ship2", "user2" },
                { "ship3", "user1" }
            };

            var shipFinder = new PlayersShipFinder(ships);
            var userId = "user1";

            // Act
            var result = shipFinder.Execute(userId);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains("ship1", result);
            Assert.Contains("ship3", result);
        }

        [Fact]
        public void Execute_ShouldReturnEmptyList_WhenUserHasNoShips()
        {
            // Arrange
            var ships = new Dictionary<string, string>
            {
                { "ship1", "user1" },
                { "ship2", "user2" }
            };

            var shipFinder = new PlayersShipFinder(ships);
            var userId = "user3";

            // Act
            var result = shipFinder.Execute(userId);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenUserIdIsNull()
        {
            // Arrange
            var ships = new Dictionary<string, string>
            {
                { "ship1", "user1" }
            };

            var shipFinder = new PlayersShipFinder(ships);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => shipFinder.Execute(null!));
            Assert.Equal("userId", exception.ParamName);
        }
    }
}
