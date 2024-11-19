using Moq;

namespace SpaceBattle.Tests{

    public class RotateCommandTest{
        [Fact]
        public void Execute_ShouldRotateObject(){
            
            //Arrange
            var rotatingObject = new Mock<IRotating>();
            var angle = new Angle(45);
            var angleVelocity = new Angle(90);
            var resultAngle = new Angle(135);
            var testRotate = new RotateCommand(rotatingObject.Object);

            rotatingObject.Setup(obj => obj.GetAngle()).Returns(angle);
            rotatingObject.Setup(obj => obj.GetVelocity()).Returns(angleVelocity);

            //Act
            testRotate.Execute();

            //Assert
            rotatingObject.VerifySet(ro => ro.SetAngle(resultAngle));
            
        }
    }

}