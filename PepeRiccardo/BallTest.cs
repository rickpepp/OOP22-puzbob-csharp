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
}