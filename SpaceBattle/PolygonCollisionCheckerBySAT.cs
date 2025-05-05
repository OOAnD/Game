namespace SpaceBattle
{
    public class PolygonCollisionCheckerBySAT : ICollisionChecker
    {
        private readonly IEnumerable<Point> _polygonA;
        private readonly IEnumerable<Point> _polygonB;

        public PolygonCollisionCheckerBySAT(IEnumerable<Point> polygonA, IEnumerable<Point> polygonB)
        {
            _polygonA = polygonA.ToList();
            _polygonB = polygonB.ToList();

            if (_polygonA.Count() < 3 || _polygonB.Count() < 3)
            {
                throw new ArgumentException("Не менее трёх точек");
            }
        }

        public bool IsCollision(int xOffset, int yOffset, int xVel, int yVel)
        {
            var movedPolygonB = _polygonB.Select(p => new Point(p.X + xOffset, p.Y + yOffset));
            var nextPolygonB = movedPolygonB.Select(p => new Point(p.X + xVel, p.Y + yVel));

            return CheckSAT(_polygonA, movedPolygonB) || CheckSAT(_polygonA, nextPolygonB);
        }

        private static bool CheckSAT(IEnumerable<Point> polyA, IEnumerable<Point> polyB)
        {
            var axes = GetAxes(polyA).Concat(GetAxes(polyB));
            return axes.All(axis => !IsSeparatingAxis(polyA, polyB, axis));
        }

        private static IEnumerable<(int X, int Y)> GetAxes(IEnumerable<Point> polygon)
        {
            var points = polygon.ToList(); // Однократное материализуем
            return points.Select((p1, i) =>
            {
                var p2 = points[(i + 1) % points.Count];
                var edgeX = p2.X - p1.X;
                var edgeY = p2.Y - p1.Y;
                return (-edgeY, edgeX);
            });
        }

        private static bool IsSeparatingAxis(IEnumerable<Point> polyA, IEnumerable<Point> polyB, (int X, int Y) axis)
        {
            if (axis.X == 0 && axis.Y == 0)
            {
                return false;
            }

            var (minA, maxA) = ProjectPolygon(polyA, axis);
            var (minB, maxB) = ProjectPolygon(polyB, axis);

            return maxA < minB || maxB < minA;
        }

        private static (int min, int max) ProjectPolygon(IEnumerable<Point> polygon, (int X, int Y) axis)
        {
            var projections = polygon.Select(p => p.X * axis.X + p.Y * axis.Y);
            return (projections.Min(), projections.Max());
        }
    }
}
