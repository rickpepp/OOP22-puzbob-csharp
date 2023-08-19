namespace PepeRiccardo.Model.Api
{
    public interface IJSONParser
    {
        // This parse a json string to a dictionary (Name of the color: Score of the color)
        Dictionary<string, int> ParserColors(string jsonString);

        // This read a json string and return a dictionary (Name of the color: List of index to put in the board)
        Dictionary<string, List<(int, int)>> ParserStarterBalls(string jsonString);

        // This parse a json string to a dictionary (Name of the color: HEX code of the color)
        Dictionary<string, string> ParserColorsView(string jsonString);

        // This parse a json string and return the Score saved
        int ParserScore(string jsonString);

        // This parse a json string and return the Level saved
        int ParserLevel(string jsonString);
        
        // Return the state passed in the arguments as JSON string
        string SaveState(int score, int level);
    }
}