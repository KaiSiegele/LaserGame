using LaserGame;
using System;
using System.Collections.Generic;

namespace LaserGameTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Game starts");
            Board board = new LaserRayedBoard(new List<int>() { 1, 5, 8, 2 }, new List<int> { 4, 2, 3, 2 });
            var game = new Game();
            game.Play(board);
        }
    }
}
