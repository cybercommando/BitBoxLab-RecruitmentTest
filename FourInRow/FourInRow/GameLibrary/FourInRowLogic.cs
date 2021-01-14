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

        public static bool Winner(char[,] _board, int boardSize, char player)
        {
            return
                checkVertical(_board, boardSize, player)
                || checkHorizontal(_board, boardSize, player)
                || checkDiagonal1(_board, boardSize, player)
                || checkDiagonal2(_board, boardSize, player);
        }

        public static bool Drawn(char[,] _board, int boardSize, char player)
        {
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
        private static bool checkVertical(char[,] field, int boardSize, char player)
        {
            for (int i = 0; i < boardSize; i++)
            {
                if (field[0, i] == player
                    && field[1, i] == player
                    && field[2, i] == player
                    && field[3, i] == player
                ) return true;

                if (field[1, i] == player
                    && field[2, i] == player
                    && field[3, i] == player
                    && field[4, i] == player
                ) return true;

                if (field[2, i] == player
                    && field[3, i] == player
                    && field[4, i] == player
                    && field[5, i] == player
                ) return true;

                if (field[3, i] == player
                    && field[4, i] == player
                    && field[5, i] == player
                    && field[6, i] == player
                ) return true;
            }
            return false;
        }

        private static bool checkHorizontal(char[,] field, int boardSize, char player)
        {
            for (int i = 0; i < boardSize; i++)
            {
                if (field[i, 0] == player
                    && field[i, 1] == player
                    && field[i, 2] == player
                    && field[i, 3] == player
                ) return true;

                if (field[i, 1] == player
                    && field[i, 2] == player
                    && field[i, 3] == player
                    && field[i, 4] == player
                ) return true;

                if (field[i, 2] == player
                    && field[i, 3] == player
                    && field[i, 4] == player
                    && field[i, 5] == player
                ) return true;

                if (field[i, 3] == player
                    && field[i, 4] == player
                    && field[i, 5] == player
                    && field[i, 6] == player
                ) return true;
            }
            return false;
        }

        private static bool checkDiagonal1(char[,] field, int boardSize, char player)
        {
            return false;
        }

        private static bool checkDiagonal2(char[,] field, int boardSize, char player)
        {
            return false;
        }
    }
}
