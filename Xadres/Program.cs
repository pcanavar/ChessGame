using System;
using board;
using Chess;

namespace Xadres
{
    class Program
    {
        static void Main(string[] args)
        {
            Board tab = new Board(8, 8);
            tab.PlacePiece(new Rook(tab, Colour.White), new Position(0, 0));
            tab.PlacePiece(new Rook(tab, Colour.White), new Position(1, 3));
            tab.PlacePiece(new King(tab, Colour.White), new Position(2, 4));


            Screen.PrintBoard(tab);
            
            
            
            
            
            Console.ReadLine();



        }
    }
}
