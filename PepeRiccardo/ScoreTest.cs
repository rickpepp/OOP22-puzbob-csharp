namespace PepeRiccardo;

using Xunit;
using PepeRiccardo.Model.Impl;

public class ScoreTest
{
    // Test score starting from zero
    [Fact]
    public void ScoreZeroTest()
    {
        var score = new Score();

        score.IncScore(50);
        Assert.Equal(50, score.ScoreValue);

        score.IncScore(50);
        Assert.Equal(100, score.ScoreValue);

        // As to not increment, negative values are not accepted
        score.IncScore(-50);
        Assert.Equal(100, score.ScoreValue);
    }

    // Test score starting from a number that is not zero
    [Fact]
    public void ScoreNonZeroTest()
    {
        var score = new Score(100);

        score.IncScore(50);
        Assert.Equal(150, score.ScoreValue);

        score.IncScore(50);
        Assert.Equal(200, score.ScoreValue);

        // As to not increment, negative values are not accepted
        score.IncScore(-50);
        Assert.Equal(200, score.ScoreValue);
    }
}