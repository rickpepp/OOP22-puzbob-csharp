namespace PepeRiccardo.Model.Api
{
    public interface IJSONWReader
    {
        // This method read the text file
        public string ReadJSON(string path);

        // This method write a text file. If the dirPath doesn't exist, it create it
        public void WriteJSON(string dirPath, string path, string json);

        // This method delete the text file
        public void DeleteJSON(string path);
    }
}