using Moq;

namespace SpaceBattle.Tests
{

    public class RotateCommandTest
    {
        [Fact]
        public void Execute_ShouldRotateObject()
        {

            //Arrange
            var rotatingObject = new Mock<IRotating>();
            var angle = new Angle(45);
            var angleVelocity = new Angle(90);
            var resultAngle = new Angle(135);
            var testRotate = new RotateCommand(rotatingObject.Object);

            rotatingObject.Setup(obj => obj.Angle).Returns(angle);
            rotatingObject.Setup(obj => obj.Velocity).Returns(angleVelocity);

            //Act
            testRotate.Execute();

            //Assert
            rotatingObject.VerifySet(ro => ro.Angle = resultAngle);

        }
        [Fact]
        public void Define_ValueShouldBeGreaterThanZero()
        {
            //Arrange & Act
            var angle = new Angle(-100000, 360);
            var isPositive = angle.Value >= 0;

            //Assert
            Assert.True(isPositive);
        }
        [Fact]
        public void Define_ValueShouldBeLessThanBorder()
        {
            //Arrange & Act
            var angle = new Angle(100000, 360);
            var isLessThanBorder = angle.Value < angle.Border;

            //Assert
            Assert.True(isLessThanBorder);
        }
        [Fact]
        public void Define_BorderShouldBeGreaterThanZero()
        {
            //Arrange & Act & Assert
            Assert.Throws<Exception>(() => new Angle(90, -360));
        }
        [Fact]
        public void Sum_ShouldBeGreaterThanZero()
        {
            //Arrange
            var angle1 = new Angle(100, 120);
            var angle2 = new Angle(120, 150);

            //Act
            var angleSum = angle1 + angle2;
            var isGreaterThanZero = angleSum.Value >= 0;

            //Assert
            Assert.True(isGreaterThanZero);
        }
        [Fact]
        public void Sum_ShouldBeLessThanBorder()
        {
            //Arrange
            var angle1 = new Angle(100, 120);
            var angle2 = new Angle(120, 150);
            //Act
            var angleSum = angle1 + angle2;
            var isLessThanBorder = angleSum.Value < angleSum.Border;

            //Assert
            Assert.True(isLessThanBorder);

        }
    }
}
