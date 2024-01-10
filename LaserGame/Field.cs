using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace LaserGame
{
    class Field : IEquatable<Field>
    {
        public bool Equals([AllowNull] Field other)
        {
            return IsEqual(other.X, other.Y);
        }

        public bool IsEqual(int x, int y)
        {
            return X == x && Y == y;
        }
        public Field() : this(0,0)
        {

        }
        public override int GetHashCode()
        {
            return 100 * X + Y;
        }

        public override string ToString()
        {
            return string.Format("X: {0}, Y: {1}", X, Y);
        }

        public Field(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; private set; }
        public int Y { get; private set; }
    }
}
