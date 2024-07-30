using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, World!");

        string[,] board = {
            { " ", " ", " " },
            { " ", " ", " " },
            { " ", " ", " " }
        };

        int[,,] winningSquares = {
            { {0, 0}, {0, 1}, {0, 2} },
            { {1, 0}, {1, 1}, {1, 2} },
            { {2, 0}, {2, 1}, {2, 2} },
            { {0, 0}, {1, 0}, {2, 0} },
            { {0, 1}, {1, 1}, {2, 1} },
            { {0, 2}, {1, 2}, {2, 2} },
            { {0, 0}, {1, 1}, {2, 2} },
            { {2, 0}, {1, 1}, {0, 2} }
        };

        bool finished = false;
        Random random = new Random();
        bool playerTurn = true; // True if it's the player's turn, false if it's the computer's turn

        while (!finished)
        {
            PrintBoard(board);

            if (playerTurn)
            {
                Console.WriteLine("Enter your move (row and column): ");

                int row, col;
                while (true)
                {
                    Console.Write("Row: ");
                    string? rowInput = Console.ReadLine();
                    Console.Write("Column: ");
                    string? colInput = Console.ReadLine();

                    if (int.TryParse(rowInput, out row) && int.TryParse(colInput, out col) && row >= 0 && row < 3 && col >= 0 && col < 3)
                    {
                        if (board[row, col] == " ")
                        {
                            board[row, col] = "X";
                            playerTurn = false;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("This spot is already taken. Try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter numbers between 0 and 2.");
                    }
                }
            }
            else
            {
                bool turnFinished = false;
                while (!turnFinished)
                {
                    int x = random.Next(0, 3);
                    int y = random.Next(0, 3);
                    if (board[x, y] == " ")
                    {
                        board[x, y] = "O";
                        turnFinished = true;
                        playerTurn = true;
                    }
                }
            }

            string winner = CheckWinner(board, winningSquares);
            if (winner != " ")
            {
                PrintBoard(board);
                Console.WriteLine("Winner is: " + winner);
                finished = true;
            }
            else if (IsBoardFull(board))
            {
                PrintBoard(board);
                Console.WriteLine("It's a draw!");
                finished = true;
            }
        }
    }

    static void PrintBoard(string[,] board)
    {
        Console.WriteLine("  0 1 2");
        for (int i = 0; i < 3; i++)
        {
            Console.Write(i + " ");
            for (int j = 0; j < 3; j++)
            {
                Console.Write(board[i, j]);
                if (j < 2) Console.Write("|");
            }
            Console.WriteLine();
            if (i < 2) Console.WriteLine("  -----");
        }
    }

    static string CheckWinner(string[,] board, int[,,] winningSquares)
    {
        for (int i = 0; i < winningSquares.GetLength(0); i++)
        {
            string first = board[winningSquares[i, 0, 0], winningSquares[i, 0, 1]];
            string second = board[winningSquares[i, 1, 0], winningSquares[i, 1, 1]];
            string third = board[winningSquares[i, 2, 0], winningSquares[i, 2, 1]];
            if (first != " " && first == second && second == third)
            {
                return first;
            }
        }
        return " ";
    }

    static bool IsBoardFull(string[,] board)
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] == " ")
                {
                    return false;
                }
            }
        }
        return true;
    }
}
