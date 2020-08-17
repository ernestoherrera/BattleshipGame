using System;
using System.Collections.Generic;
using System.Collections;


namespace BattleshipGame.DataStructures
{
    /// <summary>
    /// Battleship is made up of a linked list of BattleshipSquares.
    /// This data structure helps identifying the status of the ship: afloat or sunk, shot or not
    /// </summary>
    internal class Battleship : LinkedList<BattleshipSquare>
    {
        public Battleship(BattleshipSquare[] battleshipParts )
        {
            _isSunk = false;
            BuildBattleship(battleshipParts);
        }

        private bool _isSunk { get; set; }

        /// <summary>
        /// Battleship size
        /// </summary>
        public int Length { get; internal set; }

        /// <summary>
        /// It checks to see if the entire ship has been shot
        /// by looping through the link list.
        /// Each battleship square has a reference to it.
        /// </summary>
        /// <returns></returns>
        public bool IsBattleshipSunk()
        {
            if (_isSunk) return _isSunk;

            var current = First;
            var isSunk = true;

            if (current == null)
                throw new Exception("Battleship can't have zero nodes.");

            //Battleships of one node length
            if (current.Next == current)
            {
                _isSunk = current.Value.IsShot;
                return _isSunk;
            }

            while (current != null)
            {
                if (!current.Value.IsShot)
                {
                    isSunk = false;
                    break;
                }
                current = current.Next;
            }

            _isSunk = isSunk;
            return isSunk;
        }

        /// <summary>
        /// It links all the battleshipNodes to put together the battleship
        /// </summary>
        /// <param name="battleshipNodes"></param>
        private void BuildBattleship(BattleshipSquare[] battleshipNodes)
        {
            if (battleshipNodes == null)
            {
                throw new ArgumentNullException(nameof(battleshipNodes));
            }

            if (battleshipNodes.Length == 0)
            {
                throw new ArgumentOutOfRangeException("Battleships must have at least one BattleshipSquare.");
            }

            for (var i = 0; i < battleshipNodes.Length; i++)
            {
                AddSingleBattleshipPart(battleshipNodes[i]);
                Length++;
            }
        }

        /// <summary>
        /// Adds one battleship part of the battleship.
        /// It helps keeping track of the opponents battleships
        /// </summary>
        /// <param name="battleshipPart"></param>
        public void AddBattleshipPart(BattleshipSquare battleshipPart)
        {
            AddSingleBattleshipPart(battleshipPart);
            Length++;
        }

        /// <summary>
        /// Adds one battleship part of the battleship.
        /// </summary>
        /// <param name="battleshipPart"></param>
        private void AddSingleBattleshipPart(BattleshipSquare battleshipPart)
        {
            var node = new LinkedListNode<BattleshipSquare>(battleshipPart);
            if (First == null)
            {
                AddFirst(node);
            }
            else
            {
                AddLast(node);
            }
        }

        /// <summary>
        /// Sinks the battleship if the ShotResponse is Sink.
        /// </summary>
        public void SunkBattleship()
        {
            var current = First;

            while (current != null)
            {
                current.Value.IsShot = true;
                current = current.Next;
            }

            _isSunk = true;
        }
    }
}
