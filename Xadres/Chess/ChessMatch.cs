using board;

namespace Chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        private int Turn;
        private Colour CurrentPlayer;
        public bool Finished { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8,8);
            Turn = 1;
            CurrentPlayer = Colour.White;
            Finished = false;
            PlacePieces();
        }
        private void PlacePieces()
        {
            Board.PlacePiece(new Rook(Board, Colour.White), new ChessPosition('c', 1).ToPosition());
            Board.PlacePiece(new Rook(Board, Colour.White), new ChessPosition('c', 2).ToPosition());
            Board.PlacePiece(new Rook(Board, Colour.White), new ChessPosition('d', 2).ToPosition());
            Board.PlacePiece(new Rook(Board, Colour.White), new ChessPosition('e', 1).ToPosition());
            Board.PlacePiece(new Rook(Board, Colour.White), new ChessPosition('e', 2).ToPosition());
            Board.PlacePiece(new King(Board, Colour.White), new ChessPosition('d', 1).ToPosition());

            Board.PlacePiece(new Rook(Board, Colour.Black), new ChessPosition('c', 7).ToPosition());
            Board.PlacePiece(new Rook(Board, Colour.Black), new ChessPosition('c', 8).ToPosition());
            Board.PlacePiece(new Rook(Board, Colour.Black), new ChessPosition('d', 7).ToPosition());
            Board.PlacePiece(new Rook(Board, Colour.Black), new ChessPosition('e', 7).ToPosition());
            Board.PlacePiece(new Rook(Board, Colour.Black), new ChessPosition('e', 8).ToPosition());
            Board.PlacePiece(new King(Board, Colour.Black), new ChessPosition('d', 8).ToPosition());



        }
        public void NewMovement(Position origin, Position destination)
        {
            Pieces p = Board.RemovePiece(origin);
            p.IncreaseMovementNo();
            Pieces captured = Board.RemovePiece(destination);
            Board.PlacePiece(p, destination);
        }

    }
}
