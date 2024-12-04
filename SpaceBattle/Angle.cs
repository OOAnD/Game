﻿namespace SpaceBattle
{
    public class Angle
    {
        public int _value;
        public int _border;
        public int Value
        {
            get => _value;
            set => _value = value;
        }
        public int Border => _border;
        public Angle(int value, int border = 360)
        {
            if (border < 0)
            {
                throw new Exception("Wrong, can not be zero or less");
            }
            else
            {
                _border = border;
            }

            _value = ((value % _border) + _border) % _border;
        }
        public static Angle operator +(Angle angle, Angle velocity)
        {
            var denominator = angle.Border * velocity.Border; // тут надо по другому
            var numerator = angle.Value * velocity.Border + angle.Border * velocity.Value;

            var gcd = GCD(denominator, numerator);
            numerator /= gcd;
            denominator /= gcd;

            return new Angle(numerator, denominator);
        }
        public static bool operator ==(Angle thisAngle, Angle otherAngle)
        {
            return (thisAngle.Value == otherAngle.Value) && (thisAngle.Border == otherAngle.Border);
        }
        public static bool operator !=(Angle thisAngle, Angle otherAngle)
        {
            return (thisAngle.Value != otherAngle.Value) || (thisAngle.Border != otherAngle.Border);
        }

        private static int GCD(int a, int b)
        {

            a = Math.Abs(a);
            b = Math.Abs(b);

            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}