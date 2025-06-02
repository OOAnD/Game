using App;
using App.Scopes;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencyTreeTest
    {
        [Fact]
        public void Execute_ShouldCreateTree()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var quadruples = new List<(int, int, int, int)> { (1, 2, 3, 4), (5, 6, 7, 8) };

            var register = new RegisterIoCDependencyTree();

            // Act
            register.Execute();

            // Assert
            var testTree = Ioc.Resolve<QuadrupleTree>("Tree.Create", quadruples);
            Assert.IsType<QuadrupleTree>(testTree);
        }

        [Fact]
        public void Execute_ShouldAddTree()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var form1 = "square";
            var form2 = "circle";

            var quadruples = new List<(int, int, int, int)> { (1, 2, 3, 4), (5, 6, 7, 8) };
            var tree = new QuadrupleTree(quadruples);

            var register = new RegisterIoCDependencyTree();

            // Act
            register.Execute();
            var testCommand = Ioc.Resolve<ICommand>("Tree.Add", form1, form2, tree);

            // Assert
            Assert.IsType<TreeAddToStorageCommand>(testCommand);
        }
    }
}
