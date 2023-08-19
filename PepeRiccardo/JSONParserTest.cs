namespace PepeRiccardo;

using Xunit;
using PepeRiccardo.Model.Impl;

public class JSONParserTest
{
    // TEst the ParserColors method
    [Fact]
    public void ParseColorTest()
    {
        String colors = 
        "{\"ColorsList\":[[\"RED\",10],[\"YELLOW\",10],[\"BLUE\",10],[\"GREEN\",10],[\"BLACK\",10],[\"PURPLE\",10],[\"ORANGE\",10],[\"GREY\",10]]}";
    
        Dictionary<string, int> dictionaryExpected = new Dictionary<string, int>
        {
            { "RED", 10 },
            { "YELLOW", 10 },
            { "BLUE", 10 },
            { "GREEN", 10 },
            { "BLACK", 10 },
            { "PURPLE", 10 },
            { "ORANGE", 10 },
            { "GREY", 10 }
        };

        Console.WriteLine(colors);

        Assert.Equal(dictionaryExpected, JSONParser.GetIstance().ParserColors(colors));
    }

    // Test the ParserStarterBalls method
    [Fact]
    public void ParseBallsTest()
    {
        String level = 
        "{\"level\":{\"RED\":[[0,0],[0,1]],\"BLUE\":[[0,4],[0,5]],\"YELLOW\":[[0,2]],\"GREEN\":[[0,6]]}}";

        Dictionary<string, List<(int, int)>> dictionaryExpected = new Dictionary<string, List<(int, int)>>();

        List<(int, int)> listRED = new List<(int, int)>
        {
            (0, 0),
            (0, 1)
        };

        List<(int, int)> listYELLOW = new List<(int, int)>
        {
            (0, 2)
        };

        List<(int, int)> listBLUE = new List<(int, int)>
        {
            (0, 4),
            (0, 5)
        };

        List<(int, int)> listGREEN = new List<(int, int)>
        {
            (0, 6)
        };

        dictionaryExpected.Add("RED", listRED);
        dictionaryExpected.Add("BLUE", listBLUE);
        dictionaryExpected.Add("YELLOW", listYELLOW);
        dictionaryExpected.Add("GREEN", listGREEN);

        Assert.Equal(dictionaryExpected, JSONParser.GetIstance().ParserStarterBalls(level));
    }

    // Test the SaveState, ParserScore and ParserLevel methods
    [Fact]
    public void ParseSaveTest()
    {
        string save = JSONParser.GetIstance().SaveState(1574, 22);

        Console.WriteLine(save);

        Assert.Equal(1574, JSONParser.GetIstance().ParserScore(save));
        Assert.Equal(22, JSONParser.GetIstance().ParserLevel(save));
    }

    // Test the ParserColorsView method
    [Fact]
    public void ParseColorViewTest()
    {
        string colors = "{\"ColorsList\":[[\"RED\", \"#dd4538\"],[\"YELLOW\", \"#f6e357\"],[\"BLUE\", \"#2c5de9\"],[\"GREEN\", \"#63d661\"],[\"BLACK\", \"#7e7c81\"],[\"PURPLE\", \"#af70ca\"],[\"ORANGE\", \"#e4852f\"],[\"GREY\", \"#d7daea\"]]}";

        Dictionary<string, string> dictionaryExpected = new Dictionary<string, string>
        {
            { "RED", "#dd4538" },
            { "YELLOW", "#f6e357" },
            { "BLUE", "#2c5de9" },
            { "GREEN", "#63d661" },
            { "BLACK", "#7e7c81" },
            { "PURPLE", "#af70ca" },
            { "ORANGE", "#e4852f" },
            { "GREY", "#d7daea" }
        };

        Assert.Equal(dictionaryExpected, JSONParser.GetIstance().ParserColorsView(colors));

    }
}