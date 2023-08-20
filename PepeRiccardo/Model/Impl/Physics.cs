using PepeRiccardo.Model.Api;

namespace PepeRiccardo.Model.Impl
{
    public class Physics : IPhysics
    {
        private const double COMPLEMENTARY_ANGLE = 90;
        private const double BALL_ANGLE = 30;

        private readonly (double, double) _boardDimension;
        private readonly double _velocity;
        private readonly double _ballDimension;
        private readonly (double, double) _cannonPosition;
        private readonly double _rowDistance;

        public Physics((double, double) boardDimension, double velocity, double ballDimension, (double, double) cannonPosition)
        {
            _boardDimension = boardDimension;
            _velocity = velocity;
            _ballDimension = ballDimension;
            _cannonPosition = cannonPosition;
            _rowDistance = ballDimension * Math.Cos(BALL_ANGLE * (Math.PI / 180));
        }

        // This method return the ball position after some time elapsed
        public (double, double) CalcBallPosition(FlyingBall ball, int cannonAngle, double time)
        {
            return CalcFunctionPosition(_cannonPosition, cannonAngle, time, true, _ballDimension / 2);
        }

        private (double, double) CalcFunctionPosition((double, double) startingPosition, int cannonAngle, 
            double time, Boolean direction, double halfBallDimension)
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
            return startingPosition.Item2 + (Math.Abs(x - startingPosition.Item1) * Math.Cos(angle));
        }

        // Calc the time left to the movement after the bounce
        private double CalcNewTime(double x, double y, double time)
        {
            double distance = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            double timeToSubtract = distance / _velocity;
            return time - timeToSubtract;
        }

        public (int, int)? IsAttached(double wallHeight, IBall[,] matrixBall, FlyingBall ball)
        {
            bool result = false;

            // Calc the relative row index
            int rowIndex;
            if (ball.Position.Item2 > _boardDimension.Item2 - wallHeight || ball.Position.Item2 < 0)
                rowIndex = 0;
            else
                rowIndex = (int) Math.Floor((_boardDimension.Item2 - wallHeight - ball.Position.Item2 - 
                    ((_ballDimension - _rowDistance) / 2)) / _rowDistance);
            
            int columnIndex;

            if (rowIndex % 2 == 0)
                columnIndex = (int) Math.Floor(ball.Position.Item1 / _ballDimension);
            else if (ball.Position.Item1 < _ballDimension / 2)
                columnIndex = 0;
            else
                columnIndex = (int) Math.Floor((ball.Position.Item1 - (_ballDimension / 2)) / _ballDimension);

            (int, int) possibleIndexes = (columnIndex, rowIndex);

            // If it touch the upper wall and is empty add it. It need to touch the wall
            if (rowIndex == 0 && matrixBall[rowIndex, columnIndex] == null && 
                ball.Position.Item2 >= (_boardDimension.Item2 - (_ballDimension / 2) - wallHeight))
            {
                return possibleIndexes;
            }

            // Maximum column value
            int maxColumnIndex = (int) Math.Floor(_boardDimension.Item1 / _ballDimension) - 1;

            // This near cells are in common between odd and even row
            List<(int, int)> neighbourList = new List<(int, int)>
            {
                (columnIndex - 1, rowIndex),
                (columnIndex + 1, rowIndex),
                (columnIndex, rowIndex + 1),
                (columnIndex, rowIndex - 1)
            };

            if (rowIndex % 2 == 0)
            {
                // Even Row
                neighbourList.Add((columnIndex - 1, rowIndex - 1));
                neighbourList.Add((columnIndex - 1, rowIndex + 1));
            }
            else
            {
                // Odd Row
                neighbourList.Add((columnIndex + 1, rowIndex - 1));
                neighbourList.Add((columnIndex + 1, rowIndex + 1));
            }

            // Check if there are any ball near to attach
            foreach ((int, int) neighbour in neighbourList)
            {
                result = result | (IsPresent(neighbour, matrixBall) & IsNear(neighbour, ball.Position, wallHeight));
            }

            // If there are some adiacent balls return the index
            if (result && IsValid(possibleIndexes, maxColumnIndex))
                return (columnIndex, rowIndex);

            // Else return null
            return null;
        }

        // Check if there is a ball in the specified cell
        private Boolean IsPresent((int, int) indexes, IBall[,] matrixBall)
        {
            try 
            {
                return matrixBall[indexes.Item1, indexes.Item2] != null;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }

        // Check if the index are out of the board
        private bool IsValid((int, int) actualBallIndexes, int maxColumnIndex)
        {
            return !(actualBallIndexes.Item1 > maxColumnIndex || 
                (actualBallIndexes.Item2 % 2 != 0 && actualBallIndexes.Item1 > (maxColumnIndex - 1)));
        }

        // Check if two balls are tounching
        private bool IsNear((int, int) nearBallIndexes, (double, double) actualPosition, double wallHeight)
        {
            // Calc near ball position
            double yNear = _boardDimension.Item2 - wallHeight - (_rowDistance * nearBallIndexes.Item2) - (_ballDimension / 2);
            double xNear;

            // x coordinate depends if the row is odd or even
            if (nearBallIndexes.Item2 % 2 == 0)
                xNear = (_ballDimension * nearBallIndexes.Item1) + (_ballDimension / 2);
            else
                xNear = (_ballDimension * nearBallIndexes.Item1) + _ballDimension;

            // Calc the distance between the two balls
            double distance = Math.Sqrt(Math.Pow(actualPosition.Item1 - xNear, 2) + Math.Pow(actualPosition.Item2 - yNear, 2));

            return distance <= _ballDimension;
        }
    }
}