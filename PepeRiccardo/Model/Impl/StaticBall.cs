using PepeRiccardo.Model.Api;

namespace PepeRiccardo.Model.Impl
{
    public class StaticBall : IBall 
    {
        string Color { get; private set; };
        int Score { get; private set; };
        double Size { get; private set; };

        public StaticBall(string color, int score, double size) 
        {
            Color = color;
            Score = score;
            Size = size;
        }

        public string StringRapresentation => "StaticBall Color: " + Color + " Score: " + Score + " Size: " + Size;
    }
}