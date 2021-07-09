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
                    try
                    {

                        Console.Clear();
                        Screen.PrintBoard(Match.Board);
                        Console.WriteLine();
                        Console.WriteLine("Turn: " + Match.Turn);
                        Console.WriteLine("Waiting Play from: " + Match.CurrentPlayer);

                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Position origin = Screen.ReadPositionChess().ToPosition();
                        Match.ValidatePositionOrigin(origin);
                        bool[,] possibleMoviments = Match.Board.Piece(origin).PossibleMovements();
                        Console.Clear();
                        Screen.PrintBoard(Match.Board, possibleMoviments);

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Position destination = Screen.ReadPositionChess().ToPosition();
                        Match.ValidatePositionDestination(origin, destination);

                        Match.RunPlay(origin, destination);
                    }
                    catch(BoardException bEx)
                    {
                        Console.WriteLine(bEx.Message);
                        Console.ReadLine();
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Unknown Exception: " + e.Message);
                        Console.ReadLine();
                    }
                }


            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
