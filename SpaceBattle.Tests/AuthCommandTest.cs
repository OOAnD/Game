using Moq;
namespace SpaceBattle.Tests
{
    public class AuthCommandTest
    {
        [Fact]
        public void Execute_ShouldThrowException()
        {
            // Arrange
            var testSenderId   = "player_1";
            var testReceiverId = "notplayer_1";
            var testAuthCommand = new AuthCommand(testSenderId,testReceiverId);

            // Act & Assert 
            Assert.Throws<Exception>(testAuthCommand.Execute);
        }
        [Fact]
        public void Execute_ShouldContinue()
        {
            // Arrange
            var testSenderId   = "player_1";
            var testReceiverId = "player_1";
            var testAuthCommand = new AuthCommand(testSenderId,testReceiverId);

            // Act & Assert
            testAuthCommand.Execute();
        }
    }
}