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
        public void GCD_ShouldReturnCorrectAnswer()
        {
            // Arrange 
            var testInt1 = 4;
            var testInt2 = 8;

            // Act
            var gcd = NaturalFraction.GCD(testInt1, testInt2);

            // Assert
            Assert.Equal(4, gcd);
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
        public void Comparison_ShouldReturnFalse_WhenValuesIsDifferent_ThroughOperator()
        {
            // Arrange
            var testFraction1 = new NaturalFraction(1, 2);
            var testFraction2 = new NaturalFraction(3, 5);

            // Act
            var testIsCorrect = testFraction1 == testFraction2;

            // Assert
            Assert.False(testIsCorrect);
        }
        [Fact]
        public void Comparison_ShouldReturnTrue_WhenValuesIsDifferent_ThroughOperator()
        {
            // Arrange
            var testFraction1 = new NaturalFraction(1, 2);
            var testFraction2 = new NaturalFraction(3, 5);

            // Act
            var testIsCorrect = testFraction1 != testFraction2;

            // Assert
            Assert.True(testIsCorrect);
        }
        [Fact]
        public void Comparison_ShouldReturnTrue_WhenValuesIsSame_ThroughOperator()
        {
            // Arrange
            var testFraction1 = new NaturalFraction(1, 2);
            var testFraction2 = new NaturalFraction(2, 4);

            // Act
            var testIsCorrect = testFraction1 == testFraction2;

            // Assert
            Assert.True(testIsCorrect);
        }
        [Fact]
        public void Comparison_ShouldReturnFalse_WhenValuesIsSame_ThroughOperator()
        {
            // Arrange
            var testFraction1 = new NaturalFraction(1, 2);
            var testFraction2 = new NaturalFraction(2, 4);

            // Act
            var testIsCorrect = testFraction1 != testFraction2;

            // Assert
            Assert.False(testIsCorrect);
        }
        [Fact]
        public void Comparison_ShouldReturnFalse_WhenValuesIsDifferent_ThroughMethod()
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
        public void Comparison_ShouldReturnTrue_WhenValuesIsSame_ThroughMethod()
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
