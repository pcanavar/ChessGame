using System;
using board;
using Chess;

namespace Xadres
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessPosition pos = new ChessPosition('a', 1);
            Console.WriteLine(pos);
            Console.WriteLine(pos.ToPosition());





            Console.ReadLine();
        }
    }
}
