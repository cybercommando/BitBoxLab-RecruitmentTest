using System;
using System.Collections.Generic;
using System.Text;

namespace FourInRow.GameLibrary
{
    static class FourInRowLogic
    {

        //===========================================================
        // Game Decision
        //===========================================================

        public static bool Winner(char[,] _board, char player)
        {
            return
                checkRows(_board, player)
                || checkColumns(_board, player)
                || checkMainDiagonal(_board, player)
                || checkCounterDiagonal(_board, player);
        }

        public static bool Drawn(char[,] _board)
        {
            int boardSize = _board.GetLength(0);
            bool decision = true;
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (_board[i, j] == ' ')
                    {
                        return false;
                    }
                }
            }
            return decision;
        }

        //===========================================================
        // Game Logic
        //===========================================================
        private static bool checkRows(char[,] _board, char player)
        {
            int boardSize = _board.GetLength(0);
            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize - 3; col++)
                {
                    if (player == _board[row, col] &&
                        player == _board[row, col + 1] &&
                        player == _board[row, col + 2] &&
                        player == _board[row, col + 3])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool checkColumns(char[,] _board, char player)
        {
            int boardSize = _board.GetLength(0);
            for (int row = 0; row < boardSize - 3; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    if (player == _board[row, col] &&
                        player == _board[row + 1,col] &&
                        player == _board[row + 2,col] &&
                        player == _board[row + 3,col])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool checkMainDiagonal(char[,] _board, char player)
        {
            int boardSize = _board.GetLength(0);
            for (int row = 0; row < boardSize - 3; row++)
            {
                for (int col = 0; col < boardSize - 3; col++)
                {
                    if (player == _board[row, col] &&
                        player == _board[row + 1, col + 1] &&
                        player == _board[row + 2, col + 2] &&
                        player == _board[row + 3, col + 3])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool checkCounterDiagonal(char[,] _board, char player)
        {
            int boardSize = _board.GetLength(0);
            for (int row = 0; row < boardSize - 3; row++)
            {
                for (int col = 3; col < boardSize; col++)
                {
                    if (player == _board[row, col] &&
                        player == _board[row + 1, col - 1] &&
                        player == _board[row + 2, col - 2] &&
                        player == _board[row + 3, col - 3])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
