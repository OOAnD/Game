using App;

namespace SpaceBattle
{
    public class RegisterIoCDependencyTree : ICommand
    {
        public void Execute()
        {
            var treeStorage = new Dictionary<(string, string), QuadrupleTree>();

            Ioc.Resolve<ICommand>("IoC.Register", "Tree.Create", (object[] args) =>
                {
                    var list = (IEnumerable<(int, int, int, int)>)args[0];

                    var tree = new QuadrupleTree(list);
                    return tree;
                }).Execute();

            Ioc.Resolve<ICommand>("IoC.Register", "Tree.Add", (object[] args) =>
                {
                    var form1 = args[0];
                    var form2 = args[1];
                    var tree = (QuadrupleTree)args[2];

                    return new TreeAddToStorageCommand([form1, form2, tree, treeStorage]);
                }).Execute();
        }
    }
}
