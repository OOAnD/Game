namespace SpaceBattle
{
    public class NaturalFraction
    {
        public long Numerator { get; set; }
        public long Denominator { get; set; }

        public NaturalFraction(long numerator, long denominator)
        {
            _ = 1 / denominator;

            var gcd = GCD(numerator, denominator);
            Numerator = numerator / gcd;
            Denominator = denominator / gcd;

            Numerator *= Math.Sign(Denominator);
            Denominator *= Math.Sign(Denominator);

        }

        public static NaturalFraction operator +(NaturalFraction a, NaturalFraction b)
        {
            var lcm = NaturalFraction.LCM(a.Denominator, b.Denominator);
            var numerator = a.Numerator * (lcm / a.Denominator) + b.Numerator * (lcm / b.Denominator);
            return new NaturalFraction(numerator, lcm);
        }

        public static NaturalFraction operator -(NaturalFraction a, NaturalFraction b)
        {
            var lcm = NaturalFraction.LCM(a.Denominator, b.Denominator);
            var numerator = a.Numerator * (lcm / a.Denominator) - b.Numerator * (lcm / b.Denominator);
            return new NaturalFraction(numerator, lcm);
        }

        public static NaturalFraction operator *(NaturalFraction a, NaturalFraction b)
        {
            var numerator = a.Numerator * b.Numerator;
            var denominator = a.Denominator * b.Denominator;
            return new NaturalFraction(numerator, denominator);
        }

        public static NaturalFraction operator *(NaturalFraction a, int b)
        {
            var numerator = a.Numerator * b;
            return new NaturalFraction(numerator, a.Denominator);
        }

        public static NaturalFraction operator /(NaturalFraction a, NaturalFraction b)
        {
            var numerator = a.Numerator * b.Denominator;
            var denominator = a.Denominator * b.Numerator;
            return new NaturalFraction(numerator, denominator);
        }

        public static NaturalFraction operator /(NaturalFraction a, int b)
        {
            var _ = 1 / b;
            var denominator = a.Denominator * b;
            return new NaturalFraction(a.Numerator, denominator);
        }

        public static bool operator ==(NaturalFraction a, NaturalFraction b)
        {
            return a.Numerator == b.Numerator && a.Denominator == b.Denominator;
        }

        public static bool operator !=(NaturalFraction a, NaturalFraction b)
        {
            return !(a == b);
        }

        public override bool Equals(object? obj)
        {
            return obj is NaturalFraction fraction && (Numerator == fraction.Numerator) && (Denominator == fraction.Denominator) || (obj is null && (this is null));
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Numerator, Denominator);
        }

        public NaturalFraction Pow(int n) => n == 1 ? this : this * Pow(n - 1);

        public static long GCD(long a, long b) => b == 0 ? a : GCD(b, a % b);

        public static long LCM(long a, long b) => a * b / GCD(a, b);

    }
}
