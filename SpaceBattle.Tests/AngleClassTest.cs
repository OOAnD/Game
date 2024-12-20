namespace SpaceBattle.Tests
{
    public class AngleClassTest
    {
        [Fact]
        public void Define_NumeratorShouldReduce()
        {
            // Arrange
            Angle._denominator = 8;
            var testAngle = new Angle(9);

            // Act & Assert
            Assert.Equal(1, testAngle.Numerator);
        }
        [Fact]
        public void Define_NumeratorShouldBePositive()
        {
            // Arrange
            Angle._denominator = 8;
            var testAngle = new Angle(-7);

            // Act & Assert
            Assert.Equal(1, testAngle.Numerator);
        }
        [Fact]
        public void Sum_NumeratorShouldCorrect()
        {
            // Arrange
            Angle._denominator = 8;
            var testAngle1 = new Angle(5);
            var testAngle2 = new Angle(7);

            // Act
            var resultAngle = testAngle1 + testAngle2;

            // Assert
            Assert.Equal(new Angle(4), resultAngle);
        }
        [Fact]
        public void Dif_NumeratorShouldCorrect()
        {
            // Arrange
            Angle._denominator = 8;
            var testAngle1 = new Angle(5);
            var testAngle2 = new Angle(7);

            // Act
            var resultAngle = testAngle1 - testAngle2;

            // Assert
            Assert.Equal(new Angle(6), resultAngle);
        }
        [Fact]
        public void ComparisonThroughMethod_ShouldReturnTrue_WhenSameValues()
        {
            // Arrange
            var testAngle1 = new Angle(15);
            var testAngle2 = new Angle(23);

            // Act & Assert
            Assert.Equal(testAngle1, testAngle2);
        }
        [Fact]
        public void ComparisonThroughMethod_ShouldReturnFalse_WhenDifferentValues()
        {
            // Arrange
            var testAngle1 = new Angle(1);
            var testAngle2 = new Angle(2);

            // Act & Assert
            Assert.NotEqual(testAngle1, testAngle2);
        }
        [Fact]
        public void ComparisonThroughOperator_ShouldReturnTrue_WhenSameValues()
        {
            // Arrange
            var testAngle1 = new Angle(15);
            var testAngle2 = new Angle(23);

            // Act & Assert
            Assert.True(testAngle1 == testAngle2);
        }
        [Fact]
        public void ComparisonThroughOperator_ShouldReturnTrue_WhenDifferentValues()
        {
            // Arrange
            var testAngle1 = new Angle(1);
            var testAngle2 = new Angle(2);

            // Act & Assert
            Assert.True(testAngle1 != testAngle2);
        }
        [Fact]
        public void Comparison_ShouldReturnFalse_WhenNull()
        {
            // Arrange & Act & Assert
            Assert.False(new Angle(1).Equals(null));
        }
        [Fact]
        public void GetHashCode_ShouldReturnHashCode()
        {
            // Arrange & Act & Assert
            new Angle(1).GetHashCode();
        }
    }
}
