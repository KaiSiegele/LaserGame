using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace LaserGame
{
    public class ObstacleFilledBoard : Board
    {
        public bool ReadFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                Debug.WriteLine("Parse {0} lines from file {1}", lines.Length, filePath);
                if (ReadLines(lines))
                {
                    SetFields(_obstacles.Count - 1, _cols - 1);
                    return true;
                }
            }
            return false;
        }

        internal override int CalculatePenalty(Field field)
        {
            CheckIsOnBoard(field);
            return _obstacles[field.X][field.Y];
        }

        private bool ReadLines(IEnumerable<string> lines)
        {
            int lineno = 1;
            foreach(var line in lines)
            {
                var obstacles = ReadLine(lineno, line);
                if (obstacles == null)
                {
                    return false;
                }
                if (lineno == 1)
                {
                    _cols = obstacles.Count;
                    if (_cols == 0)
                    {
                        TraceParsing(true, lineno, "Line is empty");
                        return false;
                    }
                    _obstacles.Add(obstacles);
                }
                else
                {
                    if (_cols == obstacles.Count)
                    {
                        TraceParsing(true, lineno, string.Format("Line has {0} obstacles, expected {1}", obstacles.Count, _cols));
                        return false;
                    }
                    else
                    {
                        TraceParsing(false, lineno, string.Format("Get {0} obstacles", obstacles.Count));
                        _obstacles.Add(obstacles);
                    }                   
                    lineno++;
                }
            }
            return true;        
        }

        private List<int> ReadLine(int lineno, string line)
        {
            var obstacles = line.Split(" ");
            List<int> convertedObstacles = new List<int>();
            bool result = true;
            for (int i = 0; result && i < obstacles.Length; i++)
            {
                try
                {
                    int obstacle = System.Convert.ToInt32(obstacles[i]);
                    convertedObstacles.Add(obstacle);
                }
                catch (Exception ex)
                {
                    TraceParsing(true, lineno, string.Format("Cannot convert {0} to int", obstacles[i]));
                    result = false;
                }
            }
            if (result)
                return convertedObstacles;
            else
                return null;
        }

        private static void TraceParsing(bool error, int lineno, string message)
        {
            Debug.WriteLine("{0}Line {1}: {2}", error ? "Error in" : " ", lineno, message);
        }

        private int _cols = 0;

        private List<List<int>> _obstacles = new List<List<int>>();
    }
}
