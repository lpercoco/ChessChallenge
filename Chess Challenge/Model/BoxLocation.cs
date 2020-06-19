using System;
using System.Collections.Generic;
using System.Text;

namespace Chess_Challenge
{
    public class BoxLocation
    {
        public int Id { get; set; }
        public string BoardReference { get; set; }
        public string BackgroundColor { get; set; }

        public BoxLocation() { }
        public BoxLocation(int id , string boardReference, string backgroundColor)
        {
            Id = id;
            BoardReference = boardReference;
            BackgroundColor = backgroundColor;
        }
    }
}
