using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
{
    static const int maxCellValue = 9;
    static const int minCellValue = 1;

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

        //Constructor for this class.
        internal SudokuCell(int row, int col)
        {
            this.row = row;
            this.col = col;
            this.value = 0;
            this.possibilities = new BitArray(new int[] { 0x01ff });
            this.possibilitiesAmount = maxCellValue;
        }

        // Get row for this cell into a Sudoku board.
        public int Row
        {
            get{return this.row;}
        }

        // Get column for this cell into a Sudoku board (zero-based).
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
            get { return (this.value >= minCellValue && this.value <=  maxCellValue);}
        }

        // Number of possible values for this cell.
        public int NumPossibilities
        {
            get
            {
                if (this.IsValid)
                {
                    return 0;
                }
                return this.numPossibilities;
            }
        }

        //  Return an Int array with the possible values for this cell.
        public int[] GetPossibilities()
        {
            if (this.IsValid)
            {
                return new int[] { };
            }

            List<int> possibilities = new List<int>();

            for (int i = 0; i < maxCellValue; i++)
            {
                if (this.possibilities.Get(i))
                {
                    possibilities.Add(i + 1);
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

        // Removes a value from the bitArray of possible values.
        internal void RemovePossibility(int value)
        {
            this.CheckValue(value);
            if (this.possibilities.Get(value - 1))
            {
                this.possibilities.Set(value - 1, false);
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

            this.CheckValue(value);

            int[] possibilities = this.GetPossibilities();

            if (Array.IndexOf<int>(possibilities, value) == -1)
            {
                throw new ApplicationException(
                    string.Format("Invalid value {2} for cell at [{0}, {1}].",
                    this.row + 1, this.col + 1, value));
            }

            this.value = value;
        }

        // Checks if the specified value is valid for a cell.
        private void CheckValue(int value)
        {
            if (value < minCellValue || value > maxCellValue)
            {
                throw new ApplicationException(
                    string.Format("Invalid value {2} for cell at [{0}, {1}].",
                    this.row + 1, this.col + 1, value));
            }
        }

    }

}