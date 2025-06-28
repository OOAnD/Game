using App;
using App.Scopes;

namespace SpaceBattle.Tests
{
    public class SaveGameObjectCommandTest
    {
        [Fact]
        public void Execute_ShouldSaveGameObject_WhenObjectIsValid()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var gameObjectId = "object1";
            var gameObject = new Dictionary<string, object> { { "id", gameObjectId } };

            var objects = new Dictionary<string, IDictionary<string, object>>();

            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.GetAll", (Func<object[], IDictionary<string, IDictionary<string, object>>>)(_ => objects)).Execute();

            var command = new SaveGameObjectCommand(gameObject);

            // Act
            command.Execute();

            // Assert
            Assert.True(objects.ContainsKey(gameObjectId));
            Assert.Equal(gameObject, objects[gameObjectId]);
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenObjectIdIsMissing()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var gameObject = new Dictionary<string, object>();

            var objects = new Dictionary<string, IDictionary<string, object>>();

            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.GetAll", (Func<object[], IDictionary<string, IDictionary<string, object>>>)(_ => objects)).Execute();

            var command = new SaveGameObjectCommand(gameObject);

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => command.Execute());
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenObjectsCollectionIsNotInitialized()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var gameObjectId = "object1";
            var gameObject = new Dictionary<string, object> { { "id", gameObjectId } };

            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.GetAll", (Func<object[], IDictionary<string, IDictionary<string, object>>?>)(_ => null)).Execute();

            var command = new SaveGameObjectCommand(gameObject);

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => command.Execute());
        }
    }
}
