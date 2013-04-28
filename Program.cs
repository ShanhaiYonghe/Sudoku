using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            #region Display Sudoku

            //int n = 9;
            //GenerateClosingStage ccs = new GenerateClosingStage(n);

            //int index = 0;
            //for (int row = 0; row < n; row++, index = 0)
            //{
            //    for (int col = 0; col < n; col++, index++)
            //    {
            //        if (index == 0)
            //            Console.WriteLine();
            //        Console.Write(ccs._sudokuMatrix[row][col].ToString());
            //    }
            //}

            #endregion

            Sudoku sudoku = new Sudoku();
            Console.Read();
        }
    }
}
