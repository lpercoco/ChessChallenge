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
        private int RefenceXPosition;

        public ChessBoardUI()
        {
            ColumnsReference = new List<string>(new string[] { "A", "B", "C", "D", "E", "F", "G", "H" });
            BoxSizeX = 8;
            BoxSizeY = 4;
            RefenceXPosition = 75;
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

        public void PrintMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(RefenceXPosition, 21);
            Console.Write(message);
            Console.ResetColor();
        }

        public string SelectBox(string message)
        {
            string input;
            do
            {
                Console.SetCursorPosition(RefenceXPosition, 20);
                Console.WriteLine(message);

                Console.SetCursorPosition(RefenceXPosition, 22);
                Console.Write("                                ");

                Console.SetCursorPosition(RefenceXPosition, 22);
                Console.Write("Ingrese casillero:");

                input = Console.ReadLine();

                if (String.IsNullOrEmpty(input))
                {
                    PrintMessage("Ingreso vacio, intente nuevamente");
                }
                else
                {
                    CleanMessage();
                };

            } while (String.IsNullOrEmpty(input));

            return input.ToUpper();
        }

        private void CleanMessage()
        {
            PrintMessage("                                          ");
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
            pieces.ForEach( p => 
                    {
                        if(p.Player == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                        }
                        PrintPiece(p);
                    });

            Console.ResetColor();
        }

        public void PrintGameReferences()
        {
            Console.SetCursorPosition(RefenceXPosition, 1);
            Console.WriteLine("***CHESS CHALLENGE***");

            Console.SetCursorPosition(RefenceXPosition, 3);
            Console.WriteLine("JUGADORES");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(RefenceXPosition, 5);
            Console.WriteLine("JUGADOR : 1");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(RefenceXPosition, 6);
            Console.WriteLine("JUGADOR : 2");

            Console.ResetColor();

            Console.SetCursorPosition(RefenceXPosition, 8);
            Console.WriteLine("REFERENCIAS");

            Console.SetCursorPosition(RefenceXPosition, 10);
            Console.WriteLine("PEON = P");
            Console.SetCursorPosition(RefenceXPosition, 11);
            Console.WriteLine("TORRE = T");
            Console.SetCursorPosition(RefenceXPosition, 12);
            Console.WriteLine("CABALLO = C");
            Console.SetCursorPosition(RefenceXPosition, 13);
            Console.WriteLine("ALFIL = A");
            Console.SetCursorPosition(RefenceXPosition, 14);
            Console.WriteLine("REINA = Ra");
            Console.SetCursorPosition(RefenceXPosition, 15);
            Console.WriteLine("REY = Ry");

            Console.SetCursorPosition(RefenceXPosition, 17);
            Console.WriteLine("INGRESO DE CASILLEROS:");
            Console.SetCursorPosition(RefenceXPosition, 18);
            Console.WriteLine("FILACOLUMNA -> EJEMPLO: 1A");

        }

        public void UpdateChessBoard(List<ChessPiece> chessPieces)
        {
            //refactorizar para solamente limpiar y actualizar las ubicaciones involucradas
            CleanChessBoardPieces();

            PrintPieces(chessPieces);
        }

        private void CleanChessBoardPieces()
        {
            BoxLocation location = new BoxLocation();

            ChessPiece blackBox = new ChessPiece("  ");
            ChessPiece whiteBox = new ChessPiece("██");

            for (int row = 8; row > 0; row--)
            {
                for (int col = 1; col < 9; col++)
                {
                    location.Id = (int.Parse(row.ToString() + col.ToString()));
                    if (((row % 2 == 0) && (col % 2 != 0)) || ((row % 2 != 0) && (col % 2 == 0)))
                    {
                        whiteBox.BoardLocation = location;
                        PrintPiece(whiteBox);
                        
                    } else
                    {
                        blackBox.BoardLocation = location;
                        PrintPiece(blackBox);
                    };
                }
            }
        }

        public void PrintGameOverMessage()
        {
            for (int i = 19; i < 23; i++)
            {
                Console.SetCursorPosition(RefenceXPosition, i);
                CleanMessage();
            }

            Console.SetCursorPosition(RefenceXPosition, 19);
            Console.WriteLine("GAME OVER!!!");
        }
    }
}
