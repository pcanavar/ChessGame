using board;

namespace Chess
{
    class Knight : Pieces
    {
        public Knight(Board Board, Colour colour) : base(colour, Board)
        {
        }
        public override string ToString()
        {
            return "N";
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


            
            pos.DefineValues(Position.Line - 1, Position.Column - 2);
            if (Board.IsValidPosition(pos) && IsAbleToMove(pos)) { mat[pos.Line, pos.Column] = true; }
            
            pos.DefineValues(Position.Line - 2, Position.Column - 1);
            if (Board.IsValidPosition(pos) && IsAbleToMove(pos)) { mat[pos.Line, pos.Column] = true; }
            
            pos.DefineValues(Position.Line - 2, Position.Column + 1);
            if (Board.IsValidPosition(pos) && IsAbleToMove(pos)) { mat[pos.Line, pos.Column] = true; }
            
            pos.DefineValues(Position.Line - 1, Position.Column + 2);
            if (Board.IsValidPosition(pos) && IsAbleToMove(pos)) { mat[pos.Line, pos.Column] = true; }
            
            pos.DefineValues(Position.Line + 1, Position.Column + 2);
            if (Board.IsValidPosition(pos) && IsAbleToMove(pos)) { mat[pos.Line, pos.Column] = true; }
            
            pos.DefineValues(Position.Line + 2, Position.Column + 1);
            if (Board.IsValidPosition(pos) && IsAbleToMove(pos)) { mat[pos.Line, pos.Column] = true; }
            
            pos.DefineValues(Position.Line + 2, Position.Column - 1);
            if (Board.IsValidPosition(pos) && IsAbleToMove(pos)) { mat[pos.Line, pos.Column] = true; }
            
            pos.DefineValues(Position.Line + 1, Position.Column - 2);
            if (Board.IsValidPosition(pos) && IsAbleToMove(pos)) { mat[pos.Line, pos.Column] = true; }


            return mat;
        }
    }
}
