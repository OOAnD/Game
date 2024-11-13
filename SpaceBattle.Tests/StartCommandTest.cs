using Moq;
using Xunit;
using System;

namespace SpaceBattle.Tests
{
    public class StartCommandTest
    {
        private Mock<ICommand> anyCommand;
        private Mock<ICommandQueue> commandQueue;
        private Mock<ICommandBox> commandBox;
        private Mock<IMacroCommand> macroCommand;
        private Mock<ICommand> commandSetToQueue;
        private Mock<IUtilCommandFactory> utilCommandFactory;
        private StartCommand startCommand;

        private void SetupMocks()
        {
            anyCommand = new Mock<ICommand>();
            commandQueue = new Mock<ICommandQueue>();
            commandBox = new Mock<ICommandBox>();
            macroCommand = new Mock<IMacroCommand>();
            commandSetToQueue = new Mock<ICommand>();
            utilCommandFactory = new Mock<IUtilCommandFactory>();

            utilCommandFactory.Setup(u => u.CreateQueueSetter(commandBox.Object)).Returns(commandSetToQueue.Object);
            utilCommandFactory.Setup(u => u.CreateCommandBox()).Returns(commandBox.Object);
            utilCommandFactory.Setup(u => u.CreateMacroCommand()).Returns(macroCommand.Object);

            startCommand = new StartCommand(utilCommandFactory.Object, anyCommand.Object, commandQueue.Object);
        }

        [Fact]
        public void Execute_ShouldCorrectBuildCommandAndPutIntoQueue()
        {
            // Arrange
            SetupMocks();

            // Act
            startCommand.Execute();

            // Assert
            macroCommand.Verify(cwns => cwns.Put(commandSetToQueue.Object));
            macroCommand.Verify(cwns => cwns.Put(anyCommand.Object));
            commandBox.Verify(cb => cb.Set(macroCommand.Object));
            commandQueue.Verify(cq => cq.Put(commandBox.Object));
        }

        [Fact]
        public void GetStopCommand_ShouldReturnCorrectStopCommand()
        {
            // Arrange
            SetupMocks();
            var stopCommand = new Mock<ICommand>();
            utilCommandFactory.Setup(u => u.CreateStopCommand(commandBox.Object)).Returns(stopCommand.Object);

            // Act
            startCommand.Execute();
            var receivedStopCommand = startCommand.StopCommand;

            // Assert
            utilCommandFactory.Verify(ucf => ucf.CreateStopCommand(commandBox.Object));
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenStopCommandCreationFails()
        {
            // Arrange
            anyCommand = new Mock<ICommand>();
            commandQueue = new Mock<ICommandQueue>();
            utilCommandFactory = new Mock<IUtilCommandFactory>();

            utilCommandFactory.Setup(u => u.CreateStopCommand(It.IsAny<ICommandBox>())).Throws(new Exception("StopCommand creation failed"));

            startCommand = new StartCommand(utilCommandFactory.Object, anyCommand.Object, commandQueue.Object);

            // Act & Assert
            Assert.Throws<Exception>(() => {
                startCommand.Execute();
                var receivedStopCommand = startCommand.StopCommand;
            });
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenCommandBoxCreationFails()
        {
            // Arrange
            anyCommand = new Mock<ICommand>();
            commandQueue = new Mock<ICommandQueue>();
            utilCommandFactory = new Mock<IUtilCommandFactory>();

            utilCommandFactory.Setup(u => u.CreateCommandBox()).Throws(new Exception("CommandBox creation failed"));

            startCommand = new StartCommand(utilCommandFactory.Object, anyCommand.Object, commandQueue.Object);

            // Act & Assert
            Assert.Throws<Exception>(() => startCommand.Execute());
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenMacroCommandCreationFails()
        {
            // Arrange
            SetupMocks();
            utilCommandFactory.Setup(u => u.CreateMacroCommand()).Throws(new Exception("MacroCommand creation failed"));

            // Act & Assert
            Assert.Throws<Exception>(() => startCommand.Execute());
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenQueueSetterCreationFails()
        {
            // Arrange
            SetupMocks();
            utilCommandFactory.Setup(u => u.CreateQueueSetter(commandBox.Object)).Throws(new Exception("QueueSetter creation failed"));

            // Act & Assert
            Assert.Throws<Exception>(() => startCommand.Execute());
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenAddingCommandToMacroCommandFails()
        {
            // Arrange
            SetupMocks();
            macroCommand.Setup(m => m.Put(It.IsAny<ICommand>())).Throws(new Exception("Failed to add command"));

            // Act & Assert
            Assert.Throws<Exception>(() => startCommand.Execute());
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenPuttingCommandBoxIntoQueueFails()
        {
            // Arrange
            SetupMocks();
            commandQueue.Setup(cq => cq.Put(commandBox.Object)).Throws(new Exception("Failed to put CommandBox into queue"));

            // Act & Assert
            Assert.Throws<Exception>(() => startCommand.Execute());
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenSettingMacroCommandIntoCommandBoxFails()
        {
            // Arrange
            SetupMocks();
            commandBox.Setup(cb => cb.Set(macroCommand.Object)).Throws(new Exception("Failed to set MacroCommand into CommandBox"));

            // Act & Assert
            Assert.Throws<Exception>(() => startCommand.Execute());
        }

        [Fact]
        public void Execute_ShouldThrowArgumentNullException_WhenCommandIsNull()
        {
            // Arrange
            commandQueue = new Mock<ICommandQueue>();
            utilCommandFactory = new Mock<IUtilCommandFactory>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new StartCommand(utilCommandFactory.Object, null, commandQueue.Object));
        }
    }
}