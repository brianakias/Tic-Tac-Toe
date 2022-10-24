using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tic_Tac_Toe
{
    public partial class MainWindow : Window

    {
        public Button[,] Buttons { get; set; }
        private Grid grid { get; set; }
        public GameEngine GameEngine { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            grid = myGrid;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (GameEngine.GameOver)
            {
                GameEngine.NewGame();
                return;
            }

            var (button, row, column) = GetButtonAndItsLocation(sender);

            bool isButtonAlreadyPressed = GameEngine.BoardState[row, column] != Player.None;
            if (isButtonAlreadyPressed) return;

            GameEngine.UpdateBoardState(row, column, GameEngine.ItsPlayerXsTurn);
            SetButtonContent(button, GameEngine.ItsPlayerXsTurn);
            GameEngine.CheckGridForWinner();
            GameEngine.SetNextPlayer();
        }

        private void GiveButtonsAClickEventHandler()
        {
            foreach (Button button in Buttons)
            {
                button.Click += Button_Click;
            }
        }

        public (Button button, int row, int column) GetButtonAndItsLocation(object sender)
        {
            var button = sender as Button;
            int row = Grid.GetRow(button);
            int column = Grid.GetColumn(button);

            return (button, row, column);
        }
        public void DefaultButtonFormat()
        {
            grid.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.Gray;
            });
        }
        public void SetButtonContent(Button button, bool ItsPlayerXsTurn)
        {
            button.Content = ItsPlayerXsTurn ? "X" : "O";
        }
        public void CreateGrid(int gridSize)
        {
            for (int i = 0; i < gridSize; i++)
            {
                RowDefinition Row = new RowDefinition();
                Row.Height = new GridLength(1.0, GridUnitType.Star);
                grid.RowDefinitions.Add(Row);

                ColumnDefinition Column = new ColumnDefinition();
                Column.Width = new GridLength(1.0, GridUnitType.Star);
                grid.ColumnDefinitions.Add(Column);
                AddButtonsToGrid(i, gridSize);
            }
        }
        private void AddButtonsToGrid(int i, int gridSize)
        {
            for (int j = 0; j < gridSize; j++)
            {
                Button myButton = new Button();
                Grid.SetRow(myButton, i);
                Grid.SetColumn(myButton, j);
                grid.Children.Add(myButton);
                Buttons[i, j] = myButton;
            }
        }
        private void NewGrid(int gridSize)
        {
            GameEngine = new GameEngine(gridSize);
            Buttons = new Button[GameEngine.GridSize, GameEngine.GridSize];
            CreateGrid(GameEngine.GridSize);
            GameEngine.NewGame();
            DefaultButtonFormat();
            GiveButtonsAClickEventHandler();
        }
        private void Button_Click_3x3(object sender, RoutedEventArgs e)
        {
            NewGrid(3);
        }
        private void Button_Click_4x4(object sender, RoutedEventArgs e)
        {
            NewGrid(4);
        }
        private void Button_Click_5x5(object sender, RoutedEventArgs e)
        {
            NewGrid(5);
        }
        private void Button_Click_6x6(object sender, RoutedEventArgs e)
        {
            NewGrid(6);
        }
        private void Button_Click_7x7(object sender, RoutedEventArgs e)
        {
            NewGrid(7);
        }
        private void Button_Click_8x8(object sender, RoutedEventArgs e)
        {
            NewGrid(8);
        }
        private void Button_Click_9x9(object sender, RoutedEventArgs e)
        {
            NewGrid(9);
        }
        private void Button_Click_10x10(object sender, RoutedEventArgs e)
        {
            NewGrid(10);
        }
        private void Button_Click_11x11(object sender, RoutedEventArgs e)
        {
            NewGrid(11);
        }
        private void Button_Click_12x12(object sender, RoutedEventArgs e)
        {
            NewGrid(12);
        }
    }
}
