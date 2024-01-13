using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace LaserGame
{
    public class LaserRayedBoard : Board
    {
        public override string ToString()
        {
            return "laser rayed board";
        }

        public override bool ReadFromFile(string filePath)
        {
            bool result = false;

            var lines = File.ReadAllLines(filePath);
            Debug.WriteLine("Parse {0} lines from file {1}", lines.Length, filePath);
            const int numberOfLines = 2;
            if (lines.Length == numberOfLines)
            {
                const int verticalRaysLine = 1;
                const int horizontalRaysLine = 2;

                var verticalLaserRays = ReadLaserRays(verticalRaysLine, "V", lines[verticalRaysLine - 1]);
                if (verticalLaserRays != null)
                {
                    TraceParsing(false, verticalRaysLine, string.Format("Get {0} vertical rays", verticalLaserRays.Count));
                    var horizontalLaserRays = ReadLaserRays(horizontalRaysLine, "H", lines[horizontalRaysLine - 1]);
                    if (horizontalLaserRays != null)
                    {
                        result = true;
                        TraceParsing(false, horizontalRaysLine, string.Format("Get {0} horizontal rays", horizontalLaserRays.Count));
                        _verticalLasers = verticalLaserRays;
                        _horizontalLasers = horizontalLaserRays;
                        SetFields(_verticalLasers.Count() - 1, _horizontalLasers.Count() - 1);
                    }
                }
            }
            else
                Debug.WriteLine("Wrong number of lines: Get {0}, expected {1}", lines.Length, numberOfLines);

            return result;
        }

        internal override int CalculatePenalty(Field field)
        {
            CheckIsOnBoard(field);
            return _verticalLasers[field.X] + _horizontalLasers[field.Y];
        }

        private List<int> ReadLaserRays(int lineno, string id, string line)
        {
            var rays = line.Split(" ");
            if (rays.Length > 2)
            {
                if (rays[0] == id)
                {
                    List<int> convertedRays = new List<int>();
                    bool result = true;
                    for (int i = 1; result && i < rays.Length; i++)
                    {
                        try
                        {
                            int ray = System.Convert.ToInt32(rays[i]);
                            convertedRays.Add(ray);
                        }
                        catch (Exception ex)
                        {
                            TraceParsing(true, lineno, string.Format("Cannot convert {0} to int", rays[i]));
                            result = false;
                        }
                    }
                    if (result)
                    {
                        return convertedRays;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    TraceParsing(true, lineno, string.Format("Find wrong first element {0} (expected {1})", rays[0], id));
                    return null;
                }
            }
            else
            {
                TraceParsing(true, lineno, string.Format("Cannot convert get enough rays (only {0} elements)", rays.Length));
                return null;
            }
        }

        private List<int> _horizontalLasers;
        private List<int> _verticalLasers;
    }
}
