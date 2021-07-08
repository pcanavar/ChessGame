using board;

namespace Chess
{
    class King : Pieces
    {
        public King(Board Board, Colour colour) : base(colour, Board)
        {

        }
        public override string ToString()
        {
            return "K";
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
            if (Board.IsValidPosition(pos) && IsAbleToMove(pos)) { mat[pos.Line, pos.Column] = true; }
            //NE
            pos.DefineValues(Position.Line - 1, Position.Column + 1);
            if (Board.IsValidPosition(pos) && IsAbleToMove(pos)) { mat[pos.Line, pos.Column] = true; }
            //Right
            pos.DefineValues(Position.Line, Position.Column + 1);
            if (Board.IsValidPosition(pos) && IsAbleToMove(pos)) { mat[pos.Line, pos.Column] = true; }
            //SE
            pos.DefineValues(Position.Line + 1, Position.Column + 1);
            if (Board.IsValidPosition(pos) && IsAbleToMove(pos)) { mat[pos.Line, pos.Column] = true; }
            //Down
            pos.DefineValues(Position.Line + 1, Position.Column);
            if (Board.IsValidPosition(pos) && IsAbleToMove(pos)) { mat[pos.Line, pos.Column] = true; }
            //SW
            pos.DefineValues(Position.Line + 1, Position.Column - 1);
            if (Board.IsValidPosition(pos) && IsAbleToMove(pos)) { mat[pos.Line, pos.Column] = true; }
            //Left
            pos.DefineValues(Position.Line, Position.Column - 1);
            if (Board.IsValidPosition(pos) && IsAbleToMove(pos)) { mat[pos.Line, pos.Column] = true; }
            //NW
            pos.DefineValues(Position.Line - 1, Position.Column - 1);
            if (Board.IsValidPosition(pos) && IsAbleToMove(pos)) { mat[pos.Line, pos.Column] = true; }

            return mat;
        }
    }
}
