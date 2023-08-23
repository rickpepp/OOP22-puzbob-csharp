using BarbaBeatrice.API;

namespace BarbaBeatrice.Impl{
    public class BallImpl : IBall{
        string _color;
        int _score;
        double _size;

        public BallImpl(string color, int score, double size)
        {
            _color = color;
            _score = score;
            _size = size;
        }

        string IBall.getColor => _color;
        int IBall.getScore => _score;
        double IBall.getBallSize => _size;

        public override string ToString() => "Color ball: " + _color + ", Score: " + _score + ", Size: " + _size;

    }
}