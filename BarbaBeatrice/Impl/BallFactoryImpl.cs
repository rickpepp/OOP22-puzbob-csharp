using BarbaBeatrice.API;

namespace BarbaBeatrice.Impl{
    public class BallFactoryImpl : IBallFactory
    {
        private Dictionary<string, int> _colors;
        private double _size;

        public BallFactoryImpl(Dictionary<string,int> colors, double size){
            _colors = colors;
            _size = size;
        }
        public IBall CreateFlyingBall(string color, Tuple<double, double> position)
        {
            return new FlyingBallImpl(color, _colors[color],_size, position);
        }

        public IBall CreateStaticBall(string color)
        {
            return new BallImpl(color, _colors[color], _size);
        }

        public double GetBallDimension() => _size;
    }
}