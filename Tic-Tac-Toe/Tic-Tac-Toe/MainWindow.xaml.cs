using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    enum AcceptableCellStates { EMPTY, X, O }

    public partial class MainWindow : Window
    {
        private AcceptableCellStates[,] boardState = new AcceptableCellStates[3, 3];
        private bool hasTheGameEnded;
        private bool itsPlayerXsTurn;
        private int turnsPassed = 0;



        public MainWindow()
        {
            InitializeComponent();

            StartNewGame();
        }

        private void StartNewGame()
        {
            // reset the button contents and appearance
            // NEED TO JUMP TO 26:19 AND ADD THE REST?
            myGrid.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.Gray;
            });

            // reset the boardState array
            // I DONT THINK I NEED THIS INITIALISATION
            //boardState = new AcceptableCellStates[3, 3];

            for (int i = 0; i < boardState.GetLength(0); i++)
            {
                for (int j = 0; j < boardState.GetLength(1); j++)
                {
                    boardState[i, j] = AcceptableCellStates.EMPTY;
                }
            }

            // set player X as active player
            itsPlayerXsTurn = true;

            // set game state as not ended
            hasTheGameEnded = false;

            // reset turn counter
            turnsPassed = 0;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // if the game has ended, start a new game
            if (hasTheGameEnded)
            {
                StartNewGame();
                return;
            }

            // identify which cell/button was pressed
            var buttonPressed = sender as Button;
            var row = Grid.GetRow(buttonPressed);
            var column = Grid.GetColumn(buttonPressed);

            // ignore cell override attempts
            if (boardState[row, column] != AcceptableCellStates.EMPTY) return;

            // update the board array and the actual board
            boardState[row, column] = itsPlayerXsTurn ? AcceptableCellStates.X : AcceptableCellStates.O;
            buttonPressed.Content = itsPlayerXsTurn ? 'X' : 'O';

            // togle player turn
            itsPlayerXsTurn = !itsPlayerXsTurn;

            turnsPassed++;

            CheckForWinner();

        }

        private void CheckForWinner()
        {
            /*
             * I abandoned this approach, as I wasn't sure how to colour the cells this way - Reminder to ask Brent
             * Given the way I have named the buttons, is there a way to use i to dynamically select the buttons to colour?
             * I guess that instead of colouring I could do something like MessageBox.Show("Player X won in row 0") to display the winner.
             * 
            // check for horizontal wins
            for (int i = 0; i < 3; i++)
            {
                if (boardState[i, 0] != AcceptableCellStates.EMPTY && (boardState[i, 0] == boardState[i, 1] && boardState[i, 1] == boardState[i, 2]))
                {
                    // someone won do something
                }

                // check for vertical wins
                if (boardState[0, i] != AcceptableCellStates.EMPTY && (boardState[0, i] == boardState[1, i] && boardState[1, i] == boardState[2, i]))
                {
                    // someone won do something
                }
            }
            */

            // check wins on rows
            // Row 0
            if (boardState[0, 0] != AcceptableCellStates.EMPTY && (boardState[0, 0] == boardState[0, 1] && boardState[0, 1] == boardState[0, 2]))
            {
                hasTheGameEnded = true;
                Button00.Background = Button01.Background = Button02.Background = Brushes.DarkOliveGreen;
            }
            // Row 1
            if (boardState[1, 0] != AcceptableCellStates.EMPTY && (boardState[1, 0] == boardState[1, 1] && boardState[1, 1] == boardState[1, 2]))
            {
                hasTheGameEnded = true;
                Button10.Background = Button11.Background = Button12.Background = Brushes.DarkOliveGreen;
            }
            // Row 2
            if (boardState[2, 0] != AcceptableCellStates.EMPTY && (boardState[2, 0] == boardState[2, 1] && boardState[2, 1] == boardState[2, 2]))
            {
                hasTheGameEnded = true;
                Button20.Background = Button21.Background = Button22.Background = Brushes.DarkOliveGreen;
            }

            // check for wins on columns
            // Column 0
            if (boardState[0, 0] != AcceptableCellStates.EMPTY && (boardState[0, 0] == boardState[1, 0] && boardState[1, 0] == boardState[2, 0]))
            {
                hasTheGameEnded = true;
                Button00.Background = Button10.Background = Button20.Background = Brushes.DarkOliveGreen;
            }
            // Column 1
            if (boardState[0, 1] != AcceptableCellStates.EMPTY && (boardState[0, 1] == boardState[1, 1] && boardState[1, 1] == boardState[2, 1]))
            {
                hasTheGameEnded = true;
                Button01.Background = Button11.Background = Button21.Background = Brushes.DarkOliveGreen;
            }
            // Column 2
            if (boardState[0, 2] != AcceptableCellStates.EMPTY && (boardState[0, 2] == boardState[1, 2] && boardState[1, 2] == boardState[2, 2]))
            {
                hasTheGameEnded = true;
                Button02.Background = Button12.Background = Button22.Background = Brushes.DarkOliveGreen;
            }

            // check for the two diagonal wins
            if (boardState[1, 1] != AcceptableCellStates.EMPTY && (boardState[0, 0] == boardState[1, 1] && boardState[1, 1] == boardState[2, 2]))
            {
                hasTheGameEnded = true;
                Button00.Background = Button11.Background = Button22.Background = Brushes.DarkOliveGreen;
            }

            if (boardState[1, 1] != AcceptableCellStates.EMPTY && (boardState[0, 2] == boardState[1, 1] && boardState[1, 1] == boardState[2, 0]))
            {
                hasTheGameEnded = true;
                Button02.Background = Button11.Background = Button20.Background = Brushes.DarkOliveGreen;
            }

            // check for tie
            if (turnsPassed == 9)
            {
                hasTheGameEnded = true;
                // reminder to ask Brent how can I colour all buttons in one go
                Button00.Background = Button01.Background = Button02.Background = Button10.Background = Button11.Background = Button12.Background = Button20.Background = Button21.Background = Button22.Background = Brushes.Orange;
            }

        }
    }
}