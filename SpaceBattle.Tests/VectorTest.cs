namespace SpaceBattle.Tests
{
    public class VectorTest
    {
        [Fact]
        public void Sum_ShouldThrow_WhenVectorsHaveDifferentSize()
        {
            //Arrange
            var vector1 = new Vector(3);
            var vector2 = new Vector(4);

            //Act & Assert
            Assert.Throws<ArgumentException>(() => vector1 + vector2);
        }

        [Fact]
        public void Sum_ShouldBeCorrect()
        {
            // Arrange
            var vector1 = new Vector(2);
            var vector2 = new Vector(2);
            var correct = new Vector(2);

            vector1.Set(12, 5);
            vector2.Set(-7, 3);
            correct.Set(5, 8);

            // Act
            var result = vector1 + vector2;

            // Assert
            Assert.True(result == correct);
        }

        [Fact]
        public void Comparison_ShouldReturnFalse_ForDifferentValues()
        {
            // Arrange
            var vector1 = new Vector(2);
            var vector2 = new Vector(2);

            vector1.Set(12, 5);
            vector2.Set(12, 6);

            // Act & Assert
            Assert.False(vector1 == vector2);
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
    }
}
