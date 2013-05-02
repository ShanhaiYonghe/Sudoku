using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku
{
    /// <summary>
    /// 解决所有的洞
    /// </summary>
    public class SolveSudoku
    {
         #region 字段和属性
        
        private List<Hole> _holeList;
        private bool _sudokuIsSolved;
        private List<Hole> _holeWithOneValue;

        private int _sudokuMatrixNum;
        private int[][] _sudokuMatrix;
        private bool[][] _isHoleMatrix;
        public bool SudokuIsSolved
        {
            get { return _sudokuIsSolved; }
        }

         #endregion 

        public SolveSudoku(Sudoku sudoku)
        {
            _holeList = new List<Hole>();
            _sudokuMatrixNum = sudoku.MatrixNum;

            #region CreateSudokuMatrix

            _sudokuMatrix =new int[_sudokuMatrixNum][];
            for (int i = 0; i < _sudokuMatrixNum; i++)
            {
                int[] intArr = new int[_sudokuMatrixNum];
                _sudokuMatrix[i] = intArr;
            }
            DeepCopy(_sudokuMatrix,sudoku.SudokuMatrix);
            #endregion

            #region CreateIsHoleMatrix

            _isHoleMatrix=new bool[_sudokuMatrixNum][];
            for (int i = 0; i < _sudokuMatrixNum; i++)
            {
                bool[] boolArr = new bool[_sudokuMatrixNum];
                _isHoleMatrix[i] = boolArr;
            }
            DeepCopy(_isHoleMatrix, sudoku.IsHoleMatrix);
            #endregion

            _sudokuIsSolved = false;
            _holeWithOneValue = new List<Hole>();

            GetHoles();
            AnalysisHoles();
            Solve();
        }
       
        /// <summary>
        /// 获取数独中所有洞
        /// </summary>
        private void GetHoles()
        {
            for (int row = 0; row < _sudokuMatrixNum; row++)
            {
                for (int col = 0; col < _sudokuMatrixNum; col++)
                {
                    if (_isHoleMatrix[row][col] == true)
                    {
                        Hole hole = new Hole(row, col);
                        for (int i = 0; i < _sudokuMatrixNum; i++)
                            hole.ProbableValues.Add(i + 1);
                       
                        _holeList.Add(hole);
                    }
                }
            }
        }

        //分析洞的可能值，缩减范围
        private void AnalysisHoles()
        {
            foreach (Hole hole in _holeList)
            {
                #region CheckRow

                for (int col = 0; col < _sudokuMatrixNum; col++)
                {
                    int value = _sudokuMatrix[hole.RowIndex][col];
                    if (_isHoleMatrix[hole.RowIndex][col] == false &&
                            hole.ProbableValues.Contains(value))
                        hole.ProbableValues.Remove(value);
                }

                #endregion

                #region CheckCol

                for (int row = 0; row < _sudokuMatrixNum; row++)
                {
                    int value = _sudokuMatrix[row][hole.ColIndex];
                    if (_isHoleMatrix[row][hole.ColIndex] == false &&
                                    hole.ProbableValues.Contains(value))
                        hole.ProbableValues.Remove(value);
                }

                #endregion

                #region CheckBlock

                int num = Convert.ToInt32(Math.Sqrt(_sudokuMatrixNum));
                int blockRowIndex = hole.RowIndex / num;
                int blockColINdex = hole.ColIndex / num;

                for (int row = num * blockRowIndex; row < num + num * blockRowIndex; row++)
                {
                    for (int col = num * blockColINdex; col < num + num * blockColINdex; col++)
                    {
                        int value = _sudokuMatrix[row][col];
                        if (_isHoleMatrix[row][col] == false &&
                                    hole.ProbableValues.Contains(value))
                            hole.ProbableValues.Remove(value);
                    }
                }

                #endregion
            }
            
        }

        //解决思路：缩减每个洞的可能值范围，移除单值洞
        private void Solve()
        {
            int recursionDepth = 0;
            while (!_sudokuIsSolved && recursionDepth<50)
            {
                if (_holeList.Count>0)
                {
                    RemoveHole();
                    AnalysisHoles();
                    Console.WriteLine("解决数独，递归次数:"+recursionDepth++);
                }
            }
        }
        
        /// <summary>
        /// 如果洞的可能值单一，用此值填补洞，消除此洞
        /// </summary>
        private void RemoveHole()
        {
            foreach (Hole hole in _holeList)
            {
                if (hole.ProbableValues.Count == 1)
                    _holeWithOneValue.Add(hole);
            }

            if (_holeWithOneValue.Count>0)
            {
                foreach (Hole item in _holeWithOneValue)
                {
                    int value = item.ProbableValues.ToArray()[0];
                    _sudokuMatrix[item.RowIndex][item.ColIndex] = value;
                    _isHoleMatrix[item.RowIndex][item.ColIndex] = false;
                    _holeList.Remove(item);
                    if (_holeList.Count == 0)
                        _sudokuIsSolved = true;
                }   
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

        /// <summary>
        /// 深拷贝IsHoleMatrix
        /// </summary>
        /// <param name="param">新对象</param>
        /// <param name="paramCloneSource">拷贝源</param>
        private void DeepCopy(bool[][] param, bool[][] paramCloneSource)
        {
            for (int row = 0; row < param.Length; row++)
            {
                for (int col = 0; col < param[row].Length; col++)
                {
                    param[row][col] = paramCloneSource[row][col];
                }
            }
        }
    }
}
