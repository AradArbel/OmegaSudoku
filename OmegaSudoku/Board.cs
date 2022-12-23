using System;
using System.Collections.Generic;
using System.Text;
{

    // Representation of a Sudoku board
    internal class SudokuBoard
    {
        private SudokuCell[,] sudokuBoard;

        public SudokuCell this[int row, int col]
        {
            get
            {
                return this.sudokuBoard[row, col];
            }
        }

        /// Constructor for this class.
        public SudokuBoard()
        {
            this.sudokuBoard = new SudokuCell[maxCellValue, maxCellValue];

            // Initialize the Sudoku table
            for (int row = 0; row < maxCellValue; row++)
            {
                for (int col = 0; col < maxCellValue; col++)
                {
                    this.sudokuBoard[row, col] = new SudokuCell(row, col);
                }
            }

        }
        /// Sets a value for a cell.
        public void SetValue(int row, int col, int value)
        {
            this[row, col].SetValue(value);

            for (int cellIndex = 0; cellIndex < maxCellValue; cellIndex++)
            {
                this[row, cellIndex].RemovePossibility(value); // remove possible value for the row
                this[cellIndex, col].RemovePossibility(value); // remove possible value for the col

                int rowS = row + (cellIndex / Math.Sqrt(maxCellValue));
                int colS = col + (cellIndex % Math.Sqrt(maxCellValue));
                this[rowS, colS].RemovePossibility(value); // remove possible value for the square
            }
        }

    }
}