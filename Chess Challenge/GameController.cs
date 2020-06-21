﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Chess_Challenge
{
    public class GameController
    {
        public ChessBoardUI myChessBoardUI;

        public List<ChessPiece> ChessPieces;
        public List<BoxLocation> Locations;

        public GameController()
        {
            myChessBoardUI = new ChessBoardUI();
            ChessPieces = GenerateChessPieces();
            Locations = LocationInitialization();
        }


        public void InizializeUI()
        {
            myChessBoardUI.PrintChessBoard();
            myChessBoardUI.PrintGameReferences();
        }

        //valido que la pieza se pueda mover a la ubicacion destino
        //como si el tablero estuviera vacio
        public Boolean MovementValidation(ChessPiece piece, BoxLocation destinationBox)
        {
            List<BoxLocation> posibleLocations = new List<BoxLocation>();

            int xAux;
            int yAux;
            int calculatedLocation;

            //I get the axis of the piece to move
            int col = (int)Char.GetNumericValue(piece.BoardLocation.Id.ToString()[1]);
            int row = (int)Char.GetNumericValue(piece.BoardLocation.Id.ToString()[0]);

            //I get the axis of the destiny location
            int colDestinyLocation = (int)Char.GetNumericValue(destinationBox.Id.ToString()[1]);
            int rowDestinyLocation = (int)Char.GetNumericValue(destinationBox.Id.ToString()[0]);

            switch (piece.ShortName)
            {
                case "P":
                    {
                        //Los peones solo pueden avanzar hacia adelante.
                        //(para las blancas, de fila 1 hacia fila 8
                        //col para las negras de fila 8 hacia fila 1, a menos que tengan otra pieza enfrente bloqueando
                        //el camino)
                    }
                    break;
                case "T":
                    {

                        int startIndex;
                        int endIndex;
                        string destinyAxis;

                        if(colDestinyLocation == col || rowDestinyLocation == row )
                        {
                            //dependiendo el eje y la ubicacion del destino respecto a la pieza
                            //construyo los limites de los indices
                            if(rowDestinyLocation == row)
                            {
                                destinyAxis = "row";

                                if (colDestinyLocation > col)
                                {
                                    startIndex = col + 1;
                                    endIndex = colDestinyLocation;
                                }
                                else
                                {
                                    startIndex = colDestinyLocation;
                                    endIndex = col - 1;
                                };
                            }
                            else
                            {
                                destinyAxis = "col";

                                if (rowDestinyLocation > row)
                                {
                                    startIndex = row + 1;
                                    endIndex = rowDestinyLocation;
                                }
                                else
                                {
                                    startIndex = rowDestinyLocation;
                                    endIndex = row - 1;
                                };
                            };

                            //recorro las ubicaciones entre el destino y la pieza a mover
                            for (int indexAux = startIndex; indexAux <= endIndex; indexAux++)
                            {
                                //I calculate the location based on the destiny axis
                                if(destinyAxis == "row")
                                {
                                    calculatedLocation = int.Parse(row.ToString() + indexAux.ToString());
                                }
                                else
                                {
                                    calculatedLocation = int.Parse(indexAux.ToString() + col.ToString());
                                };
                                ChessPiece pieceLocation = ChessPieces.Where(p => p.BoardLocation.Id == calculatedLocation).FirstOrDefault();

                                if (calculatedLocation == destinationBox.Id &&
                                    pieceLocation != null &&
                                    pieceLocation.Available == true &&
                                    pieceLocation.Player == piece.Player)
                                {
                                    //if there is an available piece of the same player in the location,
                                    //it is a invalid movement
                                    myChessBoardUI.PrintMessage("Movimiento invalido");
                                    return false;
                                }

                                if (calculatedLocation != destinationBox.Id &&
                                    pieceLocation != null &&
                                    pieceLocation.Available == true)
                                {
                                    // if there is a available piece in the way to the destiny location,
                                    //it is a invalid movement
                                    myChessBoardUI.PrintMessage("Movimiento invalido");
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            // if there the destiny location is not in any of the axis of the piece location,
                            //it is a invalid movement
                            myChessBoardUI.PrintMessage("Movimiento invalido");
                            return false;
                        };
                        return true;
                    }
                case "C":
                    {
                        /*
                        Los caballos se pueden mover en “L”, para atrás o adelante, izquierda o derecha. Esto significa que:
                        Pueden moverse 1 casillero hacia atrás / adelante, col 2 a la derecha / izquierda.
                        O, alternativamente, 2 casilleros hacia atrás / adelante, col 1 a la derecha / izquierda.
                        Adicionalmente, los caballos son las únicas piezas que pueden “saltear” obstáculos
                        (otras piezas, blancas col negras) al moverse.
                        */
                    }
                    break;
                case "A":
                    {
                        // I calculated the possible moves of the piece using two linear functions 
                        // f(y) = mx +b
                        // ascending diagonal => m = 1
                        // descending diagonal => m = -1

                        //calculation of the start of the ascending diagonal
                        int startRow = Math.Abs(row - col) + 1;

                        //calculation of the start of the descending  diagonal
                        //revisar
                        int startCol = Math.Abs((col + row) - 8);

                        //calculating locations on the upward diagonal
                        int j = 1;
                        for (int i = startRow; i < 9; i++)
                        {

                            calculatedLocation = int.Parse(i.ToString() + j.ToString());

                            //discard the  piece's location
                            if (calculatedLocation != (piece.BoardLocation.Id))
                            {
                                BoxLocation location = Locations.Where(lo => lo.Id == calculatedLocation).FirstOrDefault();

                                if (location != null)
                                {
                                    posibleLocations.Add(location);
                                };
                            };
                            j = j + 1;
                        };

                        //calculating locations on the downward diagonal
                        j = 8;
                        for (int i = startCol; i < 9; i++)
                        {
                            calculatedLocation = int.Parse(j.ToString() + i.ToString()); ;

                            //discard the  piece's location
                            if (calculatedLocation != (piece.BoardLocation.Id))
                            {
                                BoxLocation location = Locations.Where(lo => lo.Id == calculatedLocation).FirstOrDefault();

                                if (location != null)
                                {
                                    posibleLocations.Add(location);
                                };

                            };
                            j = j - 1;
                        }
                        return true;
                    }
                case "Ra":
                    {
                        /*
                           La reina puede moverse en diagonal, hacia atrás col adelante, col hacia los costados,
                           tantas piezas como quieran. (a menos que sea obstaculizada)
                        */
                    }
                    break;
                case "Ry":
                    {
                        for (int i = -1; i <= 1; i++)
                        {
                            yAux = row + i;
                            for (int j = -1; j <= 1; j++)
                            {
                                xAux = col + j;

                                calculatedLocation = int.Parse(yAux.ToString() + xAux.ToString());

                                //discard the  piece's location
                                if (calculatedLocation != (piece.BoardLocation.Id))
                                {
                                    BoxLocation location = Locations.Where(lo => lo.Id == calculatedLocation).FirstOrDefault();

                                    if (location != null)
                                    {
                                        posibleLocations.Add(location);
                                    };
                                };
                            }
                        }

                        //hacer un metodo para esto?
                        //discard locations that have an available piece of the same player
                        posibleLocations.ForEach(location => {
                            ChessPiece pieceLocation = ChessPieces.Where(p => p.BoardLocation == location).FirstOrDefault();

                            if (pieceLocation != null && pieceLocation.Player == piece.Player && pieceLocation.Available == true)
                            {
                                posibleLocations.Remove(location);
                            };
                        });

                        if()
                        {

                        }

                        return true;
                    }
                default:
                    Console.WriteLine("Default case");
                    break;
            }
            return true;
        }


        public void PlayGame()
        {
            bool inputFlag = false;
            ChessPiece selectedPiece = null;

            //seleccion pieza
            do
            {
                string selectedBox = myChessBoardUI.SelectBox("Ingrese ubicación de la pieza a mover");
                    
                inputFlag = Locations.Any(l => l.BoardReference.Equals(selectedBox));
                
                if(inputFlag == false)
                {
                    myChessBoardUI.PrintMessage("Ingreso NO valido - Revise las Referencias");
                }
                else
                {
                    selectedPiece = ChessPieces.Find(p => p.BoardLocation.BoardReference.Equals(selectedBox) && p.Available);
                    if (selectedPiece == null)
                    {
                        myChessBoardUI.PrintMessage("No hay una pieza en el casillero ingresado");
                    }

                }
            } while (inputFlag == false || selectedPiece == null);

            //seleccion destino
            do
            {
                string selectedBox = myChessBoardUI.SelectBox("Ingrese el destino de la pieza");

                BoxLocation selectedDestinyBox = Locations.Where(l => l.BoardReference.Equals(selectedBox)).FirstOrDefault();

                if (selectedDestinyBox == null )
                {
                    myChessBoardUI.PrintMessage("Ingreso NO valido - Revise las Referencias");
                } else if(selectedDestinyBox.Id == selectedPiece.BoardLocation.Id)
                {
                    myChessBoardUI.PrintMessage("Ingreso NO valido - Ingreso repetido");
                }
                else
                {
                    ChessPiece piece = ChessPieces.Find(p => p.BoardLocation.Id == selectedDestinyBox.Id);

                    //validar movimiento
                    bool b = MovementValidation(selectedPiece, selectedDestinyBox);

                    myChessBoardUI.PrintMessage("se puede mover: " + b);

                    //si se puede mover,
                    // actualzar posicion de la pieza movida
                    //si se come una pieza, actualizar el estado de esa pieza

                    //actualizar pantalla

                }

            } while (true);
        }



        public void GenerateInitialRandomDistribution()
        {
            //refactorizar, se repite codigo

            //locate not Alfil pieces
            ChessPieces.Where(p => p.Name != "Alfil").ToList()
             .ForEach(i => {
                 do
                 {
                     BoxLocation randomLocation = GetRandomLocation();

                     ChessPiece searchPiece = ChessPieces.Find(p => p.BoardLocation != null &&
                                                                    p.BoardLocation.Id == randomLocation.Id);

                     if (searchPiece == null)
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
                int colorFlag = 0;
                ChessPieces.Where(p => p.Name == "Alfil" && p.Player == player).ToList().
                ForEach(i => {
                    if (colorFlag == 0)
                    {
                        do
                        {
                            BoxLocation randomLocation = GetRandomLocation("dark");

                            ChessPiece searchPiece = ChessPieces.Find(p => p.BoardLocation != null &&
                                                                           p.BoardLocation.Id == randomLocation.Id);

                            if (searchPiece == null)
                            {
                                i.BoardLocation = randomLocation;
                            }

                        } while (i.BoardLocation == null);
                    }
                    else
                    {
                        do
                        {
                            BoxLocation randomLocation = GetRandomLocation("light");

                            ChessPiece foundPiece = ChessPieces.Find(fp => fp.BoardLocation != null &&
                                                                           fp.BoardLocation.Id == randomLocation.Id);

                            if (foundPiece == null)
                            {
                                i.BoardLocation = randomLocation;
                            }

                        } while (i.BoardLocation == null);
                    }
                    colorFlag ^= 1;
                });
            }
            //va aca?
            myChessBoardUI.PrintPieces(ChessPieces);
        }


        private BoxLocation GetRandomLocation()
        {
            Random r = new Random();
            int rInt = r.Next(1, 64);
            return Locations[rInt];
        }

        private BoxLocation GetRandomLocation(string color)
        {
            BoxLocation location;
            do
            {
                location = GetRandomLocation();
            } while (location.BackgroundColor != color);
            return location;
        }

        private List<ChessPiece> GenerateChessPieces()
        {
            List<ChessPiece> chessPieces = new List<ChessPiece>();

            for (int player = 1; player < 3; player++)
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

        private List<BoxLocation> LocationInitialization()
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
    }
}