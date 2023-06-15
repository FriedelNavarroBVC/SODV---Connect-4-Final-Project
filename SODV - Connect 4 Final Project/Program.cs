using System;

namespace SODV___Connect_4_Final_Project
{
    internal class Program
    {
        static void (Main(string[] args)
            {
            Game game = new Game();
        game.Start();

        }
    }

class Game

{
    private Board board;
    private Player player1;
    private Player player2;
    private Player currentPlayer;
    private bool isGamefinished;

    public Game()
    {
        board = new Board();
        player1 = new Player('X', "Player 1");
        Player2 = new Player('O', "Computer");
        currentPlayerPlayer = player1;
        isGamefinished = false;
    }

}

class Player
{
    public char Symbol { get; }
    public string Name { get; }

    public Player (char symbol, string name)
    {
        Symbol= symbol;
        Name= name;
    }
}
