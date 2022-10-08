using System;
using System.Collections.Generic;
using System.Linq;
// the 2 below namespaces are front end?
using System.Windows;
using System.Windows.Controls;

namespace Tic_Tac_Toe
{
    public class GameEngine
    {
        private Player[,] BoardState { get; set; }
        private bool ItsPlayerXsTurn { get; set; } = true;
        private bool GameOver { get; set; } = false;
        private FrontEndManager FrontEndManager { get; set; }
        private List<string> helperList { get; set; }
        private int? currentRow { get; set; }
        private int? currentColumn { get; set; }


        public GameEngine(FrontEndManager frontEndManager)
        {
            FrontEndManager = frontEndManager;
            GiveButtonsAClickEventHandler(frontEndManager);
            BoardState = new Player[FrontEndManager.GridSize, FrontEndManager.GridSize];

        }

        private void GiveButtonsAClickEventHandler(FrontEndManager frontEndManager)
        {
            foreach (Button button in frontEndManager.Buttons)
            {
                button.Click += Button_Click;
            }
        }
        public void NewGame()
        {
            DefaultBoardStateArray();
            FrontEndManager.DefaultButtonFormat();
            ItsPlayerXsTurn = true;
            GameOver = false;
            currentRow = null;
            currentColumn = null;
        }
        private void DefaultBoardStateArray()
        {
            BoardState = new Player[FrontEndManager.GridSize, FrontEndManager.GridSize];

            for (int i = 0; i < BoardState.GetLength(0); i++)
            {
                for (int j = 0; j < BoardState.GetLength(1); j++)
                {
                    BoardState[i, j] = Player.None;
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (GameOver)
            {
                NewGame();
                return;
            }

            var (button, row, column) = FrontEndManager.GetButtonAndItsLocation(sender);
            bool isButtonAlreadyPressed = BoardState[row, column] != Player.None;
            if (isButtonAlreadyPressed) return;

            UpdateBoardState(row, column, ItsPlayerXsTurn);
            FrontEndManager.SetButtonContent(button, ItsPlayerXsTurn);
            CheckGridForWinner();
            SetNextPlayer();
        }
        private void UpdateBoardState(int row, int column, bool itsPlayerXsTurn)
        {
            BoardState[row, column] = itsPlayerXsTurn ? Player.X : Player.O;
        }
        private void SetNextPlayer()
        {
            ItsPlayerXsTurn = !ItsPlayerXsTurn;
        }
        private void CheckGridForWinner()
        {
            CheckRowsForWinner();
            CheckColumnsForWinner();
            CheckMainDiagonalForWinner();
            CheckSecondaryDiagonalForWinner();
            CheckForTie();
        }
        private void CheckRowsForWinner()
        {
            helperList = new List<string>();
            for (int i = 0; i < FrontEndManager.GridSize; i++)
            {
                currentRow = i;
                for (int j = 0; j < FrontEndManager.GridSize; j++)
                {
                    currentColumn = j;
                    helperList.Add((string)FrontEndManager.Buttons[i, j].Content);
                }

                bool areAllButtonContentsEqual = helperList.TrueForAll(x => x.Equals(helperList.First()));

                if (helperList.First() != String.Empty && areAllButtonContentsEqual)
                {
                    GameOver = true;
                    FrontEndManager.HighlightRowWon((int)currentRow);
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
            helperList = new List<string>();
            for (int i = 0; i < FrontEndManager.GridSize; i++)
            {
                currentColumn = i;

                for (int j = 0; j < FrontEndManager.GridSize; j++)
                {
                    currentRow = j;
                    helperList.Add((string)FrontEndManager.Buttons[j, i].Content);
                }

                bool areAllButtonContentsEqual = helperList.TrueForAll(x => x.Equals(helperList.First()));

                if (helperList.First() != String.Empty && areAllButtonContentsEqual)
                {
                    GameOver = true;
                    FrontEndManager.HighlightColumnWon((int)currentColumn);
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
            helperList = new List<string>();

            for (int i = 0; i < FrontEndManager.GridSize; i++) // 0,0  1,1  2,2
            {
                helperList.Add((string)FrontEndManager.Buttons[i, i].Content);
            }

            bool areAllButtonContentsEqual = helperList.TrueForAll(x => x.Equals(helperList.First()));

            if (helperList.First() != String.Empty && areAllButtonContentsEqual)
            {
                GameOver = true;
                FrontEndManager.HighlightMainDiagonal();
                return;
            }
            else
            {
                helperList.Clear();
            }
        }
        private void CheckSecondaryDiagonalForWinner()
        {
            helperList = new List<string>();

            int antiDiagonalCounter = FrontEndManager.GridSize - 1;
            for (int i = 0; i < FrontEndManager.GridSize; i++) // 0,2  1,1  2,0
            {
                helperList.Add((string)FrontEndManager.Buttons[i, antiDiagonalCounter].Content);
                antiDiagonalCounter--;
            }

            bool areAllButtonContentsEqual = helperList.TrueForAll(x => x.Equals(helperList.First()));

            if (helperList.First() != String.Empty && areAllButtonContentsEqual)
            {
                GameOver = true;
                FrontEndManager.HighlightSecondaryDiagonal();
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
                FrontEndManager.ColourGridToDenoteTie();
                return;
            }
        }
    }
}
