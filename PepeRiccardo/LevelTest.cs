namespace PepeRiccardo;

using Xunit;
using PepeRiccardo.Model.Impl;
using PepeRiccardo.Model.Api;

public class LevelTest
{
    
    [Fact]
    public void StartingLevelTest()
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

        // Create the dictionary that contain the balls Position
        Dictionary<string, List<(int, int)>> ballsPosition = new Dictionary<string, List<(int, int)>>
        {
            {"RED", new List<(int, int)> {(0, 0), (1, 0), (2, 0)}},
            {"BLUE", new List<(int, int)> {(0, 1)}}
        };

        // New Level
        var level = new Level(factory, (3, 3));

        // Decode the dictionary in a matrix
        IBall?[,] startingBall = level.GetStartBalls(ballsPosition);

        // Check if the values are the expected
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++) 
            {
                var indexes = (i, j);
                var ball = startingBall[i, j];
                if (ball != null)
                {
                    switch (indexes)
                    {
                        case (0, 0):
                        case (1, 0):
                        case (2, 0): 
                            Assert.Equal("StaticBall Color: RED Score: 10 Size: 15", ball.StringRapresentation());
                            break;
                        case (0, 1): 
                            Assert.Equal("StaticBall Color: BLUE Score: 20 Size: 15", ball.StringRapresentation());
                            break;
                        default:
                            break;
                    }
                }
            } 
        }
    }
}