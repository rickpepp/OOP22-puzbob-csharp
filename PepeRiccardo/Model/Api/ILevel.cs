namespace PepeRiccardo.Model.Api
{
    public interface ILevel 
    {
        IBall[,] GetStartBalls(Dictionary<string, List<(int, int)>> ballsPosition);
    }
}