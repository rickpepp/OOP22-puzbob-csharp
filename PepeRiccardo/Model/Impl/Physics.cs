using PepeRiccardo.Model.Api;

namespace PepeRiccardo.Model.Impl
{
    public class Physics : IPhysics
    {
        private const double COMPLEMENTARY_ANGLE = 90;
        private const double BALL_ANGLE = 30;

        private (double, double) _boardDimension;
        private double _velocity;
        private double _ballDimension;
        private (double, double) _cannonPosition;

        public Physics((double, double) boardDimension, double velocity, double ballDimension, (double, double) cannonPosition)
        {
            _boardDimension = boardDimension;
            _velocity = velocity;
            _ballDimension = ballDimension;
            _cannonPosition = cannonPosition;
        }

        // This method return the ball position after some time elapsed
        public (double, double) CalcBallPosition(FlyingBall ball, int cannonAngle, double time)
        {
            return CalcFunctionPosition(_cannonPosition, cannonAngle, time, true, _ballDimension / 2);
        }

        private (double, double) CalcFunctionPosition((double, double) startingPosition, int cannonAngle, double time, Boolean direction, double halfBallDimension)
        {

            // This resolve some calc problem that accour when the ball is too close to the wall
            if (time < 0)
                return startingPosition;

            // The angle need to be in radians
            double angle = (cannonAngle - COMPLEMENTARY_ANGLE) * (Math.PI / 180);
            double x = startingPosition.Item1;
            double y;

            // Positive change the direction after a bouncing
            if (direction)
                x += _velocity * time * Math.Sin(angle);
            else
                x -= _velocity * time * Math.Sin(angle);

            // If the ball after this time bounce
            if (x < 0 + halfBallDimension || x > _boardDimension.Item1 - halfBallDimension)
            {

                // Put the x in one of the extreme point
                if (x < 0 + halfBallDimension)
                    x = 0 + halfBallDimension;
                else if (x > _boardDimension.Item1 - halfBallDimension)
                    x = _boardDimension.Item1 - halfBallDimension;
                
                // getYBouncing calc the y coordinate
                y = GetYBouncing(startingPosition, angle, x);

                // newtime is the time left to the movement
                double newTime = CalcNewTime(Math.Abs(x - startingPosition.Item1), y - startingPosition.Item2, time);

                // Call this function recursively
                return CalcFunctionPosition((x, y), cannonAngle, newTime, !direction, halfBallDimension);
            }

            // If is not recursive, calc the y coordinate
            y = startingPosition.Item2 + (_velocity * time * Math.Cos(angle));

            // Return the result
            return (x, y);
        }

        // Calc the y when the ball bounces
        private double GetYBouncing((double, double) startingPosition, double angle, double x)
        {
            return startingPosition.Item2 + (Math.Abs(x - startingPosition.Item1) * (Math.Cos(angle)));
        }

        // Calc the time left to the movement after the bounce
        private double CalcNewTime(double x, double y, double time)
        {
            double distance = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            double timeToSubtract = distance / _velocity;
            return time - timeToSubtract;
        }

        public (int, int) IsAttached(double wallHeight, IBall[,] matrixBall, FlyingBall ball)
        {
            throw new NotImplementedException();
        }
    }
}