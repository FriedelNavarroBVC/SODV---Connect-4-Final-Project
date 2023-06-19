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

}
