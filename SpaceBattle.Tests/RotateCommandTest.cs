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
            var angleVelocity = new Angle(45);
            var resultAngle = new Angle(90);
            var testRotate = new RotateCommand(rotatingObject.Object);

            rotatingObject.Setup(obj => obj.Angle).Returns(angle);
            rotatingObject.Setup(obj => obj.Velocity).Returns(angleVelocity);

            rotatingObject.SetupSet(obj => obj.Angle = It.IsAny<Angle>()).Callback<Angle>(a => angle = a);
            //Act
            testRotate.Execute();
            //Assert
            Assert.True(resultAngle == angle);
        }
        [Fact]
        public void Execute_ShouldThrowException_WhenNoAngle()
        {
            var rotatingObject = new Mock<IRotating>();
            var testRotate = new RotateCommand(rotatingObject.Object);
            var angleVelocity = new Angle(45);
            rotatingObject.Setup(obj => obj.Velocity).Returns(angleVelocity);

            Assert.ThrowsAny<Exception>(testRotate.Execute);
        }
        [Fact]
        public void Execute_ShouldThrowException_WhenNoVelocity()
        {
            var rotatingObject = new Mock<IRotating>();
            var testRotate = new RotateCommand(rotatingObject.Object);
            var angle = new Angle(45);
            rotatingObject.Setup(obj => obj.Angle).Returns(angle);

            Assert.ThrowsAny<Exception>(testRotate.Execute);
        }
        [Fact]
        public void Execute_ShouldThrowException_WhenCantRotate()
        {
            var rotatingObject = new Mock<IRotating>();
            var testRotate = new RotateCommand(rotatingObject.Object);

            rotatingObject.SetupGet(obj => obj.Angle).Throws(new Exception());

            Assert.Throws<Exception>(testRotate.Execute);
        }
    }
}
