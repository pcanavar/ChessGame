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
        public bool Check { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Colour.White;
            Finished = false;
            Check = false;
            AllPieces = new HashSet<Pieces>();
            CapturedPieces = new HashSet<Pieces>();
            PlacePieces();
        }
        private Colour EnemyColour(Colour colour)
        {
            if (colour == Colour.White) { return Colour.Black; }
            else { return Colour.White; }
        }
        private Pieces King(Colour colour)
        {
            foreach (Pieces piece in GetPiecesInGame(colour))
            {
                if (piece is King)
                {
                    return piece;
                }
            }
            return null;
        }
        public bool IsCheckStatus(Colour colour)
        {
            Pieces king = King(colour);
            if (king == null) { throw new BoardException(colour + " King is missing from the game"); }
            foreach (Pieces piece in GetPiecesInGame(EnemyColour(colour)))
            {
                bool[,] mat = piece.PossibleMovements();
                if (mat[king.Position.Line, king.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsCheckMate(Colour colour)
        {
            if (!IsCheckStatus(colour)) { return false; }
            foreach (Pieces piece in GetPiecesInGame(colour))
            {
                bool[,] mat = piece.PossibleMovements();
                for (int i = 0; i < Board.Lines; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = piece.Position;
                            Position destination = new Position(i, j);
                            Pieces captured = NewMovement(origin, destination);
                            bool isCheck = IsCheckStatus(colour);
                            UndoMovement(origin, destination, captured);
                            if (!isCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        public void PlaceNewPiece(char column, int line, Pieces piece)
        {
            Board.PlacePiece(piece, new ChessPosition(column, line).ToPosition());
            AllPieces.Add(piece);
        }
        private void PlacePieces()
        {
            PlaceNewPiece('h', 7, new Rook(Board, Colour.White));
            PlaceNewPiece('c', 1, new Rook(Board, Colour.White));
            PlaceNewPiece('d', 1, new King(Board, Colour.White));

            PlaceNewPiece('a', 8, new King(Board, Colour.Black));
            PlaceNewPiece('b', 8, new Rook(Board, Colour.Black));
        }
        public Pieces NewMovement(Position origin, Position destination)
        {
            Pieces p = Board.RemovePiece(origin);
            p.IncreaseMovementNo();
            Pieces captured = Board.RemovePiece(destination);
            Board.PlacePiece(p, destination);
            if (captured != null) { CapturedPieces.Add(captured); }
            return captured;
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
            if (!Board.Piece(origin).PossibleMovement(destination))
            {
                throw new BoardException("This piece cannot move to the destination selected");
            }
        }
        public void RunPlay(Position origin, Position destination)
        {
            Pieces captured = NewMovement(origin, destination);
            if (IsCheckStatus(CurrentPlayer))
            {
                UndoMovement(origin, destination, captured);
                throw new BoardException("You cannot place yourself in check");
            }
            if (IsCheckStatus(EnemyColour(CurrentPlayer))) { Check = true; } else { Check = false; }
            if (IsCheckMate(EnemyColour(CurrentPlayer))) { Finished = true; }
            else { Turn++; ChangePlayer(); }
        }
        public void UndoMovement(Position origin, Position destination, Pieces captured)
        {
            Pieces p = Board.RemovePiece(destination);
            if (captured != null)
            {
                Board.PlacePiece(captured, destination);
                CapturedPieces.Remove(captured);
            }
            Board.PlacePiece(p, origin);
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
