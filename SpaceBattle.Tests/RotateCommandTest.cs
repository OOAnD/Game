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
            Assert.Equal(resultAngle, angle);
        }
        [Fact]
        public void Execute_ShouldThrowException_WhenNoAngle()
        {
            // Act
            var rotatingObject = new Mock<IRotating>();
            var testRotate = new RotateCommand(rotatingObject.Object);
            var angleVelocity = new Angle(45);
            rotatingObject.Setup(obj => obj.Angle).Throws(new Exception());
            rotatingObject.Setup(obj => obj.Velocity).Returns(angleVelocity);

            // Act & Assert
            Assert.ThrowsAny<Exception>(testRotate.Execute);
        }
        [Fact]
        public void Execute_ShouldThrowException_WhenNoVelocity()
        {
            // Act
            var rotatingObject = new Mock<IRotating>();
            var testRotate = new RotateCommand(rotatingObject.Object);
            var angle = new Angle(45);
            rotatingObject.Setup(obj => obj.Angle).Returns(angle);
            rotatingObject.Setup(obj => obj.Velocity).Throws(new Exception());

            // Act & Assert
            Assert.ThrowsAny<Exception>(testRotate.Execute);
        }
        [Fact]
        public void Execute_ShouldThrowException_WhenCantRotate()
        {
            // Act
            var rotatingObject = new Mock<IRotating>();
            var testRotate = new RotateCommand(rotatingObject.Object);
            var angleVelocity = new Angle(45);
            rotatingObject.SetupGet(obj => obj.Angle).Throws(new Exception());
            rotatingObject.SetupGet(obj => obj.Velocity).Returns(angleVelocity);

            // Act & Assert
            Assert.Throws<Exception>(testRotate.Execute);
        }
    }
}
