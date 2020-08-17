using BattleshipGame.Enums;
using System;
using System.Collections.Generic;

namespace BattleshipGame.DataStructures
{
    /// <summary>
    /// BattleshipBoard represents one side of the two player game.
    /// </summary>
    internal class BattleshipBoard
    {
        private const int MINIMUM_BOARD_SIZE = 4;
        //The Key value of the dictionary represent the Y coordinates
        //The Value represents an array where the index represents the X coordinates.
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
                var seaSquareRow = GetSeaSquareRow(size);
                var seaSquareRowOpponent = GetSeaSquareRow(size);

                _board.Add(i, seaSquareRow);
                _OpponentsBoard.Add(i, seaSquareRowOpponent);
            }
        }

        public Dictionary<int, SeaSquare[]> Board { get { return _board; } }

        /// <summary>
        /// Gets a sea-square from a board
        /// </summary>
        /// <param name="board"></param>
        /// <param name="coord"></param>
        /// <returns></returns>
        private SeaSquare GetSeaSquare(Dictionary<int, SeaSquare[]> board, Coordinate coord)
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
                if (seaSquare.HasBattleship)
                {
                    var isBattleshipSunk = seaSquare.Battleship.IsBattleshipSunk();

                    if (seaSquare.BattleshipSquare != null & !seaSquare.BattleshipSquare.IsShot)
                        seaSquare.BattleshipSquare.IsShot = true;

                    return isBattleshipSunk ? ShotResponse.Sunk : ShotResponse.Hit;
                }

                return ShotResponse.Miss;
            }

            seaSquare.HasBeenShot = true;

            if (seaSquare.HasBattleship)
            {
                if (seaSquare.BattleshipSquare != null && !seaSquare.BattleshipSquare.IsShot)
                    seaSquare.BattleshipSquare.IsShot = true;

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
                Console.WriteLine("You have shot there before. You can't win the game by shooting the same place twice.");
                return;
            }
            else
            {
                var response = (ShotResponse)int.Parse(Console.ReadLine());

                //store the opponents shot result on the opponents board
                var seaSquare = GetSeaSquare(_OpponentsBoard, coord);

                switch (response)
                {
                    case ShotResponse.Miss:
                        seaSquare.HasBeenShot = true;
                        break;
                    case ShotResponse.Hit:

                        seaSquare.HasBeenShot = true;
                        if (seaSquare.BattleshipSquare == null)
                        {
                            var battleshipSquare = new BattleshipSquare(coord);
                            battleshipSquare.IsShot = true;

                            seaSquare.AddBattleshipSquare(battleshipSquare);

                        }
                        else
                        {
                            // if it falls in here, it has been shot before.
                            seaSquare.BattleshipSquare.IsShot = true;
                        }
                        break;
                    case ShotResponse.Sunk:
                        
                        if (seaSquare.Battleship != null)
                        {
                            seaSquare.Battleship.SunkBattleship();
                        }
                        break;
                    default:
                        Console.WriteLine("Unknown response.");
                        Console.WriteLine("Appropriate values: [1] Miss, [2] Hit, [3] Sunk");
                        break;
                }
            }
        }

        /// <summary>
        /// Make sure there is no conflict with an existing battleship
        /// </summary>
        /// <param name="battleship"></param>
        /// <returns></returns>
        public bool CanAddBattleship(Battleship battleship)
        {
            //TODO: Make sure there is no conflict with an existing battleship
            return true;
        }

        /// <summary>
        /// Links all BattleshipSquares to its respective SeaSquare
        /// </summary>
        /// <param name="battleship"></param>
        public void PositionBattleship(Battleship battleship)
        {
            if (battleship == null)
            {
                throw new ArgumentNullException(nameof(battleship));
            }

            var current = battleship.First;

            while (current != null)
            {
                var battleshipCoord = current.Value.Coordinate;
                var seaSquare = GetSeaSquare(_board, battleshipCoord);

                seaSquare.LinkBattleshipSquare(current.Value);
                seaSquare.LinkBattleship(battleship);
                current = current.Next;
            }
        }

        public void DisplayBoard()
        {
            foreach (var item in _board)
            {
                var seaSquares = item.Value;
                foreach (var seaSquare in seaSquares)
                {
                    if (seaSquare.HasBattleship)
                        Console.Write(" + ");
                    else
                        Console.Write(" - ");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Checks if it has been shot before.
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        private bool HasShotThereBefore(Coordinate coord)
        {
            if (coord == null)
            {
                throw new ArgumentNullException(nameof(coord));
            }

            var seaSquare = GetSeaSquare(_OpponentsBoard, coord);

            return (seaSquare != null && seaSquare.HasBeenShot) ? true : false;

        }

        /// <summary>
        /// Builds the sea squares for the board
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private SeaSquare[] GetSeaSquareRow(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }

            var seaSquareArray = new SeaSquare[size];

            for (int i = 0; i < size; i++)
            {
                var seaSquare = new SeaSquare();
                seaSquareArray[i] = seaSquare;
            }

            return seaSquareArray;
        }

    }
}
