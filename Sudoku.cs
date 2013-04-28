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
        private int[][] _sudokuMatrixRollBack;
        private int _prompt;    //提示数字的个数，即总数减去挖洞数


        private bool[][] _isHoleMatrix;
        private SolveSudoku _solveSudoku;
        private Random _random;


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
        public int Prompt
        {
            set
            {
                if (value>=17)
                {
                    _prompt = value;
                    Init();
                }
            }
        }

        public Sudoku()
        {
            _matrixNum = 9;
            _prompt = 30;
            _random = new Random();
            CreateSudokuMatrix();
            CreateIsHoleMatrix();

            GenerateClosingStage ccs = new GenerateClosingStage(_matrixNum);

            #region Display ClosingStage
             
            int index = 0;
            Console.WriteLine("Display ClosingStage");
            for (int row = 0; row < _matrixNum; row++, index = 0)
            {
                for (int col = 0; col < _matrixNum; col++, index++)
                {
                    if (index == 0)
                        Console.WriteLine();
                    Console.Write(ccs._sudokuMatrix[row][col].ToString());
                }
            }
            Console.WriteLine();
            Console.WriteLine("End");

            #endregion

            DeepCopy(_sudokuMatrix, ccs.GetSudokuMatrix());
            DeepCopy(_sudokuMatrixRollBack, ccs.GetSudokuMatrix());

            ccs = null;
            Init();

            #region Display SudokuMatrix

            index = 0;
            Console.WriteLine("Display SudokuMatrix");
            for (int row = 0; row < _matrixNum; row++, index = 0)
            {
                for (int col = 0; col < _matrixNum; col++, index++)
                {
                    if (index == 0)
                        Console.WriteLine();
                    if (_isHoleMatrix[row][col]!=true)
                    {
                        Console.Write(_sudokuMatrixRollBack[row][col].ToString());
                    }
                    else
                    {
                        Console.Write(0);
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("End");

            #endregion
        }

        private void CreateSudokuMatrix()
        { 
         _sudokuMatrix = new int[_matrixNum][];
            _sudokuMatrixRollBack = new int[_matrixNum][];

            for (int i = 0; i < _matrixNum; i++)
            {
                int[] baseArr = new int[_matrixNum];
                _sudokuMatrix[i] = baseArr;
                _sudokuMatrixRollBack[i] = baseArr;
            }
        }
        private void CreateIsHoleMatrix()
        {
            _isHoleMatrix = new bool[_matrixNum][];
            for (int i = 0; i < _matrixNum; i++)
            {
                var arr = new bool[_matrixNum];
                _isHoleMatrix[i] = arr;
            }
        }

        /// <summary>
        /// 生成数独题目
        /// </summary>
        private void Init()
        {
            for (int i = 0; i < _matrixNum * _matrixNum - _prompt; i++)
            {
                DigHole();
                if (i>45)
                {
                    Console.WriteLine("DigHole Times: " + i);
                }
            }
        }

        /// <summary>
        /// 挖洞
        /// </summary>
        private void DigHole()
        {
            int rowIndex = _random.Next(0, _matrixNum);
            int colIndex = _random.Next(0, _matrixNum);
            int randomItemValue = _sudokuMatrix[rowIndex][colIndex];
            if (randomItemValue != 0)
            {
                randomItemValue = 0;
                _isHoleMatrix[rowIndex][colIndex] = true;
            }
            //if (!JudgeSukokuHasOnlySolution())
            var solveSudoku = new SolveSudoku(this);
            if (!solveSudoku.SudokuIsSolved)
            {
                solveSudoku = null;
                DeepCopy(_sudokuMatrix, _sudokuMatrixRollBack);
                DigHole();
            }
            else
            {
                DeepCopy(_sudokuMatrixRollBack, _sudokuMatrix);
                solveSudoku = null;
            }
        }
        /// <summary>
        /// 深拷贝SudokuMatrix
        /// </summary>
        /// <param name="param">新对象</param>
        /// <param name="paramCloneSource">拷贝源</param>
        private void DeepCopy(int[][] param, int[][] paramCloneSource)
        {
            for (int row = 0; row < param.Length; row++)
            {
                for (int col = 0; col < param[row].Length; col++)
                {
                    param[row][col] = paramCloneSource[row][col];
                }
            }
        }


        //判断挖洞后的数独是否有解
        private bool JudgeSukokuHasOnlySolution()
        {
            _solveSudoku = new SolveSudoku(this);
            return _solveSudoku.SudokuIsSolved;
        }
    }
}
