namespace PepeRiccardo.Model.Impl
{
    public class Score 
    {
        private int _scoreValue = 0;

        // Property that check if the value to increment is positive
        public int ScoreValue 
        { 
            get => _scoreValue; 
            private set {
                if (value >= 0)
                {
                    _scoreValue += value;
                }
            }
        }

        public Score(int baseScore)
        {
            ScoreValue = baseScore;
        }

        // If there are not arguments, starting value is zero
        public Score() : this(0) {}

        // Method that inc the value
        public void IncScore(int value) 
        {
            ScoreValue = value;
        }

    }
    
}