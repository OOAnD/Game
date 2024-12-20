namespace SpaceBattle.Tests
{
    public class NaturalFractionTest
    {
        [Fact]
        public void Define_ShouldThrowException_WhenDivideOnZero()
        {
            // Arrange & Act & Assert
            Assert.ThrowsAny<Exception>(() => { new NaturalFraction(1, 0); });
        }
        [Fact]
        public void Define_ShouldReduce_WhenPossible()
        {
            // Arrange
            var testFraction = new NaturalFraction(2, 4);

            // Act & Assert
            Assert.Equal(new NaturalFraction(1, 2), testFraction);
        }
        [Fact]
        public void Define_ShouldBeCorrect_WhenNegative()
        {
            // Arrange
            var testFraction = new NaturalFraction(1, -2);

            // Act & Assert
            Assert.Equal(new NaturalFraction(-1, 2), testFraction);
        }
        [Fact]
        public void Sum_ShouldBeCorrect()
        {
            // Arrange
            var testFraction1 = new NaturalFraction(1, 2);
            var testFraction2 = new NaturalFraction(3, 5);

            // Act
            var resultFraction = testFraction1 + testFraction2;

            // Assert
            Assert.Equal(new NaturalFraction(11, 10), resultFraction);
        }
        [Fact]
        public void Dif_ShouldBeCorrect()
        {
            // Arrange
            var testFraction1 = new NaturalFraction(1, 2);
            var testFraction2 = new NaturalFraction(3, 5);

            // Act
            var resultFraction = testFraction1 - testFraction2;

            // Assert
            Assert.Equal(new NaturalFraction(-1, 10), resultFraction);
        }
        [Fact]
        public void Prod_ShouldBeCorrect()
        {
            // Arrange
            var testFraction1 = new NaturalFraction(1, 2);
            var testFraction2 = new NaturalFraction(3, 5);

            // Act
            var resultFraction = testFraction1 * testFraction2;

            // Assert
            Assert.Equal(new NaturalFraction(3, 10), resultFraction);
        }
        [Fact]
        public void ProdInt_ShouldBeCorrect()
        {
            // Arrange
            var testFraction1 = new NaturalFraction(1, 2);
            var testInt = 5;

            // Act
            var resultFraction = testFraction1 * testInt;

            // Assert
            Assert.Equal(new NaturalFraction(5, 2), resultFraction);
        }
        [Fact]
        public void Div_ShouldBeCorrect()
        {
            // Arrange
            var testFraction1 = new NaturalFraction(1, 2);
            var testFraction2 = new NaturalFraction(3, 5);

            // Act
            var resultFraction = testFraction1 / testFraction2;

            // Assert
            Assert.Equal(new NaturalFraction(5, 6), resultFraction);
        }
        [Fact]
        public void DivInt_ShouldBeCorrect()
        {
            // Arrange
            var testFraction1 = new NaturalFraction(1, 2);
            var testInt = 5;

            // Act
            var resultFraction = testFraction1 / testInt;

            // Assert
            Assert.Equal(new NaturalFraction(1, 10), resultFraction);
        }
        [Fact]
        public void DivInt_ShouldThrowException_WhenIntIsZero()
        {
            // Arrange 
            var testFraction = new NaturalFraction(1, 2);

            // Act & Assert
            Assert.ThrowsAny<Exception>(() => { var _ = testFraction / 0; });
        }
        [Fact]
        public void ComparisonThroughOperator_ShouldReturnTrue_WhenValuesIsSame()
        {
            // Arrange
            var testFraction1 = new NaturalFraction(1, 2);
            var testFraction2 = new NaturalFraction(2, 4);

            // Act &  Assert
            Assert.True(testFraction1 == testFraction2);
        }
        [Fact]
        public void ComparisonThroughOperator_ShouldReturnFalse_WhenValuesIsDifferent()
        {
            // Arrange
            var testFraction1 = new NaturalFraction(1, 2);
            var testFraction2 = new NaturalFraction(3, 5);

            // Act & Assert
            Assert.False(testFraction1 == testFraction2);
        }
        [Fact]
        public void ComparisonThroughOperator_ShouldReturnTrue_WhenValuesIsDifferent()
        {
            // Arrange
            var testFraction1 = new NaturalFraction(1, 2);
            var testFraction2 = new NaturalFraction(3, 5);

            // Act & Assert
            Assert.True(testFraction1 != testFraction2);
        }
        [Fact]
        public void ComparisonThroughOperator_ShouldReturnFalse_WhenValuesIsSame()
        {
            // Arrange
            var testFraction1 = new NaturalFraction(1, 2);
            var testFraction2 = new NaturalFraction(2, 4);

            // Act & Assert
            Assert.False(testFraction1 != testFraction2);
        }
        [Fact]
        public void ComparisonThroughMethod_ShouldReturnFalse_WhenValuesIsDifferent()
        {
            // Arrange
            var testFraction1 = new NaturalFraction(1, 2);
            var testFraction2 = new NaturalFraction(3, 5);

            // Act
            var testIsCorrect = testFraction1.Equals(testFraction2);

            // Assert
            Assert.False(testIsCorrect);
        }
        [Fact]
        public void ComparisonThroughMethod_ShouldReturnTrue_WhenValuesIsSame()
        {
            // Arrange
            var testFraction1 = new NaturalFraction(1, 2);
            var testFraction2 = new NaturalFraction(2, 4);

            // Act
            var testIsCorrect = testFraction1.Equals(testFraction2);

            // Assert
            Assert.True(testIsCorrect);
        }
        [Fact]
        public void Pow_ShouldBeCorrect()
        {
            // Arrange
            var testFraction = new NaturalFraction(1, 2);
            var testInt = 5;

            // Act
            var resultFraction = testFraction.Pow(testInt);

            // Assert
            Assert.Equal(new NaturalFraction(1, 32), resultFraction);
        }
        [Fact]
        public void GetHashCode_ShouldReturnHashCode()
        {
            // Arrange & Act & Assert
            Assert.IsType<int>(new NaturalFraction(1, 2).GetHashCode());
        }
    }
}
