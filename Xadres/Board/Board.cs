namespace board
{
    class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        private Pieces[,] Pieces;
        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            Pieces = new Pieces[lines, columns];
        }
        public Pieces Piece (int line, int column)
        {
            return Pieces[line, column];
        }
        public Pieces Piece (Position pos)
        {
            return Pieces[pos.Line, pos.Column];
        }
        public bool IsPositionOccupied(Position pos)
        {
            ValidatePosition(pos);
            return Piece(pos) != null;
        }
        public void PlacePiece (Pieces p, Position pos)
        {
            if (IsPositionOccupied(pos)) { throw new BoardException("Position Already Occuppied"); }
            Pieces[pos.Line, pos.Column] = p;
            p.Position = pos;
        }
        public bool IsValidPosition(Position pos)
        {
            if (pos.Line<0 || pos.Line>=Lines || pos.Column<0 || pos.Column >= Columns) { return false; }
            return true;
        }
        public void ValidatePosition(Position pos)
        {
            if (!IsValidPosition(pos))
            {
                throw new BoardException("Invalid position");
            }
        }
    }
}
