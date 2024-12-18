namespace SpaceBattle
{
    public static class MathExt
    {
        public static NaturalFraction Sin(NaturalFraction bf)
        {
            var rad = bf * new NaturalFraction(22, 7) * 2;
            var a = rad * 5040;
            var b = rad.Pow(3) * 840;
            var c = rad.Pow(5) * 42;
            var d = rad.Pow(7);

            var sin = a - b + c - d;
            sin /= 5040;

            return sin;
        }

        public static NaturalFraction Cos(NaturalFraction bf)
        {
            var rad = bf * new NaturalFraction(22, 7) * 2;
            var a = new NaturalFraction(720, 1);
            var b = rad.Pow(2) * 360;
            var c = rad.Pow(4) * 30;
            var d = rad.Pow(6);

            var cos = a - b + c - d;
            cos /= 720;

            return cos;
        }
    }
}
