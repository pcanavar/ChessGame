using board;

namespace Chess
{
    class Bishop : Pieces
    {
        public Bishop(Board Board, Colour colour) : base(colour, Board)
        {
        }
        public override string ToString()
        {
            return "B";
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

            
            //NW
            pos.DefineValues(Position.Line - 1, Position.Column - 1);
            while (Board.IsValidPosition(pos) && IsAbleToMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Colour != Colour) { break; }
                pos.DefineValues(Position.Line - 1, pos.Column - 1);
            }
            //NE
            pos.DefineValues(Position.Line - 1, Position.Column + 1);
            while (Board.IsValidPosition(pos) && IsAbleToMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Colour != Colour) { break; }
                pos.DefineValues(Position.Line - 1, pos.Column + 1);
            }
            //SE
            pos.DefineValues(Position.Line + 1, Position.Column + 1);
            while (Board.IsValidPosition(pos) && IsAbleToMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Colour != Colour) { break; }
                pos.DefineValues(Position.Line + 1, pos.Column + 1);
            }
            //SW
            pos.DefineValues(Position.Line + 1, Position.Column - 1);
            while (Board.IsValidPosition(pos) && IsAbleToMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Colour != Colour) { break; }
                pos.DefineValues(Position.Line + 1, pos.Column - 1);
            }


            return mat;
        }
    }
}
