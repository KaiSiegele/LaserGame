using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace LaserGame
{
    public abstract class Board
    {
        public abstract bool ReadFromFile(string filePath);

        protected void SetFields(int xEnd, int yEnd)
        {
            _xEnd = xEnd;
            _yEnd = yEnd;

            _endField = new Field(_xEnd, _yEnd);
        }

        internal abstract int CalculatePenalty(Field field);

        internal List<Field> CalcualteNextFields(Field field)
        {
            CheckIsOnBoard(field);
            List<Field> nextFields = new List<Field>();
            if (field.X < _xEnd)
            {
                nextFields.Add(new Field(field.X + 1, field.Y));
            }
            if (field.Y < _yEnd)
            {
                nextFields.Add(new Field(field.X, field.Y + 1));
            }
            return nextFields;
        }

        internal Field EndField
        {
            get
            {
                return _endField;
            }
        }

        internal protected static void TraceParsing(bool error, int lineno, string message)
        {
            if (error)
                Console.WriteLine("Error in Line {0}: {1}", lineno, message);
            else
                Debug.WriteLine("Line {0}: {1}", lineno, message);
        }

        [Conditional("DEBUG")]
        internal void CheckIsOnBoard(Field field)
        {
            Debug.Assert(field.X <= _xEnd);
            Debug.Assert(field.Y <= _yEnd);
        }

        private int _xEnd;
        private int _yEnd;
        private Field _endField;
    }
}
