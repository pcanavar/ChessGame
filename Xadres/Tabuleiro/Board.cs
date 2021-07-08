namespace Tabuleiro
{
    class Board
    {
        public int Line { get; set; }
        public int Column { get; set; }
        private Pieces[,] Pieces;

        public Board(int line, int column)
        {
            Line = line;
            Column = column;
            Pieces = new Pieces[line, column];
        }
    }
}
