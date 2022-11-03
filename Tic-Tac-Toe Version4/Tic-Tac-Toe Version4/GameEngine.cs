using System.Collections.Generic;
using System.Linq;

namespace Tic_Tac_Toe
{
    public class GameEngine
    {
        public Player[,] BoardState { get; set; }
        public int GridSize { get; set; }
        public bool GameOver { get; set; } = false;
        public bool IsTie { get; set; } = false;
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
            IsTie = false;
            currentRow = null;
            currentColumn = null;
        }
        private void DefaultBoardStateArray()
        {
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
            if (row < 0 || column < 0)
            {
                throw new NegativeBoardLocationException("The board cannot have negative coordinates");
            }

            BoardState[row, column] = itsPlayerXsTurn ? Player.X : Player.O;
        }
        public void SetNextPlayer()
        {
            ItsPlayerXsTurn = !ItsPlayerXsTurn;
        }
        public void CheckGridForWinner()
        {
            CheckRowsForWinner();
            if (!GameOver)
                CheckColumnsForWinner();
            if (!GameOver)
                CheckMainDiagonalForWinner();
            if (!GameOver)
                CheckSecondaryDiagonalForWinner();
            if (!GameOver)
                CheckForTie();
        }
        public (Player, int?) CheckRowsForWinner()
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
                    return (helperList.First(), (int)currentRow);
                }
                else
                {
                    helperList.Clear();
                }
            }

            return (Player.None, null);
        }
        public (Player, int?) CheckColumnsForWinner()
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
                    return (helperList.First(), (int)currentColumn);
                }
                else
                {
                    helperList.Clear();
                }
            }

            return (Player.None, null);
        }
        public Player CheckMainDiagonalForWinner()
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
                return helperList.First();
            }
            else
            {
                helperList.Clear();
            }

            return Player.None;
        }
        public Player CheckSecondaryDiagonalForWinner()
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
                return helperList.First();
            }
            else
            {
                helperList.Clear();
            }

            return Player.None;
        }
        public void CheckForTie()
        {
            bool areAllCellsFilled = !BoardState.Cast<Player>().Any(cell => cell == Player.None);
            if (areAllCellsFilled)
            {
                GameOver = true;
                IsTie = true;
            }
        }
    }
}
