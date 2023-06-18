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




}
