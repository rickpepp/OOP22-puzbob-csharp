using BarbaBeatrice.API;

namespace BarbaBeatrice.Impl{
    public class Cannon : ICannon
    {
        private const int MAX_ANGLE = 160;
        private const int MIN_ANGLE = 20;
        private const int CENTER_ANGLE = 90;

        private int _angle;
        private Tuple<double,double> _cannonPosition;
        private IBall? _ball;
        private IBallFactory _ballFactory;

        public Cannon(IBallFactory ballFactory, Tuple<double,double> cannonPosition){
            _angle = CENTER_ANGLE;
            _ballFactory = ballFactory;
            _cannonPosition = cannonPosition;
        }
        public void ChangeAngle(int angle)
        {
           _angle = _angle + angle;
            if(_angle < MIN_ANGLE){
                _angle = MIN_ANGLE;
            }else if(_angle > MAX_ANGLE){
                _angle = MAX_ANGLE;
            }
        }

        public void CreateBall(List<string> colors)
        {
            Random indexColor = new();
            string color = colors[indexColor.Next(0, colors.Count + 1)];
            if(_ball == null){
                _ball = _ballFactory.CreateFlyingBall(color, _cannonPosition);
            }
        }

        public int GetAngle() => _angle;

        public Tuple<double, double> GetCannonPosition() => _cannonPosition;

        public IBall? GetCurrentBall() => _ball;

        public void Shot() => _ball = null;

        public override string ToString() => "Cannon has angle: " + _angle + ", his position is: " + _cannonPosition.ToString() + (_ball==null ? "" : ", his ball is: " + _ball.ToString());
    }
}