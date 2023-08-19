namespace PepeRiccardo.Model.Api
{
    public interface ILevel 
    {
        // Read every item on the dictionary and put it in the correct place
        IBall[,] GetStartBalls(Dictionary<string, List<(int, int)>> ballsPosition);
    }
}