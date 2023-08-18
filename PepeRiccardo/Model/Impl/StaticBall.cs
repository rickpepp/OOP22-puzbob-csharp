using PepeRiccardo.Model.Api;

namespace PepeRiccardo.Model.Impl
{
    /*
    This is an implementation of IBall with a color, Score and Size
    */
    public class StaticBall : IBall 
    {
        public string Color { get; protected set; }
        public int Score { get; protected set; }
        public double Size { get; protected set; }

        // Constructor
        public StaticBall(string color, int score, double size) 
        {
            Color = color;
            Score = score;
            Size = size;
        }

        // ToString Method
        public virtual string StringRapresentation() => "StaticBall Color: " + Color + " Score: " + Score + " Size: " + Size;
    }
}