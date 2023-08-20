namespace PepeRiccardo;

using Xunit;
using PepeRiccardo.Model.Impl;

public class PhysicsTest
{
    public const double X_STARTING_POSITION = 7.5;
    public const double Y_STARTING_POSITION = 0.0;
    public const double BALL_SIZE = 1.875;

    // Testing if the positionc calc work as expected
    [Fact]
    public void CalcPositionTest()
    {
        FlyingBall ball = new FlyingBall("RED", 10, BALL_SIZE, (X_STARTING_POSITION, Y_STARTING_POSITION));
        var physics = new Physics((15, 0), 5, BALL_SIZE, (X_STARTING_POSITION, Y_STARTING_POSITION));

        // Shot without bounce 25°
        Assert.Equal(2.968, physics.CalcBallPosition(ball, 25, 1).Item1, 0.2);
        Assert.Equal(2.113, physics.CalcBallPosition(ball, 25, 1).Item2, 0.2);

        // Shot without bounce 165°
        Assert.Equal(12.330, physics.CalcBallPosition(ball, 165, 1).Item1, 0.2);
        Assert.Equal(1.294, physics.CalcBallPosition(ball, 165, 1).Item2, 0.2);

        // Shot with 1 bounce 25°
        Assert.Equal(3.543, physics.CalcBallPosition(ball, 25, 2).Item1, 0.2);
        Assert.Equal(3.961, physics.CalcBallPosition(ball, 25, 2).Item2, 0.2);

        // Shot with 1 bounce 150°
        Assert.Equal(11.756, physics.CalcBallPosition(ball, 150, 2).Item1, 0.2);
        Assert.Equal(4.446, physics.CalcBallPosition(ball, 150, 2).Item2, 0.2);

        // Shot with 2 bounces 25°
        Assert.Equal(9.899, physics.CalcBallPosition(ball, 45, 6).Item1, 0.2);
        Assert.Equal(18.084, physics.CalcBallPosition(ball, 45, 6).Item2, 0.2);
    }
}