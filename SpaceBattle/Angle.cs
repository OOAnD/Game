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
