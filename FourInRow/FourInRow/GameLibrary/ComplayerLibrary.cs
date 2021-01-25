using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using FourInRow.Models;
using FourInRow.Services;

namespace FourInRow.GameLibrary
{
    class ComplayerLibrary
    {
        private const string app_title = "Four In A Row !!!";
        private const char blank = ' ';
        private const char Red = 'R';
        private const char Blue = 'B';
        private const int sizeCol = 7;
        private const int sizeRow = 6;

        private ContentPage _page;
        private Label _PlayerTurn;
        private Label _PlayerWin;
        private bool _won = false;
        private char _player = blank;
        private char _YourColor = blank;
        private DifficultyLevel _DifficultyLevel = DifficultyLevel.EASY;
        private char[,] _board = new char[sizeRow, sizeCol];

        public void Show(string content, string title)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                _page.DisplayAlert(title, content, "Ok");
            });
        }

        private async Task<bool> ConfirmAsync(string content, string title, string ok, string cancel)
        {
            return await _page.DisplayAlert(title, content, ok, cancel);
        }

        private Grid Piece()
        {
            Grid grid = new Grid()
            {
                HeightRequest = 40,
                WidthRequest = 40,
            };
            if (_player == Blue)
            {
                BoxView Dot = new BoxView()
                {
                    Color = Color.FromHex("#2196F3"),
                    HeightRequest = 30,
                    WidthRequest = 30,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    CornerRadius = 15
                };
                grid.Children.Add(Dot);
            }
            else if (_player == Red)
            {
                BoxView Dot = new BoxView()
                {
                    Color = Color.Crimson,
                    HeightRequest = 30,
                    WidthRequest = 30,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    CornerRadius = 15
                };
                grid.Children.Add(Dot);
            }
            return grid;
        }

        private async void AutoTurnExecute(object sender)
        {
            var decisionPoint = GridPointDecisionService.GetGridPointDecision(_board, blank, _player, _DifficultyLevel);

            if (!_won)
            {
                Grid element = (Grid)sender;

                int ElemRow = decisionPoint.GridRow;
                int ElemCol = decisionPoint.GridCol;
                int ElemIndex = (ElemRow * sizeCol) + ElemCol;

                //Step1: Add Piece
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    ((Grid)((Grid)element.Parent).Children[ElemIndex]).Children.Add(Piece());
                });
                await Task.Delay(1);
                bool firstClicked = true;

                //Step2: Check Next
                int prevIndex = ElemIndex;
                int nextElemRow = ElemRow + 1;
                int nextIndex = ElemIndex + sizeCol;
                for (int a = nextElemRow; a < sizeRow; a++)
                {
                    //Checking next in Row (If next already have value then ignore)
                    if (((Grid)((Grid)element.Parent).Children[nextIndex]).Children.Count > 0)
                    {
                        break;
                    }
                    else
                    {
                        firstClicked = false;
                        //Remove Previous
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            ((Grid)((Grid)element.Parent).Children[prevIndex]).Children.Clear();
                        });

                        //Add New
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            ((Grid)((Grid)element.Parent).Children[nextIndex]).Children.Add(Piece());
                        });
                        await Task.Delay(1);

                        prevIndex = nextIndex;
                        nextElemRow = a;
                    }

                    nextIndex += sizeCol;
                }

                //StepLast: Add in Board
                if (firstClicked)
                    _board[ElemRow, ElemCol] = _player;
                else
                    _board[nextElemRow, ElemCol] = _player;

                //-----------------------------------------------------------------------
                if (FourInRowLogic.Winner(_board, _player))
                {
                    _won = true;
                    Show($"{_player} wins!", app_title);
                    PlayerWinningStatus();
                }
                else if (FourInRowLogic.Drawn(_board))
                {
                    Show("Draw!", app_title);
                    PlayerWinningStatus();
                }
                else
                {
                    _player = (_player == Blue ? Red : Blue); // Swap Players
                    PlayerTurnShift();
                }
            }
            else
            {
                Show("Game Over!", app_title);
            }

        }

        private async void Grid_Tapped(object sender, EventArgs e)
        {
            if (!_won)
            {
                Grid element = (Grid)sender;
                if ((element.Children.Count < 1))
                {
                    int ElemRow = (int)element.GetValue(Grid.RowProperty);
                    int ElemCol = (int)element.GetValue(Grid.ColumnProperty);
                    int ElemIndex = (ElemRow * sizeCol) + ElemCol;

                    //Step1: Add Piece
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        element.Children.Add(Piece());
                    });
                    await Task.Delay(1);
                    bool firstClicked = true;

                    //Step2: Check Next
                    int prevIndex = ElemIndex;
                    int nextElemRow = ElemRow + 1;
                    int nextIndex = ElemIndex + sizeCol;
                    for (int a = nextElemRow; a < sizeRow; a++)
                    {
                        //Checking next in Row (If next already have value then ignore)
                        if (((Grid)((Grid)element.Parent).Children[nextIndex]).Children.Count > 0)
                        {
                            break;
                        }
                        else
                        {
                            firstClicked = false;
                            //Remove Previous
                            MainThread.BeginInvokeOnMainThread(() =>
                            {
                                ((Grid)((Grid)element.Parent).Children[prevIndex]).Children.Clear();
                            });

                            //Add New
                            MainThread.BeginInvokeOnMainThread(() =>
                            {
                                ((Grid)((Grid)element.Parent).Children[nextIndex]).Children.Add(Piece());
                            });
                            await Task.Delay(1);

                            prevIndex = nextIndex;
                            nextElemRow = a;
                        }

                        nextIndex += sizeCol;
                    }

                    //StepLast: Add in Board
                    if (firstClicked)
                        _board[ElemRow, ElemCol] = _player;
                    else
                        _board[nextElemRow, ElemCol] = _player;

                    //-----------------------------------------------------------------------
                    if (FourInRowLogic.Winner(_board, _player))
                    {
                        _won = true;
                        Show($"{_player} wins!", app_title);
                        PlayerWinningStatus();
                    }
                    else if (FourInRowLogic.Drawn(_board))
                    {
                        Show("Draw!", app_title);
                        PlayerWinningStatus();
                    }
                    else
                    {
                        _player = (_player == Blue ? Red : Blue); // Swap Players
                        PlayerTurnShift();
                        await Task.Delay(100);
                        AutoTurnExecute(sender);
                    }
                }
            }
            else
            {
                Show("Game Over!", app_title);
            }
        }

        private void Add(ref Grid grid, int row, int column)
        {
            Grid element = new Grid()
            {
                HeightRequest = 40,
                WidthRequest = 40,
                Margin = new Thickness(0.1),
                BackgroundColor = Color.WhiteSmoke
            };

            TapGestureRecognizer tapped = new TapGestureRecognizer();
            tapped.Tapped += Grid_Tapped;

            element.GestureRecognizers.Add(tapped);
            element.SetValue(Grid.ColumnProperty, column);
            element.SetValue(Grid.RowProperty, row);
            grid.Children.Add(element);
        }

        private void Layout(ref Grid grid)
        {
            grid.Children.Clear();
            grid.ColumnDefinitions.Clear();
            grid.RowDefinitions.Clear();
            _PlayerTurn.Text = "";
            _PlayerWin.Text = "";
            // Setup Grid
            for (int index = 0; (index < sizeRow); index++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            for (int index = 0; (index < sizeCol); index++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            // Setup Board
            for (int row = 0; (row < sizeRow); row++)
            {
                for (int column = 0; (column < sizeCol); column++)
                {
                    Add(ref grid, row, column);
                    _board[row, column] = blank;
                }
            }
        }

        public void New(ContentPage page, Grid grid, DifficultyLevel difficultyLevel, Label playerTurnLabel, Label playerWinLabel)
        {
            _page = page;
            _PlayerTurn = playerTurnLabel;
            _PlayerWin = playerWinLabel;
            _DifficultyLevel = difficultyLevel;
            Layout(ref grid);
            _won = false;
            _player = Blue;
            _YourColor = _player;
            PlayerTurnShift();
        }

        private void PlayerTurnShift()
        {
            if (_player == Blue)
            {
                _PlayerTurn.Text = "BLUE";
                _PlayerTurn.BackgroundColor = Color.FromHex("#2196F3");
            }
            else
            {
                _PlayerTurn.Text = "RED";
                _PlayerTurn.BackgroundColor = Color.Crimson;
            }
        }

        private void PlayerWinningStatus()
        {
            if (_won)
            {
                _PlayerWin.Text = (_player == _YourColor) ? "YOU WIN" : "YOU LOSE";
                _PlayerWin.TextColor = (_player == Blue) ? Color.FromHex("#2196F3") : Color.Crimson;
            }
            else
            {
                _PlayerWin.Text = "GAME DRAW";
                _PlayerWin.TextColor = Color.Gray;
            }
        }
    }
}
