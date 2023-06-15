using System;

namespace SODV___Connect_4_Final_Project
{
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
        private Player player1; // Represents player 1
        private Player player2; // Represents player 2 or the computer
        private Player currentPlayer; // Represents the current player
        private bool isGameFinished; // Indicates if the game is finished

        public Game()
        {
            board = new Board();
            player1 = new Player('X', "Player 1");
            player2 = new Player('O', "Player 2");
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
    }

    class Board
    {
        private char[,] grid; // Represents the game board grid

        public Board()
        {
            grid = new char[6, 7]; // Initialize the game board grid with a size of 6x7
        }
    }

    class Player
    {
        public char Symbol { get; } // Represents the player's symbol (X or O)
        public string Name { get; } // Represents the player's name

        public Player(char symbol, string name)
        {
            Symbol = symbol;
            Name = name;
        }
    }
}
