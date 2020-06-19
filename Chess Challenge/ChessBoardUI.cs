using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Chess_Challenge
{
    public class ChessBoardUI
    {
        //refactor this
        private List<string> columnsReference = new List<string>(new string[] { "A", "B", "C", "D", "E", "F", "G", "H" });
        const int BOXSIZE = 8;

        public void printChessBoard()
        {
            printLettersReference(2, 1);
            printNumbersReference(1, 0);
            printRows(3, 2);
            printColumns(2, 2);
            printWhiteBoxs(3,3);
        }

        //printLettersReference(2,1);
        public void printLettersReference(int x, int y)
        {
            for (int i = 0; i < 8; i++)
            {
                Console.SetCursorPosition(x + (BOXSIZE * i) + (BOXSIZE / 2), y);
                Console.Write(columnsReference[i]);
            }
        }

        //printNumbersReference(1,1);
        public void printNumbersReference(int x, int y)
        {
            for (int i = 1; i < 9; i++)
            {
                Console.SetCursorPosition(x, (y + ((BOXSIZE / 2) * i)));
                Console.Write(9-i);
            }
        }

        //printColumns(2, 2);
        public void printColumns(int x, int y)
        {
            int lineHeight = BOXSIZE * 4;
            for (int lineNumber = 0; lineNumber < 9; lineNumber++)
            {
                for (int l = 1; l < (lineHeight); l++)
                {
                    Console.SetCursorPosition(x + (lineNumber * BOXSIZE), y + l);
                    Console.Write("|");
                }
            }
        }

        //printRows(3, 2);
        public void printRows(int x, int y)
        {
            int hyphensCount = 8 * BOXSIZE;
            string hyphens = new string('-', hyphensCount);

            for (int lineNumber = 0; lineNumber < 9; lineNumber++)
            {
                Console.SetCursorPosition(x, y + ((BOXSIZE / 2) * lineNumber));
                Console.Write(hyphens.ToString());
            }
        }

        //refactor this method
        public void printWhiteBoxs(int x, int y)
        {
            int printWidht = (BOXSIZE - 1);
            int printHeight = ((BOXSIZE / 2) - 1);
            string whiteLine = new string('█', printWidht);

            for (int row = 8; row > 0; row--)
            {
                for (int col = 1; col < 9; col++)
                {
                    if( ((row % 2 == 0) && (col % 2 != 0)) || ((row % 2 != 0) && (col % 2 == 0)) )
                    {
                        for (int j = 0; j < printHeight; j++)
                        {
                            Console.SetCursorPosition(x + ( (col - 1) * BOXSIZE) , y + j + ((BOXSIZE / 2) * (8 - row)));
                            Console.WriteLine(whiteLine);
                        }
                    }
                }
            }
        }

        public void PrintGameReferences()
        {
            Console.SetCursorPosition(80,1);
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

        //refactor
        public void PrintPiece(ChessPiece p)
        {
 
            int col = (int)Char.GetNumericValue(p.BoardLocation.Id.ToString()[1]);
            int row = (int)Char.GetNumericValue(p.BoardLocation.Id.ToString()[0]);

            Console.SetCursorPosition((col * BOXSIZE) - 2, (9 - row) * (BOXSIZE / 2));
            Console.Write(p.ShortName);
        }
        public void PrintPieces(List<ChessPiece> pieces)
        {
            //print player 1
            Console.ForegroundColor = ConsoleColor.Red;
            pieces.Where( piece => piece.Player == 1).ToList()
                .ForEach( p => 
                    {
                        PrintPiece(p);
                    });

            //print player 2
            Console.ForegroundColor = ConsoleColor.Green;
            pieces.Where(piece => piece.Player == 2).ToList()
                .ForEach(p =>
                    {
                        PrintPiece(p);
                    });

            Console.ResetColor();
            Console.ReadLine();
        }
    }
}
