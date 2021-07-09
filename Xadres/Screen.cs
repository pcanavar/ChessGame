using System;
using System.Collections.Generic;
using board;
using Chess;

namespace Xadres
{
    class Screen
    {
        public static void PrintMatch(ChessMatch match)
        {
            Screen.PrintBoard(match.Board);
            Console.WriteLine();
            PrintCapturedPieces(match);
            Console.WriteLine();
            Console.WriteLine("Turn: " + match.Turn);
            if (!match.Finished)
            {
                Console.WriteLine("Waiting Play from: " + match.CurrentPlayer);
                if (match.Check) { Console.WriteLine("Check !"); }
            }
            else
            {
                Console.WriteLine("CHECKMATE !");
                Console.WriteLine("Winner is: " + match.CurrentPlayer);
            }
        }
        public static void PrintCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Captured Pieces: ");
            Console.Write("Whites: ");
            PrintSet(match.GetCapturedPieces(Colour.White));
            Console.WriteLine();
            Console.Write("Blacks: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintSet(match.GetCapturedPieces(Colour.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }
        public static void PrintSet(HashSet<Pieces> set)
        {
            Console.Write("[");
            foreach (Pieces piece in set)
            {
                Console.Write(piece + " ");
            }
            Console.Write("]");
        }
        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    PrintPieces(board.Piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintBoard(Board board, bool[,] possibleMovements)
        {
            ConsoleColor original = Console.BackgroundColor;

            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (possibleMovements[i, j]) { Console.BackgroundColor = ConsoleColor.DarkGray; }
                    else { Console.BackgroundColor = original; }
                    PrintPieces(board.Piece(i, j));
                    Console.BackgroundColor = original;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = original;
        }


        public static ChessPosition ReadPositionChess()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1].ToString());
            return new ChessPosition(column, line);
        }
        public static void PrintPieces(Pieces piece)
        {
            if (piece == null) { Console.Write("-" + " "); }
            else
            {
                if (piece.Colour == Colour.White) { Console.Write(piece); }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}
