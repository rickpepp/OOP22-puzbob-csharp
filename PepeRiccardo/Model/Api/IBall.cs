namespace PepeRiccardo.Model.Api
{
    public interface IBall
    {
        // Color of the Ball
        string Color { get; };

        // Score of the Ball
        int Score { get; };

        // Size of the Ball
        double Size { get; };
    }
}