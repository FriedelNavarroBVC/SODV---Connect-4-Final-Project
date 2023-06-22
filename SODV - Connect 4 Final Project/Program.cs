using System;

namespace SODV___Connect_4_Final_Project
{
        interface IPlayer
    {
        char Symbol { get; } // Represents the player's symbol (X or O)
        string Name { get; } // Represents the player's name
        int GetMove(Board board); // Get the player's move
    }

    class HumanPlayer : IPlayer
    {
        public char Symbol { get; }
        public string Name { get; }

        public HumanPlayer(char symbol, string name)
        {
            Symbol = symbol;
            Name = name;
        }

        public int GetMove(Board board)
        {
            int column = 0;
            bool isValidMove = false;

            while (!isValidMove)
            {
                string input = Console.ReadLine();
                isValidMove = int.TryParse(input, out column) && board.IsColumnValid(column);

                if (!isValidMove)
                {
                    Console.WriteLine("Invalid input. Please select a valid column (1-7).");
                }
            }

            return column;
        }
    }

    class ComputerPlayer : IPlayer
    {
        public char Symbol { get; }
        public string Name { get; }

        public ComputerPlayer(char symbol, string name)
        {
            Symbol = symbol;
            Name = name;
        }

        public int GetMove(Board board)
        {
            Random random = new Random();
            int column;

            do
            {
                column = random.Next(1, 8);
            } while (!board.IsColumnValid(column));

            return column;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }

    class Game
    {
        private Board board; // Represents the game board
        private IPlayer player1; // Represents player 1
        private IPlayer player2; // Represents player 2 or the computer
        private IPlayer currentPlayer; // Represents the current player
        private bool isGameFinished; // Indicates if the game is finished

        public Game()
        {
            board = new Board();
            player1 = new HumanPlayer('X', "Player 1");
            player2 = new HumanPlayer('O', "Player 2");
            currentPlayer = player1;
            isGameFinished = false;
        }

        public void Start()
        {
            Console.WriteLine("Welcome to Connect Four!");
            Console.WriteLine("Please select a mode:");
            Console.WriteLine("1. 2-Player Mode");
            Console.WriteLine("2. 1-Player Mode (Player vs Computer)");
            Console.WriteLine("3. Exit");

            char mode = Console.ReadKey().KeyChar; // Read the selected mode from the user
            Console.WriteLine();

            switch (mode)
            {
                case '1':
                    TwoPlayerMode(); // Start the 2-player mode
                    break;
                case '2':
                    SinglePlayerMode(); // Start the 1-player mode
                    break;
                case '3':
                    Environment.Exit(0); // Exit the application
                    break;
                default:
                    Console.WriteLine("Invalid input. Please try again.\n");
                    Start(); // Prompt the user to select a valid mode
                    break;
            }
        }

        private void TwoPlayerMode()
        {
            board.Initialize(); // Initialize the game board
            currentPlayer = player1; // Set the current player to player 1
            isGameFinished = false; // Reset the game finished flag

            while (!isGameFinished) // Continue the game until it is finished
            {
                Console.Clear(); // Clear the console
                board.Draw(); // Draw the game board

                Console.WriteLine($"{currentPlayer.Name}'s turn ({currentPlayer.Symbol})");
                int move = currentPlayer.GetMove(board); // Get the current player's move

                if (board.MakeMove(move, currentPlayer.Symbol)) // Make the move and check if it was successful
                {
                    if (board.IsWinningMove(move, currentPlayer.Symbol)) // Check if the move resulted in a win
                    {
                        Console.Clear();
                        board.Draw();
                        Console.WriteLine($"{currentPlayer.Name} wins!");
                        isGameFinished = true;
                    }
                    else if (board.IsBoardFull()) // Check if the board is full (draw)
                    {
                        Console.Clear();
                        board.Draw();
                        Console.WriteLine("It's a draw!");
                        isGameFinished = true;
                    }
                    else
                    {
                        currentPlayer = (currentPlayer == player1) ? player2 : player1; // Switch to the next player
                    }
                }
                else
                {
                    Console.WriteLine("Invalid move. Please try again.");
                }
            }

            Start(); // Restart the game
        }

        private void SinglePlayerMode()
        {
            board.Initialize(); // Initialize the game board
            player1 = new HumanPlayer('X', "Player 1"); // Set player 1 as a human player
            player2 = new ComputerPlayer('O', "AI"); // Set player 2 as a computer player
            currentPlayer = player1; // Set the current player to player 1
            isGameFinished = false; // Reset the game finished flag

            while (!isGameFinished) // Continue the game until it is finished
            {
                Console.Clear(); // Clear the console
                board.Draw(); // Draw the game board

                Console.WriteLine($"{currentPlayer.Name}'s turn ({currentPlayer.Symbol})");
                int move = currentPlayer.GetMove(board); // Get the current player's move

                if (board.MakeMove(move, currentPlayer.Symbol)) // Make the move and check if it was successful
                {
                    if (board.IsWinningMove(move, currentPlayer.Symbol)) // Check if the move resulted in a win
                    {
                        Console.Clear();
                        board.Draw();
                        Console.WriteLine($"{currentPlayer.Name} wins!");
                        isGameFinished = true;
                    }
                    else if (board.IsBoardFull()) // Check if the board is full (draw)
                    {
                        Console.Clear();
                        board.Draw();
                        Console.WriteLine("It's a draw!");
                        isGameFinished = true;
                    }
                    else
                    {
                        currentPlayer = (currentPlayer == player1) ? player2 : player1; // Switch to the next player
                    }
                }
                else
                {
                    Console.WriteLine("Invalid move. Please try again.");
                }
            }

            Start(); // Restart the game
        }

    }

    class Board
    {
        private char[,] grid; // Represents the game grid
        private const int Rows = 6; // Number of rows in the grid
        private const int Columns = 7; // Number of columns in the grid

        public Board()
        {
            grid = new char[Rows, Columns];
        }

        public void Initialize()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    grid[row, col] = ' ';
                }
            }
        }

        public void Draw()
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine("   *** CONNECT FOUR GAME ***");
            Console.WriteLine("-------------------------------");
            for (int row = Rows - 1; row >= 0; row--)
            {
                Console.Write(" | ");
                for (int col = 0; col < Columns; col++)
                {
                    Console.Write($"{grid[row, col]} | ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("-------------------------------");
            Console.WriteLine("   1   2   3   4   5   6   7");
            Console.WriteLine("-------------------------------\n");

        }

        public bool IsColumnValid(int column)
        {
            return column >= 1 && column <= Columns && grid[Rows - 1, column - 1] == ' ';
        }

        public bool MakeMove(int column, char symbol)
        {
            if (!IsColumnValid(column))
            {
                return false;
            }

            for (int row = 0; row < Rows; row++)
            {
                if (grid[row, column - 1] == ' ')
                {
                    grid[row, column - 1] = symbol;
                    return true;
                }
            }

            return false;
        }

        public bool IsWinningMove(int column, char symbol)
        {
            int row = 0;

            while (row < Rows && grid[row, column - 1] != symbol)
            {
                row++;
            }

            if (row >= Rows)
            {
                return false;
            }

            // Check for vertical win
            if (row <= Rows - 4 && grid[row + 1, column - 1] == symbol && grid[row + 2, column - 1] == symbol && grid[row + 3, column - 1] == symbol)
            {
                return true;
            }

            // Check for horizontal win
            int count = 1;
            int left = column - 2;
            int right = column;

            while (left >= 0 && grid[row, left] == symbol)
            {
                count++;
                left--;
            }

            while (right < Columns && grid[row, right] == symbol)
            {
                count++;
                right++;
            }

            if (count >= 4)
            {
                return true;
            }

            // Check for diagonal win (top-left to bottom-right)
            count = 1;
            int diagLeft = column - 2;
            int diagRight = column;

            while (row + count < Rows && diagLeft >= 0 && grid[row + count, diagLeft] == symbol)
            {
                count++;
                diagLeft--;
            }

            while (row - count >= 0 && diagRight < Columns && grid[row - count, diagRight] == symbol)
            {
                count++;
                diagRight++;
            }

            if (count >= 4)
            {
                return true;
            }

            // Check for diagonal win (top-right to bottom-left)
            count = 1;
            diagLeft = column - 2;
            diagRight = column;

            while (row - count >= 0 && diagLeft >= 0 && grid[row - count, diagLeft] == symbol)
            {
                count++;
                diagLeft--;
            }

            while (row + count < Rows && diagRight < Columns && grid[row + count, diagRight] == symbol)
            {
                count++;
                diagRight++;
            }

            return count >= 4;
        }

        public bool IsBoardFull()
        {
            for (int col = 0; col < Columns; col++)
            {
                if (grid[Rows - 1, col] == ' ')
                {
                    return false;
                }
            }
            return true;
        }
    }

}
