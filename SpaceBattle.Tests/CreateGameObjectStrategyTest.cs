namespace SpaceBattle.Tests
{
    public class CreateGameObjectStrategyTests
    {
        [Fact]
        public void Create_ReturnsNonNullDictionary()
        {
            // Act
            var result = CreateGameObjectStrategy.Create();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Create_ReturnsDictionaryType()
        {
            // Act
            var result = CreateGameObjectStrategy.Create();

            // Assert
            Assert.IsType<Dictionary<string, object>>(result);
        }

        [Fact]
        public void Create_DictionaryContainsIdKey()
        {
            // Act
            var result = (Dictionary<string, object>)CreateGameObjectStrategy.Create();

            // Assert
            Assert.True(result.ContainsKey("id"));
        }

        [Fact]
        public void Create_IdIsValidGuidString()
        {
            // Act
            var result = (Dictionary<string, object>)CreateGameObjectStrategy.Create();
            var id = result["id"].ToString();

            // Assert
            Assert.True(Guid.TryParse(id, out _));
        }

        [Fact]
        public void Create_ReturnsNewInstanceEachTime()
        {
            // Act
            var result1 = (Dictionary<string, object>)CreateGameObjectStrategy.Create();
            var result2 = (Dictionary<string, object>)CreateGameObjectStrategy.Create();

            // Assert
            Assert.NotSame(result1, result2);
            Assert.NotEqual(result1["id"], result2["id"]);
        }
    }
}
