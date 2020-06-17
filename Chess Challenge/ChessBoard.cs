using System;
using System.Collections.Generic;
using System.Text;

namespace Chess_Challenge
{
    public class ChessBoard
    {
        //improve
        private List<string> columnsReference = new List<string>(new string[] { "A", "B", "C", "D", "E", "F", "G", "H" });
        const int BOXSIZE = 8;

        public void printChessBoard()
        {
            printLettersReference(2, 1);
            printNumbersReference(1, 0);
            printRows(3, 2);
            printColumns(2, 2);
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
                Console.SetCursorPosition(x, y + ((BOXSIZE / 2) * i));
                Console.Write(i);
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
    }
}
