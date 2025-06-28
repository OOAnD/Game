namespace SpaceBattle.Tests
{
    public class EmptyCommandTest
    {
        [Fact]
        public void Execute_ShouldDoNothing()
        {
            // Arrange
            var emptyCommand = new EmptyCommand();

            // Act
            emptyCommand.Execute();
        }
    }
}
