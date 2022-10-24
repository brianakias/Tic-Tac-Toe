using NUnit.Framework;
using Tic_Tac_Toe;

namespace nUnit_Tests_Tic_Tac_Toe
{
    public class TicTacToeTests
    {
        private GameEngine gameEngine;
        private int gridSize = 3;

        [SetUp]
        public void Setup()
        {
            gameEngine = new GameEngine(gridSize);
        }

        [Test]
        public void Constructor_Initializes_GridSize_Property()
        {
            Assert.That(gameEngine.GridSize, Is.EqualTo(gridSize));
        }

        [Test]
        public void Constructor_Initializes_BoardState_Property()
        {
            Assert.That(gameEngine.BoardState, Is.EqualTo(new Player[gameEngine.GridSize, gameEngine.GridSize]));
        }

        [Test]
        public void NewGame_Defaults_Initial_Values()
        {
            Player[,] boardState = gameEngine.BoardState;
            Player[,] defaultBoardState = new Player[gridSize, gridSize];

            boardState[0, 0] = Player.O;
            gameEngine.ItsPlayerXsTurn = false;
            gameEngine.GameOver = true;
            gameEngine.IsTie = false;

            gameEngine.NewGame();

            Assert.That(gameEngine.ItsPlayerXsTurn, Is.True);
            Assert.That(gameEngine.GameOver, Is.False);
            Assert.That(boardState, Is.EqualTo(defaultBoardState));
        }

        [Test]
        public void Updating_BoardState_Based_On_Active_Player_Is_X()
        {
            Player[,] boardState = gameEngine.BoardState;
            gameEngine.ItsPlayerXsTurn = true;
            gameEngine.UpdateBoardState(0, 0, gameEngine.ItsPlayerXsTurn);
            Assert.That(boardState[0, 0], Is.EqualTo(Player.X));
        }

        [Test]
        public void Updating_BoardState_Based_On_Active_Player_Is_O()
        {
            Player[,] boardState = gameEngine.BoardState;
            gameEngine.ItsPlayerXsTurn = false;
            gameEngine.UpdateBoardState(0, 0, gameEngine.ItsPlayerXsTurn);
            Assert.That(boardState[0, 0], Is.EqualTo(Player.O));
        }

        [Test]
        public void Setting_Next_Player_From_X_To_O()
        {
            gameEngine.ItsPlayerXsTurn = true;
            gameEngine.SetNextPlayer();
            Assert.That(gameEngine.ItsPlayerXsTurn, Is.False);
        }

        [Test]
        public void Setting_Next_Player_From_O_To_X()
        {
            gameEngine.ItsPlayerXsTurn = false;
            gameEngine.SetNextPlayer();
            Assert.That(gameEngine.ItsPlayerXsTurn, Is.True);
        }
        [Test]
        public void Player_X_Won_In_First_Row()
        {
            int row = 0;
            for (int i = 0; i < gameEngine.BoardState.GetLength(1); i++)
            {
                gameEngine.BoardState[row, i] = Player.X;
            }
            var (playerWon, rowWon) = gameEngine.CheckRowsForWinner();
            Assert.That(gameEngine.GameOver, Is.True);
            Assert.That(playerWon, Is.EqualTo(Player.X));
            Assert.That(rowWon, Is.EqualTo(row));
        }

        [Test]
        public void Player_O_Won_In_Second_Column()
        {
            int column = 1;
            for (int i = 0; i < gameEngine.BoardState.GetLength(1); i++)
            {
                gameEngine.BoardState[i, column] = Player.O;
            }
            var (playerWon, columnWon) = gameEngine.CheckColumnsForWinner();
            Assert.That(gameEngine.GameOver, Is.True);
            Assert.That(playerWon, Is.EqualTo(Player.O));
            Assert.That(columnWon, Is.EqualTo(column));
        }

        [Test]
        public void Player_X_Won_In_Main_Diagonal()
        {
            for (int i = 0; i < gameEngine.GridSize; i++)
            {
                gameEngine.BoardState[i, i] = Player.X;
            }

            Player playerWon = gameEngine.CheckMainDiagonalForWinner();
            Assert.That(gameEngine.GameOver, Is.True);
            Assert.That(playerWon, Is.EqualTo(Player.X));
        }

        [Test]
        public void Player_O_Won_In_Secondary_Diagonal()
        {
            int antiDiagonalCounter = gameEngine.GridSize - 1;

            for (int i = 0; i < gameEngine.GridSize; i++)
            {
                gameEngine.BoardState[i, antiDiagonalCounter] = Player.O;
                antiDiagonalCounter--;
            }

            Player playerWon = gameEngine.CheckSecondaryDiagonalForWinner();
            Assert.That(gameEngine.GameOver, Is.True);
            Assert.That(playerWon, Is.EqualTo(Player.O));
        }

        [Test]
        public void Game_Resulted_In_Tie()
        {
            /*
             X O X
             X O O
             O X X
             */

            gameEngine.BoardState[0, 0] = Player.X;
            gameEngine.BoardState[0, 1] = Player.O;
            gameEngine.BoardState[0, 2] = Player.X;
            gameEngine.BoardState[1, 0] = Player.X;
            gameEngine.BoardState[1, 1] = Player.O;
            gameEngine.BoardState[1, 2] = Player.O;
            gameEngine.BoardState[2, 0] = Player.O;
            gameEngine.BoardState[2, 1] = Player.X;
            gameEngine.BoardState[2, 2] = Player.X;

            gameEngine.CheckForTie();
            Assert.That(gameEngine.GameOver, Is.True);
            Assert.That(gameEngine.IsTie, Is.True);
        }
    }
}