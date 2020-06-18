using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess_Challenge
{
    class Program
    {

        public static List<ChessPiece> ChessPieces;
        public static List<BoxLocation> Locations;

        static void Main(string[] args)
        {
            //ChessBoardUI myChessBoard = new ChessBoardUI();
            //myChessBoard.printChessBoard();


            GenerateInitialRandomDistribution();

            Console.Read();
        }

        public static void GenerateInitialRandomDistribution()
        {
            ChessPieces = GenerateChessPieces();
            Locations = LocationInitialization();

            //locate not Alfil pieces
            ChessPieces.Where(p => p.Name != "Alfil").ToList()
             .ForEach(i => {
                 Random r = new Random();

                 do
                 {
                     int rInt = r.Next(1, 64);

                     BoxLocation randomLocation = Locations[rInt];

                     ChessPiece foundPiece = ChessPieces.Find(fp => fp.BoardLocation != null &&
                                                                    fp.BoardLocation.Id == randomLocation.Id);

                     if (foundPiece == null)
                     {
                         i.BoardLocation = randomLocation;
                     }

                 } while (i.BoardLocation == null);
             });

            //locate Alfil pieces
            for (int player = 1; player < 3; player++)
            {
                //0 = dark
                //1 = light
                Console.WriteLine("player: " + player);
                int colorFlag = 0;
                ChessPieces.Where(p => p.Name == "Alfil" && p.Player == player).ToList().
                ForEach(i => {
                    if (colorFlag == 0)
                    {
                    //set dark               
                    }
                    else
                    {
                    //set light
                    }
                    colorFlag ^= 1;
                });
            }
        
            //just for testing
            for (int i = 0; i < ChessPieces.Count; i++)
            {
                if (ChessPieces[i].BoardLocation != null)
                {
                    Console.WriteLine(ChessPieces[i].Name + " - " + ChessPieces[i].Player + " - " + ChessPieces[i].BoardLocation.BoardReference);
                }
                else
                {
                    Console.WriteLine(ChessPieces[i].Name + " - " + ChessPieces[i].Player + " - " + "no ubicado");
                }
            }
            Console.Read();
        }

        private static List<BoxLocation> LocationInitialization()
        {
           return new List<BoxLocation>()
           {
            new BoxLocation(11, "1A", "dark"),
            new BoxLocation(12, "1B", "light"),
            new BoxLocation(13, "1C", "dark"),
            new BoxLocation(14, "1D", "light"),
            new BoxLocation(15, "1E", "dark"),
            new BoxLocation(16, "1F", "light"),
            new BoxLocation(17, "1G", "dark"),
            new BoxLocation(18, "1H", "light"),
            new BoxLocation(21, "2A", "light"),
            new BoxLocation(22, "2B", "dark"),
            new BoxLocation(23, "2C", "light"),
            new BoxLocation(24, "2D", "dark"),
            new BoxLocation(25, "2E", "light"),
            new BoxLocation(26, "2F", "dark"),
            new BoxLocation(27, "2G", "light"),
            new BoxLocation(28, "2H", "dark"),
            new BoxLocation(31, "3A", "dark"),
            new BoxLocation(32, "3B", "light"),
            new BoxLocation(33, "3C", "dark"),
            new BoxLocation(34, "3D", "light"),
            new BoxLocation(35, "3E", "dark"),
            new BoxLocation(36, "3F", "light"),
            new BoxLocation(37, "3G", "dark"),
            new BoxLocation(38, "3H", "light"),
            new BoxLocation(41, "4A", "light"),
            new BoxLocation(42, "4B", "dark"),
            new BoxLocation(43, "4C", "light"),
            new BoxLocation(44, "4D", "dark"),
            new BoxLocation(45, "4E", "light"),
            new BoxLocation(46, "4F", "dark"),
            new BoxLocation(47, "4G", "light"),
            new BoxLocation(48, "4H", "dark"),
            new BoxLocation(51, "5A", "dark"),
            new BoxLocation(52, "5B", "light"),
            new BoxLocation(53, "5C", "dark"),
            new BoxLocation(54, "5D", "light"),
            new BoxLocation(55, "5E", "dark"),
            new BoxLocation(56, "5F", "light"),
            new BoxLocation(57, "5G", "dark"),
            new BoxLocation(58, "5H", "light"),
            new BoxLocation(61, "6A", "light"),
            new BoxLocation(62, "6B", "dark"),
            new BoxLocation(63, "6C", "light"),
            new BoxLocation(64, "6D", "dark"),
            new BoxLocation(65, "6E", "light"),
            new BoxLocation(66, "6F", "dark"),
            new BoxLocation(67, "6G", "light"),
            new BoxLocation(68, "6H", "dark"),
            new BoxLocation(71, "7A", "dark"),
            new BoxLocation(72, "7B", "light"),
            new BoxLocation(73, "7C", "dark"),
            new BoxLocation(74, "7D", "light"),
            new BoxLocation(75, "7E", "dark"),
            new BoxLocation(76, "7F", "light"),
            new BoxLocation(77, "7G", "dark"),
            new BoxLocation(78, "7H", "light"),
            new BoxLocation(81, "8A", "light"),
            new BoxLocation(82, "8B", "dark"),
            new BoxLocation(83, "8C", "light"),
            new BoxLocation(84, "8D", "dark"),
            new BoxLocation(85, "8E", "light"),
            new BoxLocation(86, "8F", "dark"),
            new BoxLocation(87, "8G", "light"),
            new BoxLocation(88, "8H", "dark")
           };
        }

        public static List<ChessPiece> GenerateChessPieces()
        {
            List<ChessPiece> chessPieces = new List<ChessPiece>();

            for (int player = 1; player<3; player++)
            {
                for (int i = 0; i < 8; i++)
                {
                    ChessPiece peon = new ChessPiece("Peon", "P", player);
                    chessPieces.Add(peon);
                }

                for (int i = 0; i < 2; i++)
                {
                    ChessPiece torre = new ChessPiece("Torre", "T", player);
                    chessPieces.Add(torre);
                }

                for (int i = 0; i < 2; i++)
                {
                    ChessPiece alfil = new ChessPiece("Alfil", "A", player);
                    chessPieces.Add(alfil);
                }

                for (int i = 0; i < 2; i++)
                {
                    ChessPiece caballo = new ChessPiece("Caballo", "C", player);
                    chessPieces.Add(caballo);
                }

                ChessPiece rey = new ChessPiece("Rey", "Ry", player);
                chessPieces.Add(rey);

                ChessPiece reina = new ChessPiece("Reina", "Ra", player);
                chessPieces.Add(reina);
            }
            return chessPieces;
        }          
    }
}