using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace LaserGame
{
    public class ObstacleFilledBoard : Board
    {
        public override string ToString()
        {
            return "obstacle filled board";
        }

        public override bool ReadFromFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            Debug.WriteLine("Parse {0} lines from file {1}", lines.Length, filePath);
            if (ParseLines(lines))
            {
                SetFields(_obstacles.Count - 1, _cols - 1);
                return true;
            }
            return false;
        }

        internal override int CalculatePenalty(Field field)
        {
            CheckIsOnBoard(field);
            return _obstacles[field.X][field.Y];
        }

        private bool ParseLines(IEnumerable<string> lines)
        {
            int lineno = 1;
            foreach(var line in lines)
            {
                var obstacles = ReadObstacles(lineno, line);
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
                    else if (_cols < 2)
                    {
                        TraceParsing(true, lineno, string.Format("Line has only {0} obstacles (expected {1})", _cols, 2));
                        return false;

                    }
                    TraceParsing(false, lineno, string.Format("Get {0} obstacles", obstacles.Count));
                    _obstacles.Add(obstacles);
                }
                else
                {
                    if (_cols == obstacles.Count)
                    {
                        TraceParsing(false, lineno, string.Format("Get {0} obstacles", obstacles.Count));
                        _obstacles.Add(obstacles);
                    }
                    else
                    {
                        TraceParsing(true, lineno, string.Format("Line has {0} obstacles, expected {1}", obstacles.Count, _cols));
                        return false;
                    }
                }
                lineno++;
            }
            return true;        
        }

        private List<int> ReadObstacles(int lineno, string line)
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

        private int _cols = 0;

        private List<List<int>> _obstacles = new List<List<int>>();
    }
}
