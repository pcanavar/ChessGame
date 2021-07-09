using board;
using System.Collections.Generic;

namespace Chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Colour CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }
        private HashSet<Pieces> AllPieces;
        private HashSet<Pieces> CapturedPieces;

        public ChessMatch()
        {
            Board = new Board(8,8);
            Turn = 1;
            CurrentPlayer = Colour.White;
            Finished = false;
            AllPieces = new HashSet<Pieces>();
            CapturedPieces = new HashSet<Pieces>();
            PlacePieces();
        }
        public void PlaceNewPiece(char column, int line, Pieces piece)
        {
            Board.PlacePiece(piece, new ChessPosition(column, line).ToPosition());
            AllPieces.Add(piece);
        }
        private void PlacePieces()
        {
            PlaceNewPiece('c', 1, new Rook(Board, Colour.White));
            PlaceNewPiece('c', 2, new Rook(Board, Colour.White));
            PlaceNewPiece('d', 2, new Rook(Board, Colour.White));
            PlaceNewPiece('e', 1, new Rook(Board, Colour.White));
            PlaceNewPiece('e', 2, new Rook(Board, Colour.White));
            PlaceNewPiece('d', 1, new King(Board, Colour.White));
            
            PlaceNewPiece('c', 7, new Rook(Board, Colour.Black));
            PlaceNewPiece('c', 8, new Rook(Board, Colour.Black));
            PlaceNewPiece('d', 7, new Rook(Board, Colour.Black));
            PlaceNewPiece('e', 7, new Rook(Board, Colour.Black));
            PlaceNewPiece('e', 8, new Rook(Board, Colour.Black));
            PlaceNewPiece('d', 8, new King(Board, Colour.Black));
        }
        public void NewMovement(Position origin, Position destination)
        {
            Pieces p = Board.RemovePiece(origin);
            p.IncreaseMovementNo();
            Pieces captured = Board.RemovePiece(destination);
            Board.PlacePiece(p, destination);
            if (captured != null) { CapturedPieces.Add(captured); }
        }
        public void ValidatePositionOrigin(Position pos)
        {
            if (Board.Piece(pos) == null)
            {
                throw new BoardException("No pieces at this position !");
            }
            if (CurrentPlayer != Board.Piece(pos).Colour)
            {
                throw new BoardException("Please select a " + CurrentPlayer + " piece");
            }
            if (!Board.Piece(pos).IsPossibleMovementsAvailable())
            {
                throw new BoardException("This Piece has no movements");
            }
        }
        public void ValidatePositionDestination(Position origin, Position destination)
        {
            if (!Board.Piece(origin).IsAbleToMoveTo(destination))
            {
                throw new BoardException("This piece cannot move to the destination selected");
            }
        }
        public void RunPlay(Position origin, Position destination)
        {
            NewMovement(origin, destination);
            Turn++;
            ChangePlayer();
        }
        private void ChangePlayer()
        {
            if (CurrentPlayer == Colour.White) { CurrentPlayer = Colour.Black; }
            else { CurrentPlayer = Colour.White; }
        }

        public HashSet<Pieces> GetCapturedPieces(Colour colour)
        {
            HashSet<Pieces> aux = new HashSet<Pieces>();
            foreach (Pieces piece in CapturedPieces)
            {
                if (piece.Colour == colour) { aux.Add(piece); }
            }
            return aux;
        }
        public HashSet<Pieces> GetPiecesInGame(Colour colour)
        {
            HashSet<Pieces> aux = new HashSet<Pieces>();
            foreach (Pieces piece in AllPieces)
            {
                if (piece.Colour == colour) { aux.Add(piece); }
            }
            aux.ExceptWith(GetCapturedPieces(colour));
            return aux;
        }

    }
}
