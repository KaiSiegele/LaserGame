using LaserGame;
using System;
using System.Collections.Generic;
using System.IO;

namespace LaserGameTest
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string file = @"..\..\..\Dateien\ObstacleFilledBoard.txt";
            var obstacleFilledBoard = new ObstacleFilledBoard();
            PlayGame(file, obstacleFilledBoard);

            file = @"..\..\..\Dateien\LaserRayedBoard.txt";
            var laserRayedBoard = new LaserRayedBoard();
            PlayGame(file, laserRayedBoard);
        }

        static void PlayGame(string file, Board board)
        {
            Console.WriteLine("\nPlay game with {0}", board);
            if (File.Exists(file))
            {
                if (board.ReadFromFile(file))
                {
                    var game = new Game();
                    game.Play(board);
                }
                else
                    Console.WriteLine("Cannot create board from file {0}", file);
            }
            else
                Console.WriteLine("File {0} doesnot exists", file);            
        }
    }
}
