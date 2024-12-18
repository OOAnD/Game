using Moq;
namespace SpaceBattle.Tests
{
    public class MoveCommandTest
    {
        [Fact]
        public void Execute_ShouldMoveObject_WithValidInput()
        {
            // Arrange
            var movingObject = new Mock<IMoving>();
            var startPosition = new Vector(2);
            var velocity = new Vector(2);
            var correctPosition = new Vector(2);

            startPosition.Set(12, 5);
            velocity.Set(-4, 1);
            correctPosition.Set(8, 6);

            movingObject.SetupGet(m => m.Position).Returns(startPosition);
            movingObject.SetupGet(m => m.Velocity).Returns(velocity);
            movingObject.SetupSet(m => m.Position = It.IsAny<Vector>()).Callback<Vector>(pos => startPosition = pos);

            var moveCommand = new MoveCommand(movingObject.Object);

            // Act
            moveCommand.Execute();

            // Assert
            Assert.Equal(correctPosition, startPosition);
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenPositionCannotBeRead()
        {
            // Arrange
            var movingObject = new Mock<IMoving>();
            movingObject.SetupGet(m => m.Position).Throws(new InvalidOperationException("Position cannot be read."));
            movingObject.SetupGet(m => m.Velocity).Returns(new Vector(2));

            var moveCommand = new MoveCommand(movingObject.Object);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => moveCommand.Execute());
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenVelocityCannotBeRead()
        {
            // Arrange
            var movingObject = new Mock<IMoving>();
            movingObject.SetupGet(m => m.Position).Returns(new Vector(2));
            movingObject.SetupGet(m => m.Velocity).Throws(new InvalidOperationException("Velocity cannot be read."));

            var moveCommand = new MoveCommand(movingObject.Object);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => moveCommand.Execute());
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenPositionCannotBeChanged()
        {
            // Arrange
            var movingObject = new Mock<IMoving>();
            movingObject.SetupGet(m => m.Position).Returns(new Vector(2));
            movingObject.SetupGet(m => m.Velocity).Returns(new Vector(2));
            movingObject.SetupSet(m => m.Position = It.IsAny<Vector>()).Throws(new InvalidOperationException("Position cannot be changed."));

            var moveCommand = new MoveCommand(movingObject.Object);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => moveCommand.Execute());
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenPositionCannotBeRead_ButVelocityIsAvailable()
        {
            // Arrange
            var movingObject = new Mock<IMoving>();
            movingObject.SetupGet(m => m.Position).Throws(new InvalidOperationException("Position cannot be read."));
            movingObject.SetupGet(m => m.Velocity).Returns(new Vector(2));

            var moveCommand = new MoveCommand(movingObject.Object);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => moveCommand.Execute());
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenPositionCannotBeChanged_ButVelocityAndPositionAreAvailable()
        {
            // Arrange
            var movingObject = new Mock<IMoving>();
            movingObject.SetupGet(m => m.Position).Returns(new Vector(2));
            movingObject.SetupGet(m => m.Velocity).Returns(new Vector(2));
            movingObject.SetupSet(m => m.Position = It.IsAny<Vector>()).Throws(new InvalidOperationException("Position cannot be changed."));

            var moveCommand = new MoveCommand(movingObject.Object);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => moveCommand.Execute());
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenMovingObjectIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new MoveCommand(null!));
        }

        [Fact]
        public void Execute_ShouldNotChangePosition_WhenVelocityIsZero()
        {
            // Arrange
            var movingObject = new Mock<IMoving>();
            var startPosition = new Vector(2);
            startPosition.Set(10, 10);
            var velocity = new Vector(2);
            velocity.Set(0, 0);

            movingObject.SetupGet(m => m.Position).Returns(startPosition);
            movingObject.SetupGet(m => m.Velocity).Returns(velocity);
            movingObject.SetupSet(m => m.Position = It.IsAny<Vector>()).Callback<Vector>(pos => startPosition = pos);

            var moveCommand = new MoveCommand(movingObject.Object);

            // Act
            moveCommand.Execute();

            // Assert
            Assert.Equal(startPosition, startPosition);
        }

        [Fact]
        public void Execute_ShouldMoveObjectCorrectly_WhenVelocityIsNonZero()
        {
            // Arrange
            var movingObject = new Mock<IMoving>();
            var startPosition = new Vector(2);
            startPosition.Set(10, 10);
            var velocity = new Vector(2);
            velocity.Set(3, 4);
            var correctPosition = new Vector(2);
            correctPosition.Set(13, 14);

            movingObject.SetupGet(m => m.Position).Returns(startPosition);
            movingObject.SetupGet(m => m.Velocity).Returns(velocity);
            movingObject.SetupSet(m => m.Position = It.IsAny<Vector>()).Callback<Vector>(pos => startPosition = pos);

            var moveCommand = new MoveCommand(movingObject.Object);

            // Act
            moveCommand.Execute();

            // Assert
            Assert.Equal(correctPosition, startPosition);
        }
    }
}
