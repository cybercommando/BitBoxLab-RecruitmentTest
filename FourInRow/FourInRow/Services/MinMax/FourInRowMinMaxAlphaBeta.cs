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
        private int iterations;

        private List<int> PossibleNonWinningMoves = new List<int>();
        private List<int> OpponentWinningMoves = new List<int>();

        public int GetDecision(char[,] _board, char _player, DifficultyLevel _difficultyLevel)
        {
            var winningmoves = getOpponentWinningMoves(_board, _player);

            //Block winning Move
            if (winningmoves.Count > 0)
            {
                return winningmoves[0];
            }
            //------------------


            iterations = 0;
            PlayerEnum player = (_player == 'B') ? PlayerEnum.BLUE : PlayerEnum.RED;
            int Depth = (int)_difficultyLevel;

            int[,] tempBoard = new int[6, 7];
            for (int r = 0; r < 6; r++)
            {
                for (int c = 0; c < 7; c++)
                {
                    int v;
                    if (_board[r, c] == 'R')
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

        private Move MaximizePlay(Board board, int depth, int alpha, int beta)
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
            var max = new Move() { Column = -1, Score = -100000 };

            // For all possible moves
            for (var column = 0; column < Cols; column++)
            {
                var new_board = board.CopyBoard(); // Create new board

                if (new_board.Place(column))
                {
                    iterations++; //debug

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
            var min = new Move() { Column = -1, Score = 100000 };

            for (var column = 0; column < Cols; column++)
            {
                var new_board = board.CopyBoard();

                if (new_board.Place(column))
                {
                    iterations++; //debug

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

        private List<int> getPossibleNonWinningMoves()
        {
            return null;
        }

        private List<int> getOpponentWinningMoves(char[,] _board, char _player)
        {
            char player = (_player == 'B') ? 'R' : 'B';
            for (int c = 0; c < _board.GetLength(1); c++)
            {
                if (_board[0, c] != ' ')
                {
                    continue;
                }

                var tempBoard = new char[6, 7];

                for (int xr = 0; xr < _board.GetLength(0); xr++)
                {
                    for (int xc = 0; xc < _board.GetLength(1); xc++)
                    {
                        tempBoard[xr, xc] = _board[xr, xc];
                    }
                }

                for (int r = _board.GetLength(0) - 1; r >= 0; r--)
                {
                    if (tempBoard[r, c] == ' ')
                    {
                        tempBoard[r, c] = player;
                        break;
                    }
                }

                FourInRowLogic fourInRowLogic = new FourInRowLogic(tempBoard, player);
                if (fourInRowLogic.Winner())
                {
                    OpponentWinningMoves.Add(c);
                }
            }
            return OpponentWinningMoves;
        }

    }
}
