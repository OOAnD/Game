namespace SpaceBattle
{
    public class QuadrupleTree
    {
        private readonly Dictionary<int, Dictionary<int, Dictionary<int, HashSet<int>>>> _tree = [];

        public QuadrupleTree(IEnumerable<(int, int, int, int)> quadruples)
        {
            _ = quadruples
        .Select(q =>
        {
            AddQuadruple(q.Item1, q.Item2, q.Item3, q.Item4);
            return 0;
        }).Count();
        }

        public void AddQuadruple(int a, int b, int c, int d)
        {
            var level1 = _tree.TryGetValue(a, out var l1) ? l1 : (_tree[a] = []);
            var level2 = level1.TryGetValue(b, out var l2) ? l2 : (level1[b] = []);
            var level3 = level2.TryGetValue(c, out var l3) ? l3 : (level2[c] = []);
            level3.Add(d);
        }

        public bool Contains(int a, int b, int c, int d)
        {
            return _tree.TryGetValue(a, out var level1) &&
                   level1.TryGetValue(b, out var level2) &&
                   level2.TryGetValue(c, out var level3) &&
                   level3.Contains(d);
        }
    }
}
