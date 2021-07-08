using board;

namespace Chess
{
    class King : Pieces
    {
        public King (Board board, Colour colour) : base(colour, board)
        {
        }
        public override string ToString()
        {
            return "K";
        }
    }
}
