﻿using System.Collections.Generic;
using System.Linq;

namespace Tic_Tac_Toe
{
    public class GameEngine
    {
        public Player[,] BoardState { get; set; }
        public int GridSize { get; set; }
        public bool GameOver { get; set; } = false;
        public bool ItsPlayerXsTurn { get; set; } = true;
        private List<Player> helperList { get; set; }
        private int? currentRow { get; set; }
        private int? currentColumn { get; set; }

        public GameEngine(int gridSize)
        {
            GridSize = gridSize;
            BoardState = new Player[GridSize, GridSize];
        }
        public void NewGame()
        {
            DefaultBoardStateArray();
            ItsPlayerXsTurn = true;
            GameOver = false;
            currentRow = null;
            currentColumn = null;
        }
        private void DefaultBoardStateArray()
        {
            BoardState = new Player[GridSize, GridSize];

            for (int i = 0; i < BoardState.GetLength(0); i++)
            {
                for (int j = 0; j < BoardState.GetLength(1); j++)
                {
                    BoardState[i, j] = Player.None;
                }
            }
        }
        public void UpdateBoardState(int row, int column, bool itsPlayerXsTurn)
        {
            BoardState[row, column] = itsPlayerXsTurn ? Player.X : Player.O;
        }
        public void SetNextPlayer()
        {
            ItsPlayerXsTurn = !ItsPlayerXsTurn;
        }
        public void CheckGridForWinner()
        {
            CheckRowsForWinner();
            CheckColumnsForWinner();
            CheckMainDiagonalForWinner();
            CheckSecondaryDiagonalForWinner();
            CheckForTie();
        }
        private void CheckRowsForWinner()
        {
            helperList = new List<Player>();
            for (int i = 0; i < GridSize; i++)
            {
                currentRow = i;
                for (int j = 0; j < GridSize; j++)
                {
                    currentColumn = j;
                    helperList.Add(BoardState[i, j]);
                }

                bool areAllButtonContentsEqual = helperList.TrueForAll(x => x.Equals(helperList.First()));

                if (helperList.First() != Player.None && areAllButtonContentsEqual)
                {
                    GameOver = true;
                    return;
                }
                else
                {
                    helperList.Clear();
                }
            }
        }
        private void CheckColumnsForWinner()
        {
            helperList = new List<Player>();
            for (int i = 0; i < GridSize; i++)
            {
                currentColumn = i;

                for (int j = 0; j < GridSize; j++)
                {
                    currentRow = j;
                    helperList.Add(BoardState[j, i]);
                }

                bool areAllButtonContentsEqual = helperList.TrueForAll(x => x.Equals(helperList.First()));

                if (helperList.First() != Player.None && areAllButtonContentsEqual)
                {
                    GameOver = true;
                    return;
                }
                else
                {
                    helperList.Clear();
                }
            }
        }
        private void CheckMainDiagonalForWinner()
        {
            helperList = new List<Player>();

            for (int i = 0; i < GridSize; i++) // 0,0  1,1  2,2
            {
                helperList.Add(BoardState[i, i]);
            }

            bool areAllButtonContentsEqual = helperList.TrueForAll(x => x.Equals(helperList.First()));

            if (helperList.First() != Player.None && areAllButtonContentsEqual)
            {
                GameOver = true;
                return;
            }
            else
            {
                helperList.Clear();
            }
        }
        private void CheckSecondaryDiagonalForWinner()
        {
            helperList = new List<Player>();

            int antiDiagonalCounter = GridSize - 1;
            for (int i = 0; i < GridSize; i++) // 0,2  1,1  2,0
            {
                helperList.Add(BoardState[i, antiDiagonalCounter]);
                antiDiagonalCounter--;
            }

            bool areAllButtonContentsEqual = helperList.TrueForAll(x => x.Equals(helperList.First()));

            if (helperList.First() != Player.None && areAllButtonContentsEqual)
            {
                GameOver = true;
                return;
            }
            else
            {
                helperList.Clear();
            }
        }
        private void CheckForTie()
        {
            bool areAllCellsFilled = !BoardState.Cast<Player>().Any(cell => cell == Player.None);
            if (areAllCellsFilled)
            {
                GameOver = true;
                return;
            }
        }
    }
}