using System;
using System.Collections.Generic;
using System.Text;

namespace Chess_Challenge
{
    public class ChessPiece
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int Player { get; set; }
        public BoxLocation BoardLocation { get; set; }
        public Boolean Available { get; set; }

        public ChessPiece() { }
        public ChessPiece(string name, string shortName, int player)
        {
            Name = name;
            ShortName = shortName;
            Player = player;
            Available = true;
        }
    }
}
