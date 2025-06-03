namespace SpaceBattle.Tests
{
    public class PolygonCollisionCheckerByRaysTest
    {
        [Fact]
        public void IsCollision_WhenRayIntersectsEdge_ReturnsTrue()
        {
            // Arrange
            var staticPolygon = new List<Point> { new Point(1, 1), new Point(3, 5), new Point(5, 1) };
            var movingPolygon = new List<Point> { new Point(2, 2), new Point(4, 2), new Point(4, 4), new Point(2, 4) };
            var checker = new PolygonCollisionCheckerByRays(staticPolygon, movingPolygon);

            // Act
            var collision = checker.IsCollision(0, 0, 3, 0);

            // Assert
            Assert.True(collision);
        }

        [Fact]
        public void IsCollision_WhenNoIntersection_ReturnsFalse()
        {
            // Arrange
            var staticPolygon = new List<Point> { new Point(1, 1), new Point(3, 5), new Point(5, 1) };
            var movingPolygon = new List<Point> { new Point(10, 10), new Point(12, 10), new Point(12, 12), new Point(10, 12) };
            var checker = new PolygonCollisionCheckerByRays(staticPolygon, movingPolygon);

            // Act
            var collision = checker.IsCollision(0, 0, 1, 0);

            // Assert
            Assert.False(collision);
        }

        [Fact]
        public void IsCollision_WhenRayMissesEdge_ReturnsFalse()
        {
            // Arrange
            var staticPolygon = new List<Point> { new Point(1, 1), new Point(3, 5), new Point(5, 1) };
            var movingPolygon = new List<Point> { new Point(0, 0), new Point(0, 2), new Point(2, 2), new Point(2, 0) };
            var checker = new PolygonCollisionCheckerByRays(staticPolygon, movingPolygon);

            // Act
            var collision = checker.IsCollision(6, 6, -1, -1);

            // Assert
            Assert.False(collision);
        }

        [Fact]
        public void IsCollision_WhenRayTouchesEdge_ReturnsTrue()
        {
            // Arrange
            var staticPolygon = new List<Point> { new Point(0, 0), new Point(0, 2), new Point(2, 2), new Point(2, 0) };
            var movingPolygon = new List<Point> { new Point(-1, 1), new Point(1, 1), new Point(1, 3), new Point(-1, 3) };
            var checker = new PolygonCollisionCheckerByRays(staticPolygon, movingPolygon);

            // Act
            var collision = checker.IsCollision(0, 0, 1, 0);

            // Assert
            Assert.True(collision);
        }
    }
}
