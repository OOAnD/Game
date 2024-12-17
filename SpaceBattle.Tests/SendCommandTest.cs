using Moq;

namespace SpaceBattle.Tests
{
    public class SendCommandTest
    {
        [Fact]
        public void Execute_ShouldPutCommandIntoReciver()
        {
            // Arrange
            var command = new Mock<ICommand>();
            var receiver = new Mock<ICommandReciever>();
            var sendCommand = new SendCommand(receiver.Object, command.Object);

            // Act
            sendCommand.Execute();

            // Assert
            receiver.Verify(r => r.RecieveCommand(command.Object), Times.Once);
        }

        [Fact]
        public void Execute_ShouldThrow_WhenReciverThrow()
        {
            // Arrange
            var command = new Mock<ICommand>();
            var receiver = new Mock<ICommandReciever>();
            var sendCommand = new SendCommand(receiver.Object, command.Object);
            receiver.Setup(r => r.RecieveCommand(command.Object)).Throws(new Exception());

            // Act & Assert
            Assert.Throws<Exception>(() => sendCommand.Execute());
        }
    }
}
