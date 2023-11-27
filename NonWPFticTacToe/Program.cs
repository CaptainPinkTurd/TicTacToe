using System;
using System.Diagnostics;
using System.Linq;

namespace TicTacToe
{
    class program
    {
        static void Main()
        {
            string input;
            int size, turn = 0;
            do
            {
                Console.Write("Choose board sizes from 3 to 25: ");
                input = Console.ReadLine();
                Console.Clear();

            } while (!int.TryParse(input, out size) || (size < 3 || size > 25));

            TicTacToe game = new TicTacToe(size);
            Stack<int> madeChoice = new Stack<int>();
            Stack<int> p1LastStates = new Stack<int>();   
            Stack<int> p2LastStates = new Stack<int>();

            bool p1Turn = true;
            do
            {
                turn++;
                string Input;
                int choice;
                if (p1Turn)
                {
                    do
                    { 
                        game.drawBoard(game.board);
                        Console.Write("Player 1 turn: ");
                        Input = Console.ReadLine();
                        Console.Clear();
                    } while (!int.TryParse(Input, out choice) || (choice < 0 || choice > size*size) || madeChoice.Contains(choice));
                    if(choice == 0)
                    {
                        game.undoBoard(p1LastStates.Pop(), p2LastStates.Pop());
                        madeChoice.Pop();
                        madeChoice.Pop();
                        if (madeChoice.Count < 1) madeChoice.Push(-1); //a subsitute to satisfy the while condition below
                        continue;
                    }
                    else
                    {
                        p1LastStates.Push(choice);
                        madeChoice.Push(choice);
                        game.boardUpdate(game.board, choice.ToString(), p1Turn);
                        p1Turn = false;
                    }                                  
                }
                else
                {                   
                    do
                    {
                        game.drawBoard(game.board);
                        Console.Write("Player 2 turn: ");
                        Input = Console.ReadLine();
                        Console.Clear();
                    } while (!int.TryParse(Input, out choice) || (choice < 0 || choice > size * size) || madeChoice.Contains(choice));
                    if (choice == 0)
                    {
                        game.undoBoard(p1LastStates.Pop(), p2LastStates.Pop());
                        madeChoice.Pop();
                        madeChoice.Pop();
                        if (madeChoice.Count < 1) madeChoice.Push(-1);
                        continue;
                    }
                    else
                    {
                        p2LastStates.Push(choice);
                        madeChoice.Push(choice);
                        game.boardUpdate(game.board, choice.ToString(), p1Turn);
                        p1Turn = true;
                    }
                }
            } while (!game.Checker(game.board, p1Turn, madeChoice.Peek() - 1) && turn < size * size);
            if(turn >= size * size)
            {
                game.drawBoard(game.board);
                Console.WriteLine("This match is a draw");
            }
            else
            {
                if (p1Turn)
                {
                    game.drawBoard(game.board);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Player 2 win !!!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    game.drawBoard(game.board);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Player 1 win !!!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }            
        }
    }
}