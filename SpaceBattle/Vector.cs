namespace SpaceBattle
{
    public class Vector(params int[] coordinates)
    {
        public int[] Coordinates { get; } = coordinates;

        public static Vector operator +(Vector v1, Vector v2)
        {
            if (v1.Coordinates.Length != v2.Coordinates.Length)
            {
                throw new ArgumentException("Векторы должны иметь одинаковую размерность.");
            }

            var resultCoordinates = v1.Coordinates
                .Zip(v2.Coordinates, (a, b) => a + b)
                .ToArray();

            return new Vector(resultCoordinates);
        }

        public override bool Equals(object? obj)
        {
            return obj is Vector other && this == other;
        }

        public static bool operator ==(Vector v1, Vector v2)
        {
            return v1.Coordinates.Length == v2.Coordinates.Length && v1.Coordinates.SequenceEqual(v2.Coordinates);
        }

        public static bool operator !=(Vector v1, Vector v2)
        {
            return !(v1 == v2);
        }

        public override int GetHashCode()
        {
            return Coordinates.Aggregate(17, (hash, coordinate) => hash * 23 + coordinate.GetHashCode());
        }
    }
}
