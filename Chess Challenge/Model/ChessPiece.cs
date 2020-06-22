using System;
using System.Collections.Generic;
using System.Text;

namespace Chess_Challenge
{
    public class ChessPiece
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int Player { get; set; }
        public BoxLocation BoardLocation { get; set; }

        public ChessPiece() { }
        public ChessPiece(int id, string name, string shortName, int player)
        {
            Name = name;
            ShortName = shortName;
            Player = player;
            Id = id;
        }

        public ChessPiece(string shortName)
        {
            ShortName = shortName;
        }
    }
}
