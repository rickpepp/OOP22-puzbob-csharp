using PepeRiccardo.Model.Api;
using Newtonsoft.Json;

#nullable disable

namespace PepeRiccardo.Model.Impl
{
    // This is a class that rapresent the object rappresentation of the json save file
    public class JSONSave
    {
        public int Score { get; set; }
        public int Level { get; set; }
    }

    // This is the parser, this is a Singleton class
    public class JSONParser : IJSONParser
    {
        private static JSONParser _parser;

        private JSONParser() {}

        // To access statically
        public static JSONParser GetIstance(){
            _parser ??= new JSONParser();
            return _parser;
        }

        // This parse a json string to a dictionary (Name of the color: Score of the color)
        public Dictionary<string, int> ParserColors(string jsonString)
        {
            var colorsList = JsonConvert.DeserializeObject<Dictionary<string, List<List<Object>>>>(jsonString);

            return CheckIsNull(colorsList["ColorsList"].ToDictionary(
                colorInfo => (string)colorInfo[0],
                colorInfo => Convert.ToInt32(colorInfo[1])
            ));
        }

        // This parse a json string to a dictionary (Name of the color: HEX code of the color)
        public Dictionary<string, string> ParserColorsView(string jsonString)
        {
            var colorsList = JsonConvert.DeserializeObject<Dictionary<string, List<List<string>>>>(jsonString);

            return CheckIsNull(colorsList["ColorsList"].ToDictionary(
                colorInfo => colorInfo[0],
                colorInfo => colorInfo[1]
            ));
        }

        // This parse a json string and return the Level saved
        public int ParserLevel(string jsonString)
        {
            return CheckIsNull(JsonConvert.DeserializeObject<JSONSave>(jsonString).Level);
        }

        // This parse a json string and return the Score saved
        public int ParserScore(string jsonString)
        {
            return JsonConvert.DeserializeObject<JSONSave>(jsonString).Score;
        }

        // This read a json string and return a dictionary (Name of the color: List of index to put in the board)
        public Dictionary<string, List<(int, int)>> ParserStarterBalls(string jsonString)
        {
            var ballsDictionary = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, List<List<int>>>>>(jsonString);

            Dictionary<string, List<(int, int)>> result = new Dictionary<string, List<(int, int)>>();

            foreach (var item in ballsDictionary["level"])
            {
                string key = item.Key;
                var values = item.Value.Select(
                    token => (token[0], token[1])
                ).ToList();
                result.Add(key, values);
            }

            return CheckIsNull(result);
        }

        // Return the state passed in the arguments as JSON string
        public string SaveState(int score, int level)
        {
            JSONSave save = new JSONSave
            {
                Score = score,
                Level = level
            };

            return JsonConvert.SerializeObject(save);
        }

        // Check if the Object in input is null, if is null throw an exception
        private T CheckIsNull<T>(T inputObject)
        {
            if (inputObject != null)
            {
                return inputObject;
            }
            else
            {
                throw new Exception("Impossible to decode the string");
            }
        }
    }
}