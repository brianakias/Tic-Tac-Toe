using System.Windows;

namespace Tic_Tac_Toe
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_3x3(object sender, RoutedEventArgs e)
        {
            GameEngine _GameEngine = new GameEngine(3, myGrid);
            _GameEngine.NewGame();
        }

        private void Button_Click_4x4(object sender, RoutedEventArgs e)
        {
            GameEngine _GameEngine = new GameEngine(4, myGrid);
            _GameEngine.NewGame();
        }


        private void Button_Click_5x5(object sender, RoutedEventArgs e)
        {
            GameEngine _GameEngine = new GameEngine(5, myGrid);
            _GameEngine.NewGame();

        }

        private void Button_Click_6x6(object sender, RoutedEventArgs e)
        {
            GameEngine _GameEngine = new GameEngine(6, myGrid);
            _GameEngine.NewGame();

        }

        private void Button_Click_7x7(object sender, RoutedEventArgs e)
        {
            GameEngine _GameEngine = new GameEngine(7, myGrid);
            _GameEngine.NewGame();

        }

        private void Button_Click_8x8(object sender, RoutedEventArgs e)
        {
            GameEngine _GameEngine = new GameEngine(8, myGrid);
            _GameEngine.NewGame();

        }

        private void Button_Click_9x9(object sender, RoutedEventArgs e)
        {
            GameEngine _GameEngine = new GameEngine(9, myGrid);
            _GameEngine.NewGame();

        }

        private void Button_Click_10x10(object sender, RoutedEventArgs e)
        {
            GameEngine _GameEngine = new GameEngine(10, myGrid);
            _GameEngine.NewGame();

        }

        private void Button_Click_11x11(object sender, RoutedEventArgs e)
        {
            GameEngine _GameEngine = new GameEngine(11, myGrid);
            _GameEngine.NewGame();

        }

        private void Button_Click_12x12(object sender, RoutedEventArgs e)
        {
            GameEngine _GameEngine = new GameEngine(12, myGrid);
            _GameEngine.NewGame();

        }
    }
}