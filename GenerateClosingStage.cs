using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku
{
    public class GenerateClosingStage
    {
        #region 字段和属性

        private int _matrixBaseNum;
        private Random _random;
        private int _randomValue;
        private int[][] _sudokuMatrixRollBack;
        private List<int>[] _colAvailibleListRollBack;
        private List<int>[] _blockAvailibleListRollBack;
        private int recursionDepth = 0;  //递归深度

        public int[][] _sudokuMatrix;
        public List<int>[] _colAvailibleList;
        public List<int>[] _blockAvailibleList;

        //private int createRandomValueTimes = 0;      //Debug
        //private int createItemTimes = 0;     //Debug
        //private int checkFunctionCallTimes = 0;  //Debug
        

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="n">数独大小：n*n</param>
        public GenerateClosingStage(int n)
        {
            _matrixBaseNum = n;
            _random = new Random();

            InitialMatrix();
            CreateRandomValueToMatrix();
        }

        /// <summary>
        /// 构造终盘Matrix，每个点初始值为0
        /// 构造盘中所有列和宫的有效数字List
        /// </summary>
        private void InitialMatrix()
        {
            #region CreateMatrix

            _sudokuMatrix = new int[_matrixBaseNum][];
            _sudokuMatrixRollBack = new int[_matrixBaseNum][];

            for (int i = 0; i < _matrixBaseNum; i++)
            {
                int[] rowArr = new int[_matrixBaseNum];
                _sudokuMatrix[i] = rowArr;
                _sudokuMatrixRollBack[i] = rowArr;
            }

            #endregion

            #region AvailableList

            int[] baseArr = new int[_matrixBaseNum];
            for (int i = 0; i < _matrixBaseNum; i++)
                baseArr[i] = i + 1;

            _colAvailibleList = new List<int>[_matrixBaseNum];
            _blockAvailibleList = new List<int>[_matrixBaseNum];

            _colAvailibleListRollBack = new List<int>[_matrixBaseNum];
            _blockAvailibleListRollBack = new List<int>[_matrixBaseNum];

            for (int i = 0; i < _matrixBaseNum; i++)
            {
                int[] newColArr = new int[_matrixBaseNum];
                int[] newBlockArr = new int[_matrixBaseNum];

                baseArr.CopyTo(newColArr, 0);
                baseArr.CopyTo(newBlockArr, 0);

                _colAvailibleList[i] = new List<int>(newColArr);
                _blockAvailibleList[i] = new List<int>(newBlockArr);

                _colAvailibleListRollBack[i] = new List<int>(newColArr);
                _blockAvailibleListRollBack[i] = new List<int>(newBlockArr);
            }

            #endregion

            DeepCopy(_sudokuMatrixRollBack, _sudokuMatrix);
            DeepCopy(_colAvailibleListRollBack, _colAvailibleList);
            DeepCopy(_blockAvailibleListRollBack, _blockAvailibleList);
        }

        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <param name="param">目标参数</param>
        /// <param name="paramCloneSource">原参数</param>
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
        /// 深拷贝
        /// 供回滚使用，所以param[i].Count 小于 paramCloneSource[i].Count
        /// </summary>
        /// <param name="param"></param>
        /// <param name="paramCloneSource"></param>
        private void DeepCopy(List<int>[] param, List<int>[] paramCloneSource)
        {
            for (int i = 0; i < param.Length; i++)
                param[i] = new List<int>();
            for (int i = 0; i < paramCloneSource.Length; i++)
            {
                foreach (int item in paramCloneSource[i])
                {
                    param[i].Add(item);
                }
            }
        }

        /// <summary>
        /// 随机初始化终盘各点的值
        /// </summary>
        private void CreateRandomValueToMatrix()
        {
            for (int rowIndex = 0; rowIndex < _matrixBaseNum; rowIndex++)
            {
                for (int colIndex = 0; colIndex < _matrixBaseNum; colIndex++)
                {
                    CreateRandomValueToItem(rowIndex, colIndex);

                    //Console.WriteLine("CreateItemIndex is " + createItemTimes);
                    //Console.WriteLine("CheckFunction CallTimes" + checkFunctionCallTimes.ToString());
                    //Console.WriteLine();

                    recursionDepth = 0;
                    //createItemTimes += 1;
                }

                DeepCopy(_sudokuMatrixRollBack, _sudokuMatrix);
                DeepCopy(_colAvailibleListRollBack, _colAvailibleList);
                DeepCopy(_blockAvailibleListRollBack, _blockAvailibleList);
            }
        }

        /// <summary>
        /// 向某点初始化一个随机值
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        private void CreateRandomValueToItem(int rowIndex, int colIndex)
        {
            _randomValue = GetRandomValueWithList(_colAvailibleList[colIndex]);

            if (!CheckValue(rowIndex, colIndex))
            {
                if (recursionDepth >= 50)   //At last 30 Times
                {
                    //若递归进入死胡同
                    //清除已生成的，与这个元素同行的元素，并重新生成随机值
                    DeepCopy(_sudokuMatrix, _sudokuMatrixRollBack);
                    DeepCopy(_colAvailibleList, _colAvailibleListRollBack);
                    DeepCopy(_blockAvailibleList, _blockAvailibleListRollBack);

                    for (int i = 0; i < colIndex; i++)
                    {
                        CreateRandomValueToItem(rowIndex, i);
                    }
                }

                recursionDepth++;
                //Console.WriteLine("RecursionTimes：" + recursionTimes + "    RandomValue：" + _randomValue);
                
                CreateRandomValueToItem(rowIndex, colIndex);
            }
            else
            {
                _sudokuMatrix[rowIndex][colIndex] = _randomValue;
                _colAvailibleList[colIndex].Remove(_randomValue);

                //Console.WriteLine("CreateRandomValueTimes of  Row:" + rowIndex + " Col:" + colIndex +
                //                        " is " + createRandomValueTimes.ToString()
                //                        + "   RandomValue is" + _randomValue);
                //createRandomValueTimes = 0;
            }
        }

        /// <summary>
        /// 根据某点列方向上的有效列表，随机获取列表中的一个值
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private int GetRandomValueWithList(List<int> list)
        {
            //createRandomValueTimes += 1;
            return list.ToArray()[_random.Next(0, list.Count)];
        }

        /// <summary>
        /// 检查某点将要赋的值与之前各点的赋值是否冲突
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <returns>随机值可用返回true,不可用返回false</returns>
        private bool CheckValue(int rowIndex, int colIndex)
        {
            //checkFunctionCallTimes++;

             #region CheckInRow
            for (int i = 0; i < colIndex; i++)
            {
                
                if (_sudokuMatrix[rowIndex][i] == _randomValue)
                    return false;
            }
             #endregion

             #region CheckInCol
            for (int i = 0; i < rowIndex; i++)
            {
                if (_sudokuMatrix[i][colIndex] == _randomValue)
                    return false;
            }
             #endregion

             #region CheckInBlock
            
            int num = Convert.ToInt32(Math.Sqrt(_matrixBaseNum));
            int rowInBlock = rowIndex % num;    //元素所在宫中的行索引
            int blockRowIndex = rowIndex / num;   //元素所在的宫，在整个Matrix中的含索引
            int blockColIndex = colIndex / num; //元素所在的宫，在整个Matrix中的列索引

            for (int i = 0; i < rowInBlock; i++)
            {
                if (rowInBlock != 0)
                {
                    for (int j = blockColIndex * num; j < blockColIndex * num + num; j++)
                    {
                        if (_sudokuMatrix[blockRowIndex * num + i][j] == _randomValue)
                            return false;
                    }
                }
            }
             #endregion

            return true;
        }

        /// <summary>
        /// 获取数独终盘
        /// </summary>
        /// <returns></returns>
        public int[][] GetSudokuMatrix()
        {
            return _sudokuMatrix;
        }
    }
}
