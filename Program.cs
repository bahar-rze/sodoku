using System;

namespace sodoko
{
    class Project
    {
        private static int SIZE = 9; //? size of sudoku row & col سایز جدول سودوکو
        private static int UNASSIGNED = 0; //? نمایشگر خانه خالی سودوکو
        private static int RANDOMFILLER = 2; //? عدد ورودی متناسب با پر کردن رندوم
        private static int USERFILLER = 1; //? عدد ورودی متناسب با پر کردن توسط کاربر

        public static void Main()
        {
            int[,] sudoku = new int[SIZE, SIZE];
            SudokuSolver S = new SudokuSolver((short) UNASSIGNED, SIZE, RANDOMFILLER, UNASSIGNED);
            //? انتخاب پر کردن سودوکو با کاربر یا پر کردن رندوم توسط برنامه
            int p = 0;
            Console.WriteLine("Enter the option you want"
                              + "1-Fill sudoku yourself"
                              + "2-Fill sudoku random");
            p = Convert.ToInt16(Console.ReadLine());
            if (p == USERFILLER)
            {
                S.sudokuFiller();
            }
            else
            {
//? گرفتن میزان سختی سودوکو که همان تعداد خانه های پر شده را در یک ردیف مشخص میکند
                int difficulty = 0;
                Console.WriteLine("Enter Difficulty: (between 2-4 for quick result)");
                difficulty = Convert.ToInt32(Console.ReadLine());
                S.fillRandSudoku(difficulty);
            }

//? چاپ سودوکو اولیه
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
                                userInpInt = unassigned; //? 0 یعنی اون خونه تو سودوکو خالیه
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

            /// <summary>چاپ سودوکو فعلی</summary>
            /// <param name="sudoku">سودوکوای که باید چاپ بشه</param>
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

            /// <summary>چک کردن قوانین تکرار تو سودوکو</summary>
            /// <param name="num">عددی که قراره وارد بشه</param>
            /// <param name="sudoku">جدول سودوکو</param>
            /// <param name="x">تو چه سطری قراره وارد بشه</param>
            /// <param name="y">تو چه ستونی قراره وارد بشه</param>
            private bool checkDuplicateNum(int num, int x, int y)
            {
                bool isExists = false;
                for (int i = 0; i < size; i++)
                {
                    // Console.WriteLine(sudoku[i, y]);
                    if (sudoku[i, y] == num)
                    {
                        //? checking for num exists in row (چک کردن در ستون)
                        isExists = true;
                    }

                    // Console.WriteLine(sudoku[x, i]);
                    if (sudoku[x, i] == num)
                    {
                        //? checking for num exists in col‌ (چک کردن در سطر)
                        isExists = true;
                    }
                }

                return isExists;
            }

            /// <summary>چک کردن پر بودن سودوکو</summary>
            /// <param name="sudoku">جدول سودوکو</param>
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

            /// <summary>پر کردن رندوم جدول سودوکو</summary>
            /// <param name="sudoku">جدول سودوکو</param>
            /// <param name="difficulty">تعدادی که باید پر بشه تو هر  خونه</param>
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

            /// <summary>حل سودوکو به صورت بازگشتی</summary>
            /// <param name="sudoku">پوینتر به سودوکو برای تغییر کردن مقدار پارامتر پاس داده شده</param>
            public bool solver()
            {
                //? چاپ هر مرحله از حل سودوکو
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
                                //? اگر عددی بین صفر تا نه هست که قوانین تکرارو نقض نکنه اون رو قرار میدیم
                                if (!checkDuplicateNum(n, i, j))
                                {
                                    sudoku[i, j] = n;
                                    //? اگر به پابان رسیده حل سودوکو از متود بیا بیرون
                                    if (solver())
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        //? در غیر اینصورت یعنی تمامی اعداد بین صفر تا نه مرحله بعدی ممکن نبوده تو اون خانه قرار بگیره پس حانه قبلی رو خالی میکنیم
                                        sudoku[i, j] = unassigned;
                                    }
                                }
                            }

                            //? تمامی  اعداد ممکن نمیتوانند قرار بگیرند پس برمیگردیم مرحله قبل تا یه عدد دیگه جایگزین کنیم
                            return false;
                        }
                    }
                }

                //? حلقه اصلی تمام شد پس از متود بیا بیرون
                return true;
            }
        }
    }
}
 