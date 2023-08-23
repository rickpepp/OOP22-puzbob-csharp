using System.Runtime.InteropServices;
using BarbaBeatrice.API;

namespace BarbaBeatrice.Impl{
    public class Board : IBoard{
        private const int ROW_MATRIX = 2;
        private const int COLUMNS_MATRIX = 4;

        private Tuple<double, double> _dimension;
        private IBall?[,] _matrix;
        private Dictionary<Tuple<int, int>, IBall> _ball4Remove;
        private Dictionary<Tuple<int, int>, IBall> _ballFree4Remove;
        private Dictionary<Tuple<int, int>, IBall> _ballChecked;
        private int _score;

        public Board(double height, double width, IBall?[,] matrixBall)
        {
            _matrix = matrixBall;
            _dimension = new Tuple<double, double>(height, width);
            _ball4Remove = new();
            _ballChecked = new();
            _ballFree4Remove = new();
        }

        public void AddBall(int x, int y, IBall ball)
        {
            if(_matrix[x,y] == null){
                _matrix[x,y] = ball;
            }
        }

        public Tuple<double, double> GetBoardSize() => _dimension;
        public IBall?[,] GetStatusBoard() => _matrix;

        public List<string> GetColors()
        {
            List<string> colors = new List<string>();
            foreach(var ball in _matrix){
                if(ball != null){
                    colors.Add(ball.getColor);
                }
            }
            return colors;
        }

        // Searches for adjacent balls
        private Dictionary<Tuple<int, int>, IBall> SearchNeighbour( int row, int col)
        {
            List<Tuple<int, int>> pointToCheck = new();
            Dictionary<Tuple<int,int>, IBall> neighbour = new(); 

            pointToCheck.Add(new Tuple<int,int>(row - 1, col));
            pointToCheck.Add(new Tuple<int,int>(row, col - 1));
            pointToCheck.Add(new Tuple<int,int>(row + 1, col));
            pointToCheck.Add(new Tuple<int,int>(row, col + 1));

            if(row %2 != 0){
                pointToCheck.Add(new Tuple<int,int>(row - 1, col + 1));
                pointToCheck.Add(new Tuple<int,int>(row + 1, col + 1));
            }else{
                pointToCheck.Add(new Tuple<int,int>(row - 1, col - 1));
                pointToCheck.Add(new Tuple<int,int>(row + 1, col - 1));
            }

            foreach(var ball in pointToCheck){
                try{
                    if(_matrix[ball.Item1, ball.Item2] != null){
                        neighbour.Add(ball,_matrix[ball.Item1, ball.Item2]!);
                    }
                }catch{

                }
            }

            return neighbour;
        }
        
        // Look for neightboing balls that have the same color
        private Dictionary<Tuple<int,int>, IBall> SameColorFinder(Dictionary<Tuple<int,int>, IBall> neighbour, IBall ball){
            Dictionary<Tuple<int, int>, IBall> sameColor = new();
            foreach(var position in neighbour.Keys){
                if(neighbour[position].getColor! == ball.getColor!){
                    sameColor.Add(position, neighbour[position]);
                }
            }

            return sameColor;
        }

        // Checks which balls are to be removed recorsively
        private void CheckRemoveBall(Tuple<int,int> positionBall, IBall ball)
        {
            Dictionary<Tuple<int,int>, IBall> neighbour = new();
            Dictionary<Tuple<int,int>, IBall> neightbourSameColor = new();

            _ball4Remove.Add(positionBall,ball);
            neighbour = SearchNeighbour(positionBall.Item1, positionBall.Item2);
            neightbourSameColor = SameColorFinder(neighbour, ball);

            foreach(var position in neightbourSameColor.Keys){
                if(_ball4Remove.ContainsKey(position) == false){
                    CheckRemoveBall(position, neightbourSameColor[position]);
                }
            }
        }

        // Removes the list balls from the matrix
        private void Remove(Dictionary<Tuple<int,int>, IBall> list){
            foreach(var ball in list){
                _score = _score + _matrix[ball.Key!.Item1, ball.Key!.Item2]!.getScore;
                _matrix[ball.Key.Item1, ball.Key.Item2] = null;
            }
        }  

        // Looks for the neighbour balls until they ends
        private void CheckFreeBall(int x, int y)
        {
            Dictionary<Tuple<int, int>, IBall> neighbour = SearchNeighbour(x,y);
            foreach(var position in neighbour.Keys){
                if(_ballChecked.ContainsKey(position) == false){
                    _ballChecked.Add(position, _matrix[position!.Item1, position!.Item2]!);
                    CheckFreeBall(position.Item1, position.Item2);
                }
            }
        }

        // Returns true or false if the first line is empty or not
        private Boolean CheckFirstLineEmpty(){
            for(int i = 0; i < COLUMNS_MATRIX; i++){
                if(_matrix[0,i] != null){
                    return false;
                }
            }
            return true;
        }

        // Removes the balls and returns the score
        public int RemoveBall(int x, int y, IBall ball)
        {
            Tuple<int, int> positionBall = new(x,y);
            _score = 0;

            CheckRemoveBall(positionBall, ball);

            if(_ball4Remove.Count >= 3){
                Remove(_ball4Remove);
            }

            _ball4Remove.Clear();

            if(CheckFirstLineEmpty() == false){
                for(int i = 0; i < COLUMNS_MATRIX; i++){
                    if(_matrix[0,i] != null){
                        CheckFreeBall(0, i);
                    }
                }

                for(int i = 0; i < ROW_MATRIX; i++){
                    for(int k = 0; k < COLUMNS_MATRIX; k++){
                        if(_matrix[i,k] != null){
                            if(_ballChecked.ContainsKey(new Tuple<int,int>(i,k)) == false){
                                _ballFree4Remove.Add(new Tuple<int,int>(i,k), _matrix[i,k]!);
                            }
                        }
                    }
                }

                Remove(_ballFree4Remove);
                _ballChecked.Clear();
                _ballFree4Remove.Clear();
            }else{
                Dictionary<Tuple<int,int>, IBall> ballRemove = new();

                for(int i = 0; i < ROW_MATRIX; i++){
                    for(int k = 0; k< COLUMNS_MATRIX; k++){
                        if(_matrix[i,k] != null){
                            ballRemove.Add(new Tuple<int,int>(i,k), _matrix[i,k]!);
                        }
                    }
                }

                Remove(ballRemove);
            }

            return _score;
        }
        
        // Creates an inscription to give a print shape to the matrix
        private string ConvertMatrixToString()
        {
            string label = "";
            foreach(var ball in _matrix){
                label = label + "|" + (ball == null ? "null" : ball.ToString());
            }
            return label;
        }

        public new string ToString() => "Dimension board is: " + _dimension.Item1 + ", " + _dimension.Item2 + ". Balls in the board: " + ConvertMatrixToString();
    }
}