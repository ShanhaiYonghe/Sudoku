using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku
{
    /// <summary>
    /// 将终盘挖洞从而生成数独
    /// </summary>
    public class GenerateSudoku
    {
        #region 字段和属性

        private int _matrixNum;

        private int[][] _sudokuMatrix;
        private int[][] _sudokuMatrixRollBack;
        private int _prompt;    //提示数字的个数，即总数减去挖洞数

        private Random _random;

        #endregion

        #region 构造函数

        public GenerateSudoku()
        {
            _matrixNum = 9;

            #region CreateMatrix

            _sudokuMatrix=new int[_matrixNum][];
            _sudokuMatrixRollBack=new int[_matrixNum][];

            for (int i = 0; i < _matrixNum; i++)
            {
                int[] baseArr = new int[_matrixNum];
                _sudokuMatrix[i] = baseArr;
                _sudokuMatrixRollBack[i] = baseArr;
            }

            #endregion

            GenerateClosingStage ccs = new GenerateClosingStage(_matrixNum);
            DeepCopy(_sudokuMatrix, ccs.GetSudokuMatrix());
            DeepCopy(_sudokuMatrixRollBack, ccs.GetSudokuMatrix());

            Init();
        }

        #endregion

        /// <summary>
        /// 生成数独题目
        /// </summary>
        private void Init()
        {
            for (int i = 0; i < _matrixNum * _matrixNum - _prompt; i++)
            {
                DigHole();
            }
        }

        /// <summary>
        /// 挖洞
        /// </summary>
        private void DigHole()
        {
            int randomItemValue = _sudokuMatrix[_random.Next(0, 9)][_random.Next(0, 9)];
            if (randomItemValue!=0)
                randomItemValue = 0;
            
            if (!CheckSudokuHasUniqueAnswer())
            {
                DeepCopy(_sudokuMatrix, _sudokuMatrixRollBack);
                DigHole();
            }
            else
                DeepCopy(_sudokuMatrixRollBack, _sudokuMatrix);
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
                    paramCloneSource[row][col] = param[row][col];
                }
            }
        }

        /// <summary>
        /// 检查数独是否拥有唯一解
        /// </summary>
        /// <returns></returns>
        private bool CheckSudokuHasUniqueAnswer()
        {

            return false;
        }

        /// <summary>
        /// 设置数独难度等级
        /// </summary>
        public void SetLevel()
        {
            //random value for _prompt
            //    SetPrompt(_randomValue);
        }

        /// <summary>
        /// 设置数独提示数字个数
        /// </summary>
        /// <param name="num"></param>
        public void SetPrompt(int num)
        {

        }
    }
}
