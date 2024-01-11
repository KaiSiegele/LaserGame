using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaserGame
{
    public class LaserRayedBoard : Board
    {
        public LaserRayedBoard(IEnumerable<int> verticalLasers, IEnumerable<int> horizontalLasers)
        {
            _verticalLasers = verticalLasers.ToList();
            _horizontalLasers = horizontalLasers.ToList();

            SetFields(_verticalLasers.Count() - 1, _horizontalLasers.Count() - 1);
        }

        internal override int CalculatePenalty(Field field)
        {
            CheckIsOnBoard(field);
            return _verticalLasers[field.X] + _horizontalLasers[field.Y];
        }

        private List<int> _horizontalLasers;
        private List<int> _verticalLasers;
    }
}
