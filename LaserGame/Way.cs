using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaserGame
{
    class Way
    {
        public Way(Field field, int penalty)
            :this(new List<Field> { field }, penalty)
        {
        }
        public Way(List<Field> lstField, int penalty)
        {
            _fields = new List<Field>(lstField);
            _penalty = penalty;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            bool first = true;
            foreach (var field in _fields)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    sb.Append("->");
                }
                sb.Append(field.ToString());
            }
            sb.Append(" ");
            sb.Append(_penalty);

            return sb.ToString();
        }

        internal Way Copy()
        {
            return new Way(_fields, _penalty);
        }

        internal int Penalty
        {
            get
            {
                return _penalty;
            }
        }

        internal void AddStep(Field field, int penalty)
        {
            _fields.Add(field);
            _penalty += penalty;
        }

        readonly private List<Field> _fields;
        private int _penalty;
    }
}
