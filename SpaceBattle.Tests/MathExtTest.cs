namespace SpaceBattle.Tests
{
    public class MathExtTest
    {
        [Fact]
        public void Sin_ShouldCorrectCompute()
        {
            // Arrange
            var fr = new NaturalFraction(1, 8);

            // Act
            var sin = MathExt.Sin(fr);

            // Assert
            var sinDouble = sin.Numerator / (double)sin.Denominator;
            Assert.True(Math.Abs(sinDouble - 0.7071) < 0.001);
        }

        [Fact]
        public void Cos_ShouldCorrectCompute()
        {
            // Arrange
            var fr = new NaturalFraction(1, 8);

            // Act
            var cos = MathExt.Cos(fr);

            // Assert
            var sinDouble = cos.Numerator / (double)cos.Denominator;
            Assert.True(Math.Abs(sinDouble - 0.7071) < 0.001);
        }
    }
}
