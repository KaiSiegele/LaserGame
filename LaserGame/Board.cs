using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace LaserGame
{
    public class Board
    {
        public Board(IEnumerable<int> verticalLasers, IEnumerable<int> horizontalLasers)
        {
            _verticalLasers = verticalLasers.ToList();
            _horizontalLasers = horizontalLasers.ToList();

            _xEnd = _verticalLasers.Count() - 1;
            _yEnd = _horizontalLasers.Count() - 1;

            _endField = new Field(_xEnd, _yEnd);
        }

        internal int CalculatePenalty(Field field)
        {
            CheckIsOnBoard(field);
            return _verticalLasers[field.X] + _horizontalLasers[field.Y];
        }

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
        private void CheckIsOnBoard(Field field)
        {
            Debug.Assert(field.X <= _xEnd);
            Debug.Assert(field.Y <= _yEnd);
        }

        private List<int> _horizontalLasers;
        private List<int> _verticalLasers;

        private int _xEnd;
        private int _yEnd;
        private Field _endField;
    }
}
