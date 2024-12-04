using Moq;

namespace SpaceBattle.Tests
{
    public class StartMoveCommandTest
    {
        [Fact]
        public void Execute_ShouldCorrectBuildLongCommand()
        {
            // Arrange
            var queue = new Mock<ICommandQueue>();
            var moveCommand = new Mock<ICommand>();
            var repeatCommand = new Mock<ICommand>();
            var boxCommand = new Mock<ICommandBox>();
            var macroCommand = new Mock<ICommand>();
            var movingObject = new Mock<IMoving>();
            var gameObject = new Mock<IDictionary<string, object>>();

            var providerStrategy = new Mock<Func<string, object[], object>>();

            providerStrategy
                .Setup(p => p("Commands.Move", It.Is<object[]>(arg => arg[0] == movingObject.Object)))
                .Returns(moveCommand.Object);
            providerStrategy
                .Setup(p => p("Commands.Box", It.IsAny<object[]>()))
                .Returns(boxCommand.Object);
            providerStrategy
                .Setup(p => p("Commands.Repeat", It.Is<object[]>(arg => arg[0] == queue.Object && arg[1] == boxCommand.Object)))
                .Returns(repeatCommand.Object);
            providerStrategy
                .Setup(p => p("Commands.Macro", It.Is<object[]>(arg => arg[0] == moveCommand.Object && arg[1] == repeatCommand.Object)))
                .Returns(macroCommand.Object);

            var startCommand = new StartMoveCommand(queue.Object, movingObject.Object, gameObject.Object, providerStrategy.Object);

            // Act
            startCommand.Execute();

            // Assert
            providerStrategy.Verify(p => p("Commands.Move", new object[] { movingObject.Object }));
            providerStrategy.Verify(p => p("Commands.Box", new object[] { }));
            providerStrategy.Verify(p => p("Commands.Repeat", new object[] { queue.Object, boxCommand.Object }));
            providerStrategy.Verify(p => p("Commands.Macro", new object[] { moveCommand.Object, repeatCommand.Object }));

            boxCommand.Verify(bc => bc.Set(macroCommand.Object));
        }

        [Fact]
        public void Execute_ShouldCorrectPutLongCommandIntoQueue()
        {
            // Arrange
            var queue = new Mock<ICommandQueue>();
            var moveCommand = new Mock<ICommand>();
            var repeatCommand = new Mock<ICommand>();
            var boxCommand = new Mock<ICommandBox>();
            var macroCommand = new Mock<ICommand>();
            var movingObject = new Mock<IMoving>();
            var gameObject = new Mock<IDictionary<string, object>>();

            var providerStrategy = new Mock<Func<string, object[], object>>();

            providerStrategy
                .Setup(p => p("Commands.Move", It.Is<object[]>(arg => arg[0] == movingObject.Object)))
                .Returns(moveCommand.Object);
            providerStrategy
                .Setup(p => p("Commands.Box", It.IsAny<object[]>()))
                .Returns(boxCommand.Object);
            providerStrategy
                .Setup(p => p("Commands.Repeat", It.Is<object[]>(arg => arg[0] == queue.Object && arg[1] == boxCommand.Object)))
                .Returns(repeatCommand.Object);
            providerStrategy
                .Setup(p => p("Commands.Macro", It.Is<object[]>(arg => arg[0] == moveCommand.Object && arg[1] == repeatCommand.Object)))
                .Returns(macroCommand.Object);

            var startCommand = new StartMoveCommand(queue.Object, movingObject.Object, gameObject.Object, providerStrategy.Object);

            // Act
            startCommand.Execute();

            // Assert
            queue.Verify(q => q.Enqueue(boxCommand.Object), Times.Once);
        }

        [Fact]
        public void Execute_ShouldPutCommandBoxIntoGameObject()
        {
            // Arrange
            var queue = new Mock<ICommandQueue>();
            var moveCommand = new Mock<ICommand>();
            var repeatCommand = new Mock<ICommand>();
            var boxCommand = new Mock<ICommandBox>();
            var macroCommand = new Mock<ICommand>();
            var movingObject = new Mock<IMoving>();
            var gameObject = new Mock<IDictionary<string, object>>();

            var providerStrategy = new Mock<Func<string, object[], object>>();

            providerStrategy
                .Setup(p => p("Commands.Move", It.Is<object[]>(arg => arg[0] == movingObject.Object)))
                .Returns(moveCommand.Object);
            providerStrategy
                .Setup(p => p("Commands.Box", It.IsAny<object[]>()))
                .Returns(boxCommand.Object);
            providerStrategy
                .Setup(p => p("Commands.Repeat", It.Is<object[]>(arg => arg[0] == queue.Object && arg[1] == boxCommand.Object)))
                .Returns(repeatCommand.Object);
            providerStrategy
                .Setup(p => p("Commands.Macro", It.Is<object[]>(arg => arg[0] == moveCommand.Object && arg[1] == repeatCommand.Object)))
                .Returns(macroCommand.Object);

            var startCommand = new StartMoveCommand(queue.Object, movingObject.Object, gameObject.Object, providerStrategy.Object);

            // Act
            startCommand.Execute();

            // Assert
            gameObject.Verify(g => g.Add("Movement", boxCommand.Object));
        }

        [Fact]
        public void Define_ShouldThrow_WhenQueueIsNull()
        {
            // Arrange
            var queue = new Mock<ICommandQueue>();
            var providerStrategy = new Mock<Func<string, object[], object>>();
            var movingObject = new Mock<IMoving>();
            var gameObject = new Mock<IDictionary<string, object>>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new StartMoveCommand(null!, movingObject.Object, gameObject.Object, providerStrategy.Object));
        }

        [Fact]
        public void Define_ShouldThrow_WhenMovingObjectIsNull()
        {
            // Arrange
            var queue = new Mock<ICommandQueue>();
            var providerStrategy = new Mock<Func<string, object[], object>>();
            var movingObject = new Mock<IMoving>();
            var gameObject = new Mock<IDictionary<string, object>>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new StartMoveCommand(queue.Object, null!, gameObject.Object, providerStrategy.Object));
        }

        [Fact]
        public void Define_ShouldThrow_WhenGameObjectIsNull()
        {
            // Arrange
            var queue = new Mock<ICommandQueue>();
            var providerStrategy = new Mock<Func<string, object[], object>>();
            var movingObject = new Mock<IMoving>();
            var gameObject = new Mock<IDictionary<string, object>>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new StartMoveCommand(queue.Object, movingObject.Object, null!, providerStrategy.Object));
        }

        [Fact]
        public void Define_ShouldThrow_WhenProviderStrategyIsNull()
        {
            // Arrange
            var queue = new Mock<ICommandQueue>();
            var providerStrategy = new Mock<Func<string, object[], object>>();
            var movingObject = new Mock<IMoving>();
            var gameObject = new Mock<IDictionary<string, object>>();

            // Act & Assert 
            Assert.Throws<ArgumentNullException>(() => new StartMoveCommand(queue.Object, movingObject.Object, gameObject.Object, null!));
        }

        [Fact]
        public void Execute_ShouldThrow_WhenGettingMoveCommandThrow()
        {
            // Arrange
            var queue = new Mock<ICommandQueue>();
            var moveCommand = new Mock<ICommand>();
            var repeatCommand = new Mock<ICommand>();
            var boxCommand = new Mock<ICommandBox>();
            var macroCommand = new Mock<ICommand>();
            var movingObject = new Mock<IMoving>();
            var gameObject = new Mock<IDictionary<string, object>>();

            var providerStrategy = new Mock<Func<string, object[], object>>();

            providerStrategy
                .Setup(p => p("Commands.Move", It.Is<object[]>(arg => arg[0] == movingObject.Object)))
                .Throws(new Exception());
            providerStrategy
                .Setup(p => p("Commands.Box", It.IsAny<object[]>()))
                .Returns(boxCommand.Object);
            providerStrategy
                .Setup(p => p("Commands.Repeat", It.Is<object[]>(arg => arg[0] == queue.Object && arg[1] == boxCommand.Object)))
                .Returns(repeatCommand.Object);
            providerStrategy
                .Setup(p => p("Commands.Macro", It.Is<object[]>(arg => arg[0] == moveCommand.Object && arg[1] == repeatCommand.Object)))
                .Returns(macroCommand.Object);

            var startCommand = new StartMoveCommand(queue.Object, movingObject.Object, gameObject.Object, providerStrategy.Object);

            // Act & Assert
            Assert.Throws<Exception>(startCommand.Execute);
        }

        [Fact]
        public void Execute_ShouldThrow_WhenGettingBoxCommandThrow()
        {
            // Arrange
            var queue = new Mock<ICommandQueue>();
            var moveCommand = new Mock<ICommand>();
            var repeatCommand = new Mock<ICommand>();
            var boxCommand = new Mock<ICommandBox>();
            var macroCommand = new Mock<ICommand>();
            var movingObject = new Mock<IMoving>();
            var gameObject = new Mock<IDictionary<string, object>>();

            var providerStrategy = new Mock<Func<string, object[], object>>();

            providerStrategy
                .Setup(p => p("Commands.Move", It.Is<object[]>(arg => arg[0] == movingObject.Object)))
                .Returns(moveCommand.Object);
            providerStrategy
                .Setup(p => p("Commands.Box", It.IsAny<object[]>()))
                .Throws(new Exception());
            providerStrategy
                .Setup(p => p("Commands.Repeat", It.Is<object[]>(arg => arg[0] == queue.Object && arg[1] == boxCommand.Object)))
                .Returns(repeatCommand.Object);
            providerStrategy
                .Setup(p => p("Commands.Macro", It.Is<object[]>(arg => arg[0] == moveCommand.Object && arg[1] == repeatCommand.Object)))
                .Returns(macroCommand.Object);

            var startCommand = new StartMoveCommand(queue.Object, movingObject.Object, gameObject.Object, providerStrategy.Object);

            // Act & Assert
            Assert.Throws<Exception>(startCommand.Execute);
        }

        [Fact]
        public void Execute_ShouldThrow_WhenGettingRepeatCommandThrow()
        {
            // Arrange
            var queue = new Mock<ICommandQueue>();
            var moveCommand = new Mock<ICommand>();
            var repeatCommand = new Mock<ICommand>();
            var boxCommand = new Mock<ICommandBox>();
            var macroCommand = new Mock<ICommand>();
            var movingObject = new Mock<IMoving>();
            var gameObject = new Mock<IDictionary<string, object>>();

            var providerStrategy = new Mock<Func<string, object[], object>>();

            providerStrategy
                .Setup(p => p("Commands.Move", It.Is<object[]>(arg => arg[0] == movingObject.Object)))
                .Returns(moveCommand.Object);
            providerStrategy
                .Setup(p => p("Commands.Box", It.IsAny<object[]>()))
                .Returns(boxCommand.Object);
            providerStrategy
                .Setup(p => p("Commands.Repeat", It.Is<object[]>(arg => arg[0] == queue.Object && arg[1] == boxCommand.Object)))
                .Throws(new Exception());
            providerStrategy
                .Setup(p => p("Commands.Macro", It.Is<object[]>(arg => arg[0] == moveCommand.Object && arg[1] == repeatCommand.Object)))
                .Returns(macroCommand.Object);

            var startCommand = new StartMoveCommand(queue.Object, movingObject.Object, gameObject.Object, providerStrategy.Object);

            // Act & Assert
            Assert.Throws<Exception>(startCommand.Execute);
        }

        [Fact]
        public void Execute_ShouldThrow_WhenGettingMacroCommandThrow()
        {
            // Arrange
            var queue = new Mock<ICommandQueue>();
            var moveCommand = new Mock<ICommand>();
            var repeatCommand = new Mock<ICommand>();
            var boxCommand = new Mock<ICommandBox>();
            var macroCommand = new Mock<ICommand>();
            var movingObject = new Mock<IMoving>();
            var gameObject = new Mock<IDictionary<string, object>>();

            var providerStrategy = new Mock<Func<string, object[], object>>();

            providerStrategy
                .Setup(p => p("Commands.Move", It.Is<object[]>(arg => arg[0] == movingObject.Object)))
                .Returns(moveCommand.Object);
            providerStrategy
                .Setup(p => p("Commands.Box", It.IsAny<object[]>()))
                .Returns(boxCommand.Object);
            providerStrategy
                .Setup(p => p("Commands.Repeat", It.Is<object[]>(arg => arg[0] == queue.Object && arg[1] == boxCommand.Object)))
                .Returns(repeatCommand.Object);
            providerStrategy
                .Setup(p => p("Commands.Macro", It.Is<object[]>(arg => arg[0] == moveCommand.Object && arg[1] == repeatCommand.Object)))
                .Throws(new Exception());

            var startCommand = new StartMoveCommand(queue.Object, movingObject.Object, gameObject.Object, providerStrategy.Object);

            // Act & Assert
            Assert.Throws<Exception>(startCommand.Execute);
        }

        [Fact]
        public void Execute_ShouldThrow_WhenSettingBoxCommandThrow()
        {
            // Arrange
            var queue = new Mock<ICommandQueue>();
            var moveCommand = new Mock<ICommand>();
            var repeatCommand = new Mock<ICommand>();
            var boxCommand = new Mock<ICommandBox>();
            var macroCommand = new Mock<ICommand>();
            var movingObject = new Mock<IMoving>();
            var gameObject = new Mock<IDictionary<string, object>>();

            var providerStrategy = new Mock<Func<string, object[], object>>();

            providerStrategy
                .Setup(p => p("Commands.Move", It.Is<object[]>(arg => arg[0] == movingObject.Object)))
                .Returns(moveCommand.Object);
            providerStrategy
                .Setup(p => p("Commands.Box", It.IsAny<object[]>()))
                .Returns(boxCommand.Object);
            providerStrategy
                .Setup(p => p("Commands.Repeat", It.Is<object[]>(arg => arg[0] == queue.Object && arg[1] == boxCommand.Object)))
                .Returns(repeatCommand.Object);
            providerStrategy
                .Setup(p => p("Commands.Macro", It.Is<object[]>(arg => arg[0] == moveCommand.Object && arg[1] == repeatCommand.Object)))
                .Returns(macroCommand.Object);

            boxCommand.Setup(b => b.Set(It.IsAny<ICommand>())).Throws(new Exception());

            var startCommand = new StartMoveCommand(queue.Object, movingObject.Object, gameObject.Object, providerStrategy.Object);

            // Act & Assert
            Assert.Throws<Exception>(startCommand.Execute);
        }

        [Fact]
        public void Execute_ShouldThrow_WhenEnqueueThrow()
        {
            // Arrange
            var queue = new Mock<ICommandQueue>();
            var moveCommand = new Mock<ICommand>();
            var repeatCommand = new Mock<ICommand>();
            var boxCommand = new Mock<ICommandBox>();
            var macroCommand = new Mock<ICommand>();
            var movingObject = new Mock<IMoving>();
            var gameObject = new Mock<IDictionary<string, object>>();

            var providerStrategy = new Mock<Func<string, object[], object>>();

            providerStrategy
                .Setup(p => p("Commands.Move", It.Is<object[]>(arg => arg[0] == movingObject.Object)))
                .Returns(moveCommand.Object);
            providerStrategy
                .Setup(p => p("Commands.Box", It.IsAny<object[]>()))
                .Returns(boxCommand.Object);
            providerStrategy
                .Setup(p => p("Commands.Repeat", It.Is<object[]>(arg => arg[0] == queue.Object && arg[1] == boxCommand.Object)))
                .Returns(repeatCommand.Object);
            providerStrategy
                .Setup(p => p("Commands.Macro", It.Is<object[]>(arg => arg[0] == moveCommand.Object && arg[1] == repeatCommand.Object)))
                .Returns(macroCommand.Object);

            queue.Setup(q => q.Enqueue(It.IsAny<ICommand>())).Throws(new Exception());

            var startCommand = new StartMoveCommand(queue.Object, movingObject.Object, gameObject.Object, providerStrategy.Object);

            // Act & Assert
            Assert.Throws<Exception>(startCommand.Execute);
        }
    }
}
