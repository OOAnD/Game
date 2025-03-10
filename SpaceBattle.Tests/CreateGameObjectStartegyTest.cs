namespace SpaceBattle.Tests
{
    public class CreateTorpedoStrategyTest
    {
        [Fact]
        public void Create_ReturnsDictionary()
        {
            // Act
            var result = CreateTorpedoStrategy.Create();

            // Assert
            Assert.IsType<Dictionary<string, object>>(result);
        }

        [Fact]
        public void Create_ReturnsDictionaryWithTypeTorpedo()
        {
            // Act
            var result = CreateTorpedoStrategy.Create() as Dictionary<string, object>;

            // Assert
            Assert.NotNull(result);
            Assert.True(result.ContainsKey("type"));
            Assert.Equal("torpedo", result["type"]);
        }

        [Fact]
        public void Create_ReturnsDictionaryWithValidGuidId()
        {
            // Act
            var result = CreateTorpedoStrategy.Create() as Dictionary<string, object>;

            // Assert
            Assert.NotNull(result);
            Assert.True(result.ContainsKey("id"));
            var id = result["id"] as string;
            Assert.NotNull(id);
            Assert.True(Guid.TryParse(id, out _));
        }
    }
}
