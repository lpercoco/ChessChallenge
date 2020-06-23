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

            Controller.GenerateInitialRandomDistribution();

            Controller.InizializeUI();

            Controller.PlayGame();

            Console.Read();
        }    
    }
}