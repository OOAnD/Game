using SpaceBattle;

public class PolygonCollisionCheckerByRays(IEnumerable<Point> _staticPolygon, IEnumerable<Point> _movingPolygon) : ICollisionChecker
{

    public bool IsCollision(int xPos, int yPos, int xVel, int yVel)
    {
        var movedPoints = _movingPolygon.Select(p => new Point(p.X + xPos, p.Y + yPos)).ToArray();
        var nextPoints = movedPoints.Select(p => new Point(p.X + xVel, p.Y + yVel)).ToArray();

        var listStaticPolygon = _staticPolygon.ToList();

        return movedPoints
            .Zip(nextPoints, (current, next) => (current, next))
            .Any(pair => _staticPolygon
                .Select((point, i) => (point, next: listStaticPolygon[(i + 1) % _staticPolygon.Count()]))
                .Any(edge => RayIntersectsEdge(pair.current, pair.next, edge.point, edge.next))
            );
    }

    private static bool RayIntersectsEdge(Point rayStart, Point rayEnd, Point edgeStart, Point edgeEnd)
    {
        var rayDir = rayEnd - rayStart;
        var edgeDir = edgeEnd - edgeStart;

        var cross = rayDir.X * edgeDir.Y - rayDir.Y * edgeDir.X;

        if (Math.Abs(cross) < float.Epsilon)
        {
            return false;
        }

        var diff = edgeStart - rayStart;
        var t1 = (diff.X * edgeDir.Y - diff.Y * edgeDir.X) / cross;
        var t2 = (diff.X * rayDir.Y - diff.Y * rayDir.X) / cross;

        return t1 >= 0 && t1 <= 1 && t2 >= 0 && t2 <= 1;
    }
}
