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
        public Pieces VulnerableEnPassant { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Colour.White;
            Finished = false;
            Check = false;
            AllPieces = new HashSet<Pieces>();
            CapturedPieces = new HashSet<Pieces>();
            VulnerableEnPassant = null;
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
            PlaceNewPiece('a', 1, new Rook(Board, Colour.White));
            PlaceNewPiece('b', 1, new Knight(Board, Colour.White));
            PlaceNewPiece('c', 1, new Bishop(Board, Colour.White));
            PlaceNewPiece('d', 1, new Queen(Board, Colour.White));
            PlaceNewPiece('e', 1, new King(Board, Colour.White, this));
            PlaceNewPiece('f', 1, new Bishop(Board, Colour.White));
            PlaceNewPiece('g', 1, new Knight(Board, Colour.White));
            PlaceNewPiece('h', 1, new Rook(Board, Colour.White));
            PlaceNewPiece('a', 2, new Pawn(Board, Colour.White, this));
            PlaceNewPiece('b', 2, new Pawn(Board, Colour.White, this));
            PlaceNewPiece('c', 2, new Pawn(Board, Colour.White, this));
            PlaceNewPiece('d', 2, new Pawn(Board, Colour.White, this));
            PlaceNewPiece('e', 2, new Pawn(Board, Colour.White, this));
            PlaceNewPiece('f', 2, new Pawn(Board, Colour.White, this));
            PlaceNewPiece('g', 2, new Pawn(Board, Colour.White, this));
            PlaceNewPiece('h', 2, new Pawn(Board, Colour.White, this));

            PlaceNewPiece('a', 8, new Rook(Board, Colour.Black));
            PlaceNewPiece('b', 8, new Knight(Board, Colour.Black));
            PlaceNewPiece('c', 8, new Bishop(Board, Colour.Black));
            PlaceNewPiece('d', 8, new Queen(Board, Colour.Black));
            PlaceNewPiece('e', 8, new King(Board, Colour.Black, this));
            PlaceNewPiece('f', 8, new Bishop(Board, Colour.Black));
            PlaceNewPiece('g', 8, new Knight(Board, Colour.Black));
            PlaceNewPiece('h', 8, new Rook(Board, Colour.Black));
            PlaceNewPiece('a', 7, new Pawn(Board, Colour.Black, this));
            PlaceNewPiece('b', 7, new Pawn(Board, Colour.Black, this));
            PlaceNewPiece('c', 7, new Pawn(Board, Colour.Black, this));
            PlaceNewPiece('d', 7, new Pawn(Board, Colour.Black, this));
            PlaceNewPiece('e', 7, new Pawn(Board, Colour.Black, this));
            PlaceNewPiece('f', 7, new Pawn(Board, Colour.Black, this));
            PlaceNewPiece('g', 7, new Pawn(Board, Colour.Black, this));
            PlaceNewPiece('h', 7, new Pawn(Board, Colour.Black, this));

        }
        public Pieces NewMovement(Position origin, Position destination)
        {
            Pieces p = Board.RemovePiece(origin);
            p.IncreaseMovementNo();
            Pieces captured = Board.RemovePiece(destination);
            Board.PlacePiece(p, destination);
            if (captured != null) { CapturedPieces.Add(captured); }

            //Castling Small
            if (p is King && destination.Column == origin.Column + 2)
            {
                Position originR = new Position(origin.Line, origin.Column + 3);
                Position destinationR = new Position(origin.Line, origin.Column + 1);
                Pieces R = Board.RemovePiece(originR);
                R.IncreaseMovementNo();
                Board.PlacePiece(R, destinationR);
            }
            //Castling Big
            if (p is King && destination.Column == origin.Column - 2)
            {
                Position originR = new Position(origin.Line, origin.Column - 4);
                Position destinationR = new Position(origin.Line, origin.Column - 1);
                Pieces R = Board.RemovePiece(originR);
                R.IncreaseMovementNo();
                Board.PlacePiece(R, destinationR);
            }
            //EnPassant
            if (p is Pawn)
            {
                if (origin.Column != destination.Column && captured == null)
                {
                    Position posP;
                    if (p.Colour == Colour.White) { posP = new Position(destination.Line + 1, destination.Column); }
                    else { posP = new Position(destination.Line - 1, destination.Column); }
                    captured = Board.RemovePiece(posP);
                    CapturedPieces.Add(captured);
                }
            }

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
            Pieces p = Board.Piece(destination);
            // Promotion
            if (p is Pawn)
            {
                if ((p.Colour == Colour.White && destination.Line == 0) || (p.Colour == Colour.Black && destination.Line == 7))
                {
                    p = Board.RemovePiece(destination);
                    AllPieces.Remove(p);
                    Pieces queen = new Queen(Board, p.Colour);
                    Board.PlacePiece(queen, destination);
                }
            }
            if (IsCheckStatus(EnemyColour(CurrentPlayer))) { Check = true; } else { Check = false; }
            if (IsCheckMate(EnemyColour(CurrentPlayer))) { Finished = true; }
            else { Turn++; ChangePlayer(); }

            //#EnPassant
            if (p is Pawn && (destination.Line == origin.Line - 2 || destination.Line == origin.Line + 2)) { VulnerableEnPassant = p; }
            else { VulnerableEnPassant = null; }
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

            //Castling Small
            if (p is King && destination.Column == origin.Column + 2)
            {
                Position originR = new Position(origin.Line, origin.Column + 3);
                Position destinationR = new Position(origin.Line, origin.Column + 1);
                Pieces R = Board.RemovePiece(destinationR);
                R.DecreaseMovementNo();
                Board.PlacePiece(R, originR);
            }
            //Castling Big
            if (p is King && destination.Column == origin.Column - 2)
            {
                Position originR = new Position(origin.Line, origin.Column - 4);
                Position destinationR = new Position(origin.Line, origin.Column - 1);
                Pieces R = Board.RemovePiece(destinationR);
                R.IncreaseMovementNo();
                Board.PlacePiece(R, originR);
            }
            //EnPassant
            if (p is Pawn)
            {
                if (origin.Column != destination.Column && captured == VulnerableEnPassant)
                {
                    Pieces pawn = Board.RemovePiece(destination);
                    Position posP;
                    if (p.Colour == Colour.White) { posP = new Position(3, destination.Column); }
                    else { posP = new Position(4, destination.Column); }
                    Board.PlacePiece(pawn, posP);
                }
            }
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
