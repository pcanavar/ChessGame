﻿namespace Tabuleiro
{
    class Pieces
    {
        public Position Position { get; set; }
        public Colour Colour { get; protected set; }
        public int MovementsNo { get; protected set; }
        public Board Board { get; protected set; }

        public Pieces(Position position, Colour colour, Board board)
        {
            Position = position;
            Colour = colour;
            Board = board;
            MovementsNo = 0;
        }
    }
}