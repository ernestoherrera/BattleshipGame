using Battleship.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame.DataStructures
{
    /// <summary>
    /// Sea Square object is a part of the board where a shot may land.
    /// The sea-square may be accupied by a battleship-square.
    /// The sea-square has a reference to a battleship.
    /// </summary>
    internal class SeaSquare
    {
        /// <summary>
        /// Builds one square of the sea area.
        /// Both arguments must be provided or none.
        /// </summary>
        /// <param name="battleship"></param>
        /// <param name="battleshipPart"></param>
        public SeaSquare(Battleship battleship, BattleshipSquare battleshipPart)
        {
            if (battleship != null && battleshipPart == null)
            {
                throw new ArgumentNullException(nameof(battleshipPart));
            }

            if (battleship == null && battleshipPart != null)
            {
                throw new ArgumentNullException(nameof(battleship));
            }

            HasBeenShot = false;
            Battleship = battleship;
            BattleshipPart = battleshipPart;
        }
        public bool HasBeenShot {get; set;}

        public BattleshipSquare BattleshipPart { get; private set; }

        public Battleship Battleship { get; private set; }

        public bool HasBattleship 
        { 
            get { return Battleship != null; } 
        }

    }
}
