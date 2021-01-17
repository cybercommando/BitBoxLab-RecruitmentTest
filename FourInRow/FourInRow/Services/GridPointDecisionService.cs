using FourInRow.Models;
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
            if (_difficultyLevel == DifficultyLevel.EASY)
            {
                return EasyDicision(_board, _blank, _player);
            }
            else if (_difficultyLevel == DifficultyLevel.MEDIUM)
            {
                return MediumDicision(_board, _blank, _player);
            }
            else if (_difficultyLevel == DifficultyLevel.HARD)
            {
                return HardDicision(_board, _blank, _player);
            }
            else
            {
                return null;
            }
        }

        private static GridPoint EasyDicision(char[,] _board, char _blank, char _player)
        {
            int sizeRow = _board.GetLength(0);
            int _Row, _Col, _Index;

            do
            {
                Random _random = new Random();
                _Row = _random.Next(0, sizeRow - 1);
                _Col = _random.Next(0, sizeRow - 1);
                _Index = (_Row * sizeRow) + _Col;

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

        private static GridPoint MediumDicision(char[,] _board, char _blank, char _player)
        {
            return null;
        }

        private static GridPoint HardDicision(char[,] _board, char _blank, char _player)
        {
            return null;
        }
    }
}
