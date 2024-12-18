namespace SpaceBattle.Tests
{
    public class AngleClassTest
    {
        [Fact]
        public void Define_NumeratorShouldBeLessThanDenominator()
        {
            // Arrange
            var testAngle = new Angle(9);

            // Act & Assert
            Assert.Equal(1, testAngle.Numerator);
        }
        [Fact]
        public void Set_ShouldBeCorrect()
        {
            // Arrange
            var testAngle = new Angle(0);
            testAngle.Numerator = 9;

            // Act & Assert
            Assert.Equal(1, testAngle.Numerator);
        }
        [Fact]
        public void Define_NumeratorShouldBePositive()
        {
            // Arrange
            var testAngle = new Angle(-7);

            // Act & Assert
            Assert.Equal(1, testAngle.Numerator);
        }
        [Fact]
        public void Sum_NumeratorShouldCorrect()
        {
            // Arrange
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
            var testAngle1 = new Angle(5);
            var testAngle2 = new Angle(7);

            // Act
            var resultAngle = testAngle1 - testAngle2;

            // Assert
            Assert.Equal(new Angle(6), resultAngle);
        }
        [Fact]
        public void Comparison_ShouldReturnTrue_ForSameValues_ThroughMethod()
        {
            // Arrange
            var testAngle1 = new Angle(15);
            var testAngle2 = new Angle(23);

            // Act & Assert
            Assert.True(testAngle1.Equals(testAngle2));
        }
        [Fact]
        public void Comparison_ShouldReturnTrue_ForSameValues_ThroughOperator()
        {
            // Arrange
            var testAngle1 = new Angle(15);
            var testAngle2 = new Angle(23);

            // Act
            var testTrue = testAngle1 == testAngle2;

            // Assert
            Assert.True(testTrue);
        }
        [Fact]
        public void Comparison_ShouldReturnFalse_ForDifferentValues_ThroughMethod()
        {
            // Arrange
            var testAngle1 = new Angle(1);
            var testAngle2 = new Angle(2);

            // Act & Assert
            Assert.False(testAngle1.Equals(testAngle2));
        }
        [Fact]
        public void Comparison_ShouldReturnTrue_ForDifferentValues_ThroughOperator()
        {
            // Arrange
            var testAngle1 = new Angle(1);
            var testAngle2 = new Angle(2);

            // Act
            var testTrue = testAngle1 != testAngle2;

            // Assert
            Assert.True(testTrue);
        }
        [Fact]
        public void GetHashCode_ShouldReturnHashCode()
        {
            // Arrange & Act & Assert
            Assert.IsType<int>(new Angle(1).GetHashCode());
        }
    }
}
