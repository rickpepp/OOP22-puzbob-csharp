using PepeRiccardo.Model.Impl;

namespace PepeRiccardo.Model.Api
{
    public interface IPhysics
    {
        (double, double) CalcBallPosition(FlyingBall ball, int cannonAngle, double time);

        (int, int) IsAttached(double wallHeight, IBall[,] matrixBall, FlyingBall ball);
    }
}