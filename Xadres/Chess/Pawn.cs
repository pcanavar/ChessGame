using board;

namespace Chess
{
    class Pawn : Pieces

    {
        private ChessMatch Match;
        public Pawn(Board Board, Colour colour, ChessMatch match) : base(colour, Board)
        {
            Match = match;
        }
        public override string ToString()
        {
            return "P";
        }
        private bool EnemyExists(Position pos)
        {
            Pieces p = Board.Piece(pos);
            return p != null && p.Colour != Colour;
        }
        private bool free (Position pos)
        {
            return Board.Piece(pos) == null;
        }

        public override bool[,] PossibleMovements()
        {

            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            if (Colour == Colour.White)
            {
                pos.DefineValues(Position.Line - 1, Position.Column);
                if (Board.IsValidPosition(pos) && free(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValues(Position.Line - 2, Position.Column);
                Position p2 = new Position(Position.Line - 1, Position.Column);
                if (Board.IsValidPosition(p2) && Board.IsValidPosition(pos) && free(pos) && MovementsNo == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValues(Position.Line - 1, Position.Column - 1);
                if (Board.IsValidPosition(pos) && EnemyExists(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValues(Position.Line - 1, Position.Column + 1);
                if (Board.IsValidPosition(pos) && EnemyExists(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                //#EnPassant
                if (Position.Line == 3)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.IsValidPosition(left) && EnemyExists(left) && Board.Piece(left) == Match.VulnerableEnPassant) { mat[left.Line - 1, left.Column] = true; }
                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.IsValidPosition(right) && EnemyExists(right) && Board.Piece(right) == Match.VulnerableEnPassant) { mat[right.Line - 1, right.Column] = true; }
                }
            }
            else
            {
                pos.DefineValues(Position.Line + 1, Position.Column);
                if (Board.IsValidPosition(pos) && free(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValues(Position.Line + 2, Position.Column);
                Position p2 = new Position(Position.Line + 1, Position.Column);
                if (Board.IsValidPosition(p2) && Board.IsValidPosition(pos) && free(pos) && MovementsNo == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValues(Position.Line + 1, Position.Column - 1);
                if (Board.IsValidPosition(pos) && EnemyExists(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValues(Position.Line + 1, Position.Column + 1);
                if (Board.IsValidPosition(pos) && EnemyExists(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                //#EnPassant
                if (Position.Line == 4)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.IsValidPosition(left) && EnemyExists(left) && Board.Piece(left) == Match.VulnerableEnPassant) { mat[left.Line + 1, left.Column] = true; }
                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.IsValidPosition(right) && EnemyExists(right) && Board.Piece(right) == Match.VulnerableEnPassant) { mat[right.Line + 1, right.Column] = true; }
                }
            }
            return mat;
        }
    }
}
