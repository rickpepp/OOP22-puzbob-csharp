using PepeRiccardo.Model.Api;

namespace PepeRiccardo.Model.Impl
{
    /*
    This is a factory that produces Ball, this is needed to incapsulate the logic about the score value
    and avoid repetition and keep DRY
    */
    public class BallFactory : IBallFactory {
        // Size of the balls and the dictionary wich contains the colors
        public double Size { get; private set; }
        private Dictionary<string, int> ColorMap { get; set; }

        public BallFactory(Dictionary<string, int> colorMap, double size) 
        {
            ColorMap = colorMap;
            Size = size;
        }

        // Create Static and Flying Ball, throw an exception if the color doesn't exist
        public IBall CreateStaticBall(string color)
        {
            try 
            {
                return new StaticBall(color, ColorMap[color], Size);
            }
            catch (KeyNotFoundException) 
            {
                throw new KeyNotFoundException("The color in input doesn't exist");
            }
        }

        public IBall CreateFlyingBall(string color, (double, double) position)
        {
            try 
            {
                return new FlyingBall(color, ColorMap[color], Size, position);
            }
            catch (KeyNotFoundException) 
            {
                throw new KeyNotFoundException("The color in input doesn't exist");
            }
        }
    }
}