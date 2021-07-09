using board;

namespace Chess
{
    class King : Pieces
    {
        private ChessMatch Match;
        public King(Board Board, Colour colour, ChessMatch match) : base(colour, Board)
        {
            Match = match;
        }
        public override string ToString()
        {
            return "K";
        }
        private bool TestRookToCastling(Position pos)
        {
            Pieces p = Board.Piece(pos);
            return p != null && p is Rook && p.Colour == Colour && p.MovementsNo == 0;

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

            //Castling 
            if (MovementsNo == 0 && !Match.Check)
            {
                //Small
                Position posR1 = new Position(Position.Line, Position.Column + 3);
                if (TestRookToCastling(posR1))
                {
                    Position p1 = new Position(Position.Line, Position.Column + 1);
                    Position p2 = new Position(Position.Line, Position.Column + 2);
                    if (Board.Piece(p1) == null && Board.Piece(p2) == null)
                    {
                        mat[Position.Line, Position.Column + 2] = true;
                    }
                }
                //Big
                Position posR2 = new Position(Position.Line, Position.Column - 4);
                if (TestRookToCastling(posR2))
                {
                    Position p1 = new Position(Position.Line, Position.Column - 1);
                    Position p2 = new Position(Position.Line, Position.Column - 2);
                    Position p3 = new Position(Position.Line, Position.Column - 3);
                    if (Board.Piece(p1) == null && Board.Piece(p2) == null && Board.Piece(p3) == null)
                    {
                        mat[Position.Line, Position.Column - 2] = true;
                    }
                }
            }

            return mat;
        }
    }
}
