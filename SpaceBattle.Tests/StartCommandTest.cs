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
    }
}