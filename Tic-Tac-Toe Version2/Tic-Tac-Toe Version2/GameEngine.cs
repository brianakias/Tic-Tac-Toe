using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tic_Tac_Toe
{
    public class GameEngine
    {
        private Player[,] _BoardState { get; set; }
        private bool _ItsPlayerXsTurn { get; set; } = true;
        private bool _HasTheGameEnded { get; set; } = false;
        private int _GridSize { get; set; }
        private Button[,] _Buttons { get; set; }
        private Grid _Grid { get; set; }
        private List<string> HelperList { get; set; }
        private int? CurrentRow { get; set; }
        private int? CurrentColumn { get; set; }

        public GameEngine(int gridSize, Grid myGrid)
        {
            _GridSize = gridSize;
            _Grid = myGrid;
            _BoardState = new Player[_GridSize, _GridSize];
            _Buttons = new Button[_GridSize, _GridSize];
            CreateGrid();
        }
        public void CreateGrid()
        {
            for (int i = 0; i < _GridSize; i++)
            {
                RowDefinition Row = new RowDefinition();
                Row.Height = new GridLength(1.0, GridUnitType.Star);
                _Grid.RowDefinitions.Add(Row);

                ColumnDefinition Column = new ColumnDefinition();
                Column.Width = new GridLength(1.0, GridUnitType.Star);
                _Grid.ColumnDefinitions.Add(Column);

                AddButtonsToGrid(i);
            }

            GiveButtonsAClickEventHandler();
        }
        private void AddButtonsToGrid(int i)
        {
            for (int j = 0; j < _GridSize; j++)
            {
                Button myButton = new Button();
                //might not need to name them if I can access them by indexing the array
                myButton.Name = $"Button{i}{j}";
                Grid.SetRow(myButton, i);
                Grid.SetColumn(myButton, j);
                _Grid.Children.Add(myButton);
                _Buttons[i, j] = myButton;
            }
        }
        private void GiveButtonsAClickEventHandler()
        {
            foreach (Button button in _Buttons)
            {
                button.Click += Button_Click;
            }
        }
        public void NewGame()
        {
            _BoardState = new Player[_GridSize, _GridSize];

            for (int i = 0; i < _BoardState.GetLength(0); i++)
            {
                for (int j = 0; j < _BoardState.GetLength(1); j++)
                {
                    _BoardState[i, j] = Player.None;
                }
            }


            _Grid.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.Gray;
            });

            _ItsPlayerXsTurn = true;
            _HasTheGameEnded = false;
            CurrentRow = null;
            CurrentColumn = null;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_HasTheGameEnded)
            {
                NewGame();
                return;
            }

            // identify which cell/button was pressed
            var buttonPressed = sender as Button;
            var row = Grid.GetRow(buttonPressed);
            var column = Grid.GetColumn(buttonPressed);

            // ignore cell override attempts
            if (_BoardState[row, column] != Player.None) return;

            _BoardState[row, column] = _ItsPlayerXsTurn ? Player.X : Player.O;
            buttonPressed.Content = _ItsPlayerXsTurn ? "X" : "O";

            CheckForWinner();
            SetNextPlayer();
        }
        private void SetNextPlayer()
        {
            _ItsPlayerXsTurn = !_ItsPlayerXsTurn;
        }
        private void CheckForWinner()
        {
            #region rows

            HelperList = new List<string>();
            for (int i = 0; i < _GridSize; i++)
            {
                CurrentRow = i;
                for (int j = 0; j < _GridSize; j++)
                {
                    CurrentColumn = j;
                    HelperList.Add((string)_Buttons[i, j].Content);
                }

                bool isAllEqual_Rows = HelperList.TrueForAll(x => x.Equals(HelperList.First()));

                if (HelperList.First() != String.Empty && isAllEqual_Rows)
                {
                    _HasTheGameEnded = true;
                    // change the colour of the winning buttons
                    for (int k = 0; k < _GridSize; k++)
                    {
                        _Buttons[(int)CurrentRow, k].Background = Brushes.DarkOliveGreen;
                    }
                    return;
                }
                else
                {
                    HelperList.Clear();
                }
            }
            #endregion

            #region columns

            for (int i = 0; i < _GridSize; i++)
            {
                CurrentColumn = i;

                for (int j = 0; j < _GridSize; j++)
                {
                    CurrentRow = j;
                    HelperList.Add((string)_Buttons[j, i].Content);
                }

                bool isAllEqual_Columns = HelperList.TrueForAll(x => x.Equals(HelperList.First()));

                if (HelperList.First() != String.Empty && isAllEqual_Columns)
                {
                    _HasTheGameEnded = true;
                    // change the colour of the winning buttons
                    for (int k = 0; k < _GridSize; k++)
                    {
                        _Buttons[k, (int)CurrentColumn].Background = Brushes.DarkOliveGreen;
                    }
                    return;
                }
                else
                {
                    HelperList.Clear();
                }
            }
            #endregion

            #region diagonal
            for (int i = 0; i < _GridSize; i++) // 0,0  1,1  2,2
            {
                HelperList.Add((string)_Buttons[i, i].Content);
            }

            bool isAllEqual_Diagonal = HelperList.TrueForAll(x => x.Equals(HelperList.First()));

            if (HelperList.First() != String.Empty && isAllEqual_Diagonal)
            {
                _HasTheGameEnded = true;
                // change the colour of the winning buttons
                for (int i = 0; i < _GridSize; i++)
                {
                    _Buttons[i, i].Background = Brushes.DarkOliveGreen;
                }
                return;
            }
            else
            {
                HelperList.Clear();
            }
            #endregion

            #region anti diagonal
            int antiDiagonalCounter = _GridSize - 1;
            for (int i = 0; i < _GridSize; i++) // 0,2  1,1  2,0
            {
                HelperList.Add((string)_Buttons[i, antiDiagonalCounter].Content);
                antiDiagonalCounter--;
            }

            bool isAllEqual_AntiDiagonal = HelperList.TrueForAll(x => x.Equals(HelperList.First()));

            if (HelperList.First() != String.Empty && isAllEqual_AntiDiagonal)
            {
                _HasTheGameEnded = true;
                // change the colour of the winning buttons
                antiDiagonalCounter = _GridSize - 1;
                for (int i = 0; i < _GridSize; i++)
                {
                    _Buttons[i, antiDiagonalCounter].Background = Brushes.DarkOliveGreen;
                    antiDiagonalCounter--;
                }

                return;
            }
            else
            {
                HelperList.Clear();
            }
            #endregion

            #region tie
            if (!_BoardState.Cast<Player>().Any(cell => cell == Player.None))
            {
                _HasTheGameEnded = true;
                _Grid.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Red;
                });
                return;
            }

            #endregion

        }
    }
}