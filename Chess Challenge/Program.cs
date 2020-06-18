using System;
using System.Collections.Generic;

namespace Chess_Challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessBoard myChessBoard = new ChessBoard();
            myChessBoard.printChessBoard();

            Console.Read();         
        }
    }
}
