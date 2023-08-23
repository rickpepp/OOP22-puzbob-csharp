namespace BarbaBeatrice.API{
    public interface IBall{
        // Getter for the color of the ball
        string getColor {get;}

        // Getter for the score of the ball
        int getScore {get;}

        // Getter for the diameter of the ball
        double getBallSize {get;} 

        // Return a string describing  the state of the ball
        string ToString();
    }
}