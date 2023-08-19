using PepeRiccardo.Model.Api;
using Newtonsoft.Json;

#nullable disable

namespace PepeRiccardo.Model.Impl
{
    internal class JSONSave
    {
        internal int Score { get; set; }
        internal int Level { get; set; }
    }
    
    public class JSONParser : IJSONParser
    {
        private static JSONParser parser;

        private JSONParser() {}

        public static JSONParser GetIstance(){
            if(parser == null){
                parser = new JSONParser();
            }
            return parser;
        }

        public Dictionary<string, int> ParserColors(string jsonString)
        {
            return CheckIsNull(JsonConvert.DeserializeObject<Dictionary<string, int>>(jsonString));
        }

        public Dictionary<string, string> ParserColorsView(string jsonString)
        {
            return CheckIsNull(JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString));
        }

        public int ParserLevel(string jsonString)
        {
            return CheckIsNull(JsonConvert.DeserializeObject<JSONSave>(jsonString)).Level;
        }

        public int ParserScore(string jsonString)
        {
            return CheckIsNull(JsonConvert.DeserializeObject<JSONSave>(jsonString)).Score;
        }

        public Dictionary<string, List<(int, int)>> ParserStarterBalls(string jsonString)
        {
            return CheckIsNull(JsonConvert.DeserializeObject<Dictionary<string, List<(int, int)>>>(jsonString));
        }

        public string SaveState(int score, int level)
        {
            JSONSave save = new JSONSave
            {
                Score = score,
                Level = level
            };

            return JsonConvert.SerializeObject(save);
        }

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