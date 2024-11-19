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

            startPosition.Set(1, 2);
            velocity.Set(3, 4);
            correctPosition.Set(4, 6);

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
        public void Execute_ShouldNotMoveObject_WithZeroVelocity()
        {
            // Arrange
            var movingObject = new Mock<IMoving>();
            var startPosition = new Vector(2);
            var velocity = new Vector(2);

            startPosition.Set(1, 2);
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
        public void Execute_ShouldMoveObjectBackwards_WithNegativeVelocity()
        {
            // Arrange
            var movingObject = new Mock<IMoving>();
            var startPosition = new Vector(2);
            var velocity = new Vector(2);
            var correctPosition = new Vector(2);

            startPosition.Set(1, 2);
            velocity.Set(-3, -4);
            correctPosition.Set(-2, -2);

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
        public void Execute_ShouldThrowException_WithDifferentDimensions()
        {
            // Arrange
            var movingObject = new Mock<IMoving>();
            var startPosition = new Vector(2);
            var velocity = new Vector(3);

            startPosition.Set(1, 2);
            velocity.Set(3, 4, 5);

            movingObject.SetupGet(m => m.Position).Returns(startPosition);
            movingObject.SetupGet(m => m.Velocity).Returns(velocity);

            var moveCommand = new MoveCommand(movingObject.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => moveCommand.Execute());
        }

        [Fact]
        public void Execute_ShouldNotMoveObject_WithZeroDimension()
        {
            // Arrange
            var movingObject = new Mock<IMoving>();
            var startPosition = new Vector(0);
            var velocity = new Vector(0);

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
    }
}
