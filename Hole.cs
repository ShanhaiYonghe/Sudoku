using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku
{
    public class Hole
    {
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
            get;
            set;
        }

        public Hole(int rowIndex, int colIndex)
        {
            
        }
    }
}
