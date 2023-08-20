namespace BarbaBeatrice.API{
    public interface IBallFactory{
        // Return a static Ball
        IBall CreateStaticBall(string color);

        // Return a flying ball
        IBall CreateFlyingBall(string color, Tuple<double,double> position);

        // Getter for ball diameter
        double GetBallDimension();
    }
}