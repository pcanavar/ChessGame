namespace board
{
    abstract class Pieces
    {
        public Position Position { get; set; }
        public Colour Colour { get; protected set; }
        public int MovementsNo { get; protected set; }
        public Board Board { get; protected set; }

        public Pieces(Colour colour, Board board)
        {
            Position = null;
            Colour = colour;
            Board = board;
            MovementsNo = 0;
        }
        public void IncreaseMovementNo()
        {
            MovementsNo++;
        }
        public void DecreaseMovementNo()
        {
            MovementsNo--;
        }
        public bool IsPossibleMovementsAvailable()
        {
            bool[,] mat = PossibleMovements();
            for (int i = 0; i < Board.Lines; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }

            }
            return false;
        }
        public bool PossibleMovement(Position pos)
        {
            return PossibleMovements()[pos.Line, pos.Column];
        }
        public abstract bool[,] PossibleMovements();
    }
}
