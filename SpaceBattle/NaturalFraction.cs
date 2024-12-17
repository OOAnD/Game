namespace SpaceBattle
{
    public class NaturalFraction
    {
        public long _numerator;
        public long _denominator;

        public NaturalFraction(long numerator, long denominator)
        {
            var _ = 1 / denominator;

            var s = Math.Sign(denominator);
            _numerator = numerator * s;
            _denominator = denominator * s;

            var gcd = NaturalFraction.GCD(_numerator, _denominator);
            _numerator /= gcd;
            _denominator /= gcd;
        }

        public static NaturalFraction operator +(NaturalFraction a, NaturalFraction b)
        {
            var lcm = NaturalFraction.LCM(a._denominator, b._denominator);
            var numerator = a._numerator * (lcm / a._denominator) + b._numerator * (lcm / b._denominator);
            return new NaturalFraction(numerator, lcm);
        }

        public static NaturalFraction operator -(NaturalFraction a, NaturalFraction b)
        {
            var lcm = NaturalFraction.LCM(a._denominator, b._denominator);
            var numerator = a._numerator * (lcm / a._denominator) - b._numerator * (lcm / b._denominator);
            return new NaturalFraction(numerator, lcm);
        }

        public static NaturalFraction operator *(NaturalFraction a, NaturalFraction b)
        {
            var numerator = a._numerator * b._numerator;
            var denominator = a._denominator * b._denominator;
            return new NaturalFraction(numerator, denominator);
        }

        public static NaturalFraction operator *(NaturalFraction a, int b)
        {
            var gcd = NaturalFraction.GCD(a._denominator, b);
            var numerator = a._numerator * (b / gcd);
            return new NaturalFraction(numerator, a._denominator / gcd);
        }

        public static NaturalFraction operator /(NaturalFraction a, NaturalFraction b)
        {
            var numerator = a._numerator * b._denominator;
            var denominator = a._denominator * b._numerator;
            return new NaturalFraction(numerator, denominator);
        }

        public static NaturalFraction operator /(NaturalFraction a, int b)
        {
            _ = 1 / b;
            var gcd = NaturalFraction.GCD(a._numerator, b);
            var denominator = a._denominator * (b / gcd);
            return new NaturalFraction(a._numerator / gcd, denominator);
        }
        public static bool operator ==(NaturalFraction testFraction1, NaturalFraction testFraction2)
        {
            return testFraction1._numerator == testFraction2._numerator && testFraction1._denominator == testFraction2._denominator;
        }
        public static bool operator !=(NaturalFraction testFraction1, NaturalFraction testFraction2)
        {
            return testFraction1._numerator != testFraction2._numerator || testFraction1._denominator != testFraction2._denominator;
        }
        public override bool Equals(object obj)
        {
            return obj is NaturalFraction testFraction && (_numerator == testFraction._numerator && _denominator == testFraction._denominator) || (obj is null && !(this is NaturalFraction));
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_numerator, _denominator);
        }
        public NaturalFraction Pow(int n) => n == 1 ? this : this * Pow(n - 1);

        public static long GCD(long a, long b) => b == 0 ? a : GCD(b, a % b);

        public static long LCM(long a, long b) => a * b / GCD(a, b);

        // public override string ToString() => _numerator + "/" + _denominator;
    }
}
