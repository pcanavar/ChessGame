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

    }
}
