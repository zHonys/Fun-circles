﻿namespace Circles
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game(600, 600))
            {
                game.Run(60);
            }
        }
    }
}