using System;
using board;
using Chess;

namespace Xadres
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch Match = new ChessMatch();

                while (!Match.Finished)
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
                Console.Clear();
                Screen.PrintMatch(Match);
                Console.ReadLine();
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
        }
    }
}
