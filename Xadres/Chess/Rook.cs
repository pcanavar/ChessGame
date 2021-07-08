using board;

namespace Chess
{
    class Rook : Pieces
    {
        public Rook(Board Board, Colour colour) : base(colour, Board)
        {
        }
        public override string ToString()
        {
            return "R";
        }
        private bool IsAbleToMove(Position pos)
        {
            Pieces p = Board.Piece(pos);
            return p == null || p.Colour != Colour;
        }
        public override bool[,] PossibleMovements()
        {

            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            //UP
            pos.DefineValues(Position.Line - 1, Position.Column);
            while (Board.IsValidPosition(pos) && IsAbleToMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Colour != Colour) { break; }
                pos.Line -= 1;
            }
            //Down
            pos.DefineValues(Position.Line + 1, Position.Column);
            while (Board.IsValidPosition(pos) && IsAbleToMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Colour != Colour) { break; }
                pos.Line += 1;
            }
            //Right
            pos.DefineValues(Position.Line, Position.Column + 1);
            while (Board.IsValidPosition(pos) && IsAbleToMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Colour != Colour) { break; }
                pos.Column += 1;
            }
            //Left
            pos.DefineValues(Position.Line, Position.Column - 1);
            while (Board.IsValidPosition(pos) && IsAbleToMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Colour != Colour) { break; }
                pos.Column -= 1;
            }

            return mat;
        }
    }
}
