using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku
{
    public class Sudoku
    {
        private int _matrixNum;
        private int[][] _sudokuMatrix;
        private bool[][] _isHoleMatrix;

        public int MatrixNum
        {
            get
            {
                return _matrixNum;
            }
        }
        public int[][] SudokuMatrix
        {
            get { return _sudokuMatrix; }
            set { _sudokuMatrix = value; }
        }
        public bool[][] IsHoleMatrix
        {
            get { return _isHoleMatrix; }
            set { _isHoleMatrix = value; }
        }


        public Sudoku()
        {

        }



    }
}
