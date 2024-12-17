namespace SpaceBattle
{
    public class VectorTest
    {
        [Fact]
        public void Sum_ShouldBeCorrect()
        {
            // Arrange
            var vector1 = new Vector(3);
            var vector2 = new Vector(3);
            var correct = new Vector(3);

            vector1.Set(1, -1, 2);
            vector2.Set(-1, 1, -2);
            correct.Set(0, 0, 0);

            // Act
            var result = vector1 + vector2;

            // Assert
            Assert.True(result == correct);
        }

        [Fact]
        public void Sum_ShouldThrow_WhenVectorsHaveDifferentSize1()
        {
            //Arrange
            var vector1 = new Vector(3);
            var vector2 = new Vector(2);

            vector1.Set(1, 2, 3);
            vector2.Set(1, 2);

            //Act & Assert
            Assert.Throws<ArgumentException>(() => vector1 + vector2);
        }

        [Fact]
        public void Sum_ShouldThrow_WhenVectorsHaveDifferentSize2()
        {
            //Arrange
            var vector1 = new Vector(2);
            var vector2 = new Vector(3);

            vector1.Set(1, 2);
            vector2.Set(1, 2, 3);

            //Act & Assert
            Assert.Throws<ArgumentException>(() => vector1 + vector2);
        }

        [Fact]
        public void Comparison_ShouldReturnTrue_ForEqualsValues()
        {
            // Arrange
            var vector1 = new Vector(2);
            var vector2 = new Vector(2);

            vector1.Set(12, 5);
            vector2.Set(12, 5);

            // Act & Assert
            Assert.True(vector1.Equals(vector2));
        }

        [Fact]
        public void Comparison_ShouldReturnTrue_ForSameValues()
        {
            // Arrange
            var vector1 = new Vector(2);
            var vector2 = new Vector(2);

            vector1.Set(12, 5);
            vector2.Set(12, 5);

            // Act & Assert
            Assert.True(vector1 == vector2);
        }

        [Fact]
        public void Comparison_ShouldReturnTrue_ForDifferentEqualsValues()
        {
            // Arrange
            var vector1 = new Vector(2);
            var vector2 = new Vector(2);

            vector1.Set(12, 5);
            vector2.Set(12, 6);

            // Act & Assert
            Assert.False(vector1.Equals(vector2));
        }

        [Fact]
        public void Comparison_ShouldReturnTrue_ForDifferentValues()
        {
            // Arrange
            var vector1 = new Vector(2);
            var vector2 = new Vector(2);

            vector1.Set(12, 5);
            vector2.Set(12, 6);

            // Act & Assert
            Assert.True(vector1 != vector2);
        }

        [Fact]
        public void Availability_GetHashCode()
        {
            // Arrange
            var vector1 = new Vector(2);

            // Act & Assert
            Assert.False(vector1.GetHashCode() == 0);
        }

        [Fact]
        public void Set_ShouldThrow_WhenIncorrectNumberOfValues()
        {
            // Arrange
            var vector = new Vector(2);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => vector.Set(12));
        }

        [Fact]
        public void Set_ShouldWork_WhenCorrectNumberOfValues()
        {
            // Arrange
            var vector = new Vector(2);

            // Act
            vector.Set(12, 5);

            // Assert
            var correct = new Vector(2);
            correct.Set(12, 5);
            Assert.True(vector == correct);
        }

        [Fact]
        public void Sum_ShouldBeCorrect_WithNegativeValues()
        {
            // Arrange
            var vector1 = new Vector(3);
            var vector2 = new Vector(3);
            var correct = new Vector(3);

            vector1.Set(-1, -2, -3);
            vector2.Set(-4, -5, -6);
            correct.Set(-5, -7, -9);

            // Act
            var result = vector1 + vector2;

            // Assert
            Assert.True(result == correct);
        }

        [Fact]
        public void Sum_ShouldBeCorrect_WithZeroValues()
        {
            // Arrange
            var vector1 = new Vector(3);
            var vector2 = new Vector(3);
            var correct = new Vector(3);

            vector1.Set(0, 0, 0);
            vector2.Set(0, 0, 0);
            correct.Set(0, 0, 0);

            // Act
            var result = vector1 + vector2;

            // Assert
            Assert.True(result == correct);
        }

        [Fact]
        public void Set_ShouldWork_WithNegativeValues()
        {
            // Arrange
            var vector = new Vector(2);

            // Act
            vector.Set(-12, -5);

            // Assert
            var correct = new Vector(2);
            correct.Set(-12, -5);
            Assert.True(vector == correct);
        }

        [Fact]
        public void Set_ShouldWork_WithZeroValues()
        {
            // Arrange
            var vector = new Vector(2);

            // Act
            vector.Set(0, 0);

            // Assert
            var correct = new Vector(2);
            correct.Set(0, 0);
            Assert.True(vector == correct);
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
        public void Equals_ShouldReturnFalse_WhenComparedWithDifferentType()
        {
            // Arrange
            var vector = new Vector(2);
            var obj = new object();

            // Act & Assert
            Assert.False(vector.Equals(obj));
        }

        [Fact]
        public void GetHashCode_ShouldBeDifferent_ForDifferentNegativeValues()
        {
            // Arrange
            var vector1 = new Vector(2);
            var vector2 = new Vector(2);

            vector1.Set(-1, -2);
            vector2.Set(-2, -1);

            // Act & Assert
            Assert.NotEqual(vector1.GetHashCode(), vector2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_ShouldBeSame_ForZeroValues()
        {
            // Arrange
            var vector1 = new Vector(2);
            var vector2 = new Vector(2);

            vector1.Set(0, 0);
            vector2.Set(0, 0);

            // Act & Assert
            Assert.NotEqual(vector1.GetHashCode(), vector2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_ShouldBeDifferent_ForDifferentLargeValues()
        {
            // Arrange
            var vector1 = new Vector(2);
            var vector2 = new Vector(2);

            vector1.Set(1000, 2000);
            vector2.Set(2000, 1000);

            // Act & Assert
            Assert.NotEqual(vector1.GetHashCode(), vector2.GetHashCode());
        }

        [Fact]
        public void Sum_ShouldBeCorrect_WithLargeValues()
        {
            // Arrange
            var vector1 = new Vector(3);
            var vector2 = new Vector(3);
            var correct = new Vector(3);

            vector1.Set(1000, 2000, 3000);
            vector2.Set(4000, 5000, 6000);
            correct.Set(5000, 7000, 9000);

            // Act
            var result = vector1 + vector2;

            // Assert
            Assert.True(result == correct);
        }

        [Fact]
        public void Set_ShouldWork_WithLargeValues()
        {
            // Arrange
            var vector = new Vector(2);

            // Act
            vector.Set(1000, 2000);

            // Assert
            var correct = new Vector(2);
            correct.Set(1000, 2000);
            Assert.True(vector == correct);
        }
    }
}
