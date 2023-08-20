using PepeRiccardo.Model.Api;

namespace PepeRiccardo.Model.Impl
{
    /*
    This class decode a dictionary in a matrix of Balls, this is useful for a read of a JSON file
    */
    public class Level : ILevel 
    {
        private IBallFactory _ballFactory;
        private IBall[,] _starterBall;

        public Level(IBallFactory ballFactory, (int, int) dimension) 
        {
            _ballFactory = ballFactory;
            _starterBall = new IBall[dimension.Item1, dimension.Item2];
        }

        // Read every item on the dictionary and put it in the correct place
        public IBall?[,] GetStartBalls(Dictionary<string, List<(int, int)>> ballsPosition)
        {
            foreach ((string color, List<(int, int)> ballsList) in ballsPosition)
            {
                foreach ((int, int) position in ballsList)
                {
                    if (_starterBall[position.Item1, position.Item2] != null) 
                    {
                        throw new Exception("The next ball position is already occupated");
                    }
                    else
                    {
                        _starterBall[position.Item1, position.Item2] = _ballFactory.CreateStaticBall(color);
                    }
                }
            }
            return _starterBall;
        }
    }
}