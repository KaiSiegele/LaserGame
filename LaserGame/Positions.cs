using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaserGame
{
    class Positions
    {
        public void CalcutlateStartPosition(Board board)
        {
            Field firstField = new Field();
            int penalty = board.CalculatePenalty(firstField);
            AddPostion(firstField, new Way(firstField, penalty));
        }

        public void CalcuateNextPositions(Board board, Positions nextPostions)
        {

            foreach(var kvp in _latestPositions)
            {
                List<Field> lstFields = board.CalcualteNextFields(kvp.Key);
                foreach(var field in lstFields)
                {
                    Way way = kvp.Value.Copy();
                    int penalty = board.CalculatePenalty(field);
                    way.AddStep(field, penalty);
                    nextPostions.AddPostion(field, way);
                }
            }
        }

        public void WritePositions(int step)
        {
            Console.WriteLine("Step {0}", step);
            foreach (var kvp in _latestPositions)
            {
                Console.WriteLine(kvp.Value.ToString());
            }
        }

        public bool AtEnd(Field field)
        {
            bool result = false;
            if (_latestPositions.Count == 1)
            {
                result = (from kvp in _latestPositions select kvp.Key).Any(f => f.Equals(field));
            }
            return result;
        }
        private void AddPostion(Field lastPosition, Way way)
        {
            if (_latestPositions.ContainsKey(lastPosition))
            {
                if (_latestPositions[lastPosition].Penalty > way.Penalty)
                {
                    _latestPositions[lastPosition] = way;
                }
            }
            else
            {
                _latestPositions.Add(lastPosition, way);
            }
        }

        private Dictionary<Field, Way> _latestPositions = new Dictionary<Field, Way>();
    }
}
