using System.Collections;

namespace SpaceBattle.Tests
{
    public class CustomAdapterTests
    {
        private readonly Dictionary<string, object> _sourceData = new()
        {
            ["existing"] = "base_value",
            ["common"] = "base_common"
        };

        private readonly Dictionary<string, Func<object>> _customBehaviors = new()
        {
            ["dynamic"] = () => "behavior_value",
            ["common"] = () => "behavior_common"
        };

        [Fact]
        public void Getter_ReturnsBehaviorValue_WhenKeyExistsInBehavior()
        {
            // Arrange
            var adapter = new CustomAdapter(_sourceData, _customBehaviors);

            // Act
            var result = adapter["dynamic"];

            // Assert
            Assert.Equal("behavior_value", result);
        }

        [Fact]
        public void Getter_ReturnsBaseValue_WhenKeyNotInBehavior()
        {
            // Arrange
            var adapter = new CustomAdapter(_sourceData, _customBehaviors);

            // Act
            var result = adapter["existing"];

            // Assert
            Assert.Equal("base_value", result);
        }

        [Fact]
        public void Getter_BehaviorHasPriority_WhenKeyInBoth()
        {
            // Arrange
            var adapter = new CustomAdapter(_sourceData, _customBehaviors);

            // Act
            var result = adapter["common"];

            // Assert
            Assert.Equal("behavior_common", result);
        }

        [Fact]
        public void Getter_ThrowsKeyNotFoundException_WhenKeyMissing()
        {
            // Arrange
            var adapter = new CustomAdapter(_sourceData, _customBehaviors);

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => adapter["missing"]);
        }

        [Fact]
        public void Setter_UpdatesBaseDict_ForNormalKey()
        {
            // Arrange
            var adapter = new CustomAdapter(_sourceData, _customBehaviors);

            // Act
            adapter["existing"] = "new_value";

            // Assert
            Assert.Equal("new_value", _sourceData["existing"]);
        }

        [Fact]
        public void Setter_Throws_WhenKeyReservedForBehavior()
        {
            // Arrange
            var adapter = new CustomAdapter(_sourceData, _customBehaviors);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => adapter["dynamic"] = "new_value");
        }

        [Fact]
        public void Add_Throws_WhenKeyReservedForBehavior()
        {
            // Arrange
            var adapter = new CustomAdapter(_sourceData, _customBehaviors);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => adapter.Add("dynamic", "value"));
        }

        [Fact]
        public void Remove_Throws_WhenKeyReservedForBehavior()
        {
            // Arrange
            var adapter = new CustomAdapter(_sourceData, _customBehaviors);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => adapter.Remove("dynamic"));
        }

        [Fact]
        public void ContainsKey_ReturnsTrue_ForBehaviorKey()
        {
            // Arrange
            var adapter = new CustomAdapter(_sourceData, _customBehaviors);

            // Act & Assert
            Assert.True(adapter.ContainsKey("dynamic"));
            Assert.True(adapter.ContainsKey("existing"));
        }

        [Fact]
        public void TryGetValue_ReturnsTrue_ForBehaviorKey()
        {
            // Arrange
            var adapter = new CustomAdapter(_sourceData, _customBehaviors);

            // Act
            var result = adapter.TryGetValue("dynamic", out var value);

            // Assert
            Assert.True(result);
            Assert.Equal("behavior_value", value);
        }

        [Fact]
        public void Values_ContainsAllValues()
        {
            // Arrange
            var adapter = new CustomAdapter(_sourceData, _customBehaviors);

            // Act
            var values = adapter.Values;

            // Assert
            Assert.Contains("base_value", values);
            Assert.Contains("behavior_value", values);
            Assert.Contains("behavior_common", values);
        }

        [Fact]
        public void Count_ReturnsCombinedUniqueKeyCount()
        {
            // Arrange
            var adapter = new CustomAdapter(_sourceData, _customBehaviors);

            // Act & Assert
            Assert.Equal(3, adapter.Count);
        }

        [Fact]
        public void Clear_OnlyAffectsSourceData()
        {
            // Arrange
            var adapter = new CustomAdapter(_sourceData, _customBehaviors);

            // Act
            adapter.Clear();

            // Assert
            Assert.Empty(_sourceData);
            Assert.True(adapter.ContainsKey("dynamic"));
        }

        [Fact]
        public void Enumerator_YieldsAllItems()
        {
            // Arrange
            var adapter = new CustomAdapter(_sourceData, _customBehaviors);
            var result = new Dictionary<string, object>();

            // Act
            foreach (var kvp in adapter)
            {
                result.Add(kvp.Key, kvp.Value);
            }

            // Assert
            Assert.Equal("behavior_value", result["dynamic"]);
            Assert.Equal("base_value", result["existing"]);
            Assert.Equal("behavior_common", result["common"]);
        }

        [Fact]
        public void CopyTo_CopiesAllItems()
        {
            // Arrange
            var adapter = new CustomAdapter(_sourceData, _customBehaviors);
            var array = new KeyValuePair<string, object>[3];

            // Act
            adapter.CopyTo(array, 0);

            // Assert
            Assert.Equal(3, array.Count(kvp => kvp.Key != null));
        }

        [Fact]
        public void IsReadOnly_ShouldReflectSourceData()
        {
            // Arrange
            var readOnlyDict = new Dictionary<string, object> { ["test"] = "value" }.AsReadOnly();
            var adapter = new CustomAdapter(readOnlyDict, new Dictionary<string, Func<object>>());

            // Act & Assert
            Assert.True(adapter.IsReadOnly);
        }

        [Fact]
        public void IsReadOnly_ShouldBeFalseForWritableDictionary()
        {
            // Arrange
            var adapter = new CustomAdapter(new Dictionary<string, object>(), new Dictionary<string, Func<object>>());

            // Act & Assert
            Assert.False(adapter.IsReadOnly);
        }

        [Fact]
        public void AddKeyValuePair_ShouldAddToSourceData()
        {
            // Arrange
            var source = new Dictionary<string, object>();
            var adapter = new CustomAdapter(source, new Dictionary<string, Func<object>>());
            var item = new KeyValuePair<string, object>("newKey", "newValue");

            // Act
            adapter.Add(item);

            // Assert
            Assert.Equal("newValue", source["newKey"]);
        }

        [Fact]
        public void AddKeyValuePair_ShouldThrowForBehaviorKey()
        {
            // Arrange
            var behavior = new Dictionary<string, Func<object>> { ["reserved"] = () => "null" };
            var adapter = new CustomAdapter(new Dictionary<string, object>(), behavior);
            var item = new KeyValuePair<string, object>("reserved", "value");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => adapter.Add(item));
        }

        [Fact]
        public void ContainsKeyValuePair_ShouldReturnTrueForExistingItem()
        {
            // Arrange
            var source = new Dictionary<string, object> { ["key"] = "value" };
            var adapter = new CustomAdapter(source, new Dictionary<string, Func<object>>());
            var item = new KeyValuePair<string, object>("key", "value");

            // Act
            var result = adapter.Contains(item);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void RemoveKeyValuePair_ShouldReturnTrueAndRemove()
        {
            // Arrange
            var source = new Dictionary<string, object> { ["key"] = "value" };
            var adapter = new CustomAdapter(source, new Dictionary<string, Func<object>>());
            var item = new KeyValuePair<string, object>("key", "value");

            // Act
            var result = adapter.Remove(item);

            // Assert
            Assert.True(result);
            Assert.Empty(source);
        }

        [Fact]
        public void RemoveKeyValuePair_ShouldThrowForBehaviorKey()
        {
            // Arrange
            var behavior = new Dictionary<string, Func<object>> { ["reserved"] = () => "null" };
            var adapter = new CustomAdapter(new Dictionary<string, object>(), behavior);
            var item = new KeyValuePair<string, object>("reserved", "value");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => adapter.Remove(item));
        }

        [Fact]
        public void TryGetValue_ShouldReturnSourceValue_WhenKeyOnlyInSourceData()
        {
            // Arrange
            var source = new Dictionary<string, object> { ["baseOnly"] = "base_value" };
            var adapter = new CustomAdapter(source, new Dictionary<string, Func<object>>());

            // Act
            var result = adapter.TryGetValue("baseOnly", out var value);

            // Assert
            Assert.True(result);
            Assert.Equal("base_value", value);
        }

        [Fact]
        public void NonGenericEnumerator_ShouldWork()
        {
            // Arrange
            var adapter = new CustomAdapter(
                new Dictionary<string, object> { ["test"] = "value" },
                new Dictionary<string, Func<object>>());

            // Act
            var enumerator = ((IEnumerable)adapter).GetEnumerator();

            // Assert
            Assert.NotNull(enumerator);
            Assert.True(enumerator.MoveNext());
            Assert.False(enumerator.MoveNext());
        }
    }
}
