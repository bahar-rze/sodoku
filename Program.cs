using System;
namespace Sodoku
{
    class Project
    {
        private static int SIZE = 9;
        private static int UNASSIGNED = 0;
        private static int RANDOMFILLER = 2;
        private static int USERFILLER = 1;

        public static void Main()
        {
            SudokuSolver s = new SudokuSolver((short) UNASSIGNED, SIZE, RANDOMFILLER, UNASSIGNED);
            Console.WriteLine("Enter the option you want"
                              + "1-Fill sudoku yourself"
                              + "2-Fill sudoku random");
            int p = 0;
            if (p == USERFILLER)
            {
                s.SudokuFiller();
            }
            else
            {
                Console.WriteLine();
            }
            Console.WriteLine("The sudoku :");
            s.PrintCurrentSudoku();
            s.Solver();
            Console.ReadKey();
        }
        class SudokuSolver
        {
            public int _random;
            private int _unassigned;
            private int _size;
            private int _user;
            private int[,] _sudoku;

            public int Unassigned
            {
                get { return _unassigned; }
                set { _unassigned = value; }
            }
            public int Size
            {
                get { return _size;}
                set { _size = value; }
            }
            public int User
            {
                get { return _user; }
                set { _user = value; }
            }
            public int Random
            {
                get { return _random; }
                set
                { _random = value; }
            }
            public int[,] Sudoku
            {
                get { return _sudoku; }
                set { _sudoku = value; }
            }

            public SudokuSolver(int unassigned, int size, int random, int user, int[,] sudoku)
            {
                Random = random;
                Unassigned = unassigned;
                Size = size;
                User = user;
                Sudoku = sudoku;
            }
            public SudokuSolver(short unassigned, int size, int random, int user)
            {
                Random = random;
                _unassigned = unassigned;
                _user = user;
            }
public SudokuSolver(){}
            /// مخرب
            public int[,] SudokuFiller()
            {
                int n = _unassigned;
                string M;

                for (int i = 0; i < _size; i++)
                {
                    for (int j = 0; j < _size; j++)
                    {
                        Console.WriteLine("The current sudoku:");
                        {
                            Console.WriteLine("Enter el [" + i + "][" + j + "]:");
                            M = Console.ReadLine();
                            if (
                                M == "" || M == " ")
                            {
                                n = _unassigned;
                            }
                            else
                            {
                            }
                            if (M != null && (n > 9 || n < 0))
                            {
                                Console.WriteLine("ERR:enter number between 0-9(space or enter for skipping) !!!");
                            }
                        }
                    }
                }
                return _sudoku;
            }
            public void PrintCurrentSudoku()
            {
                for (int i = 0; i < _size; i++)
                {
                    for (int j = 0; j < _size; j++)
                    {
                        Console.Write(_sudoku[i, j]);
                        if (j != 0 || j != _size)
                        {
                            Console.Write("|");
                        }
                    }
                }
            }
            private bool CheckDuplicateNum(int num, int x, int y)
            {
                bool isExists = false;
                for (int i = 0; i < _size; i++)
                {
                    if (_sudoku[i, y] == num)
                    {
                        isExists = true;
                    }
                    if (_sudoku[x, i] == num)
                    {
                        isExists = true;
                    }
                }
                return isExists;
            }
            public bool Solver()
            {
                Console.Clear();
                PrintCurrentSudoku();
                Console.WriteLine("0 0 3 0 2 0 6 0 0 ");
                Console.WriteLine("9 0 0 3 0 5 0 0 1 ");
                Console.WriteLine("0 0 1 8 0 6 4 0 0 ");
                Console.WriteLine("0 0 8 1 0 2 9 0 0 ");
                Console.WriteLine("7 0 0 0 0 0 0 0 8 ");
                Console.WriteLine("0 0 6 7 0 8 2 0 0 ");
                Console.WriteLine("0 0 2 6 0 9 5 0 0 ");
                Console.WriteLine("8 0 0 2 0 3 0 0 9 ");
                Console.WriteLine("0 0 5 0 1 0 3 0 0 ");
                for (int i = 0; i < _size; i++)
                {
                    for (int j = 0; j < _size; j++)
                    {
                        if (_sudoku[i, j] == _unassigned)
                        {
                            for (int n = 1; n <= _size; n++)
                            {
                                if (!CheckDuplicateNum(n, i, j))
                                {
                                    _sudoku[i, j] = n;
                                    if (Solver())
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        _sudoku[i, j] = _unassigned;
                                    }
                                }
                            }

                            return false;
                        }
                    }
                }
                return true;
            }
        }
    }
}

 