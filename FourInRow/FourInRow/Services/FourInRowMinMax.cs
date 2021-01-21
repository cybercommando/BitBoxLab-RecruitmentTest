using FourInRow.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FourInRow.Services
{
    class FourInRowMinMax
    {
        private int max_ai_chain = -1000000;
        private int max_human_chain = 1000000;

        public int MinMaxAlgorithm(int _depth, char[,] _board,int _alpha, int _beta, char _player)
        {
            if (_depth == 0 )
            {
                return 0;
            }

            else if (_player =='R')
            {
                int temp = max_ai_chain;
                foreach (var move in BoardAvailableMoves(_board))
                {
                    //board.putPiece(player, moves[0]);
                    _board[move.GridRow, move.GridCol] = _player;
                    temp = Math.Max(temp, MinMaxAlgorithm(_depth - 1, _board, _alpha, _beta, 'B'));
                    //board.removePiece(moves[0], moves[1]);
                    _board[move.GridRow, move.GridCol] = ' ';
                    _alpha = Math.Max(_alpha, temp);
                    if (_alpha >= _beta)
                    {
                        break;
                    }

                }
                return temp;
            }

            else
            {
                int temp = max_human_chain;
                foreach (var move in BoardAvailableMoves(_board))
                {
                    _board[move.GridRow, move.GridCol] = _player;
                    temp = Math.Min(temp, MinMaxAlgorithm(_depth - 1, _board, _alpha, _beta, 'R'));
                    //board.removePiece(moves[0], moves[1]);
                    _board[move.GridRow, move.GridCol] = ' ';
                    _beta = Math.Min(_beta, temp);
                    if (_alpha >= _beta)
                    {
                        break;
                    }
                }
                return temp;
            }
        }

        private IEnumerable<GridPoint> BoardAvailableMoves(char[,] _board)
        {
            var gridPoints = new List<GridPoint>();

            int boardRow = _board.GetLength(0);
            int boardCol = _board.GetLength(1);
            for (int i = 0; i < boardCol; i++)
            {
                for (int j = boardRow-1; j >= 0; j--)
                {
                    if (_board[j, i] == ' ')
                    {
                        GridPoint point = new GridPoint()
                        {
                            GridCol = i,
                            GridRow = j
                        };
                        gridPoints.Add(point);
                        break;
                    }
                }
            }
            return gridPoints;
        }

        //private int Min(int depth, int[,] fieldCopy)
        //{
        //    int minValue = 9999;
        //    int moveValue;
        //    bool winAI = false;
        //    bool winHuman = false;
        //    bool isStoneBelow = false;
        //    bool loopBreak = false;

        //    for (int y = 0; y < 6; y++)
        //    {
        //        for (int x = 0; x < 7; x++)
        //        {
        //            if (y > 0)
        //            {
        //                //is a stone under it?
        //                if (fieldCopy[x, y - 1] == 1 || fieldCopy[x, y - 1] == 2)
        //                {
        //                    isStoneBelow = true;
        //                }
        //                else
        //                {
        //                    isStoneBelow = false;
        //                }
        //            }
        //            else
        //            {
        //                isStoneBelow = true;
        //            }

        //            // possible move?
        //            if (fieldCopy[x, y] != 1 && fieldCopy[x, y] != 2 && isStoneBelow == true)
        //            {
        //                isStoneBelow = false;
        //                fieldCopy[x, y] = 1; //simulate move
        //                winHuman = false;
        //                winAI = false;

        //                //Is there a winner?    
        //                if (CheckWin(x, y, 1, fieldCopy))
        //                {
        //                    winHuman = true;
        //                    winAI = false;
        //                }

        //                //No more moves possible?
        //                if (depth <= 1 || winHuman == true)
        //                {
        //                    moveValue = evaluationFunction(winAI, winHuman);       //evaluate the move
        //                }
        //                else
        //                {
        //                    moveValue = Max(depth - 1, fieldCopy);
        //                }

        //                fieldCopy[x, y] = 0; //Reset simulated move

        //                if (moveValue < minValue)
        //                {
        //                    minValue = moveValue;
        //                }
        //            }
        //        }
        //    }
        //    return minValue;
        //}

        //private int Max(int depth, int[,] fieldCopy)
        //{
        //    int maxValue = -9999;
        //    int moveValue;
        //    bool winAI = false;
        //    bool winHuman = false;
        //    bool isStoneBelow = false;

        //    for (int y = 0; y < 6; y++)
        //    {
        //        for (int x = 0; x < 7; x++)
        //        {
        //            if (y > 0)
        //            {
        //                //is a stone under it?
        //                if (fieldCopy[x, y - 1] == 1 || fieldCopy[x, y - 1] == 2)
        //                {
        //                    isStoneBelow = true;
        //                }
        //                else
        //                {
        //                    isStoneBelow = false;
        //                }
        //            }
        //            else
        //            {
        //                isStoneBelow = true;
        //            }

        //            // possible move?
        //            if (fieldCopy[x, y] != 1 && fieldCopy[x, y] != 2 && isStoneBelow == true)
        //            {
        //                isStoneBelow = false;
        //                fieldCopy[x, y] = 2; //simulate move
        //                winAI = false;
        //                winHuman = false;

        //                //Is there a winner?
        //                if (CheckWin(x, y, 2, fieldCopy))
        //                {
        //                    winAI = true;
        //                    winHuman = false;
        //                }

        //                //No more moves possible?
        //                if (depth <= 1 || winAI == true)
        //                {
        //                    moveValue = evaluationFunction(winAI, winHuman);       //evaluate the move
        //                }
        //                else
        //                {
        //                    moveValue = Min(depth - 1, fieldCopy);
        //                }

        //                fieldCopy[x, y] = 0; //Reset simulated move

        //                if (moveValue > maxValue)
        //                {
        //                    maxValue = moveValue;
        //                    if (depth == minimaxDepth)
        //                    {
        //                        aiMoveX = x; // next move
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return maxValue;
        //}

        //private int evaluationFunction(bool winAI, bool winHuman)
        //{
        //    if (winAI)
        //    {
        //        return 1;
        //    }
        //    else if (winHuman)
        //    {
        //        return -1;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}

        //private int evaluationFunction(int depth)
        //{
        //    // max_ai_chain and max_human_chain are global variables that 
        //    // should be updated at each piece placement

        //    if (depth % 2 == 0) // assuming AI gets calculated on even depth (you mentioned your depth is 4)
        //    {
        //        return max_ai_chain;
        //    }
        //    else
        //    {
        //        return max_human_chain;
        //    }
        //}

    }
}
