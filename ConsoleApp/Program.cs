using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static int[,] map = new int[9, 9];
        static int tick = 0;

        static void Main(string[] args)
        {
            //for (int i = 0; i < 9; i++)
            //{
            //    for (int j = 0; j < 9; j++)
            //    {
            //        map[i, j] = Console.Read();
            //    }
            //}
            //map = new int[,] { {0,0,0,9,0,0,0,7,0 },
            //                   {0,8,7,0,0,0,3,0,6 },
            //                   {0,5,0,0,0,2,0,1,0 },
            //                   {3,0,0,0,8,0,7,0,0 },
            //                   {0,0,0,2,5,9,0,0,0 },
            //                   {0,0,2,0,3,0,0,0,1 },
            //                   {0,1,0,8,0,0,0,5,0 },
            //                   {6,0,5,0,0,0,8,4,0 },
            //                   {0,3,0,0,0,7,0,0,0 }
            //                 };

            map = new int[,] { {8,0,0,0,0,0,0,0,0 },
                               {0,0,3,6,0,0,0,0,0 },
                               {0,7,0,0,9,0,2,0,0 },
                               {0,5,0,0,0,7,0,0,0 },
                               {0,0,0,0,4,5,7,0,0 },
                               {0,0,0,1,0,0,0,3,0 },
                               {0,0,1,0,0,0,0,6,8 },
                               {0,0,8,5,0,0,0,1,0 },
                               {0,9,0,0,0,0,4,0,0 }
                             };

            backtrace(0);
            Console.Read();
        }


        static bool isplace(int count)//判断行、列、宫是否有重复的数
        {
            int row = count / 9;
            int col = count % 9;
            int j;
            //同一行
            for (j = 0; j < 9; j++)
            {
                if (map[row, j] == map[row, col] && j != col)
                {
                    return false;
                }
            }
            //同一列
            for (j = 0; j < 9; j++)
            {
                if (map[j, col] == map[row, col] && j != row)
                    return false;
            }
            //同一个宫
            int temprow = row / 3 * 3;
            int tempcol = col / 3 * 3;
            for (j = temprow; j < temprow + 3; j++)
            {
                for (int k = tempcol; k < tempcol + 3; k++)
                {
                    if (map[j, k] == map[row, col] && j != row && k != col)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static void backtrace(int count)
        {
            tick++;
            if (count == 81)
            {
                Console.WriteLine("result : hits - "+ tick);
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        Console.Write(map[i, j] + " ");
                    }
                    Console.Write(Environment.NewLine);
                }
                return;
            }

            int row = count / 9;
            int col = count % 9;
            if (map[row, col] == 0)
            {
                for (int i = 1; i <= 9; i++)
                {
                    map[row, col] = i;//赋值
                    if (isplace(count))
                    {
                        backtrace(count + 1);//放下一个
                    }
                }
                map[row, col] = 0;//回溯
            }
            else
            {
                backtrace(count + 1);//不是空格，下一个数
            }
        }

    }
}
