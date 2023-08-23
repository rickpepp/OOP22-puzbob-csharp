using Xunit;
using BarbaBeatrice.API;
using BarbaBeatrice.Impl;

namespace BarbaBeatrice;

public class UnitTest1
{
    private const int SIZE_BALL = 15;
    private readonly Tuple<double, double> DIMENSION_BOARD = new Tuple<double, double>(300.0,200.0);
    private const int ROW_MATRIX = 2;
    private const int COLUMNS_MATRIX = 4;

    private Dictionary<string, int> _colorMap = new Dictionary<string, int>(){{"RED",10}, {"GREEN", 15}, {"YELLOW", 20}, {"BLUE", 5}, {"PURPLE", 25}};

    private IBallFactory? _ballFactory;

    private IBall[,] _matrixBall = new BallImpl[ROW_MATRIX, COLUMNS_MATRIX];

    private IBoard? _board;
 
    private string ConvertMatrix(IBall?[,] matrix){
        string srt = "";
        foreach(var ball in matrix!){
            srt += "|" + (ball != null ? ball.ToString() : "null");
        }
        return srt;
    }

    [Fact]
    public void statusBoardTest ()
    {
        _ballFactory = new BallFactoryImpl(_colorMap, SIZE_BALL);
        _matrixBall[0,0] = _ballFactory.CreateStaticBall("RED"); 
        _matrixBall[0,1] = _ballFactory.CreateStaticBall("YELLOW");      
        _matrixBall[1,0] = _ballFactory.CreateStaticBall("BLUE"); 
        _board = new Board(DIMENSION_BOARD.Item1, DIMENSION_BOARD.Item2, _matrixBall);  
         
        Assert.Equal("|Color ball: RED, Score: 10, Size: 15|Color ball: YELLOW, Score: 20, Size: 15|null|null|Color ball: BLUE, Score: 5, Size: 15|null|null|null", ConvertMatrix(_board.GetStatusBoard()));
    }

    [Fact]
    public void GetColorsTest ()
    {
        _ballFactory = new BallFactoryImpl(_colorMap, SIZE_BALL);
        _matrixBall[0,0] = _ballFactory.CreateStaticBall("RED"); 
        _matrixBall[0,1] = _ballFactory.CreateStaticBall("YELLOW");      
        _matrixBall[1,0] = _ballFactory.CreateStaticBall("BLUE"); 
        _board = new Board(DIMENSION_BOARD.Item1, DIMENSION_BOARD.Item2, _matrixBall);  

        string test = "";
        foreach(string str in _board.GetColors()){
            test += str.ToString() + ", ";
        }

        Assert.Equal("RED, YELLOW, BLUE, ", test);
    }

    [Fact]
    public void AddBallTest ()
    {
        _ballFactory = new BallFactoryImpl(_colorMap, SIZE_BALL);
        _matrixBall[0,0] = _ballFactory.CreateStaticBall("RED"); 
        _matrixBall[0,1] = _ballFactory.CreateStaticBall("YELLOW");      
        _matrixBall[1,0] = _ballFactory.CreateStaticBall("BLUE"); 
        _board = new Board(DIMENSION_BOARD.Item1, DIMENSION_BOARD.Item2, _matrixBall); 
        _board.AddBall(1, 1, _ballFactory.CreateStaticBall("BLUE"));

        Assert.Equal("|Color ball: RED, Score: 10, Size: 15|Color ball: YELLOW, Score: 20, Size: 15|null|null|Color ball: BLUE, Score: 5, Size: 15|Color ball: BLUE, Score: 5, Size: 15|null|null", ConvertMatrix(_board.GetStatusBoard()));
    }

    [Fact]
    public void RemoveBallTest()
    {
         _ballFactory = new BallFactoryImpl(_colorMap, SIZE_BALL);
        _matrixBall[0,0] = _ballFactory.CreateStaticBall("RED"); 
        _matrixBall[0,1] = _ballFactory.CreateStaticBall("RED"); 
        _matrixBall[1,1] = _ballFactory.CreateStaticBall("RED"); 
        _matrixBall[0,2] = _ballFactory.CreateStaticBall("YELLOW");  
        _matrixBall[0,3] = _ballFactory.CreateStaticBall("YELLOW");  
        _matrixBall[1,2] = _ballFactory.CreateStaticBall("YELLOW");  
        _matrixBall[1,3] = _ballFactory.CreateStaticBall("YELLOW");   
        IBall ball = _ballFactory.CreateStaticBall("RED");
        _board = new Board(DIMENSION_BOARD.Item1, DIMENSION_BOARD.Item2, _matrixBall); 
        _board.AddBall(1, 0, ball);
        _board.RemoveBall(1, 0, ball);

        Assert.Equal("|null|null|Color ball: YELLOW, Score: 20, Size: 15|Color ball: YELLOW, Score: 20, Size: 15|null|null|Color ball: YELLOW, Score: 20, Size: 15|Color ball: YELLOW, Score: 20, Size: 15", ConvertMatrix(_board.GetStatusBoard()));
    }
}