using BarbaBeatrice.Impl;

namespace BarbaBeatrice.API{
    public interface IBoard{

        // Add balls to the playing field
        void AddBall(int x, int y, IBall ball);

        //Remove all balls of the same color and those that are left free
        int RemoveBall(int x, int y, IBall ball);

        // Getter for the playing field
        IBall?[,] GetStatusBoard();

        // Getter for the size of board
        Tuple<double, double> GetBoardSize();

        // Getter a list of color present on the board
        List<string> GetColors();

        // Return a string describing  the state of the board
        string ToString();
    }
}