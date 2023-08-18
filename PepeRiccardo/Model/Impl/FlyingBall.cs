namespace PepeRiccardo.Model.Impl
{
    /*
    This is a class that extend StaticBall with a Property Position
    */
    public class FlyingBall : StaticBall 
    {
        public (double, double) Position { get; set; }

        // Contructor
        public FlyingBall(string color, int score, double size, (double, double) position) : base( color, score, size )
        { 
            Position = position;
        }

        // ToString Method
        public override string StringRapresentation() => "FlyingBall Color: " + Color + " Score: " + Score + " Size: " + Size + " XPosition: " 
            + Position.Item1 + " YPosition: " + Position.Item2;
    }
}