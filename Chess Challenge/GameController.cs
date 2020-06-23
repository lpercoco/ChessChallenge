using System;
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
            myChessBoardUI.PrintPieces(ChessPieces);
        }

        public Boolean MovementValidation(ChessPiece piece, BoxLocation destinationBox)
        {
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
                        int rowIndex;
                        List<BoxLocation> posibleLocations = new List<BoxLocation>();

                        if (piece.Player == 1)
                        {
                            //player WHITE
                            rowIndex = row + 1;
                        }
                        else
                        {
                            //player BLACK
                            rowIndex = row - 1;        
                        }

                        posibleLocations = AddPosibleLocation(rowIndex, col, posibleLocations);
                        ChessPiece destinyPiece = ChessPieces.Where(p => p.BoardLocation.Id == destinationBox.Id).FirstOrDefault();

                        if ((posibleLocations.Count == 0) || // location outside of the board 
                            (posibleLocations.Count != 0 && posibleLocations.First().Id != destinationBox.Id) || // destiny not in possible location
                            (posibleLocations.Count != 0 && posibleLocations.First().Id == destinationBox.Id &&
                             destinyPiece != null && destinyPiece.Player == piece.Player)) // piece of the same player in the destiny
                        {
                            myChessBoardUI.PrintMessage("Movimiento no sorportado por el momento");
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                case "T":
                    {
                        bool rook = ValidateRookMove(colDestinyLocation, rowDestinyLocation, col, row, piece, destinationBox);                    
                        
                        if(!rook)
                        {
                            myChessBoardUI.PrintMessage("Movimiento invalido");
                        }

                        return rook;
                    }
                case "C":
                    {
                        int startRowIndex;
                        int endRowIndex;
                        int colIndex;
                        bool isTopFlag;

                        List<BoxLocation> posibleLocations = new List<BoxLocation>();

                        //calculation of the row and col indexs
                        if (row < rowDestinyLocation)
                        {
                            startRowIndex = 1;
                            endRowIndex = 2;
                            colIndex = 2;
                            isTopFlag = true;
                        } else if (row > rowDestinyLocation)
                        {
                            startRowIndex = -2;
                            endRowIndex = -1;
                            colIndex = 1;
                            isTopFlag = false;

                        }
                        else
                        {
                            // destiny location in the same line of the piece
                            myChessBoardUI.PrintMessage("Movimiento invalido");
                            return false;
                        }

                        //get possible locations
                        for (int rowIndex = startRowIndex; rowIndex <= endRowIndex; rowIndex++)
                        {
                            //calculation of the right side
                            int r = row + rowIndex;
                            int c = col + colIndex;

                            posibleLocations = AddPosibleLocation(r, c, posibleLocations);

                            //calculation of the left side
                            if(col - colIndex == 0)
                            {
                                c = 1;
                            }
                            else
                            {
                                c = col - colIndex;
                            }

                            posibleLocations = AddPosibleLocation(r, c, posibleLocations);

                            //update index considering the position of the selected location
                            if (isTopFlag) { colIndex = colIndex - 1; }
                            else { colIndex = colIndex + 1; };
                        }

                        if(posibleLocations.Any(l => l.Id == destinationBox.Id))
                        {
                            ChessPiece pieceLocation = ChessPieces.Where(p => p.Id == destinationBox.Id).FirstOrDefault(); ;

                            if ((pieceLocation != null && pieceLocation.Player == piece.Player))
                            {
                                //There is a piece of the same player in the selected location
                                myChessBoardUI.PrintMessage("Movimiento invalido");
                                return false;
                            }
                        }
                        else
                        {
                            //The selected destiny location is not included in the possible locations
                            myChessBoardUI.PrintMessage("Movimiento invalido");
                            return false;
                        }

                        return true;
                    }
                case "A":
                    {
                        bool bishop = ValidateBishopMove(colDestinyLocation, rowDestinyLocation, col, row, piece, destinationBox);

                        if (!bishop)
                        {
                            myChessBoardUI.PrintMessage("Movimiento invalido");
                        }

                        return bishop;
                    }
                case "Ra":
                    {
                         bool rook = ValidateRookMove(colDestinyLocation, rowDestinyLocation, col, row, piece, destinationBox);

                        if(rook)
                        {
                            return true;
                        }else
                        {
                            bool bishop = ValidateBishopMove(colDestinyLocation, rowDestinyLocation, col, row, piece, destinationBox);
                            
                            if(bishop)
                            {
                                return true;
                            }
                            else
                            {
                                myChessBoardUI.PrintMessage("Movimiento invalido");
                                return false;
                            }
                        }

                    }
                case "Ry":
                    {
                        int yAux;
                        int xAux;

                        List<BoxLocation> posibleLocations = new List<BoxLocation>();

                        //calculation of all the possible locations
                        for (int i = -1; i <= 1; i++)
                        {
                            yAux = row + i;
                            for (int j = -1; j <= 1; j++)
                            {
                                xAux = col + j;

                                //discard the  piece's location
                                if (yAux != row && xAux != col)
                                {
                                    posibleLocations = AddPosibleLocation(yAux, xAux, posibleLocations);
                                }
 
                            };
                        }

                        //discard locations that have an available piece of the same player
                        for (int i = posibleLocations.Count - 1; i > 0; i--)
                        {
                            ChessPiece pieceLocation = ChessPieces.Where(p => p.BoardLocation == posibleLocations[i]).FirstOrDefault();

                            if (pieceLocation != null && pieceLocation.Player == piece.Player)
                            {
                                posibleLocations.RemoveAt(i);
                            };
                        }

                        if(posibleLocations.Any(l => l.Id == destinationBox.Id))
                        {
                            //if the destiny location is a possible locations
                            //It is avalid movement
                            return true;
                        }
                        else
                        {
                            //if the destiny location is NOT a possible locations
                            //It is not a valid movement
                            myChessBoardUI.PrintMessage("Movimiento invalido");
                            return false;
                        };
                    }
                default:
                    break;
            }
            return true;
        }

        private bool ValidateBishopMove(int colDestinyLocation, int rowDestinyLocation, int col, int row, ChessPiece piece, BoxLocation destinationBox)
        {
            int startIndexRow;
            int startIndexCol;
            int endIndexRow;

            int calculatedLocation;
            List<BoxLocation> posibleLocations = new List<BoxLocation>();

            string flag;

            //calculation of the indexs 
            if (colDestinyLocation > col && rowDestinyLocation > row)
            {
                //upward (top)
                startIndexRow = row + 1;
                endIndexRow = rowDestinyLocation;

                startIndexCol = col + 1;

                flag = "upward";
            }
            else if (colDestinyLocation < col && rowDestinyLocation < row)
            {
                //upward (bottom)
                startIndexRow = rowDestinyLocation;
                endIndexRow = row - 1;

                startIndexCol = colDestinyLocation;

                flag = "upward";
            }
            else if (rowDestinyLocation > row && colDestinyLocation < col)
            {
                //downward (top)
                startIndexRow = row + 1;
                endIndexRow = rowDestinyLocation;

                startIndexCol = col - 1;

                flag = "downward";
            }
            else
            {
                //downward (bottom)
                startIndexRow = rowDestinyLocation;
                endIndexRow = row - 1;

                startIndexCol = colDestinyLocation;

                flag = "downward";
            };


            //get possible locations
            int j = startIndexCol;
            for (int i = startIndexRow; i <= endIndexRow; i++)
            {
                calculatedLocation = int.Parse(i.ToString() + j.ToString());

                posibleLocations = AddPosibleLocation(i, j, posibleLocations);

                if (flag == "upward")
                {
                    j = j + 1;
                }
                else
                {
                    j = j - 1;
                };
            }

            if (posibleLocations.Any(l => l.Id == destinationBox.Id))
            {
                for (int i = 0; i < posibleLocations.Count; i++)
                {
                    ChessPiece pieceLocation = ChessPieces.Where(p => p.BoardLocation.Id == posibleLocations[i].Id).FirstOrDefault();

                    if (posibleLocations[i].Id == destinationBox.Id &&
                            pieceLocation != null &&
                            pieceLocation.Player == piece.Player)
                    {
                        //if there is an available piece of the same player in the destiny location,
                        //it is an invalid move
                        return false;
                    }

                    if (posibleLocations[i].Id != destinationBox.Id &&
                        pieceLocation != null)
                    {
                        // if there is a available piece in the way to the destiny location,
                        //it is an invalid move
                        return false;
                    }

                    //if it did not break at the last loop,
                    //it is a valid move
                    if ((i + 1) == posibleLocations.Count)
                    {
                        return true;
                    };
                }
            }
            else
            {
                //the selected location is not in the possible locations
                return false;
            };
            return true;
        }

        private bool ValidateRookMove(int colDestinyLocation, int rowDestinyLocation, int col, int row, ChessPiece piece, BoxLocation destinationBox)
        {
            int startIndex;
            int endIndex;
            string destinyAxis;
            int calculatedLocation;

            if (colDestinyLocation == col || rowDestinyLocation == row)
            {
                //Considering the axis and the destiny locacion from the piece
                //I calculate the limits of the indexs
                if (rowDestinyLocation == row)
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

                //I go through the locations between the destination and the piece to move
                for (int indexAux = startIndex; indexAux <= endIndex; indexAux++)
                {
                    //I calculate the location based on the destiny axis
                    if (destinyAxis == "row")
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
                        pieceLocation.Player == piece.Player)
                    {
                        //if there is an available piece of the same player in the location,
                        //it is a invalid movement
                        return false;
                    }

                    if (calculatedLocation != destinationBox.Id &&
                        pieceLocation != null)
                    {
                        // if there is a available piece in the way to the destiny location,
                        //it is a invalid movement
                        return false;
                    }
                }
            }
            else
            {
                //the destiny location is not in any of the axis of the piece location,
                //it is a invalid movement
                return false;
            };
            //refactor this
            return true;
        }

        private List<BoxLocation> AddPosibleLocation(int r, int c, List<BoxLocation> posibleLocations)
        {
            //discard the rows and columns that are outside the board
            if(r > 0 && r < 9 && c > 0 && c < 9)
            {
                int calculatedLocation = int.Parse(r.ToString() + c.ToString());
                BoxLocation location = Locations.Where(l => l.Id == calculatedLocation).FirstOrDefault();

                if (location != null)
                {
                    posibleLocations.Add(location);
                }
            }            
            return posibleLocations;
        }

        public void PlayGame()
        {
            bool gameFlag = true;
            do
            {
                PlayTurn();

                //if any player has no pieces on the board
                //GAME OVER!
                if (ChessPieces.Where( p => p.Player == 1).ToList().Count == 0 || 
                    ChessPieces.Where(p => p.Player == 2).ToList().Count == 0) gameFlag = false;
            } while (gameFlag);

            myChessBoardUI.PrintGameOverMessage();
        }

        private void PlayTurn()
        {
            bool inputFlag = true;

            ChessPiece selectedPiece = null;
            BoxLocation selectedDestiny = null;

            do
            {
                //piece selection
                string selectedBox = myChessBoardUI.SelectBox("Ingrese ubicación de la pieza a mover");

                inputFlag = Locations.Any(l => l.BoardReference.Equals(selectedBox));

                if (inputFlag == false)
                {
                    myChessBoardUI.PrintMessage("Ingreso NO valido - Revise las Referencias");
                }
                else
                {
                    selectedPiece = ChessPieces.Find(p => p.BoardLocation.BoardReference.Equals(selectedBox));
                    if (selectedPiece == null)
                    {
                        inputFlag = false;
                        myChessBoardUI.PrintMessage("No hay una pieza en el casillero ingresado");
                    }

                    //destiny selection and movement validation
                    if (inputFlag != false)
                    {
                        selectedBox = myChessBoardUI.SelectBox("Ingrese el destino de la pieza: " + selectedPiece.BoardLocation.BoardReference);

                        selectedDestiny = Locations.Where(l => l.BoardReference.Equals(selectedBox)).FirstOrDefault();

                        if (selectedDestiny == null)
                        {
                            inputFlag = false;
                            myChessBoardUI.PrintMessage("Ingreso NO valido - Revise las Referencias");
                        }
                        else if (selectedDestiny.Id == selectedPiece.BoardLocation.Id)
                        {
                            inputFlag = false;
                            myChessBoardUI.PrintMessage("Ingreso NO valido - Ingreso repetido");
                        }
                        else
                        {
                            bool movementValidation = MovementValidation(selectedPiece, selectedDestiny);

                            if (!MovementValidation(selectedPiece, selectedDestiny))
                            {
                                inputFlag = false;
                            }

                        };
                    }
                }
            } while (inputFlag == false);

            UpdateChessPieceLocation(selectedPiece, selectedDestiny);

            myChessBoardUI.UpdateChessBoard(ChessPieces);
        }

        private void UpdateChessPieceLocation(ChessPiece selectedPiece, BoxLocation selectedDestiny)
        {
            // If there is a piece in the destiny location,
            // I remove/delete the piece
            ChessPiece pieceToRemove = ChessPieces.Where(p => p.BoardLocation == selectedDestiny).FirstOrDefault();

            if (pieceToRemove != null)
            {
                ChessPieces.Remove(pieceToRemove);
            };

            //Update piece's location
            int i = ChessPieces.FindIndex(p => p.Id == selectedPiece.Id);
            ChessPieces[i].BoardLocation = selectedDestiny;           
        }

        public void GenerateInitialRandomDistribution()
        {
            //locate all non-Bishop pieces
            ChessPieces.Where(p => p.Name != "Alfil").ToList()
             .ForEach(i => {
                 i = setRandomLocation(i);
             });

            //locate all Bishop pieces
            for (int player = 1; player < 3; player++)
            {
                //colorFlag = 0 = dark
                //colorFlag = 1 = light
                int colorFlag = 0;
                ChessPieces.Where(p => p.Name == "Alfil" && p.Player == player).ToList().
                ForEach(i => {
                    if (colorFlag == 0)
                    {
                        i = setRandomLocation(i, "dark");                     
                    }
                    else
                    {
                        i = setRandomLocation(i, "light");                      
                    }
                    colorFlag ^= 1;
                });
            }
        }

        private ChessPiece setRandomLocation(ChessPiece piece, string backgroundColor)
        {
            //refactor setRandomLocation(,) and setRandomLocation()
            do
            {
                BoxLocation randomLocation = GetRandomLocation(backgroundColor);

                ChessPiece searchPiece = ChessPieces.Find(p => p.BoardLocation != null &&
                                                               p.BoardLocation.Id == randomLocation.Id);

                if (searchPiece == null)
                {
                    piece.BoardLocation = randomLocation;
                }

            } while (piece.BoardLocation == null);

            return piece;
        }

        private ChessPiece setRandomLocation(ChessPiece piece)
        {
            do
            {
                BoxLocation randomLocation = GetRandomLocation();

                ChessPiece searchPiece = ChessPieces.Find(p => p.BoardLocation != null &&
                                                               p.BoardLocation.Id == randomLocation.Id);

                if (searchPiece == null)
                {
                    piece.BoardLocation = randomLocation;
                }

            } while (piece.BoardLocation == null);

            return piece;
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
                    ChessPiece peon = new ChessPiece(chessPieces.Count + 1, "Peon", "P", player);
                    chessPieces.Add(peon);
                }

                for (int i = 0; i < 2; i++)
                {
                    ChessPiece torre = new ChessPiece(chessPieces.Count + 1,"Torre", "T", player);
                    chessPieces.Add(torre);
                }

                for (int i = 0; i < 2; i++)
                {
                    ChessPiece alfil = new ChessPiece(chessPieces.Count + 1,"Alfil", "A", player);
                    chessPieces.Add(alfil);
                }

                for (int i = 0; i < 2; i++)
                {
                    ChessPiece caballo = new ChessPiece(chessPieces.Count + 1,"Caballo", "C", player);
                    chessPieces.Add(caballo);
                }

                ChessPiece rey = new ChessPiece(chessPieces.Count + 1,"Rey", "Ry", player);
                chessPieces.Add(rey);

                ChessPiece reina = new ChessPiece(chessPieces.Count + 1,"Reina", "Ra", player);
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
