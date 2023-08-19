using PepeRiccardo.Model.Api;

namespace PepeRiccardo.Model.Impl
{
    // This class read, write and delete a string file. This is a Singleton Class
    public class JSONWReader : IJSONWReader
    {
        private static JSONWReader? _reader;

        private JSONWReader() {}

        public static JSONWReader GetIstance(){
            _reader ??= new JSONWReader();
            return _reader;
        }

        // This method delete the text file
        public void DeleteJSON(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            else
            {
                throw new FileNotFoundException("The file already doesn't exist");
            }
        }

        // This method read the text file
        public string ReadJSON(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            else
            {
                throw new FileNotFoundException("File Not Found");
            }
        }

        // This method write a text file. If the dirPath doesn't exist, it create it
        public void WriteJSON(string dirPath, string path, string json)
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            File.WriteAllText(path, json);
        }
    }
}