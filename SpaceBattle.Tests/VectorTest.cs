namespace SpaceBattle
{
    public class VectorTest
    {
        [Fact]
        public void Sum_ShouldBeCorrect()
        {
            // Arrange
            var vector1 = new Vector(1, -1, 2);
            var vector2 = new Vector(-1, 1, -2);

            // Act
            var result = vector1 + vector2;

            // Assert
            Assert.Equal(new Vector(0, 0, 0), result);

        }

        [Fact]
        public void Sum_ShouldThrow_WhenVectorsHaveDifferentSize1()
        {
            // Arrange
            var vector1 = new Vector(1, 2, 3);
            var vector2 = new Vector(1, 2);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => vector1 + vector2);
        }

        [Fact]
        public void Sum_ShouldThrow_WhenVectorsHaveDifferentSize2()
        {
            // Arrange
            var vector1 = new Vector(1, 2);
            var vector2 = new Vector(1, 2, 3);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => vector1 + vector2);
        }

        [Fact]
        public void Comparison_ShouldReturnTrue_ForEqualsValues()
        {
            // Arrange
            var vector1 = new Vector(1, 2, 3);
            var vector2 = new Vector(1, 2, 3);

            // Act
            var result = vector1.Equals(vector2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Comparison_ShouldReturnTrue_ForSameValues()
        {
            // Arrange
            var vector1 = new Vector(1, 2, 3);
            var vector2 = new Vector(1, 2, 3);

            // Act
            var result = vector1 == vector2;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Comparison_ShouldReturnFalse_ForDifferentEqualsValues()
        {
            // Arrange
            var vector1 = new Vector(1, 2, 3);
            var vector2 = new Vector(4, 5, 6);

            // Act & Assert
            Assert.False(vector1.Equals(vector2));
        }

        [Fact]
        public void Comparison_ShouldReturnTrue_ForDifferentValues()
        {
            // Arrange
            var vector1 = new Vector(1, 2, 3);
            var vector2 = new Vector(4, 5, 6);

            // Act
            var result = vector1 != vector2;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Availability_GetHashCode()
        {
            // Arrange
            var vector1 = new Vector(2);

            // Act & Assert
            vector1.GetHashCode();
        }

        [Fact]
        public void Comparison_ShouldReturnFalse_ForSameValues()
        {
            // Arrange
            var vector1 = new Vector(1, 2, 3);
            var vector2 = new Vector(1, 2, 3);

            // Act & Assert
            Assert.False(vector1 != vector2);
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenComparedWithNull()
        {
            // Arrange
            var vector = new Vector(2);

            // Act & Assert
            Assert.False(vector.Equals(null));
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenComparedWithDifferentDimension()
        {
            // Arrange
            var vector1 = new Vector(1, 2);
            var vector2 = new Vector(1, 2, 3);

            // Act & Assert
            Assert.False(vector1.Equals(vector2));
        }
    }
}
