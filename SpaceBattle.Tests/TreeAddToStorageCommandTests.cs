namespace SpaceBattle.Tests
{
    public class TreeAddToStorageCommandTest
    {
        [Fact]
        public void Execute_ShouldContinue()
        {
            //Arrange   
            var form1 = "square";
            var form2 = "square";

            var quadruples = new List<(int, int, int, int)> { (1, 2, 3, 4), (5, 6, 7, 8) };
            var tree = new QuadrupleTree(quadruples);

            var treeStorage = (Dictionary<(string, string), QuadrupleTree>)[];

            var command = new TreeAddToStorageCommand([form1, form2, tree, treeStorage]);

            //Act & Assert
            command.Execute();
        }
    }
}
