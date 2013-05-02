using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku
{
    /// <summary>
    /// 洞包含此洞行列索引以及可能的值
    /// </summary>
    public class Hole
    {
        #region 字段和属性

        private int _rowIndex;
        private int _colIndex;
        private List<int> _probableValues;

        public int RowIndex
        {
            get { return _rowIndex; }
        }
        public int ColIndex
        {
            get { return _colIndex; }
        }
        public List<int> ProbableValues
        {
            get { return _probableValues; }
            set { _probableValues = value; }
        }

        #endregion

        #region 构造函数
        
        public Hole(int rowIndex, int colIndex)
        {
            _rowIndex = rowIndex;
            _colIndex = colIndex;
            _probableValues = new List<int>();
        }

        #endregion
    }
}
