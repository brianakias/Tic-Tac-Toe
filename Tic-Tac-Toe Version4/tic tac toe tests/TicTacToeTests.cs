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
        public void GameEngine_WhenInitialized_UpdatesGridSizeProperty()
        {
            Assert.That(gameEngine.GridSize, Is.EqualTo(gridSize));
        }

        [Test]
        public void GameEngine_WhenInitialized_UpdatesBoardStateProperty()
        {
            Assert.That(gameEngine.BoardState, Is.EqualTo(new Player[gameEngine.GridSize, gameEngine.GridSize]));
        }

        [Test]
        public void NewGame_WhenCalled_ItsPlayerXsTurn()
        {
            Player[,] boardState = gameEngine.BoardState;
            Player[,] defaultBoardState = new Player[gridSize, gridSize];

            boardState[0, 0] = Player.O;
            gameEngine.ItsPlayerXsTurn = false;
            gameEngine.GameOver = true;
            gameEngine.IsTie = false;

            gameEngine.NewGame();

            Assert.That(gameEngine.ItsPlayerXsTurn, Is.True);
        }

        [Test]
        public void NewGame_WhenCalled_GameIsNotOver()
        {
            Player[,] boardState = gameEngine.BoardState;
            Player[,] defaultBoardState = new Player[gridSize, gridSize];

            boardState[0, 0] = Player.O;
            gameEngine.ItsPlayerXsTurn = false;
            gameEngine.GameOver = true;
            gameEngine.IsTie = false;

            gameEngine.NewGame();

            Assert.That(gameEngine.GameOver, Is.False);
        }

        [Test]
        public void NewGame_WhenCalled_BoardStateDefaults()
        {
            Player[,] boardState = gameEngine.BoardState;
            Player[,] defaultBoardState = new Player[gridSize, gridSize];

            boardState[0, 0] = Player.O;
            gameEngine.ItsPlayerXsTurn = false;
            gameEngine.GameOver = true;
            gameEngine.IsTie = false;

            gameEngine.NewGame();

            Assert.That(boardState, Is.EqualTo(defaultBoardState));
        }

        [Test]
        public void UpdateBoardState_WhenXIsTheActivePlayer_UpdatesBoardWithX()
        {
            Player[,] boardState = gameEngine.BoardState;
            gameEngine.ItsPlayerXsTurn = true;
            gameEngine.UpdateBoardState(0, 0, gameEngine.ItsPlayerXsTurn);
            Assert.That(boardState[0, 0], Is.EqualTo(Player.X));
        }

        [Test]
        public void UpdateBoardState_WhenOIsTheActivePlayer_UpdatesBoardWithO()
        {
            Player[,] boardState = gameEngine.BoardState;
            gameEngine.ItsPlayerXsTurn = false;
            gameEngine.UpdateBoardState(0, 0, gameEngine.ItsPlayerXsTurn);
            Assert.That(boardState[0, 0], Is.EqualTo(Player.O));
        }

        [Test]
        public void SetNextPlayer_WhenXIsTheActivePlayer_SetsOAsActivePlayer()
        {
            gameEngine.ItsPlayerXsTurn = true;
            gameEngine.SetNextPlayer();
            Assert.That(gameEngine.ItsPlayerXsTurn, Is.False);
        }

        [Test]
        public void SetNextPlayer_WhenOIsTheActivePlayer_SetsXAsActivePlayer()
        {
            gameEngine.ItsPlayerXsTurn = false;
            gameEngine.SetNextPlayer();
            Assert.That(gameEngine.ItsPlayerXsTurn, Is.True);
        }

        [Test]
        public void CheckRowsForWinner_PlayerXWonInFirstRow_GameIsOver()
        {
            int row = 0;
            for (int i = 0; i < gameEngine.BoardState.GetLength(1); i++)
            {
                gameEngine.BoardState[row, i] = Player.X;
            }
            var (playerWon, rowWon) = gameEngine.CheckRowsForWinner();
            Assert.That(gameEngine.GameOver, Is.True);
        }

        [Test]
        public void CheckRowsForWinner_PlayerXWonInFirstRow_PlayerWonIsX()
        {
            int row = 0;
            for (int i = 0; i < gameEngine.BoardState.GetLength(1); i++)
            {
                gameEngine.BoardState[row, i] = Player.X;
            }
            var (playerWon, rowWon) = gameEngine.CheckRowsForWinner();
            Assert.That(playerWon, Is.EqualTo(Player.X));
        }

        [Test]
        public void CheckRowsForWinner_PlayerXWonInFirstRow_ConfirmTheWinningRow()
        {
            int row = 0;
            for (int i = 0; i < gameEngine.BoardState.GetLength(1); i++)
            {
                gameEngine.BoardState[row, i] = Player.X;
            }
            var (playerWon, rowWon) = gameEngine.CheckRowsForWinner();
            Assert.That(rowWon, Is.EqualTo(row));
        }

        [Test]
        public void CheckColumnsForWinner_PlayerOWonInSecondColumn_GameIsOver()
        {
            int column = 1;
            for (int i = 0; i < gameEngine.BoardState.GetLength(1); i++)
            {
                gameEngine.BoardState[i, column] = Player.O;
            }
            var (playerWon, columnWon) = gameEngine.CheckColumnsForWinner();
            Assert.That(gameEngine.GameOver, Is.True);
        }

        [Test]
        public void CheckColumnsForWinner_PlayerOWonInSecondColumn_PlayerWonIsO()
        {
            int column = 1;
            for (int i = 0; i < gameEngine.BoardState.GetLength(1); i++)
            {
                gameEngine.BoardState[i, column] = Player.O;
            }
            var (playerWon, columnWon) = gameEngine.CheckColumnsForWinner();
            Assert.That(playerWon, Is.EqualTo(Player.O));
        }

        [Test]
        public void CheckColumnsForWinner_PlayerOWonInSecondColumn_ConfirmTheWinningColumn()
        {
            int column = 1;
            for (int i = 0; i < gameEngine.BoardState.GetLength(1); i++)
            {
                gameEngine.BoardState[i, column] = Player.O;
            }
            var (playerWon, columnWon) = gameEngine.CheckColumnsForWinner();
            Assert.That(columnWon, Is.EqualTo(column));
        }

        [Test]
        public void CheckMainDiagonalForWinner_PlayerXWonInMainDiagonal_GameIsOver()
        {
            for (int i = 0; i < gameEngine.GridSize; i++)
            {
                gameEngine.BoardState[i, i] = Player.X;
            }

            Player playerWon = gameEngine.CheckMainDiagonalForWinner();
            Assert.That(gameEngine.GameOver, Is.True);
        }

        [Test]
        public void CheckMainDiagonalForWinner_PlayerXWonInMainDiagonal_PlayerWonIsX()
        {
            for (int i = 0; i < gameEngine.GridSize; i++)
            {
                gameEngine.BoardState[i, i] = Player.X;
            }

            Player playerWon = gameEngine.CheckMainDiagonalForWinner();
            Assert.That(playerWon, Is.EqualTo(Player.X));
        }

        [Test]
        public void CheckSecondaryDiagonalForWinner_PlayerOWonInSecondaryDiagonal_GameIsOver()
        {
            int antiDiagonalCounter = gameEngine.GridSize - 1;

            for (int i = 0; i < gameEngine.GridSize; i++)
            {
                gameEngine.BoardState[i, antiDiagonalCounter] = Player.O;
                antiDiagonalCounter--;
            }

            Player playerWon = gameEngine.CheckSecondaryDiagonalForWinner();
            Assert.That(gameEngine.GameOver, Is.True);
        }

        [Test]
        public void CheckSecondaryDiagonalForWinner_PlayerOWonInSecondaryDiagonal_PlayerWonIsO()
        {
            int antiDiagonalCounter = gameEngine.GridSize - 1;

            for (int i = 0; i < gameEngine.GridSize; i++)
            {
                gameEngine.BoardState[i, antiDiagonalCounter] = Player.O;
                antiDiagonalCounter--;
            }

            Player playerWon = gameEngine.CheckSecondaryDiagonalForWinner();
            Assert.That(playerWon, Is.EqualTo(Player.O));
        }

        [Test]
        public void CheckForTie_NoPlayerWon_GameIsOver()
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
        }

        [Test]
        public void CheckForTie_NoPlayerWon_GameResulterInTie()
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
            Assert.That(gameEngine.IsTie, Is.True);
        }

        [Test]
        public void UpdateBoardState_PassingNegativeRow_ThrowsBoardCoordinatesOutOfBoundsException()
        {
            Player[,] boardState = gameEngine.BoardState;
            gameEngine.ItsPlayerXsTurn = true;
            Assert.Throws<BoardCoordinatesOutOfBoundsException>(() => gameEngine.UpdateBoardState(-1, 0, gameEngine.ItsPlayerXsTurn));
        }

        [Test]
        public void UpdateBoardState_PassingNegativeColumn_ThrowsBoardCoordinatesOutOfBoundsException()
        {
            Player[,] boardState = gameEngine.BoardState;
            gameEngine.ItsPlayerXsTurn = false;
            Assert.Throws<BoardCoordinatesOutOfBoundsException>(() => gameEngine.UpdateBoardState(2, -2, gameEngine.ItsPlayerXsTurn));
        }

        [Test]
        public void UpdateBoardState_PassingNegativeRowAndColumn_ThrowsBoardCoordinatesOutOfBoundsException()
        {
            Player[,] boardState = gameEngine.BoardState;
            gameEngine.ItsPlayerXsTurn = true;
            Assert.Throws<BoardCoordinatesOutOfBoundsException>(() => gameEngine.UpdateBoardState(-4, -2, gameEngine.ItsPlayerXsTurn));
        }

        [Test]
        public void GameEngine_PassingGridSizeLessThan3_ThrowsInvalidGridSizeException()
        {
            Assert.Throws<InvalidGridSizeException>(() => new GameEngine(2));
        }

        [Test]
        public void GameEngine_PassingGridSizeGreaterThan12_ThrowsInvalidGridSizeException()
        {
            Assert.Throws<InvalidGridSizeException>(() => new GameEngine(13));
        }

        [Test]
        public void UpdateBoardState_PassingRowGreaterThanTheGridSize_ThrowsBoardCoordinatesOutOfBoundsException()
        {
            Player[,] boardState = gameEngine.BoardState;
            gameEngine.ItsPlayerXsTurn = true;
            Assert.Throws<BoardCoordinatesOutOfBoundsException>(() => gameEngine.UpdateBoardState(10, 0, gameEngine.ItsPlayerXsTurn));
        }

        [Test]
        public void UpdateBoardState_PassingColumnEqualToTheGridSize_ThrowsBoardCoordinatesOutOfBoundsException()
        {
            Player[,] boardState = gameEngine.BoardState;
            gameEngine.ItsPlayerXsTurn = false;
            Assert.Throws<BoardCoordinatesOutOfBoundsException>(() => gameEngine.UpdateBoardState(2, 3, gameEngine.ItsPlayerXsTurn));
        }

        [Test]
        public void UpdateBoardState_PassingRowEqualToTheGridSizeAndAndColumnGreaterThanTheGridSize_ThrowsBoardCoordinatesOutOfBoundsException()
        {
            Player[,] boardState = gameEngine.BoardState;
            gameEngine.ItsPlayerXsTurn = true;
            Assert.Throws<BoardCoordinatesOutOfBoundsException>(() => gameEngine.UpdateBoardState(3, 4, gameEngine.ItsPlayerXsTurn));
        }
    }
}