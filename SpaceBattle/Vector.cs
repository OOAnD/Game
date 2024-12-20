namespace SpaceBattle
{
    public class Vector
    {
        private int[] numbers;
        public int Dimension => numbers.Length;

        public Vector(int dimension)
        {
            numbers = new int[dimension];
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            if (v1.Dimension != v2.Dimension)
            {
                throw new ArgumentException("Vectors must have the same dimension.");
            }

            var result = new Vector(v1.Dimension);
            result.numbers = v1.numbers.Zip(v2.numbers, (a, b) => a + b).ToArray();
            return result;
        }

        public void Set(params int[] numbers)
        {
            if (numbers.Length != Dimension)
            {
                throw new ArgumentException("Number of values must match the dimension of the vector.");
            }

            this.numbers = numbers;
        }

        public static bool operator ==(Vector v1, Vector v2)
        {
            return v1.Dimension == v2.Dimension && v1.numbers.SequenceEqual(v2.numbers);
        }

        public static bool operator !=(Vector v1, Vector v2)
        {
            return !(v1 == v2);
        }

        public override bool Equals(object? obj)
        {
            return obj is Vector other && this == other;
        }

        public override int GetHashCode()
        {
            return numbers.GetHashCode();
        }
    }
}
