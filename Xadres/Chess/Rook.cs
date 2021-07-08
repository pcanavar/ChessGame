using board;

namespace Chess
{
    class Rook : Pieces
    {
        public Rook(Board board, Colour colour) : base(colour, board)
        {
        }
        public override string ToString()
        {
            return "R";
        }
    }
}
