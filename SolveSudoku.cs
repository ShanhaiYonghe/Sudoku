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
        //private int[][] _sudokuMatrix;
        //private bool[][] _isHoleMatrix;

        private Sudoku _sudoku;
        private List<Hole> _holeList;



        public SolveSudoku(Sudoku sudoku)
        {
            _holeList = new List<Hole>();
            _sudoku = sudoku;

            GetHoles();
            AnalysisHoles();
        }

       
        //获取所有洞
        public void GetHoles()
        {
            for (int row = 0; row < _sudoku.MatrixNum; row++)
            {
                for (int col = 0; col < _sudoku.MatrixNum; col++)
                {
                    if (_sudoku.IsHoleMatrix[row][col] == true)
                    {
                        Hole hole = new Hole(row, col);
                        for (int i = 0; i < _sudoku.MatrixNum; i++)
                            hole.ProbableValues.Add(i + 1);

                        _holeList.Add(hole);
                    }
                }
            }
        }

        //分析所有洞的可能值，
        //每次分析结束，检查该洞的可能值是否对其他洞有影响，
        //如果洞值单一，则确定该点值,消除此洞
        public void AnalysisHoles()
        {
            foreach (Hole hole in _holeList)
                CalcProbableValue(hole);
            
        }

        //初始，计算一个洞的可能值
        public void CalcProbableValue(Hole hole)
        {
            #region CheckRow

            for (int col = 0; col < _sudoku.MatrixNum; col++)
            {
                int value = _sudoku.SudokuMatrix[hole.RowIndex][col];
                if (_sudoku.IsHoleMatrix[hole.RowIndex][col] == false && 
                        hole.ProbableValues.Contains(value))
                    hole.ProbableValues.Remove(value);
            }

            #endregion

            #region CheckCol

            for (int row = 0; row < _sudoku.MatrixNum; row++)
            {
                int value = _sudoku.SudokuMatrix[row][hole.ColIndex];
                if (_sudoku.IsHoleMatrix[row][hole.ColIndex] == false &&
                                hole.ProbableValues.Contains(value))
                    hole.ProbableValues.Remove(value);
            }
            
            #endregion

            #region CheckBlock
            
            int num = Convert.ToInt32(Math.Sqrt(_sudoku.MatrixNum));
            int blockRowIndex = hole.RowIndex / num;
            int blockColINdex = hole.ColIndex / num;

            for (int row = num * blockRowIndex; row < num + num * blockRowIndex; row++)
            {
                for (int col = num * blockColINdex; col < num + num * blockColINdex; col++)
                {
                    int value = _sudoku.SudokuMatrix[row][col];
                    if (_sudoku.IsHoleMatrix[row][col] == false &&
                                hole.ProbableValues.Contains(value))
                        hole.ProbableValues.Remove(value);
                }
            }

            #endregion
        }

        //如果洞的可能值单一，用此值填补洞，消除此洞
        public void RemoveHole(Hole hole)
        {
            int value = hole.ProbableValues.ToArray()[0];
            _sudoku.SudokuMatrix[hole.RowIndex][hole.ColIndex] = value;
            _sudoku.IsHoleMatrix[hole.RowIndex][hole.ColIndex] = false;
            foreach (Hole item in _holeList)
            {
                if (hole.RowIndex == item.RowIndex && hole.ColIndex == item.ColIndex)
                    _holeList.Remove(item);
            }

            if (_holeList == null)
                return;
            else
                CalcRelatedHole(hole);
        }

        //当修改一个洞的可能值时（包含确定洞值情况），计算对相关洞的可能值影响
        public void CalcRelatedHole(Hole hole)
        {
            int value = hole.ProbableValues.ToArray()[0];
            
            //TOGo: =======================================================
          
            #region Old
            #region Row

            //foreach (Hole item in _holeList)
            //{
            //    if (item.RowIndex == hole.RowIndex && item.ProbableValues.Contains(value))
            //    { 
            //        item.ProbableValues.Remove(value);
            //    }
            //}
            #endregion

            #region Col

            //foreach (Hole item in _holeList)
            //{
            //    if (item.ColIndex == hole.ColIndex && item.ProbableValues.Contains(value))
            //        item.ProbableValues.Remove(value);
            //}

            #endregion

            #region Block

            //int num = Convert.ToInt32(Math.Sqrt(_sudoku.MatrixNum));
            //int blockRowIndex = hole.RowIndex / num;
            //int blockColIndex = hole.ColIndex / num;

            //foreach (Hole item in _holeList)
            //{
            //    if (item.RowIndex >= blockRowIndex * num &&
            //        item.RowIndex < num + blockRowIndex * num &&
            //        item.ColIndex >= blockRowIndex * num &&
            //        item.ColIndex < num + blockColIndex * num)
            //    {
            //        if(item.ProbableValues.Contains(value))
            //            item.ProbableValues.Remove(value);
            //    }
            //}

            #endregion
            #endregion

            #region Row

            for (int col = 0; col < _sudoku.MatrixNum; col++)
            {
                if (_sudoku.IsHoleMatrix[hole.RowIndex][col] == true)
                {
                    foreach (Hole item in _holeList)
                    {
                        if (item.RowIndex == hole.RowIndex &&
                                    item.ColIndex == col &&
                                    item.ProbableValues.Contains(value))
                        {
                            item.ProbableValues.Remove(value);
                            
                        }
                    }
                }
            }

            #endregion

            #region Col

            for (int row = 0; row < _sudoku.MatrixNum; row++)
            {
                if (_sudoku.IsHoleMatrix[row][hole.ColIndex]==true)
                {
                    foreach (Hole item in _holeList)
                    {
                        if (item.RowIndex == hole.RowIndex &&
                                    item.ColIndex == row &&
                                    item.ProbableValues.Contains(value))
                        {
                            item.ProbableValues.Remove(value);

                        }
                    }
                }
            }

            #endregion

            #region Block

            int num = Convert.ToInt32(Math.Sqrt(_sudoku.MatrixNum));
            int blockRowIndex = hole.RowIndex / num;
            int blockColIndex = hole.ColIndex / num;

            foreach (Hole item in _holeList)
            {
                if (item.RowIndex >= blockRowIndex * num &&
                    item.RowIndex < num + blockRowIndex * num &&
                    item.ColIndex >= blockRowIndex * num &&
                    item.ColIndex < num + blockColIndex * num)
                {
                    if (item.ProbableValues.Contains(value))
                        item.ProbableValues.Remove(value);
                }
            }

            #endregion
        }


        public void FindUniqueValueHole()
        {
            foreach (Hole hole in _holeList)
                if (hole.ProbableValues.Count == 1)
                    RemoveHole(hole);
        }

        

    }
}
