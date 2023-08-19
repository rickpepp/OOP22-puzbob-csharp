namespace PepeRiccardo.Model.Api
{
    public interface IJSONParser
    {
        Dictionary<string, int> ParserColors(string jsonString);
        Dictionary<string, List<(int, int)>> ParserStarterBalls(string jsonString);
        Dictionary<string, string> ParserColorsView(string jsonString);
        int ParserScore(string jsonString);
        int ParserLevel(string jsonString);
        string SaveState(int score, int level);
    }
}