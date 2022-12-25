using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
{
    // Representation of a cell in a Sudoku board 
    class SudokuCell
    { 
        // Row for this cell into a Sudoku board.
        private int row;

        //Column for this cell into a Sudoku board.
        private int col;

        //box for this cell into a Sudoku board.
        private int box;

        // Value for this cell into a Sudoku board.
        private int value;

        // Number of possible values for this cell.
        private int possibilitiesAmount;

        // Dictionary to know for each value if it can be put int the cell or not.
        private Dictionary<int, bool> possibilities;

        //Constructor for this class.
        internal SudokuCell(int row, int col)
        {
            this.row = row;
            this.col = col;
            this.value = 0;
            this.possibilities = new Dictionary<int, bool>();
            this.possibilitiesAmount = Constants.maxCellValue;
        }

        // Get row for this cell into a Sudoku board.
        public int Row
        {
            get{return this.row;}
        } 

        // Get column for this cell into a Sudoku board.
        public int Col
        {
            get{return this.col;}
        }

        // Get Value for this cell into a Sudoku board (-1 if value is unknown)
        public int Value
        {
            get
            {
                if (this.IsValid)
                {
                    return this.value;
                }
                return -1;
            }
        }
        // Has this cell a valid value?
        public bool IsValid
        {
            get { return (this.value >= Constants.minCellValue && this.value <= Constants.maxCellValue);}
        }

        // Number of possible values for this cell.
        public int possibilitiesAmount
    {
            get
            {
                if (this.IsValid)
                {
                    return 0;
                }
                return this.possibilitiesAmount;
            }
        }

        //  Return an array with the possible values for this cell.
        public int[] GetPossibilities()
        {
            if (this.IsValid)
            {
                return new int[] { };
            }

            List<int> possibilities = new List<int>();
            
            /// add each possible value in the dictionary to the array
            for (int i = 1; i <= Constants.maxCellValue; i++)
            {
                if (this.possibilities.item[i] == true)
                {
                    possibilities.Add(i);
                }
            }
            return possibilities.ToArray();
        }

        // Randomly picks a possible value for this cell
        public int PickRandomPossibility()
        {
            int[] possibilities = this.GetPossibilities();

            if (possibilities.Length == 0)
            {
                return -1;
            }

            Random rnd = new Random(DateTime.Now.Millisecond);
            int index = rnd.Next(possibilities.Length);
            return possibilities[index];
        }

        // Removes a value from the dictionary of possible values.
        internal void RemovePossibility(int value)
        {
            this.IsVaildValue(value);
            if (this.possibilities.ContainsKey(value))
            {
                this.possibilities.Add(value, false);
                this.possibilitiesAmount--;
            }
        }

      
        // Sets a value for this cell.
        internal void SetValue(int value)
        {
            if (this.IsValid)
            {
                throw new ApplicationException(
                    string.Format("Already has been set a value for cell at [{0}, {1}].",
                    this.row + 1, this.col + 1));
            }

            this.IsVaildValue(value);
            this.value = value;
        }

        // Checks if the specified value is valid for a cell.
        private void IsVaildValue(int value)
        {
            if (value < Constants.minCellValue || value > Constants.maxCellValue)
            {
                throw new ApplicationException(
                    string.Format("Invalid value {2} for cell at [{0}, {1}].",
                    this.row + 1, this.col + 1, value));
            }
            int[] possibilities = this.GetPossibilities();

            if (Array.IndexOf<int>(possibilities, value) == -1)
            {
                throw new ApplicationException(
                    string.Format("Invalid value {2} for cell at [{0}, {1}].",
                    this.row + 1, this.col + 1, value));
            }
        }

    }

}