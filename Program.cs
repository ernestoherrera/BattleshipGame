using System;
using System.Collections.Generic;
using BattleshipGame.DataStructures;

namespace BattleshipGame
{
    class Program
    {
        private static Random _randGenerator = new Random();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //setting up a hard-coded game.
            var gameboard = StartNewBattleshipGame(10, 4, new int[4] { 2, 3, 2, 5 });
            gameboard.DisplayBoard();
            Console.ReadLine();

        }

        /// <summary>
        /// Initilization code.
        /// </summary>
        /// <param name="boardSize"></param>
        /// <param name="numberOfships"></param>
        /// <param name="shipSizes"></param>
        /// <returns></returns>
        private static BattleshipBoard StartNewBattleshipGame(int boardSize, int numberOfships, int[] shipSizes)
        {
            var gameboard = new BattleshipBoard(boardSize);

            for (int i = 0; i < numberOfships; i++)
            {
                for (int j = i; j < shipSizes.Length; j++)
                {
                    var battleship = CreateGameBattleship(shipSizes[j], boardSize);

                    if (gameboard.CanAddBattleship(battleship))
                    {
                        gameboard.PositionBattleship(battleship);
                    }

                    break;
                }
            }

            return gameboard;
        }

        private static Battleship CreateGameBattleship(int battleshipSize, int boardSize)
        {
            var coordinates = new Coordinate[battleshipSize];
            var isHorizontalPos = _randGenerator.Next(boardSize - 1) % 2 == 0;

            var xCoordinate = _randGenerator.Next(boardSize - 1);
            var yCoordinate = _randGenerator.Next(boardSize - 1);

            if (xCoordinate + battleshipSize > boardSize)
            {
                xCoordinate = GetSingleCoordinateWithinBoardGame(battleshipSize, boardSize);
            }

            if (yCoordinate + battleshipSize > boardSize)
            {
                yCoordinate = GetSingleCoordinateWithinBoardGame(battleshipSize, boardSize);
            }

            if (isHorizontalPos)
            {
                for (int i = 0; i < battleshipSize; i++)
                {
                    coordinates[i] = new Coordinate(xCoordinate, yCoordinate);
                    xCoordinate++;
                }
            }
            else
            {
                for (int i = 0; i < battleshipSize; i++)
                {
                    coordinates[i] = new Coordinate(xCoordinate, yCoordinate);
                    yCoordinate++;
                }
            }

            return BattleshipBuilder.BuildBattleShip(coordinates);
        }

        /// <summary>
        /// Returns a single coordinate point. It makes sure that the battleship fits in the board.
        /// </summary>
        /// <param name="boatSize"></param>
        /// <returns></returns>
        private static int GetSingleCoordinateWithinBoardGame(int boatSize, int boardSize)
        {
            var singleCoord = _randGenerator.Next(boardSize - 1);

            while (singleCoord + boatSize > boardSize)
            {
                singleCoord = _randGenerator.Next(boardSize - 1);
            }

            return singleCoord;
        }
    }
}
