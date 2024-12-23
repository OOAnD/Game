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

            // Создаем начальную позицию и скорость
            var startPosition = new Vector(12, 5);
            var velocity = new Vector(-4, 1);

            // Ожидаемая позиция после выполнения команды
            var correctPosition = new Vector(8, 6);

            // Настройка моков
            movingObject.SetupGet(m => m.Position).Returns(startPosition);
            movingObject.SetupGet(m => m.Velocity).Returns(velocity);
            movingObject.SetupSet(m => m.Position = It.IsAny<Vector>()).Callback<Vector>(pos => startPosition = pos);

            // Создаем команду MoveCommand
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
    }
}
