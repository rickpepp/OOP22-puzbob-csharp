namespace PepeRiccardo;

using Xunit;
using PepeRiccardo.Model.Impl;

public class JSONWReaderTest
{
    static readonly string HOME_DIR = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    static readonly char FILE_SEPARATOR = Path.DirectorySeparatorChar;
    static readonly string DIRECTORY_SAVE = HOME_DIR + FILE_SEPARATOR + "puzbob";
    static readonly string FILE_SAVE = DIRECTORY_SAVE + "test.json";

    [Fact]
    public void WriteReadDeleteTest()
    {
        // This is the string that will be write in the HOME_DIR/puzbob/test.json
        string jsonString = 
        "{\"ColorsList\":[[\"RED\",10],[\"YELLOW\",10],[\"BLUE\",10],[\"GREEN\",10],[\"BLACK\",10],[\"PURPLE\",10],[\"ORANGE\",10],[\"GREY\",10]]}";

        JSONWReader.GetIstance().WriteJSON(DIRECTORY_SAVE, FILE_SAVE, jsonString);

        string jsonRead = JSONWReader.GetIstance().ReadJSON(FILE_SAVE);

        // Check if the text read is the same written
        Assert.Equal(jsonString, jsonRead);

        // Delete and check if is deleted
        Assert.True(File.Exists(FILE_SAVE));
        JSONWReader.GetIstance().DeleteJSON(FILE_SAVE);
        Assert.False(File.Exists(FILE_SAVE));

        // Delete the directory created for the test
        if (Directory.Exists(DIRECTORY_SAVE))
        {
            Directory.Delete(DIRECTORY_SAVE);
        }
    }
}