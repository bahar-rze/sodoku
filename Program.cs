using System;
namespace sodoko
{
    class Project
    {
        private static int SIZE = 9; 
        private static int UNASSIGNED = 0; 
        private static int RANDOMFILLER = 2;
        private static int USERFILLER = 1; 

        public static void Main()
        {
            int[,] sudoku = new int[SIZE, SIZE];
            SudokuSolver S = new SudokuSolver((short) UNASSIGNED, SIZE, RANDOMFILLER, UNASSIGNED); 
            int p = 0;
            Console.WriteLine("Enter the option you want"
                              + "1-Fill sudoku yourself"
                              + "2-Fill sudoku random");
            p = Convert.ToInt16(Console.ReadLine());
            if (p == USERFILLER)
            {
                S.sudokuFiller();
            }
            else {
                int difficulty = 0;
                Console.WriteLine("Enter Difficulty: (between 2-4 for quick result)");
                difficulty = Convert.ToInt32(Console.ReadLine());
                S.fillRandSudoku(difficulty);
            }
            Console.WriteLine("The sudoku :");
            S.printCurrentSudoku();
            S.solver();
        }
        class SudokuSolver
        {
            private int unassigned;
            private int size;
            private int randomfiller;
            private int userfiller;
            private double speed;
            private int[,] sudoku;

            public int Unassigned
            {
                get { return unassigned; }
                set { unassigned = value; }
            }

            public int Size
            {
                get { return size; }
                set { size = value; }
            }

            public int Randomfiller
            {
                get { return randomfiller; }
                set { randomfiller = value; }
            }

            public int Userfiller
            {
                get { return userfiller; }
                set { userfiller = value; }
            }

            public int[,] Sudoku
            {
                get { return sudoku; }
                set { sudoku = value; }
            }

            public SudokuSolver(int unassigned, int size, int randomfiller, int userfiller, int[,] sudoku)
            {
                ///تابع سازنده
                Unassigned = unassigned;
                Size = size;
                Randomfiller = randomfiller;
                Userfiller = userfiller;
                Sudoku = sudoku;
            }

            public SudokuSolver()
            {
                ///تایع مخرب
            }

            public SudokuSolver(short s, int i, int randomfiller1, int unassigned1)
            {
                int p = 0;
            }

            public int[,] sudokuFiller()
            {
                int userInpInt = unassigned;
                string userInpStr;
                bool isExists = false;

                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        Console.WriteLine("The current sudoku:");
                        printCurrentSudoku();
                        {
                            Console.WriteLine("Enter el [" + i + "][" + j + "]:");
                            userInpStr = Console.ReadLine();
                            if (
                                userInpStr == "" || userInpStr == " " ||
                                userInpStr == null)
                            {
                                userInpInt = unassigned; 
                            }
                            else
                            {
                                userInpInt = Convert.ToInt16(userInpStr);
                            }

                            if (userInpInt > 9 || userInpInt < 0)
                            {
                                Console.WriteLine("ERR:enter number between 0-9(space or enter for skipping) !!!");
                            }

                            isExists = checkDuplicateNum(userInpInt, i, j);
                            if (isExists && userInpInt != unassigned)
                            {
                                Console.WriteLine("dont repeat two num in row or col تو سطر یا ستون یه عدد یکسان هست");
                            }
                            else
                            {
                                isExists = false;
                            }
                        }
                    }
                }

                return sudoku;
            }
            public void printCurrentSudoku()
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        Console.Write(sudoku[i, j]);
                        if (j != 0 || j != size)
                        {
                            Console.Write("|");
                        }
                    }

                    Console.WriteLine("0 0 3 0 2 0 6 0 0 ");
                    Console.WriteLine("9 0 0 3 0 5 0 0 1 ");
                    Console.WriteLine("0 0 1 8 0 6 4 0 0 ");
                    Console.WriteLine("0 0 8 1 0 2 9 0 0 ");
                    Console.WriteLine("7 0 0 0 0 0 0 0 8 ");
                    Console.WriteLine("0 0 6 7 0 8 2 0 0 ");
                    Console.WriteLine("0 0 2 6 0 9 5 0 0 ");
                    Console.WriteLine("8 0 0 2 0 3 0 0 9 ");
                    Console.WriteLine("0 0 5 0 1 0 3 0 0 ");
                }
            }
            private bool checkDuplicateNum(int num, int x, int y)
            {
                bool isExists = false;
                for (int i = 0; i < size; i++)
                { if (sudoku[i, y] == num)
                    {
                        isExists = true;
                    }
                    if (sudoku[x, i] == num)
                    {
                        isExists = true;
                    }
                }

                return isExists;
            }
            private bool isFullSudoku()
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (sudoku[i, j] == unassigned)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
            public void fillRandSudoku(int difficulty)
            {
                int[] rands = new int[difficulty];
                Random r = new Random();
                Random r2 = new Random();
                for (int i = 0; i < size; i++)
                {
                    //? Get random cell PLACE in one arr
                    for (int k = 0; k < difficulty; k++)
                    {
                        rands[k] = r.Next(0, size);
                    }

                    for (int j = 0; j < size; j++)
                    {
                        if (((IList) rands).Contains(j))
                        {
                            for (int z = 0; z < size; z++)
                            {
                                if (!checkDuplicateNum(z, i, j))
                                {
                                    sudoku[i, j] = z;
                                }
                            }
                        }
                    }
                }
            }
            public bool solver()
            {
                Console.Clear();
                printCurrentSudoku();
                System.Threading.Thread.Sleep(
                    (int) System.TimeSpan.FromSeconds(speed).TotalMilliseconds);
                Console.WriteLine("----------------------");
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (sudoku[i, j] == unassigned)
                        {
                            for (int n = 1; n <= size; n++)
                            {
                                if (!checkDuplicateNum(n, i, j))
                                {
                                    sudoku[i, j] = n;
                                    if (solver())
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        sudoku[i, j] = unassigned;
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
 