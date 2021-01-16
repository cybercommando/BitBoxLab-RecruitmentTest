using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FourInRow.GameLibrary
{
    class ComplayerLibrary
    {
        private const string app_title = "Four In A Row !!!";
        private const char blank = ' ';
        private const char Red = 'R';
        private const char Blue = 'B';
        private const int size = 7;

        private ContentPage _page;
        private Label _PlayerTurn;
        private Label _PlayerWin;
        private bool _won = false;
        private char _player = blank;
        private char[,] _board = new char[size, size];

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
                    Color = Color.Blue,
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
            int _Row = 0;
            int _Col = 0;
            int _Index = 0;
            do
            {
                Random _random = new Random();
                _Row = _random.Next(0, size - 1);
                _Col = _random.Next(0, size - 1);
                _Index = (_Row * size) + _Col;

                if (_board[_Row, _Col] == blank)
                {
                    break;
                }
            } while (true);

            if (!_won)
            {
                Grid element = (Grid)sender;
                if ((element.Children.Count < 1))
                {
                    int ElemRow = _Row;
                    int ElemCol = _Col;
                    int ElemIndex = (ElemRow * size) + ElemCol;

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
                    int nextIndex = ElemIndex + size;
                    for (int a = nextElemRow; a < size; a++)
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

                        nextIndex += size;
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
                    int ElemIndex = (ElemRow * size) + ElemCol;

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
                    int nextIndex = ElemIndex + size;
                    for (int a = nextElemRow; a < size; a++)
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

                        nextIndex = nextIndex + size;
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
            for (int index = 0; (index < size); index++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            // Setup Board
            for (int row = 0; (row < size); row++)
            {
                for (int column = 0; (column < size); column++)
                {
                    Add(ref grid, row, column);
                    _board[row, column] = blank;
                }
            }
        }

        public async void New(ContentPage page, Grid grid, Label playerTurnLabel, Label playerWinLabel)
        {
            _page = page;
            _PlayerTurn = playerTurnLabel;
            _PlayerWin = playerWinLabel;
            Layout(ref grid);
            _won = false;
            _player = await ConfirmAsync("Who goes First?", app_title, "Red", "Blue") ? Red : Blue;
            PlayerTurnShift();
        }

        private void PlayerTurnShift()
        {
            if (_player == Blue)
            {
                _PlayerTurn.Text = "BLUE";
                _PlayerTurn.BackgroundColor = Color.Blue;
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
                _PlayerWin.Text = (_player == Blue) ? "BLUE WINS" : "RED WINS";
                _PlayerWin.TextColor = (_player == Blue) ? Color.Blue : Color.Crimson;
            }
            else
            {
                _PlayerWin.Text = "GAME DRAW";
                _PlayerWin.TextColor = Color.Gray;
            }
        }
    }
}
