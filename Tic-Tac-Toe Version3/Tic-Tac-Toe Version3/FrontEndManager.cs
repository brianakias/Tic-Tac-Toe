using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tic_Tac_Toe
{
    public class FrontEndManager
    {
        public Button[,] Buttons { get; set; }
        private Grid Grid { get; set; }
        public int GridSize { get; set; }


        public FrontEndManager(int gridsize, Grid myGrid)
        {
            GridSize = gridsize;
            Grid = myGrid;
            Buttons = new Button[GridSize, GridSize];
            CreateGrid();
        }

        public void CreateGrid()
        {
            for (int i = 0; i < GridSize; i++)
            {
                RowDefinition Row = new RowDefinition();
                Row.Height = new GridLength(1.0, GridUnitType.Star);
                Grid.RowDefinitions.Add(Row);

                ColumnDefinition Column = new ColumnDefinition();
                Column.Width = new GridLength(1.0, GridUnitType.Star);
                Grid.ColumnDefinitions.Add(Column);
                AddButtonsToGrid(i);
            }
        }
        private void AddButtonsToGrid(int i)
        {
            for (int j = 0; j < GridSize; j++)
            {
                Button myButton = new Button();
                Grid.SetRow(myButton, i);
                Grid.SetColumn(myButton, j);
                Grid.Children.Add(myButton);
                Buttons[i, j] = myButton;
            }
        }
        public (Button button, int row, int column) GetButtonAndItsLocation(object sender)
        {
            var button = sender as Button;
            int row = Grid.GetRow(button);
            int column = Grid.GetColumn(button);

            return (button, row, column);
        }
        public void SetButtonContent(Button button, bool ItsPlayerXsTurn)
        {
            button.Content = ItsPlayerXsTurn ? "X" : "O";
        }
        public void DefaultButtonFormat()
        {
            Grid.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.Gray;
            });
        }
        public void HighlightRowWon(int row)
        {
            for (int i = 0; i < GridSize; i++)
            {
                Buttons[row, i].Background = Brushes.DarkOliveGreen;
            }
        }
        public void HighlightColumnWon(int column)
        {
            for (int i = 0; i < GridSize; i++)
            {
                Buttons[i, column].Background = Brushes.DarkOliveGreen;
            }
        }
        public void HighlightMainDiagonal()
        {
            for (int i = 0; i < GridSize; i++)
            {
                Buttons[i, i].Background = Brushes.DarkOliveGreen;
            }
        }
        public void HighlightSecondaryDiagonal()
        {
            int antiDiagonalCounter = GridSize - 1;
            for (int i = 0; i < GridSize; i++)
            {
                Buttons[i, antiDiagonalCounter].Background = Brushes.DarkOliveGreen;
                antiDiagonalCounter--;
            }
        }
        internal void ColourGridToDenoteTie()
        {
            Grid.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Background = Brushes.PaleVioletRed;
            });
        }
    }
}
