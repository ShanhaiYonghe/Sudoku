using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Demonstrate AvailibleList
            //int n = 9;
            //int index = 0;
            //CreateClosingStage ccs = new CreateClosingStage(n);


            //foreach (var item in ccs._rowAvailibleList)
            //{
            //    index = 0;
            //    foreach (var subItem in item)
            //    {
            //        if (index == 0)
            //            Console.WriteLine();
            //        index++;

            //        Console.Write("Row: " + subItem.ToString());
            //    }
            //}

            //foreach (var item in ccs._colAvailibleList)
            //{
            //    index = 0;
            //    foreach (var subItem in item)
            //    {
            //        if (index == 0)
            //            Console.WriteLine();
            //        index++;
            //        Console.Write("Col: " + subItem.ToString());
            //    }
            //}

            //foreach (var item in ccs._blockAvailibleList)
            //{
            //    index = 0;
            //    foreach (var subItem in item)
            //    {
            //        if (index == 0)
            //            Console.WriteLine();
            //        index++;
            //        Console.Write("Block: " + subItem.ToString());
            //    }
            //}
            #endregion

            //Console.WriteLine("Sleep Thread");
            //Thread.Sleep(5000);
            Sudoku sudoku = new Sudoku();

            Console.WriteLine("Waiting to Read");
            Console.Read();
        }
    }
}
