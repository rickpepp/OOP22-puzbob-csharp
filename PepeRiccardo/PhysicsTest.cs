namespace PepeRiccardo;

using Xunit;
using PepeRiccardo.Model.Impl;
using PepeRiccardo.Model.Api;
using Xunit.Abstractions;

public class PhysicsTest
{
    public const double X_STARTING_POSITION = 7.5;
    public const double Y_STARTING_POSITION = 0.0;
    public const double BALL_SIZE = 1.875;
    public const int MATRIX_DIMENSION = 10;
    public const string COLOR = "{\"ColorsList\":[[\"RED\",10],[\"YELLOW\",10],[\"BLUE\",10],[\"GREEN\",10],[\"BLACK\",10],[\"PURPLE\",10],[\"ORANGE\",10],[\"GREY\",10]]}";
    public const string LEVEL1 = "{\"level\":{\"RED\":[[0,0],[0,1],[1,0],[1,1],[2,4],[2,5],[3,3],[3,4]],\"YELLOW\":[[0,2],[0,3],[1,2],[1,3],[2,6],[2,7],[3,5],[3,6]],\"BLUE\":[[0,4],[0,5],[1,4],[1,5],[2,0],[2,1],[3,0]],\"GREEN\":[[0,6],[0,7],[1,6],[2,2],[2,3],[3,1],[3,2]]}}";
    public const double DELTA_TIME = 0.05;

    private Physics _physics;
    private BallFactory _factory;
    private IBall?[,] _matrixBall;

    public PhysicsTest()
    {
        _physics = new Physics((15, 10), 5, BALL_SIZE, (X_STARTING_POSITION, Y_STARTING_POSITION));
        _factory = new BallFactory(
            JSONParser.GetIstance().ParserColors(COLOR), BALL_SIZE);
        Level level = new Level(_factory, (MATRIX_DIMENSION, MATRIX_DIMENSION));
        _matrixBall = level.GetStartBalls(JSONParser.GetIstance().ParserStarterBalls(LEVEL1));
    }

    // Testing if the positionc calc work as expected
    [Fact]
    public void CalcPositionTest()
    {
        FlyingBall ball = new FlyingBall("RED", 10, BALL_SIZE, (X_STARTING_POSITION, Y_STARTING_POSITION));
       
        // Shot without bounce 25°
        Assert.Equal(2.968, _physics.CalcBallPosition(ball, 25, 1).Item1, 0.2);
        Assert.Equal(2.113, _physics.CalcBallPosition(ball, 25, 1).Item2, 0.2);

        // Shot without bounce 165°
        Assert.Equal(12.330, _physics.CalcBallPosition(ball, 165, 1).Item1, 0.2);
        Assert.Equal(1.294, _physics.CalcBallPosition(ball, 165, 1).Item2, 0.2);

        // Shot with 1 bounce 25°
        Assert.Equal(3.543, _physics.CalcBallPosition(ball, 25, 2).Item1, 0.2);
        Assert.Equal(3.961, _physics.CalcBallPosition(ball, 25, 2).Item2, 0.2);

        // Shot with 1 bounce 150°
        Assert.Equal(11.756, _physics.CalcBallPosition(ball, 150, 2).Item1, 0.2);
        Assert.Equal(4.446, _physics.CalcBallPosition(ball, 150, 2).Item2, 0.2);

        // Shot with 2 bounces 25°
        Assert.Equal(9.899, _physics.CalcBallPosition(ball, 45, 6).Item1, 0.2);
        Assert.Equal(18.084, _physics.CalcBallPosition(ball, 45, 6).Item2, 0.2);
    }

    // Test if the isAttached method work as expected in physics
    [Fact]
    public void BasicIndexesTest()
    {
        

        // Create a simple ball matrix with one ball
        IBall[,] basicMatrixBall = new IBall[10, 10];
        basicMatrixBall[0, 4] = _factory.CreateStaticBall("RED");

        // Positioning at the top-left
        FlyingBall ball = (FlyingBall) _factory.CreateFlyingBall("RED", (1, 9.1));
        (int, int)? indexes = _physics.IsAttached(0, basicMatrixBall, ball);

        if (indexes != null)
        {
            Assert.Equal(0, indexes.Value.Item2);
            Assert.Equal(0, indexes.Value.Item1);
        }
        
        // Positioning at the bottom of the existing ball
        ball.Position = (7.5, 8);
        indexes = _physics.IsAttached(0, basicMatrixBall, ball);

        if (indexes != null)
        {
            Assert.Equal(1, indexes.Value.Item2);
            Assert.Equal(3, indexes.Value.Item1);
        }

        // There is no position because is still in the air
        ball.Position = (6.5, 5);
        indexes = _physics.IsAttached(0, basicMatrixBall, ball);

        Assert.Null(indexes);

        // Position at the top with the wall down
        ball.Position = (7.4, 5);
        indexes = _physics.IsAttached(4, basicMatrixBall, ball);

        if (indexes != null)
        {
            Assert.Equal(0, indexes.Value.Item2);
            Assert.Equal(3, indexes.Value.Item1);
        }
    }

    // Test a shot simulation
    // Shot of 173° in level1
    [Fact]
    public void PhysicsTest1()
    {
        // Create a FlyingBall
        FlyingBall ball = (FlyingBall) _factory.CreateFlyingBall("RED", (X_STARTING_POSITION, Y_STARTING_POSITION));

        (int, int)? result = null;
        double time = 0;

        // This simulate a shot, check the position every 0.05 time unit
        while (result == null) {
            (double, double) position = _physics.CalcBallPosition(ball, 173, time);
            ball.Position = position;
            result = _physics.IsAttached(0, _matrixBall, ball);
            time += DELTA_TIME;
        }

        Assert.Equal(0, result.Value.Item1);
        Assert.Equal(4, result.Value.Item2);
    }

    // Shot of 15° in level1
    [Fact]
    public void PhysicsTest2()
    {
        // Create a FlyingBall
        FlyingBall ball = (FlyingBall) _factory.CreateFlyingBall("RED", (X_STARTING_POSITION, Y_STARTING_POSITION));

        (int, int)? result = null;
        double time = 0;

        // This simulate a shot, check the position every 0.05 time unit
        while (result == null) {
            (double, double) position = _physics.CalcBallPosition(ball, 15, time);
            ball.Position = position;
            result = _physics.IsAttached(0, _matrixBall, ball);
            time += DELTA_TIME;
        }

        Assert.Equal(1, result.Value.Item1);
        Assert.Equal(4, result.Value.Item2);
    }

    // Shot of 25° in level1 whitout 2 the first two balls of the 4rd row
    [Fact]
    public void PhysicsTest3()
    {
        // Create a FlyingBall
        FlyingBall ball = (FlyingBall) _factory.CreateFlyingBall("RED", (X_STARTING_POSITION, Y_STARTING_POSITION));

        _matrixBall[3, 0] = null;
        _matrixBall[3, 1] = null;

        (int, int)? result = null;
        double time = 0;

        // This simulate a shot, check the position every 0.05 time unit
        while (result == null) {
            (double, double) position = _physics.CalcBallPosition(ball, 25, time);
            ball.Position = position;
            result = _physics.IsAttached(0, _matrixBall, ball);
            time += DELTA_TIME;
        }

        Assert.Equal(1, result.Value.Item1);
        Assert.Equal(3, result.Value.Item2);
    }
}