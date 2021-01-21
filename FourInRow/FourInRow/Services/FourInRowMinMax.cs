using FourInRow.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FourInRow.Services
{
    static class FourInRowMinMax
    {
        private static int Rows;
        private static int Cols;
        private static int maxDepth;
        private const int BlueWins = 1000000;
        private const int RedWins = -BlueWins;

        private enum Mycell
        {
            BLUE = 1,
            RED = -1,
            Empty = 0
        };

        private class Board
        {
            public Mycell[] _slots;
            public Board()
            {
                _slots = new Mycell[Rows * Cols];
            }
        };

        private static int ScoreBoard(Board board)
        {
            int[] counters = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            // Horizontal spans
            for (int y = 0; y < Rows; y++)
            {
                int score = (int)board._slots[Cols * (y) + 0] + (int)board._slots[Cols * (y) + 1] + (int)board._slots[Cols * (y) + 2];
                for (int x = 3; x < Cols; x++)
                {
                    score += (int)board._slots[Cols * (y) + x];
                    counters[score + 4]++;
                    score -= (int)board._slots[Cols * (y) + x - 3];
                }
            }
            // Vertical spans
            for (int x = 0; x < Cols; x++)
            {
                int score = (int)board._slots[Cols * (0) + x] + (int)board._slots[Cols * (1) + x] + (int)board._slots[Cols * (2) + x];
                for (int y = 3; y < Rows; y++)
                {
                    score += (int)board._slots[Cols * (y) + x];
                    counters[score + 4]++;
                    score -= (int)board._slots[Cols * (y - 3) + x];
                }
            }
            // Down-right (and up-left) diagonals
            for (int y = 0; y < Rows - 3; y++)
            {
                for (int x = 0; x < Cols - 3; x++)
                {
                    int score = 0;
                    for (int ofs = 0; ofs < 4; ofs++)
                    {
                        int yy = y + ofs;
                        int xx = x + ofs;
                        score += (int)board._slots[Cols * (yy) + xx];
                    }
                    counters[score + 4]++;
                }
            }
            // up-right (and down-left) diagonals
            for (int y = 3; y < Rows; y++)
            {
                for (int x = 0; x < Cols - 3; x++)
                {
                    int score = 0;
                    for (int ofs = 0; ofs < 4; ofs++)
                    {
                        int yy = y - ofs;
                        int xx = x + ofs;
                        score += (int)board._slots[Cols * (yy) + xx];
                    }
                    counters[score + 4]++;
                }
            }

            if (counters[0] != 0)
                return RedWins;
            else if (counters[8] != 0)
                return BlueWins;
            else
                return
                    counters[5] + 2 * counters[6] + 5 * counters[7] -
                    counters[3] - 2 * counters[2] - 5 * counters[1];
        }

        private static int DropDisk(Board board, int column, Mycell color)
        {
            for (int y = Rows - 1; y >= 0; y--)
                if (board._slots[Cols * (y) + column] == Mycell.Empty)
                {
                    board._slots[Cols * (y) + column] = color;
                    return y;
                }
            return -1;
        }

        private static void MinMaxAlgorithm(bool maximizeOrMinimize, Mycell color, int depth, Board board, out int move, out int score)
        {
            if (0 == depth)
            {
                move = -1;
                score = ScoreBoard(board);
            }
            else
            {
                int bestScore = maximizeOrMinimize ? -10000000 : 10000000;
                int bestMove = -1;
                for (int column = 0; column < Cols; column++)
                {
                    if (board._slots[Cols * (0) + column] != Mycell.Empty)
                        continue;
                    int rowFilled = DropDisk(board, column, color);
                    if (rowFilled == -1)
                        continue;
                    int s = ScoreBoard(board);
                    if (s == (maximizeOrMinimize ? BlueWins : RedWins))
                    {
                        bestMove = column;
                        bestScore = s;
                        board._slots[Cols * (rowFilled) + column] = Mycell.Empty;
                        break;
                    }
                    int moveInner, scoreInner;
                    if (depth > 1)
                        MinMaxAlgorithm(!maximizeOrMinimize, color == Mycell.BLUE ? Mycell.RED : Mycell.BLUE, depth - 1, board, out moveInner, out scoreInner);
                    else
                    {
                        moveInner = -1;
                        scoreInner = s;
                    }
                    board._slots[Cols * (rowFilled) + column] = Mycell.Empty;
                    /* when loss is certain, avoid forfeiting the match, by shifting scores by depth... */
                    if (scoreInner == BlueWins || scoreInner == RedWins)
                        scoreInner -= depth * (int)color;

                    if (maximizeOrMinimize)
                    {
                        if (scoreInner >= bestScore)
                        {
                            bestScore = scoreInner;
                            bestMove = column;
                        }
                    }
                    else
                    {
                        if (scoreInner <= bestScore)
                        {
                            bestScore = scoreInner;
                            bestMove = column;
                        }
                    }
                }
                move = bestMove;
                score = bestScore;
            }
        }

        public static int GetDecision(char[,] _board, char _player, DifficultyLevel _difficultyLevel)
        {
            Rows = _board.GetLength(0);
            Cols = _board.GetLength(1);

            //Setting Board
            Mycell[] args = new Mycell[Rows * Cols];
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    if (_board[r, c] == 'B')
                    {
                        args[(r * Cols) + c] = Mycell.BLUE;
                    }
                    else if (_board[r, c] == 'R')
                    {
                        args[(r * Cols) + c] = Mycell.RED;
                    }
                    else if (_board[r, c] == ' ')
                    {
                        args[(r * Cols) + c] = Mycell.Empty;
                    }
                }
            }
            Board board = new Board
            {
                _slots = args
            };

            //Setting DifficultyLevel
            maxDepth = (int)_difficultyLevel;

            //Setting Player
            Mycell player = (_player == 'B') ? Mycell.BLUE : Mycell.RED;

            int move, score;
            MinMaxAlgorithm(true, player, maxDepth, board, out move, out score);

            return move;
        }

    }
}

