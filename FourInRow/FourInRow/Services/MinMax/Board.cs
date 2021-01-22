using System;
using System.Collections.Generic;
using System.Text;

namespace FourInRow.Services.MinMax
{
    class Board
    {
        private const int Rows = 6;
        private const int Cols = 7;
        private const int Score = 100000;

        private int[,] Field { get; set; }

        private int Player { get; set; }

        public Board(int[,] _field, int _player)
        {
            this.Field = _field;
            this.Player = _player;
        }

        public bool Place(int column)
        {
            // Check if column valid
            // 1. not empty 2. not exceeding the board size
            if (Field[0, column] == (int)PlayerEnum.Empty && column >= 0 && column < Cols)
            {
                // Bottom to top
                for (var y = Rows - 1; y >= 0; y--)
                {
                    if (Field[y, column] == (int)PlayerEnum.Empty)
                    {
                        Field[y, column] = Player; // Set current player coin
                        break; // Break from loop after inserting
                    }
                }
                Player = (Player == (int)PlayerEnum.BLUE) ? (int)PlayerEnum.RED : (int)PlayerEnum.BLUE;
                return true;
            }
            else
            {
                return false;
            }
        }

        private int ScorePosition(int row, int col, int delta_y, int delta_x)
        {
            int Blue_points = 0;
            int Red_points = 0;

            // Determine score through amount of available chips
            for (var i = 0; i < 4; i++)
            {
                if (Field[row, col] == (int)PlayerEnum.BLUE)
                {
                    Blue_points++; // Add for each human chip
                }
                else if (Field[row, col] == (int)PlayerEnum.RED)
                {
                    Red_points++; // Add for each computer chip
                }

                // Moving through our board
                row += delta_y;
                col += delta_x;
            }

            // Marking winning/returning score
            if (Blue_points == 4)
            {
                return -Score;
            }
            else if (Red_points == 4)
            {
                return Score;
            }
            else
            {
                // Return normal points
                return Red_points;
            }
        }

        public int GetScore()
        {
            int vertical_points = 0;
            int horizontal_points = 0;
            int diagonal_points1 = 0;
            int diagonal_points2 = 0;


            // Board-size: 7x6 (height x width)
            // Array indices begin with 0
            // => e.g. height: 0, 1, 2, 3, 4, 5

            // Vertical points
            // Check each column for vertical score
            // 
            // Possible situations
            //  0  1  2  3  4  5  6
            // [x][ ][ ][ ][ ][ ][ ] 0
            // [x][x][ ][ ][ ][ ][ ] 1
            // [x][x][x][ ][ ][ ][ ] 2
            // [x][x][x][ ][ ][ ][ ] 3
            // [ ][x][x][ ][ ][ ][ ] 4
            // [ ][ ][x][ ][ ][ ][ ] 5
            for (var row = 0; row < Rows - 3; row++)
            {
                // Check for each column
                for (var column = 0; column < Cols; column++)
                {
                    // Rate the column and add it to the points
                    var score = ScorePosition(row, column, 1, 0);
                    if (score == Score) return Score;
                    if (score == -Score) return -Score;
                    vertical_points += score;
                }
            }

            // Horizontal points
            // Check each row's score
            // 
            // Possible situations
            //  0  1  2  3  4  5  6
            // [x][x][x][x][ ][ ][ ] 0
            // [ ][x][x][x][x][ ][ ] 1
            // [ ][ ][x][x][x][x][ ] 2
            // [ ][ ][ ][x][x][x][x] 3
            // [ ][ ][ ][ ][ ][ ][ ] 4
            // [ ][ ][ ][ ][ ][ ][ ] 5
            for (var row = 0; row < Rows; row++)
            {
                for (var column = 0; column < Cols - 3; column++)
                {
                    var score = ScorePosition(row, column, 0, 1);
                    if (score == Score) return Score;
                    if (score == -Score) return -Score;
                    horizontal_points += score;
                }
            }

            // Diagonal points 1 (left-bottom)
            //
            // Possible situation
            //  0  1  2  3  4  5  6
            // [x][ ][ ][ ][ ][ ][ ] 0
            // [ ][x][ ][ ][ ][ ][ ] 1
            // [ ][ ][x][ ][ ][ ][ ] 2
            // [ ][ ][ ][x][ ][ ][ ] 3
            // [ ][ ][ ][ ][ ][ ][ ] 4
            // [ ][ ][ ][ ][ ][ ][ ] 5
            for (var row = 0; row < Rows - 3; row++)
            {
                for (var column = 0; column < Cols - 3; column++)
                {
                    var score = ScorePosition(row, column, 1, 1);
                    if (score == Score) return Score;
                    if (score == -Score) return -Score;
                    diagonal_points1 += score;
                }
            }

            // Diagonal points 2 (right-bottom)
            //
            // Possible situation
            //  0  1  2  3  4  5  6
            // [ ][ ][ ][x][ ][ ][ ] 0
            // [ ][ ][x][ ][ ][ ][ ] 1
            // [ ][x][ ][ ][ ][ ][ ] 2
            // [x][ ][ ][ ][ ][ ][ ] 3
            // [ ][ ][ ][ ][ ][ ][ ] 4
            // [ ][ ][ ][ ][ ][ ][ ] 5
            for (var row = 3; row < Rows; row++)
            {
                for (var column = 0; column <= Cols - 4; column++)
                {
                    var score = ScorePosition(row, column, -1, +1);
                    if (score == Score) return Score;
                    if (score == -Score) return -Score;
                    diagonal_points2 += score;
                }
            }

            return (horizontal_points + vertical_points + diagonal_points1 + diagonal_points2);
        }

        public bool isFinished(int depth, int score)
        {
            if (depth == 0 || score == Score || score == -Score || isFull())
            {
                return true;
            }
            return false;
        }

        public bool isFull()
        {
            for (var i = 0; i < Cols; i++)
            {
                if (Field[0,i] == (int)PlayerEnum.Empty)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
