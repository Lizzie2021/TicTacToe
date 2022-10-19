// See https://aka.ms/new-console-template for more information
using System;

class TicTacToe
{
    static void Main()
    {
        int[] playerPositions = { 0, 0 };
        int currentPlayer = 1;
        int winner = 0;
        string? input = null;
        
        for(int turn = 1; turn <= 10; ++turn)
        {
            DisplayBoard(playerPositions);

            if(EndGame(winner, turn, input!))
            {
                break;
            }

            input = NextMove(playerPositions, currentPlayer);

            winner = DetermineWinner(playerPositions);

            currentPlayer = (currentPlayer == 2) ? 1 : 2;
        }
         
    }

    static void DisplayBoard(int[] playerPositions)
    {
        string[] borders = {" |", " |", "\n---+---+---\n", " |", " |","\n---+---+---\n", " |", " |", ""};

        int border = 0;

        Console.Clear();

        for(int position = 1; position <= 256; position <<= 1, border++)
        {
            char token = CalculateToken(playerPositions, position);
            Console.Write($"{token} {borders[border]}");
        }
    } 

    static char CalculateToken(int[] playerPositions, int position)
    {
        char[] players ={ 'X', 'O'};
        char token;

        if((position & playerPositions[0]) == position)
        {
            token = players[0];
        }
        else if((position & playerPositions[1]) == position)
        {
            token = players[1];
        }
        else
        {
            token = ' ';
        }
        return token;
    }

    static string NextMove(int[] playerPositions, int currentPlayer)
    {
        string? input;
        bool validMove;
        do
        {
            Console.Write($"\nPlayer {currentPlayer} - Enter move:");
            input = Console.ReadLine();
            validMove = ValidateAndMove(playerPositions,currentPlayer,input!);
        }while(!validMove);

        return input!;
    }

    static bool ValidateAndMove(int[] playerPositions, int currentPlayer, string input)
    {
        bool valid = false;

        switch(input)
        {
            case "1":
            case "2":
            case "3":
            case "4":
            case "5":
            case "6":
            case "7":
            case "8":
            case "9":
                int shifter;
                int position;

                shifter = int.Parse(input) - 1;
                position = 1 << shifter;

                playerPositions[currentPlayer - 1] |= position;

                valid = true;
                break;

            case "":
            case "quit":
                valid = true;
                break;
            
            default:
                Console.WriteLine("\nERROR: Enter a value from 1-9." + "Push Enter to quit");
                break;

        }

        return valid;
    }

    static bool EndGame(int winner, int turn, string input)
    {
        bool endGame = false;
        if(winner > 0)
        {
            Console.WriteLine($"\nPlayer {winner} has won!!!");
            endGame = true;
        }
        else if(turn == 10)
        {
            Console.WriteLine("\nThe game was a tie!");
            endGame = true;
        }
        else if(input == "" || input =="quit")
        {
            Console.WriteLine("\nThe last player quit.");
            endGame = true;
        }
        
        return endGame;
    }

    static int DetermineWinner(int[] playerPositions)
    {
        int winner = 0;

        int[] winningMasks = {7,56,448,73,146,292,84,273};

        foreach(int mask in winningMasks)
        {
            if((mask & playerPositions[0]) == mask)
            {
                winner = 1;
                break;
            }
            else if((mask & playerPositions[1]) == mask)
            {
                winner = 2;
                break;
            }
        }

        return winner;
    }
}
