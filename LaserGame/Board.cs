using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace LaserGame
{
    public abstract class Board
    {
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
