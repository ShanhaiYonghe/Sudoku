using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku
{
    public class CreateClosingStage
    {
        private int _matrixBaseNum;
        
        public int[][] _sudokuMatrix;
        //public List<int>[] _rowAvailibleList;
        public List<int>[] _colAvailibleList;
        public List<int>[] _blockAvailibleList;

        private Random _random;
        private int _randomValue;

        int times = 0;      //Debug
        int nTimes = 0;     //Debug
        int functionTimes = 0;  //Debug
        public CreateClosingStage(int n)
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
            for (int i = 0; i < _matrixBaseNum; i++)
            {
                int[] rowArr = new int[_matrixBaseNum];
                _sudokuMatrix[i] = rowArr;
            }

            #endregion

            #region AvailableList

            int[] baseArr = new int[_matrixBaseNum];
            for (int i = 0; i < _matrixBaseNum; i++)
                baseArr[i] = i + 1;

            //_rowAvailibleList = new List<int>[_matrixBaseNum];
            _colAvailibleList = new List<int>[_matrixBaseNum];
            _blockAvailibleList = new List<int>[_matrixBaseNum];

            for (int i = 0; i < _matrixBaseNum; i++)
            {
                //int[] newRowArr = new int[_matrixBaseNum];
                int[] newColArr = new int[_matrixBaseNum];
                int[] newBlockArr = new int[_matrixBaseNum];

                //baseArr.CopyTo(newRowArr, 0);
                baseArr.CopyTo(newColArr, 0);
                baseArr.CopyTo(newBlockArr, 0);

                //_rowAvailibleList[i] = new List<int>(newRowArr);
                _colAvailibleList[i] = new List<int>(newColArr);
                _blockAvailibleList[i] = new List<int>(newBlockArr);
            }

            #endregion
           
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
                    Console.WriteLine("Times is " + nTimes);
                    Console.WriteLine("Function Call Times" + functionTimes.ToString());

                    nTimes += 1;
                }
            }    
        }

        /// <summary>
        /// 向某点初始化值
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        private void CreateRandomValueToItem(int rowIndex, int colIndex)
        {
            _randomValue = GetRandomValueWithList(_colAvailibleList[colIndex]);

            //if(false)
            if (!CheckValue(rowIndex, colIndex))
            {
                CreateRandomValueToItem(rowIndex, colIndex);
            }
            else 
            {
                _sudokuMatrix[rowIndex][colIndex] = _randomValue;
                _colAvailibleList[colIndex].Remove(_randomValue);

                //Console.WriteLine("-----------------Row:" + rowIndex + "  Col:" + colIndex + 
                //                    "  Value:" + _sudokuMatrix[rowIndex][colIndex]);
                Console.WriteLine("Random Times of  Row:" + rowIndex + " Col:" + colIndex + " is " + times.ToString());
                //Console.WriteLine("");
                times = 0;
            }
        }

        /// <summary>
        /// 根据某点列方向上的有效列表，随机获取列表中的一个值
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private int GetRandomValueWithList(List<int> list)
        {
            times += 1;
            return list.ToArray()[_random.Next(0, list.Count)];
        }

        /// <summary>
        /// 检查某点将要赋的值与之前各点的赋值是否冲突
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        private bool CheckValue(int rowIndex, int colIndex)
        {
            //if (rowIndex==4&&colIndex==4)
            //{

            //}

            ////CheckInRow
            //for (int i = 0; i < colIndex; i++)
            //{
            //    functionTimes++;
            //    if (_sudokuMatrix[rowIndex][i] == _randomValue)
            //        return false;
            //}

            ////CheckInCol
            //for (int i = 0; i < rowIndex; i++)
            //{
            //    functionTimes++;
            //    if (_sudokuMatrix[i][colIndex] == _randomValue)
            //        return false;
            //}
         
            //CheckInBlock
            int num = Convert.ToInt32(Math.Sqrt(_matrixBaseNum));

            int rowInBlock = rowIndex % num;    //元素所在宫中的行索引
            int blockColIndex = colIndex / num; //元素所在的宫，在整个Matrix中的列索引

            for (int i = 0; i < rowInBlock; i++)
            {
                functionTimes++;
                if (rowInBlock != 0)
                {
                    for (int j = blockColIndex * num; j < num; j++)
                    {
                        //if (_sudokuMatrix[rowIndex - i][j] == _randomValue)
                        if (_sudokuMatrix[i][j] == _randomValue) 
                            return false;
                    }
                }
            }
            return true;
        }
    }
}
