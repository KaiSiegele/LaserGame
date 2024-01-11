using LaserGame;
using System;
using System.Collections.Generic;

namespace LaserGameTest
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string file = @"..\..\..\Dateien\ObstacleFilledBoard.txt";
            var obstacleFilledBoard = new ObstacleFilledBoard();
            if (obstacleFilledBoard.ReadFromFile(file))
            {
                PlayGame("Play with obstacle fille board", obstacleFilledBoard);
            }

            Board board = new LaserRayedBoard(new List<int>() { 1, 5, 8, 2 }, new List<int> { 4, 2, 3, 2 });
            PlayGame("Play with laser rayed board", board);
        }

        static void PlayGame(string message, Board board)
        {
            Console.WriteLine(message);
            var game = new Game();
            game.Play(board);
        }
    }
}
