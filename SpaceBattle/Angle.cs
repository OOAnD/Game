namespace SpaceBattle
{
    public class Angle
    {
        public int _numerator;
        public static int _denominator = 8;
        public int Numerator
        {
            get => _numerator;
            set => _numerator = ((value % _denominator) + _denominator) % _denominator;
        }
        public static int Denominator => _denominator;
        public Angle(int numerator)
        {
            _numerator = ((numerator % _denominator) + _denominator) % _denominator;
        }
        public static Angle operator +(Angle thisAngle, Angle otherAngle)
        {
            return new Angle(thisAngle.Numerator + otherAngle.Numerator);
        }
        public static Angle operator -(Angle thisAngle, Angle otherAngle)
        {
            return new Angle(thisAngle.Numerator - otherAngle.Numerator);
        }
        public static bool operator ==(Angle thisAngle, Angle otherAngle)
        {
            return thisAngle.Numerator == otherAngle.Numerator;
        }
        public static bool operator !=(Angle thisAngle, Angle otherAngle)
        {
            return thisAngle.Numerator != otherAngle.Numerator;
        }
        public NaturalFraction Sin()
        {
            var x = ToNaturalFraction();

            var x1 = x;
            var x3 = new NaturalFraction(1, 6) * x.Pow(3);
            var x5 = new NaturalFraction(1, 120) * x.Pow(5);
            var x7 = new NaturalFraction(1, 5040) * x.Pow(7);

            var sin = x1 - x3 + x5 - x7;
            return sin;
        }
        public NaturalFraction Cos()
        {
            var x = ToNaturalFraction();

            var x0 = new NaturalFraction(1, 1);
            var x2 = new NaturalFraction(1, 2) * x.Pow(2);
            var x4 = new NaturalFraction(1, 24) * x.Pow(4);
            var x6 = new NaturalFraction(1, 720) * x.Pow(6);
            var x8 = new NaturalFraction(1, 40320) * x.Pow(8);

            var cos = x0 - x2 + x4 - x6 + x8;
            return cos;
        }
        public NaturalFraction ToNaturalFraction()
        {
            return new NaturalFraction(Numerator, Angle.Denominator);
        }
        public override bool Equals(object obj)
        {
            return obj is Angle angle && (Numerator == angle.Numerator) || (obj is null && !(this is Angle));
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_numerator, _denominator);
        }
    }
}
