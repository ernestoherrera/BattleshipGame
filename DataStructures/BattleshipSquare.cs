using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame.DataStructures
{
    /// <summary>
    /// Battleship square is an object that has coordinates and
    /// points to the next part of the battleship
    /// It keeps track of the shot.
    /// </summary>
    internal class BattleshipSquare
    {
        private Coordinate _coordinate;
        public BattleshipSquare(Coordinate coordinate)
        {
            _coordinate = coordinate;
            IsShot = false;

        }
        public bool IsShot { get; set; }

        public Coordinate Coordinate { get { return this._coordinate; } }

        public BattleshipSquare Next { get; set; }

    }
}
