namespace SpaceBattle.Tests
{
    public class PolygonCollisionCheckerBySATTest
    {
        [Fact]
        public void IsCollision_WhenPolygonsOverlap_ReturnsTrue()
        {
            // Arrange
            var polyA = new[] { new Point(0, 0), new Point(0, 2), new Point(2, 2), new Point(2, 0) };
            var polyB = new[] { new Point(1, 1), new Point(1, 3), new Point(3, 3), new Point(3, 1) };
            var checker = new PolygonCollisionCheckerBySAT(polyA, polyB);

            // Act
            var result = checker.IsCollision(0, 0, 0, 0); // Оба полигона статичны

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsCollision_WhenPolygonsDontTouch_ReturnsFalse()
        {
            // Arrange
            var polyA = new[] { new Point(0, 0), new Point(0, 1), new Point(1, 1), new Point(1, 0) };
            var polyB = new[] { new Point(2, 2), new Point(2, 3), new Point(3, 3), new Point(3, 2) };
            var checker = new PolygonCollisionCheckerBySAT(polyA, polyB);

            // Act
            var result = checker.IsCollision(0, 0, 0, 0);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Constructor_WhenPolygonHasLessThan3Points_ThrowsArgumentException()
        {
            // Arrange
            var invalidPolygon = new[] { new Point(0, 0), new Point(1, 1) };
            var validPolygon = new[] { new Point(0, 0), new Point(0, 1), new Point(1, 1) };

            // Act & Assert для первого полигона
            var ex1 = Assert.Throws<ArgumentException>(() => new PolygonCollisionCheckerBySAT(invalidPolygon, validPolygon));

            // Act & Assert для второго полигона
            var ex2 = Assert.Throws<ArgumentException>(() => new PolygonCollisionCheckerBySAT(validPolygon, invalidPolygon));

            // Act & Assert для обоих полигонов
            var ex3 = Assert.Throws<ArgumentException>(() => new PolygonCollisionCheckerBySAT(invalidPolygon, invalidPolygon));
        }

        [Fact]
        public void IsCollision_WhenZeroAxis_ReturnsFalse()
        {
            // Arrange
            // Создаем полигон с вырожденным ребром (две одинаковые точки)
            var polyA = new[] { new Point(0, 0), new Point(0, 0), new Point(1, 1) }; // Первое ребро нулевое
            var polyB = new[] { new Point(2, 2), new Point(3, 3), new Point(3, 2) };

            var checker = new PolygonCollisionCheckerBySAT(polyA, polyB);

            // Act
            var result = checker.IsCollision(0, 0, 0, 0);

            // Assert
            Assert.False(result);
        }
    }
}
