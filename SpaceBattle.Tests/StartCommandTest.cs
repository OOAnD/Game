using Moq;

namespace SpaceBattle.Tests
{
    public class StartCommandTest
    {
        [Fact]
        public void Execute_ShouldCorrectBuildCommandAndPutIntoQueue()
        {
            // Arrange
            var anyCommand = new Mock<ICommand>();
            var commandQueue = new Mock<ICommandQueue>();
            var commandBox = new Mock<ICommandBox>();
            var macroCommand = new Mock<IMacroCommand>();
            var commandSetToQueue = new Mock<ICommand>();

            var utilCommandFactory = new Mock<IUtilCommandFactory>();

            utilCommandFactory.Setup(u => u.CreateQueueSetter(commandBox.Object)).Returns(commandSetToQueue.Object);
            utilCommandFactory.Setup(u => u.CreateCommandBox()).Returns(commandBox.Object);
            utilCommandFactory.Setup(u => u.CreateMacroCommand()).Returns(macroCommand.Object);

            var startCommand = new StartCommand(utilCommandFactory.Object, anyCommand.Object, commandQueue.Object);

            // Act
            startCommand.Execute();

            //Assert
            macroCommand.Verify(cwns => cwns.Put(commandSetToQueue.Object));
            macroCommand.Verify(cwns => cwns.Put(anyCommand.Object));
            commandBox.Verify(cb => cb.Set(macroCommand.Object));
            commandQueue.Verify(cq => cq.Put(commandBox.Object));
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenCommandBoxCreationFails()
        {
            // Arrange
            var anyCommand = new Mock<ICommand>();
            var commandQueue = new Mock<ICommandQueue>();
            var utilCommandFactory = new Mock<IUtilCommandFactory>();

            utilCommandFactory.Setup(u => u.CreateCommandBox()).Throws(new Exception("CommandBox creation failed"));

            var startCommand = new StartCommand(utilCommandFactory.Object, anyCommand.Object, commandQueue.Object);

            // Act & Assert
            Assert.Throws<Exception>(() => startCommand.Execute());
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenMacroCommandCreationFails()
        {
            // Arrange
            var anyCommand = new Mock<ICommand>();
            var commandQueue = new Mock<ICommandQueue>();
            var commandBox = new Mock<ICommandBox>();
            var utilCommandFactory = new Mock<IUtilCommandFactory>();

            utilCommandFactory.Setup(u => u.CreateCommandBox()).Returns(commandBox.Object);
            utilCommandFactory.Setup(u => u.CreateMacroCommand()).Throws(new Exception("MacroCommand creation failed"));

            var startCommand = new StartCommand(utilCommandFactory.Object, anyCommand.Object, commandQueue.Object);

            // Act & Assert
            Assert.Throws<Exception>(() => startCommand.Execute());
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenQueueSetterCreationFails()
        {
            // Arrange
            var anyCommand = new Mock<ICommand>();
            var commandQueue = new Mock<ICommandQueue>();
            var commandBox = new Mock<ICommandBox>();
            var utilCommandFactory = new Mock<IUtilCommandFactory>();
            var macroCommand = new Mock<IMacroCommand>();

            utilCommandFactory.Setup(u => u.CreateCommandBox()).Returns(commandBox.Object);
            utilCommandFactory.Setup(u => u.CreateMacroCommand()).Returns(macroCommand.Object);
            utilCommandFactory.Setup(u => u.CreateMacroCommand()).Throws(new Exception("QueueSetter creation failed"));

            var startCommand = new StartCommand(utilCommandFactory.Object, anyCommand.Object, commandQueue.Object);

            // Act & Assert
            Assert.Throws<Exception>(() => startCommand.Execute());
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenAddingCommandToMacroCommandFails()
        {
            // Arrange
            var anyCommand = new Mock<ICommand>();
            var commandQueue = new Mock<ICommandQueue>();
            var commandBox = new Mock<ICommandBox>();
            var macroCommand = new Mock<IMacroCommand>();
            var utilCommandFactory = new Mock<IUtilCommandFactory>();

            utilCommandFactory.Setup(u => u.CreateCommandBox()).Returns(commandBox.Object);
            utilCommandFactory.Setup(u => u.CreateMacroCommand()).Returns(macroCommand.Object);
            macroCommand.Setup(m => m.Put(It.IsAny<ICommand>())).Throws(new Exception("Failed to add command"));

            var startCommand = new StartCommand(utilCommandFactory.Object, anyCommand.Object, commandQueue.Object);

            // Act & Assert
            Assert.Throws<Exception>(() => startCommand.Execute());
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenPuttingCommandBoxIntoQueueFails()
        {
            // Arrange
            var anyCommand = new Mock<ICommand>();
            var commandQueue = new Mock<ICommandQueue>();
            var commandBox = new Mock<ICommandBox>();
            var macroCommand = new Mock<IMacroCommand>();
            var commandSetToQueue = new Mock<ICommand>();

            var utilCommandFactory = new Mock<IUtilCommandFactory>();

            utilCommandFactory.Setup(u => u.CreateCommandBox()).Returns(commandBox.Object);
            utilCommandFactory.Setup(u => u.CreateMacroCommand()).Returns(macroCommand.Object);
            utilCommandFactory.Setup(u => u.CreateQueueSetter(commandBox.Object)).Returns(commandSetToQueue.Object);
            commandQueue.Setup(cq => cq.Put(commandBox.Object)).Throws(new Exception("Failed to put CommandBox into queue"));

            var startCommand = new StartCommand(utilCommandFactory.Object, anyCommand.Object, commandQueue.Object);

            // Act & Assert
            Assert.Throws<Exception>(() => startCommand.Execute());
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenSettingMacroCommandIntoCommandBoxFails()
        {
            // Arrange
            var anyCommand = new Mock<ICommand>();
            var commandQueue = new Mock<ICommandQueue>();
            var commandBox = new Mock<ICommandBox>();
            var macroCommand = new Mock<IMacroCommand>();
            var commandSetToQueue = new Mock<ICommand>();

            var utilCommandFactory = new Mock<IUtilCommandFactory>();

            utilCommandFactory.Setup(u => u.CreateCommandBox()).Returns(commandBox.Object);
            utilCommandFactory.Setup(u => u.CreateMacroCommand()).Returns(macroCommand.Object);
            utilCommandFactory.Setup(u => u.CreateQueueSetter(commandBox.Object)).Returns(commandSetToQueue.Object);
            commandBox.Setup(cb => cb.Set(macroCommand.Object)).Throws(new Exception("Failed to set MacroCommand into CommandBox"));

            var startCommand = new StartCommand(utilCommandFactory.Object, anyCommand.Object, commandQueue.Object);

            // Act & Assert
            Assert.Throws<Exception>(() => startCommand.Execute());
        }

        [Fact]
        public void Execute_ShouldThrowArgumentNullException_WhenCommandIsNull()
        {
            // Arrange
            var commandQueue = new Mock<ICommandQueue>();
            var utilCommandFactory = new Mock<IUtilCommandFactory>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new StartCommand(utilCommandFactory.Object, null, commandQueue.Object));
        }
    }
}
