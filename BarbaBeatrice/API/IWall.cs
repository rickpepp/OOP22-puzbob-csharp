namespace BarbaBeatrice.API{
    public interface IWall{
        // Brings down the wall
        void GoDown(double size);

        // Getter the position of the wall
        double GetPosition();

        // Descriptive string
        string ToString();
    }
}