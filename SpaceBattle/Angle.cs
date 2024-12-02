namespace SpaceBattle
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
        private static int GCD(int a, int b)
        {
        return Enumerable.Range(0, int.MaxValue)
                         .Select(i => new { A = a, B = b })
                         .TakeWhile(pair => pair.A != 0 && pair.B != 0)
                         .Aggregate((a, b) => new { A = a.A > a.B ? a.A % a.B : a.A, B = a.B > a.A ? a.B % a.A : a.B })
                         .A | (a | b);
        }
    }
}
