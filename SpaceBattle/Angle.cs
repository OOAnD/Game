namespace SpaceBattle{
    public class Angle{
        public int _value;
        public Angle(int value){
            _value = value;
        }
        public int GetValue(){
            return _value;
        }

        public Angle Sum(Angle additiveAngle){
            var valueToAdd = additiveAngle.GetValue();
            var sumOfValues = _value + valueToAdd;
            var newAngle = new Angle(sumOfValues);
            return newAngle;
        }
    }
}