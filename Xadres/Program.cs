using System;
using board;
using Chess;

namespace ChessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessMatch Match = new ChessMatch();

            while (!Match.Finished)
            {
                try
                {
                    Console.Clear();
                    Screen.PrintMatch(Match);

                    Console.WriteLine();
                    Console.Write("Origin: ");
                    Position origin = Screen.ReadPositionChess().ToPosition();
                    Match.ValidatePositionOrigin(origin);
                    bool[,] possibleMoviments = Match.Board.Piece(origin).PossibleMovements();
                    Console.Clear();
                    Screen.PrintBoard(Match.Board, possibleMoviments);

                    Console.WriteLine();
                    Console.Write("Destination: ");
                    Position destination = Screen.ReadPositionChess().ToPosition();
                    Match.ValidatePositionDestination(origin, destination);

                    Match.RunPlay(origin, destination);
                }
                catch (BoardException bEx)
                {
                    Console.WriteLine(bEx.Message);
                    Console.ReadLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unknown Exception: " + e.Message);
                    Console.ReadLine();
                }
                Console.Clear();
                Screen.PrintMatch(Match);
            }
        }
    }
}
