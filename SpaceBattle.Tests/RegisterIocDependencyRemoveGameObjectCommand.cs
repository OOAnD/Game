using App;
using App.Scopes;

namespace SpaceBattle.Tests
{
    public class RegisterIocDependencyRemoveGameObjectCommandTest
    {
        [Fact]
        public void Execute_ShouldRemoveGameObject()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var testId = "test_obj_123";
            var gameObject = new Dictionary<string, object> { ["id"] = testId };
            var objectsDict = new Dictionary<string, IDictionary<string, object>>
            {
                [testId] = gameObject
            };

            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.GetAll",
                (Func<object[], IDictionary<string, IDictionary<string, object>>>)(_ => objectsDict)).Execute();

            var command = new RemoveGameObjectCommand(gameObject);

            // Act
            command.Execute();

            // Assert
            Assert.False(objectsDict.ContainsKey(testId));
        }
    }
}
