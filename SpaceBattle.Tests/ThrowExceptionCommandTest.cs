namespace SpaceBattle.Tests
{
    public class ThrowExceptionCommandTest
    {
        [Fact]
        public void Execute_ShouldThrowException()
            => Assert.Throws<Exception>(() => new ThrowExceptionCommand().Execute());
    }
}
