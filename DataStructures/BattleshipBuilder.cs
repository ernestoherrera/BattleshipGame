using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame.DataStructures
{
    internal class BattleshipBuilder
    {
        public static Battleship BuildBattleShip(Coordinate[] coordinates)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException(nameof(coordinates));
            }
            if (coordinates.Length == 0)
            {
                throw new ArgumentOutOfRangeException($"A battleship can not be of zero length.");
            }

            var battleshipSize = coordinates.Length;
            var battleshipSquares = new BattleshipSquare[battleshipSize];
            var counter = 0;

            foreach (var coord in coordinates)
            {
                var bPart = new BattleshipSquare(coord);

                battleshipSquares[counter] = bPart;
                counter++;
            }

            var battleship = new Battleship(battleshipSquares);

            return battleship;

        }
    }
}
