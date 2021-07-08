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
                    Screen.PrintBoard(Match.Board);
                    Console.WriteLine();
                    Console.Write("Origem: ");
                    Position origin = Screen.ReadPositionChess().ToPosition();

                    bool[,] possibleMoviments = Match.Board.Piece(origin).PossibleMovements();
                    Console.Clear();
                    Screen.PrintBoard(Match.Board, possibleMoviments);

                    Console.WriteLine();
                    Console.Write("Destino: ");
                    Position destination = Screen.ReadPositionChess().ToPosition();

                    Match.NewMovement(origin, destination);
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
