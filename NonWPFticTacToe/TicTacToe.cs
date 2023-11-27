namespace TicTacToe
{
    public class TicTacToe
    {
        private int n;

        public string[,] board;
        public TicTacToe(int size)
        {
            n = size;  
            board = new string[n, n];
            int field = 1;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    board[i, j] = field.ToString();
                    field++;
                }
            }
        }

        public void undoBoard(int p1LastMove, int p2LastMove)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int position = i * n + j;
                    if (position == p1LastMove - 1) board[i, j] = p1LastMove.ToString();
                    if (position == p2LastMove - 1) board[i, j] = p2LastMove.ToString();
                }
            }
        }
        public void boardUpdate(string[,] board, string updateValue, bool p1Turn)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (board[i, j] == updateValue && p1Turn)
                    {
                        board[i, j] = "O";
                    } 
                    else if(board[i, j] == updateValue && !p1Turn)
                    {
                        board[i, j] = "X";
                    }
                }
            }
        }
        public void drawBoard(string[,] board)
        {
            int I = 0;
            
            int verticalLimit = 2;
            int horizontalLimit = 1;
            string grid = "|";
            string horizontalPad = "_";

            string modifiedHorizontal = grid.PadLeft(6, '_');
            string modifiedVertical = grid.PadLeft(6, ' ');
            string addNum = board[0,0].PadLeft(3, ' ');
            for (int i = 0; i < n * 3; i++)
            {
                if ((i + 1) % 3 == 0 && horizontalLimit < n)
                {
                    for (int j = 0; j < n - 1; j++)
                    {
                        Console.Write(modifiedHorizontal);
                    }
                    Console.WriteLine(horizontalPad.PadLeft(5, '_'));
                    horizontalLimit++; //to prevent drawing the horizontal line abundantly
                }
                else
                {
                    if (verticalLimit == 3)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            addNum = board[I, j].PadLeft(3, ' ');
                            if (j == n - 1)
                            {
                                if(board[I, j] == "O")
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write(addNum);
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                }
                                else if(board[I,j] == "X")
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write(addNum);
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                }
                                else Console.Write(addNum);

                            }
                            else
                            {
                                if (board[I, j] == "O")
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write(addNum);
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    Console.Write(grid.PadLeft(3, ' '));
                                }
                                else if (board[I, j] == "X")
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write(addNum);                                    
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    Console.Write(grid.PadLeft(3, ' '));
                                }
                                else Console.Write(addNum + grid.PadLeft(3, ' '));
                            }
                        }
                        I++;
                        verticalLimit = 2;
                    }
                    else
                    {
                        for (int j = 0; j < n - 1; j++)
                        {
                            Console.Write(modifiedVertical);
                        }
                        verticalLimit++;
                    }
                    Console.WriteLine();
                }
            }
        }
        public bool Checker(string[,] board, bool p2Turn, int currentPos)
        {
            int diagI1 = 0, diagJ1 = 0, diagI2 = 0, diagJ2 = 0;
            bool hasBroke = false;
            for(int i = 0; i < board.GetLength(0); i++)
            {
                for(int j = 0; j < board.GetLength(0); j++)
                {
                    if(i * board.GetLength(0) + j == currentPos)
                    {
                        diagI1 = i;
                        diagJ1 = j;
                        diagI2 = i;
                        diagJ2 = j;
                        hasBroke = true;
                        break;
                    }
                }
                if (hasBroke)
                {
                    while(diagJ1 > 0 && diagI1 > 0)
                    {
                        diagI1--;
                        diagJ1--;
                    }
                    while(diagJ2 < board.GetLength(0) - 1 && diagI2 > 0)
                    {
                        diagI2--;
                        diagJ2++;   
                    }
                    break;
                }
            }
            string mark = p2Turn == true ? "X" : "O";
            int count = 0, countCon = board.GetLength(0) > 8 ? 5 : board.GetLength(0) == 3 ? 3 : 4;
            // Diagonal
            while(diagI1 < board.GetLength(0) && diagJ1 < board.GetLength(0))
            {
                if (board[diagI1, diagJ1] == mark)
                {
                    count++;
                    diagI1++;
                    diagJ1++;
                } 
                else
                {
                    count = 0;
                    diagI1++;
                    diagJ1++;
                }
                if (count >= countCon) return true;
            }
            count = 0;

            while (diagI2 < board.GetLength(0) && diagJ2 >= 0)
            {
                if (board[diagI2, diagJ2] == mark)
                {
                    count++;
                    diagI2++;
                    diagJ2--;
                }
                else
                {
                    count = 0;
                    diagI2++;
                    diagJ2--;
                }
                if (count >= countCon) return true;
            }

            //horizontal
            for (int j = 0; j < board.GetLength(1); j++)
            {
                count = 0;
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    if (board[i, j] == mark) count++;

                    else count = 0;

                    if (count >= countCon) return true;
                }
            }

            //vertical
            for (int i = 0; i < board.GetLength(0); i++)
            {
                count = 0;
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == mark) count++;

                    else count = 0;

                    if (count >= countCon) return true;
                }
            }
            return false;
        }
    }
}