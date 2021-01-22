using FourInRow.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FourInRow.Services.MinMax
{
    class FourInRowMinMaxAlphaBeta
    {
        private const int Rows = 6;
        private const int Cols = 7;
        //private Move move = new Move();

        public int GetDecision(char[,] _board, char _player, DifficultyLevel _difficultyLevel)
        {
            PlayerEnum player = (_player == 'B') ? PlayerEnum.BLUE : PlayerEnum.RED;
            int Depth = (int)_difficultyLevel;

            int[,] tempBoard = new int[6, 7];
            for (int r = 0; r < 6; r++)
            {
                for (int c = 0; c < 7; c++)
                {
                    int v;
                    if (_board[r,c] == 'R')
                    {
                        v = (int)PlayerEnum.RED;
                    }
                    else if (_board[r, c] == 'B')
                    {
                        v = (int)PlayerEnum.BLUE;
                    }
                    else
                    {
                        v = (int)PlayerEnum.Empty;
                    }

                    tempBoard[r, c] = v;
                }
            }

            Board board = new Board(tempBoard, (int)player);

            var move = new Move();
            move = MaximizePlay(board, Depth, -100000, 100000);

            return move.Column;
        }

        private Move MaximizePlay(Board board,int depth, int alpha, int beta)
        {
            // Call score of our board
            var score = board.GetScore();

            // Break
            if (board.isFinished(depth, score))
            {
                var move = new Move();
                move.Column = -1;
                move.Score = score;
                return move;
            }

            // Column, Score
            var max = new Move() { Column = -1, Score = alpha };

            // For all possible moves
            for (var column = 0; column < Cols; column++)
            {
                var new_board = board; // Create new board

                if (new_board.Place(column))
                {
                    Move next_move = MinimizePlay(new_board, depth - 1, alpha, beta); // Recursive calling

                    // Evaluate new move
                    if (max.Column == -1 || next_move.Score > max.Score)
                    {
                        max.Column = column;
                        max.Score = next_move.Score;
                        alpha = next_move.Score;
                    }

                    if (alpha >= beta) return max;
                }
            }
            return max;
        }

        private Move MinimizePlay(Board board, int depth, int alpha, int beta)
        {
            // Call score of our board
            var score = board.GetScore();

            // Break
            if (board.isFinished(depth, score))
            {
                var move = new Move();
                move.Column = -1;
                move.Score = score;
                return move;
            }

            // Column, Score
            var min = new Move() { Column = -1, Score = beta };

            for (var column = 0; column < Cols; column++)
            {
                var new_board = board;

                if (new_board.Place(column))
                {
                    var next_move = MaximizePlay(new_board, depth - 1, alpha, beta);

                    if (min.Column == -1 || next_move.Score < min.Score)
                    {
                        min.Column = column;
                        min.Score = next_move.Score;
                        beta = next_move.Score;
                    }

                    if (alpha >= beta) return min;
                }
            }
            return min;
        }
    }
}
