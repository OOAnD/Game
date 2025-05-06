namespace SpaceBattle.Tests
{
    public class GridTests
    {
        private readonly Grid _grid = new(0, 0, 10);

        [Theory]
        [InlineData(5, 5, 8, 8, true)]
        [InlineData(0, 0, 0, 0, true)]
        [InlineData(5, 5, 15, 5, false)]
        [InlineData(5, 5, 5, 15, false)]
        [InlineData(0, 0, 10, 10, false)]
        [InlineData(-5, -5, -8, -8, true)]
        [InlineData(-5, -5, -15, -5, false)]

        public void IsInOneCell_VariousCoordinates_ReturnsCorrectResult(
            int x1, int y1,
            int x2, int y2,
            bool expected)
        {
            // Arrange
            var obj1 = new Vector((int)x1, (int)y1);
            var obj2 = new Vector((int)x2, (int)y2);

            // Act
            var result = _grid.IsInOneCell(obj1, obj2);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void IsInOneCell_DifferentGridOffset_WorksCorrectly()
        {
            // Arrange
            var grid = new Grid(100, 100, 10);
            var obj1 = new Vector(105, 105);
            var obj2 = new Vector(104, 104);

            // Act & Assert
            Assert.True(grid.IsInOneCell(obj1, obj2));
        }

        [Fact]
        public void IsInOneCell_LargeCellSize_WorksCorrectly()
        {
            // Arrange
            var grid = new Grid(0, 0, 100);
            var obj1 = new Vector(50, 50);
            var obj2 = new Vector(99, 99);

            // Act & Assert
            Assert.True(grid.IsInOneCell(obj1, obj2));
        }

        [Fact]
        public void IsInOneCell_EdgeCaseCoordinates_WorksCorrectly()
        {
            // Arrange
            var grid = new Grid(0, 0, 10);
            var obj1 = new Vector(10, 10);
            var obj2 = new Vector(9, 9);

            // Act & Assert
            Assert.True(grid.IsInOneCell(obj1, obj2));
        }
    }
}
