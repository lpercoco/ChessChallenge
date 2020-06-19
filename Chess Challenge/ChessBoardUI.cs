using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Chess_Challenge
{
    public class ChessBoardUI
    {
        private List<string> ColumnsReference;
        private int BoxSizeX;
        private int BoxSizeY;

        public ChessBoardUI()
        {
            ColumnsReference = new List<string>(new string[] { "A", "B", "C", "D", "E", "F", "G", "H" });
            BoxSizeX = 8;
            BoxSizeY = 4;
        }

        public void PrintChessBoard()
        {
            PrintLettersReference(2, 1);
            PrintNumbersReference(1, 0);
            PrintRows(3, 2);
            PrintColumns(2, 2);
            PrintWhiteBoxs(3,3);
        }

        public void PrintLettersReference(int x, int y)
        {
            for (int i = 0; i < 8; i++)
            {
                Console.SetCursorPosition(x + (BoxSizeX * i) + (BoxSizeX / 2), y);
                Console.Write(ColumnsReference[i]);
            }
        }

        public void PrintNumbersReference(int x, int y)
        {
            for (int i = 1; i < 9; i++)
            {
                Console.SetCursorPosition(x, (y + (BoxSizeY * i)));
                Console.Write(9-i);
            }
        }

        //refactor
        public void PrintColumns(int x, int y)
        {
            int lineHeight = BoxSizeX * 4;
            for (int lineNumber = 0; lineNumber < 9; lineNumber++)
            {
                for (int l = 1; l < (lineHeight); l++)
                {
                    Console.SetCursorPosition(x + (lineNumber * BoxSizeX), y + l);
                    Console.Write("|");
                }
            }
        }

        //refactor
        public void PrintRows(int x, int y)
        {
            int hyphensCount = 16 * BoxSizeY;
            string hyphens = new string('-', hyphensCount);

            for (int lineNumber = 0; lineNumber < 9; lineNumber++)
            {
                Console.SetCursorPosition(x, y + (BoxSizeY * lineNumber));
                Console.Write(hyphens.ToString());
            }
        }

        //refactor this method
        public void PrintWhiteBoxs(int x, int y)
        {
            int printWidht = (BoxSizeX - 1);
            int printHeight = (BoxSizeY - 1);
            string whiteLine = new string('█', printWidht);

            for (int row = 8; row > 0; row--)
            {
                for (int col = 1; col < 9; col++)
                {
                    if( ((row % 2 == 0) && (col % 2 != 0)) || ((row % 2 != 0) && (col % 2 == 0)) )
                    {
                        for (int j = 0; j < printHeight; j++)
                        {
                            Console.SetCursorPosition(x + ( (col - 1) * BoxSizeX) , y + j + (BoxSizeY * (8 - row)));
                            Console.WriteLine(whiteLine);
                        }
                    }
                }
            }
        }

        //refactor
        public void PrintPiece(ChessPiece p)
        {
 
            int col = (int)Char.GetNumericValue(p.BoardLocation.Id.ToString()[1]);
            int row = (int)Char.GetNumericValue(p.BoardLocation.Id.ToString()[0]);

            Console.SetCursorPosition((col * BoxSizeX) - 2, (9 - row) * BoxSizeY);
            Console.Write(p.ShortName);
        }
        public void PrintPieces(List<ChessPiece> pieces)
        {
            pieces.Where( piece => piece.Available == true ).ToList()
                .ForEach( p => 
                    {
                        if(p.Player == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        PrintPiece(p);
                    });

            Console.ResetColor();
            Console.ReadLine();
        }

        public void PrintGameReferences()
        {
            Console.SetCursorPosition(80, 1);
            Console.WriteLine("***CHESS CHALLENGE***");

            Console.SetCursorPosition(80, 3);
            Console.WriteLine("JUGADORES");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(80, 5);
            Console.WriteLine("JUGADOR : 1");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(80, 6);
            Console.WriteLine("JUGADOR : 2");

            Console.ResetColor();

            Console.SetCursorPosition(80, 8);
            Console.WriteLine("REFERENCIAS");

            Console.SetCursorPosition(80, 10);
            Console.WriteLine("PEON = P");
            Console.SetCursorPosition(80, 11);
            Console.WriteLine("TORRE = T");
            Console.SetCursorPosition(80, 12);
            Console.WriteLine("CABALLO = C");
            Console.SetCursorPosition(80, 13);
            Console.WriteLine("ALFIL = A");
            Console.SetCursorPosition(80, 14);
            Console.WriteLine("REINA = Ra");
            Console.SetCursorPosition(80, 15);
            Console.WriteLine("REY = Ry");
        }
    }
}
