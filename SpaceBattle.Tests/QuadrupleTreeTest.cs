namespace SpaceBattle.Tests
{

    public class QuadrupleTreeTests
    {
        [Fact]
        public void Contains_ShouldReturnTrue_ForExistingQuadruple()
        {
            // Arrange
            var quadruples = new List<(int, int, int, int)> { (5, 6, 7, 8) };
            var tree = new QuadrupleTree(quadruples);

            // Act & Assert
            Assert.True(tree.Contains(5, 6, 7, 8));
        }

        [Fact]
        public void Contains_ShouldReturnFalse_ForNonExistingQuadruple()
        {
            // Arrange
            var quadruples = new List<(int, int, int, int)> { (1, 2, 3, 4), (5, 6, 7, 8) };
            var tree = new QuadrupleTree(quadruples);

            // Act & Assert
            Assert.False(tree.Contains(5, 6, 7, 9));
        }

        [Fact]
        public void Contains_ShouldReturnFalse_ForEmptyTree()
        {
            // Arrange
            var quadruples = new List<(int, int, int, int)>();
            var tree = new QuadrupleTree(quadruples);

            // Act & Assert
            Assert.False(tree.Contains(1, 2, 3, 4));
        }

        [Fact]
        public void Contains_ShouldReturnTrue_ForDuplicateQuadruples()
        {
            // Arrange
            var quadruples = new List<(int, int, int, int)> { (1, 2, 3, 4), (1, 2, 3, 4) };
            var tree = new QuadrupleTree(quadruples);

            // Act & Assert
            Assert.True(tree.Contains(1, 2, 3, 4));
        }

        [Fact]
        public void Contains_ShouldReturnFalse_ForPartialMatch()
        {
            // Arrange
            var quadruples = new List<(int, int, int, int)> { (1, 2, 3, 5) };
            var tree = new QuadrupleTree(quadruples);

            // Act & Assert
            Assert.False(tree.Contains(1, 2, 3, 6));
        }

        [Fact]
        public void Contains_ShouldReturnTrue_ForLargeNumbers()
        {
            // Arrange
            var quadruples = new List<(int, int, int, int)>
        {
            (int.MaxValue, int.MinValue, 0, 42),
            (1000000, 2000000, 3000000, 4000000)
        };
            var tree = new QuadrupleTree(quadruples);

            // Act & Assert
            Assert.True(tree.Contains(int.MaxValue, int.MinValue, 0, 42));
        }

        [Fact]
        public void Contains_ShouldReturnFalse_ForLargeNumbers()
        {
            // Arrange
            var quadruples = new List<(int, int, int, int)>
        {
            (int.MaxValue, int.MinValue, 0, 42),
            (1000000, 2000000, 3000000, 4000000)
        };
            var tree = new QuadrupleTree(quadruples);

            // Act & Assert
            Assert.False(tree.Contains(1000000, 2000000, 3000000, 4000001));
        }
    }
}
