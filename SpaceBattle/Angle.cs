namespace SpaceBattle
{
    public class Angle
    {
        private readonly int _numerator;
        public static int _denominator = 8;
        public int Numerator => _numerator;
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
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (Angle)obj;
            return Numerator == other.Numerator;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_numerator, _denominator);
        }
    }
}
