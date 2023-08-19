namespace PepeRiccardo;

using Xunit;
using PepeRiccardo.Model.Impl;
using PepeRiccardo.Model.Api;

public class BallTest
{
    // Testing the Static Ball
    [Fact]
    public void StaticBallTest()
    {
        IBall ball1 = new StaticBall("RED", 15, 15.2);
        IBall ball2 = new StaticBall("BLUE", 32, 31.2);

        Assert.Equal("StaticBall Color: RED Score: 15 Size: 15,2", ball1.StringRapresentation());
        Assert.Equal("StaticBall Color: BLUE Score: 32 Size: 31,2", ball2.StringRapresentation());
    }

    // Testing the Flying Ball
    [Fact]
    public void FlyingBallTest()
    {
        IBall ball1 = new FlyingBall("RED", 15, 15.2, (0.0, 0.0));
        IBall ball2 = new FlyingBall("BLUE", 32, 31.2, (26.8, 87.1));

        Assert.Equal("FlyingBall Color: RED Score: 15 Size: 15,2 XPosition: 0 YPosition: 0", ball1.StringRapresentation());
        Assert.Equal("FlyingBall Color: BLUE Score: 32 Size: 31,2 XPosition: 26,8 YPosition: 87,1", ball2.StringRapresentation());
    }

    // Testing BallFactory
    [Fact]
    public void BallFactoryTest()
    {
        // New colorMap
        Dictionary<string, int> colorMap = new Dictionary<string, int>
        {
            ["RED"] = 10,
            ["BLUE"] = 20,
            ["YELLOW"] = 30
        };

        // New factory
        IBallFactory factory = new BallFactory(colorMap, 15);

        // StaticBall
        IBall ball1 = factory.CreateStaticBall("RED");
        IBall ball2 = factory.CreateStaticBall("YELLOW");

        Assert.Equal("StaticBall Color: RED Score: 10 Size: 15", ball1.StringRapresentation());
        Assert.Equal("StaticBall Color: YELLOW Score: 30 Size: 15", ball2.StringRapresentation());
        Assert.Throws<KeyNotFoundException>(() => factory.CreateStaticBall("WHITE"));

        // FlyingBall
        IBall ball3 = factory.CreateFlyingBall("BLUE", (0, 0));
        IBall ball4 = factory.CreateFlyingBall("YELLOW", (30, 5));

        // Needed because a warning if ball3 and ball4 are null, but is check if is null too
        #pragma warning disable CS8600, CS8602

        if (ball3 is FlyingBall && ball4 is FlyingBall) 
        {
            ball3 = ball3 as FlyingBall;
            ball4 = ball4 as FlyingBall;
            Assert.Equal("FlyingBall Color: BLUE Score: 20 Size: 15 XPosition: 0 YPosition: 0", ball3.StringRapresentation());
            Assert.Equal("FlyingBall Color: YELLOW Score: 30 Size: 15 XPosition: 30 YPosition: 5", ball4.StringRapresentation());
            Assert.Throws<KeyNotFoundException>(() => factory.CreateFlyingBall("WHITE", (0, 0)));
        }

        
    }
}