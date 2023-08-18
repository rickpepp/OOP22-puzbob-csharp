namespace PepeRiccardo.Model.Impl
{
    public class FlyingBall : StaticBall 
    {
        public (double, double) Position { get; set; }

        public FlyingBall(string color, int score, double size, (double, double) position) : base( color, score, size )
        { 
            Position = position;
        }

        public string StringRapresentation => "FlyingBall Color: " + color + " Score: " + score + " Size: " + size + " XPosition: " 
            + Position.Item1 + " YPosition: " + Position.Item2;
    }
}