using board;

namespace Chess
{
    class Pawn : Pieces
    {
        public Pawn(Board Board, Colour colour) : base(colour, Board)
        {
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
            }
            return mat;
        }
    }
}
