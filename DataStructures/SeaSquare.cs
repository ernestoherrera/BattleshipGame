using System;

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
        /// Constructor for a sea square without a battleship on it.
        /// </summary>
        public SeaSquare()
        {
        }

        public bool HasBeenShot {get; set;}

        public BattleshipSquare BattleshipSquare { get; private set; }

        public Battleship Battleship { get; private set; }

        public bool HasBattleship 
        { 
            get { return Battleship != null; } 
        }

        /// <summary>
        /// Adds a battleship square to the sea square to track opponents pieces
        /// </summary>
        /// <param name="battleshipSquare"></param>
        public void AddBattleshipSquare(BattleshipSquare battleshipSquare)
        {
            if (battleshipSquare == null)
            {
                throw new ArgumentNullException(nameof(battleshipSquare));
            }

            BattleshipSquare = battleshipSquare;

            if (HasBattleship)
            {
                Battleship.AddBattleshipPart(battleshipSquare);
            }
            else
            {
                var battleship = new Battleship(new BattleshipSquare[] { battleshipSquare });
                Battleship = battleship;
            }
        }

        /// <summary>
        /// Initializes a battleship on the board
        /// </summary>
        /// <param name="battleship"></param>
        public void LinkBattleshipSquare(BattleshipSquare battleshipSquare)
        {
            if (battleshipSquare == null)
            {
                throw new ArgumentNullException(nameof(battleshipSquare));
            }

            BattleshipSquare = battleshipSquare;
        }

        public void LinkBattleship(Battleship battleship)
        {
            if (battleship == null)
            {
                throw new ArgumentNullException(nameof(battleship));
            }
            Battleship = battleship;
        }
    }
}
