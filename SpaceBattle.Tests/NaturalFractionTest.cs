namespace SpaceBattle.Tests
{
    public class NaturalFractionTest
    {
        [Fact]
        public void Define_ShouldThrowException()
        {
            Assert.ThrowsAny<Exception>(() => { new NaturalFraction(1, 0); });
        }
        [Fact]
        public void Define_ShouldBeCorrect_WhenUnReduced()
        {
            var testFraction = new NaturalFraction(2, 4);

            var testIsCorrect = testFraction == new NaturalFraction(1, 2);

            Assert.True(testIsCorrect);
        }
        [Fact]
        public void Comparison_ShouldReturnFalse_ForDifferentValues_ThroughOperator()
        {
            var testFraction1 = new NaturalFraction(1, 2);
            var testFraction2 = new NaturalFraction(3, 5);

            var testIsCorrect = testFraction1 == testFraction2;

            Assert.False(testIsCorrect);
        }
        [Fact]
        public void Comparison_ShouldReturnTrue_ForDifferentValues_ThroughOperator()
        {
            var testFraction1 = new NaturalFraction(1, 2);
            var testFraction2 = new NaturalFraction(3, 5);

            var testIsCorrect = testFraction1 != testFraction2;

            Assert.True(testIsCorrect);
        }
        [Fact]
        public void Comparison_ShouldReturnTrue_ForSameValues_ThroughOperator()
        {
            var testFraction1 = new NaturalFraction(1, 2);
            var testFraction2 = new NaturalFraction(2, 4);

            var testIsCorrect = testFraction1 == testFraction2;

            Assert.True(testIsCorrect);
        }
        [Fact]
        public void Comparison_ShouldReturnFalse_ForSameValues_ThroughOperator()
        {
            var testFraction1 = new NaturalFraction(1, 2);
            var testFraction2 = new NaturalFraction(2, 4);

            var testIsCorrect = testFraction1 != testFraction2;

            Assert.False(testIsCorrect);
        }
        [Fact]
        public void Comparison_ShouldReturnFalse_ForDifferentValues_ThroughMethod()
        {
            var testFraction1 = new NaturalFraction(1, 2);
            var testFraction2 = new NaturalFraction(3, 5);

            var testIsCorrect = testFraction1.Equals(testFraction2);

            Assert.False(testIsCorrect);
        }

        [Fact]
        public void Comparison_ShouldReturnTrue_ForSameValues_ThroughMethod()
        {
            var testFraction1 = new NaturalFraction(1, 2);
            var testFraction2 = new NaturalFraction(2, 4);

            var testIsCorrect = testFraction1.Equals(testFraction2);

            Assert.True(testIsCorrect);
        }
        [Fact]
        public void Define_ShouldBeCorrect_WhenNegative()
        {
            var testFraction = new NaturalFraction(1, -2);

            var testIsCorrect = testFraction == new NaturalFraction(-1, 2);

            Assert.True(testIsCorrect);
        }
        [Fact]
        public void Sum_ShouldBeCorrect()
        {
            var testFraction1 = new NaturalFraction(1, 2);
            var testFraction2 = new NaturalFraction(3, 5);

            var resultFraction = testFraction1 + testFraction2;

            var testIsCorrect = resultFraction == new NaturalFraction(11, 10);

            Assert.True(testIsCorrect);
        }
        [Fact]
        public void Dif_ShouldBeCorrect()
        {
            var testFraction1 = new NaturalFraction(1, 2);
            var testFraction2 = new NaturalFraction(3, 5);

            var resultFraction = testFraction1 - testFraction2;

            var testIsCorrect = resultFraction == new NaturalFraction(-1, 10);

            Assert.True(testIsCorrect);
        }
        [Fact]
        public void Prod_ShouldBeCorrect()
        {
            var testFraction1 = new NaturalFraction(1, 2);
            var testFraction2 = new NaturalFraction(3, 5);

            var resultFraction = testFraction1 * testFraction2;

            var testIsCorrect = resultFraction == new NaturalFraction(3, 10);

            Assert.True(testIsCorrect);
        }
        [Fact]
        public void ProdInt_ShouldBeCorrect()
        {
            var testFraction1 = new NaturalFraction(1, 2);
            var testInt = 5;

            var resultFraction = testFraction1 * testInt;

            var testIsCorrect = resultFraction == new NaturalFraction(5, 2);

            Assert.True(testIsCorrect);
        }
        [Fact]
        public void Div_ShouldBeCorrect()
        {
            var testFraction1 = new NaturalFraction(1, 2);
            var testFraction2 = new NaturalFraction(3, 5);

            var resultFraction = testFraction1 / testFraction2;

            var testIsCorrect = resultFraction == new NaturalFraction(5, 6);

            Assert.True(testIsCorrect);
        }
        [Fact]
        public void DivInt_ShouldBeCorrect()
        {
            var testFraction1 = new NaturalFraction(1, 2);
            var testInt = 5;

            var resultFraction = testFraction1 / testInt;

            var testIsCorrect = resultFraction == new NaturalFraction(1, 10);

            Assert.True(testIsCorrect);
        }
        [Fact]
        public void Pow_ShouldBeCorrect()
        {
            var testFraction = new NaturalFraction(1, 2);
            var testInt = 5;

            var resultFraction = testFraction.Pow(testInt);

            var testIsCorrect = resultFraction == new NaturalFraction(1, 32);

            Assert.True(testIsCorrect);
        }
        [Fact]
        public void GetHashCode_ShouldReturnHashCode()
        {
            var testFraction = new NaturalFraction(1, 2);
            var testFractionHashCode = testFraction.GetHashCode();

            var testTrue = testFractionHashCode is int;

            Assert.True(testTrue);
        }
    }
}
