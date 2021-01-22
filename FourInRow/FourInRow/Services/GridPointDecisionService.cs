using FourInRow.Models;
using FourInRow.Services.MinMax;
using System;
using System.Collections.Generic;
using System.Text;

namespace FourInRow.Services
{
    static class GridPointDecisionService
    {
        private static GridPoint _gridPoint;

        public static GridPoint GetGridPointDecision(char[,] _board, char _blank, char _player, DifficultyLevel _difficultyLevel)
        {
            if (_difficultyLevel == DifficultyLevel.BASE)
            {
                return BaseDicision(_board, _blank);
            }
            else
            {
                FourInRowMinMaxAlphaBeta minmax = new FourInRowMinMaxAlphaBeta();

                GridPoint gp = new GridPoint();
                int Col = minmax.GetDecision(_board, _player, _difficultyLevel);
                if (Col >= 0 && Col < _board.GetLength(1))
                {
                    gp.GridCol = Col;
                    gp.GridRow = 0;
                    gp.GridIndex = (0 * _board.GetLength(1)) + Col;
                }
                else
                {
                    return BaseDicision(_board, _blank);
                }
                return gp;
            }
        }

        private static GridPoint BaseDicision(char[,] _board, char _blank)
        {
            int sizeRow = _board.GetLength(0);
            int sizeCol = _board.GetLength(1);
            int _Row, _Col, _Index;

            do
            {
                Random _random = new Random();
                _Row = _random.Next(0, sizeRow - 1);
                _Col = _random.Next(0, sizeCol - 1);
                _Index = (_Row * sizeCol) + _Col;

                if (_board[_Row, _Col] == _blank)
                {
                    break;
                }
            } while (true);

            _gridPoint = new GridPoint()
            {
                GridRow = _Row,
                GridCol = _Col,
                GridIndex = _Index
            };
            return _gridPoint;
        }

    }
}
