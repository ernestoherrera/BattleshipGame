using Battleship.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame.DataStructures
{
    /// <summary>
    /// BattleshipBoard represents one side of the two player game.
    /// </summary>
    internal class BattleshipBoard
    {
        private const int MINIMUM_BOARD_SIZE = 4;
        //The Key value of the dictionary represent the X coordinates
        //The Value represents an array where the index represents the Y coordinates.
        //This board is used to keep track of the ships and opponents shots
        private Dictionary<int, SeaSquare[]> _board = new Dictionary<int, SeaSquare[]>();

        //Used to record the shots fired against the opponent.
        private Dictionary<int, SeaSquare[]> _OpponentsBoard = new Dictionary<int, SeaSquare[]>();

        public BattleshipBoard(int size)
        {
            if (size < MINIMUM_BOARD_SIZE)
            {
                throw new ArgumentOutOfRangeException($"Board sizes must be at least {MINIMUM_BOARD_SIZE}.");
            }

            for (var i = 0; i < size; i++)
            {
                _board.Add(i, new SeaSquare[size]);
                _OpponentsBoard.Add(i, new SeaSquare[size]);
            }
        }

        /// <summary>
        /// Gets a sea-square from a board
        /// </summary>
        /// <param name="board"></param>
        /// <param name="coord"></param>
        /// <returns></returns>
        public SeaSquare GetSeaSquare(Dictionary<int, SeaSquare[]> board, Coordinate coord)
        {
            if (coord == null)
            {
                throw new ArgumentNullException(nameof(coord));
            }
            if (board == null)
            {
                throw new ArgumentNullException(nameof(board));
            }

            return board[coord.X][coord.Y];
        }

        /// <summary>
        /// Processes and records the opponents shots.
        /// It keeps track of the shots on the sea-square and battleship-square.
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        public ShotResponse ProcessShot(Coordinate coord)
        {
            if (coord == null)
            {
                throw new ArgumentNullException(nameof(coord));
            }

            var seaSquare = GetSeaSquare(_board, coord);

            if (seaSquare.HasBeenShot)
            {
                //This coordinates have already been processed.
                return (seaSquare.HasBattleship) ? ShotResponse.Hit : ShotResponse.Miss;
            }

            seaSquare.HasBeenShot = true;

            if (seaSquare.HasBattleship)
            {
                if (seaSquare.BattleshipPart != null && !seaSquare.BattleshipPart.IsShot)
                {
                    seaSquare.BattleshipPart.IsShot = true;
                }

                return seaSquare.Battleship.IsBattleshipSunk() ? ShotResponse.Sunk : ShotResponse.Hit;
            }
            else
            {
                return ShotResponse.Miss;
            }
        }

        /// <summary>
        /// Shoots the opponents board
        /// </summary>
        /// <param name="coord"></param>
        public void Shoot(Coordinate coord)
        {
            if (coord == null)
            {
                throw new ArgumentNullException(nameof(coord));
            }

            if (HasShotThereBefore(coord))
            {
                //Do analysis of the board and try to shoot again?
            }
            else
            {
                var response = (ShotResponse)int.Parse(Console.ReadLine());
                //process the response?
            }
        }

        private bool HasShotThereBefore(Coordinate coord)
        {
            if (coord == null)
            {
                throw new ArgumentNullException(nameof(coord));
            }

            var seaSquare = GetSeaSquare(_OpponentsBoard, coord);

            if (seaSquare != null && seaSquare.HasBeenShot)
            {
                Console.WriteLine("You have shot there before. You can't win the game by shooting the same place twice.");
                return true;
            }
            return false;
        }
    }
}
