namespace SpaceBattle.Tests
{
    public class AngleClassTest
    {
        [Fact]
        public void Define_NumeratorShouldBeLessThanDenominator()
        {
            var testAngle = new Angle(143);

            Assert.True(testAngle.Numerator < Angle.Denominator);
        }
        [Fact]
        public void Set_ShouldBeCorrect()
        {
            var testAngle = new Angle(0);
            testAngle.Numerator = 143;
            Assert.True(testAngle.Numerator < Angle.Denominator);
        }
        [Fact]
        public void Define_NumeratorShouldBePositive()
        {
            var testAngle = new Angle(-143);

            Assert.True(testAngle.Numerator >= 0);
        }
        [Fact]
        public void Sum_NumeratorShouldBeEqualToSumWithinZeroToDenominator()
        {
            var testAngle1 = new Angle(5);
            var testAngle2 = new Angle(7);

            var resultAngle = testAngle1 + testAngle2;
            var testTrue = resultAngle == new Angle(4);

            Assert.True(testTrue);
        }
        [Fact]
        public void Dif_NumeratorShouldBeEqualToDifWithinZeroToDenominator()
        {
            var testAngle1 = new Angle(5);
            var testAngle2 = new Angle(7);

            var resultAngle = testAngle1 - testAngle2;
            var testTrue = resultAngle == new Angle(6);

            Assert.True(testTrue);
        }
        [Fact]
        public void Comparison_ShouldReturnTrue_ForSameValues_ThroughMethod()
        {
            var testAngle1 = new Angle(15);
            var testAngle2 = new Angle(23);

            var testTrue = Equals(testAngle1, testAngle2);

            Assert.True(testTrue);
        }
        [Fact]
        public void Comparison_ShouldReturnTrue_ForSameValues_ThroughOperator()
        {
            var testAngle1 = new Angle(15);
            var testAngle2 = new Angle(23);

            var testTrue = testAngle1 == testAngle2;

            Assert.True(testTrue);
        }
        [Fact]
        public void Comparison_ShouldReturnFalse_ForDifferentValues_ThroughMethod()
        {
            var testAngle1 = new Angle(1);
            var testAngle2 = new Angle(2);

            var testTrue = Equals(testAngle1, testAngle2);

            Assert.False(testTrue);
        }
        [Fact]
        public void Comparison_ShouldReturnTrue_ForDifferentValues_ThroughOperator()
        {
            var testAngle1 = new Angle(1);
            var testAngle2 = new Angle(2);

            var testTrue = testAngle1 != testAngle2;

            Assert.True(testTrue);
        }
        [Fact]
        public void ToNaturalFraction_ShouldReturnNaturalFraction()
        {
            var testAngle = new Angle(1);
            var testNaturalFraction = testAngle.ToNaturalFraction();

            Assert.IsType<NaturalFraction>(testNaturalFraction);
        }
        [Fact]
        public void Sin_ShouldReturnSinWithinError()
        {
            var testAngle = new Angle(4);

            var testDouble = testAngle.ToNaturalFraction().ToDouble();

            var testSin = testAngle.Sin().ToDouble();

            Assert.Equal(Math.Sin(testDouble), testSin, 1e-4);
        }
        [Fact]
        public void Cos_ShouldReturnCosWithinError()
        {
            var testAngle = new Angle(0);

            var testDouble = testAngle.ToNaturalFraction().ToDouble();

            var testCos = testAngle.Cos().ToDouble();

            Assert.Equal(Math.Cos(testDouble), testCos, 1e-4);
        }
        [Fact]
        public void GetHashCode_ShouldReturnHashCode()
        {
            var testAngle = new Angle(1);
            var testAngleHashCode = testAngle.GetHashCode();

            Assert.IsType<int>(testAngleHashCode);
        }
    }
}
