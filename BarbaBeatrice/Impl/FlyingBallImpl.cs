using BarbaBeatrice.API;

namespace BarbaBeatrice.Impl{
    public class FlyingBallImpl : BallImpl
    {
        private Tuple<double, double> _position;

        public FlyingBallImpl(string color, int score, double size, Tuple<double, double> position) :  base (color, score, size){
            _position = position;
        }

        public Tuple<double, double> GetPosition() => _position;

        public void SetPosition(Tuple<double, double> position){
            _position = position;
        }

        public new string ToString() => ToString() + ", position is:" + _position;
    }
}