namespace PepeRiccardo.Model.Api
{
    /*
    This is an interface for a ball factory, which incapsulate the logic about the creation of the balls
    */
    public interface IBallFactory 
    {
        double Size { get; }
        IBall CreateStaticBall(string color);
        IBall CreateFlyingBall(string color, (double, double) position);
    }
}