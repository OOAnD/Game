using App;
using App.Scopes;

namespace SpaceBattle.Tests
{
    public class AuthCommandTest
    {
        [Fact]
        public void Execute_ShouldContinue()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var testplayerId = "player";
            var testObjectId = "object";
            var testGameObjects = new List<string> { testObjectId };

            var testAuthCommand = new AuthCommand(testplayerId, testObjectId);

            Ioc.Resolve<ICommand>("IoC.Register", "Players.GetBelongings", (object[] args) => testGameObjects).Execute();

            // Act & Assert 
            testAuthCommand.Execute();
        }
        [Fact]
        public void Execute_ShouldThrowException()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var testplayerId = "player";
            var testObjectId = "object";
            var testWrongId = "wrongid";
            var testGameObjects = new List<string> { testWrongId };

            var testAuthCommand = new AuthCommand(testplayerId, testObjectId);

            Ioc.Resolve<ICommand>("IoC.Register", "Players.GetBelongings", (object[] args) => testGameObjects).Execute();

            // Act & Assert 
            Assert.ThrowsAny<Exception>(testAuthCommand.Execute);
        }
    }
}
