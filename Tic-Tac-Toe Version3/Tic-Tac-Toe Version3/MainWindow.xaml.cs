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
            GameEngine GameEngine = new GameEngine(new FrontEndManager(3, myGrid));
            GameEngine.NewGame();
        }
        private void Button_Click_4x4(object sender, RoutedEventArgs e)
        {
            GameEngine GameEngine = new GameEngine(new FrontEndManager(4, myGrid));
            GameEngine.NewGame();
        }
        private void Button_Click_5x5(object sender, RoutedEventArgs e)
        {
            GameEngine GameEngine = new GameEngine(new FrontEndManager(5, myGrid));
            GameEngine.NewGame();
        }
        private void Button_Click_6x6(object sender, RoutedEventArgs e)
        {
            GameEngine GameEngine = new GameEngine(new FrontEndManager(6, myGrid));
            GameEngine.NewGame();
        }
        private void Button_Click_7x7(object sender, RoutedEventArgs e)
        {
            GameEngine GameEngine = new GameEngine(new FrontEndManager(7, myGrid));
            GameEngine.NewGame();
        }
        private void Button_Click_8x8(object sender, RoutedEventArgs e)
        {
            GameEngine GameEngine = new GameEngine(new FrontEndManager(8, myGrid));
            GameEngine.NewGame();
        }
        private void Button_Click_9x9(object sender, RoutedEventArgs e)
        {
            GameEngine GameEngine = new GameEngine(new FrontEndManager(9, myGrid));
            GameEngine.NewGame();
        }
        private void Button_Click_10x10(object sender, RoutedEventArgs e)
        {
            GameEngine GameEngine = new GameEngine(new FrontEndManager(10, myGrid));
            GameEngine.NewGame();
        }
        private void Button_Click_11x11(object sender, RoutedEventArgs e)
        {
            GameEngine GameEngine = new GameEngine(new FrontEndManager(11, myGrid));
            GameEngine.NewGame();
        }
        private void Button_Click_12x12(object sender, RoutedEventArgs e)
        {
            GameEngine GameEngine = new GameEngine(new FrontEndManager(12, myGrid));
            GameEngine.NewGame();
        }
    }
}
