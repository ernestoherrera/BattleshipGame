using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame.DataStructures
{
    /// <summary>
    /// Coordinates are zero based
    /// </summary>
    internal class Coordinate
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Coordinate(int x, int y)
        {
            if (x < 0 || y < 0)
            {
                throw new ArgumentOutOfRangeException("Coordinates must be given in positive values");
            }
            X = x;
            Y = y;
        }

    }
}
