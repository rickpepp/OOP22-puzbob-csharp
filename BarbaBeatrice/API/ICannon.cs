namespace BarbaBeatrice.API{

    public interface ICannon{
        // Changes the angle of the cannon 
        void ChangeAngle(int angle);

        // Getter for cannon angle
        int GetAngle();

        // Getter for the ball present on the cannon
        IBall? GetCurrentBall();

        // Getter for the position of the cannon
        Tuple<double, double> GetCannonPosition();

        // Creates the next ball on the cannon
        void CreateBall(List<string> colors);

        // Launching a ball
        void Shot();

        // Descriptive string
        string ToString();
    }
}