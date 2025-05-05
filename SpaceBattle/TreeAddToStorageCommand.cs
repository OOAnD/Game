using App;

namespace SpaceBattle
{
    public class TreeAddToStorageCommand : ICommand
    {
        public string _form1;
        public string _form2;

        public QuadrupleTree _tree;
        public Dictionary<(string, string), QuadrupleTree> _storage;

        public TreeAddToStorageCommand(object[] args)
        {
            _form1 = (string)args[0];
            _form2 = (string)args[1];
            _tree = (QuadrupleTree)args[2];
            _storage = (Dictionary<(string, string), QuadrupleTree>)args[3];
        }

        public void Execute()
        {
            _storage[(_form1, _form2)] = _tree;
        }
    }
}
