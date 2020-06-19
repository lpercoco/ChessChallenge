using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess_Challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            GameController Controller = new GameController();

            Controller.InizializeUI();

            Controller.GenerateInitialRandomDistribution();

            Console.Read();
        }    
    }
}