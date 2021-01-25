using FourInRow.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FourInRow.Services
{
    class FourInRowLogic
    {
        private char[,] _board = new char[6, 7];

        private char player;

        private List<GridPoint> WinningSequence = new List<GridPoint>();

        public FourInRowLogic(char[,] _board, char _player)
        {
            this._board = _board;
            this.player = _player;
        }

        //===========================================================
        // Game Decision
        //===========================================================

        public bool Winner()
        {
            return
                checkRows()
                || checkColumns()
                || checkMainDiagonal()
                || checkCounterDiagonal();
        }

        public bool Drawn()
        {
            int boardRow = _board.GetLength(0);
            int boardCol = _board.GetLength(1);
            for (int i = 0; i < boardRow; i++)
            {
                for (int j = 0; j < boardCol; j++)
                {
                    if (_board[i, j] == ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public IEnumerable<GridPoint> GetWinningSequence()
        {
            if (Winner())
            {
                return WinningSequence;
            }
            return null;
        }

        //===========================================================
        // Game Logic
        //===========================================================
        private bool checkRows()
        {
            int boardRow = _board.GetLength(0);
            int boardCol = _board.GetLength(1);
            for (int row = 0; row < boardRow; row++)
            {
                for (int col = 0; col < boardCol - 3; col++)
                {
                    if (player == _board[row, col] &&
                        player == _board[row, col + 1] &&
                        player == _board[row, col + 2] &&
                        player == _board[row, col + 3])
                    {
                        WinningSequence = new List<GridPoint>();
                        for (int i = 0; i < 4; i++)
                        {
                            GridPoint gp = new GridPoint()
                            {
                                GridCol = col + i,
                                GridRow = row,
                                GridIndex = (row * 7) + (col + i)
                            };
                            WinningSequence.Add(gp);
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        private bool checkColumns()
        {
            int boardRow = _board.GetLength(0);
            int boardCol = _board.GetLength(1);
            for (int row = 0; row < boardRow - 3; row++)
            {
                for (int col = 0; col < boardCol; col++)
                {
                    if (player == _board[row, col] &&
                        player == _board[row + 1, col] &&
                        player == _board[row + 2, col] &&
                        player == _board[row + 3, col])
                    {
                        WinningSequence = new List<GridPoint>();
                        for (int i = 0; i < 4; i++)
                        {
                            GridPoint gp = new GridPoint()
                            {
                                GridCol = col,
                                GridRow = row + i,
                                GridIndex = ((row + i) * 7) + col
                            };
                            WinningSequence.Add(gp);
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        private bool checkMainDiagonal()
        {
            int boardRow = _board.GetLength(0);
            int boardCol = _board.GetLength(1);

            for (int row = 0; row < boardRow - 3; row++)
            {
                for (int col = 0; col < boardCol - 3; col++)
                {
                    if (player == _board[row, col] &&
                        player == _board[row + 1, col + 1] &&
                        player == _board[row + 2, col + 2] &&
                        player == _board[row + 3, col + 3])
                    {
                        WinningSequence = new List<GridPoint>();
                        for (int i = 0; i < 4; i++)
                        {
                            GridPoint gp = new GridPoint()
                            {
                                GridCol = col + i,
                                GridRow = row + i,
                                GridIndex = ((row + i) * 7) + (col + i)
                            };
                            WinningSequence.Add(gp);
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        private bool checkCounterDiagonal()
        {
            int boardRow = _board.GetLength(0);
            int boardCol = _board.GetLength(1);
            for (int row = 0; row < boardRow - 3; row++)
            {
                for (int col = 3; col < boardCol; col++)
                {
                    if (player == _board[row, col] &&
                        player == _board[row + 1, col - 1] &&
                        player == _board[row + 2, col - 2] &&
                        player == _board[row + 3, col - 3])
                    {
                        WinningSequence = new List<GridPoint>();
                        for (int i = 0; i < 4; i++)
                        {
                            GridPoint gp = new GridPoint()
                            {
                                GridCol = col - i,
                                GridRow = row + i,
                                GridIndex = ((row + i) * 7) + (col - i)
                            };
                            WinningSequence.Add(gp);
                        }
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
