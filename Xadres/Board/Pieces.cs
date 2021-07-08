namespace board
{
    class Pieces
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
    }
}
