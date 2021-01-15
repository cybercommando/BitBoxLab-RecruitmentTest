using FourInRow.GameLibrary;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

public class Library
{
    private const string app_title = "Four In A Row !!!";
    private const char blank = ' ';
    private const char Red = 'R';
    private const char Blue = 'B';
    private const int size = 7;

    private ContentPage _page;
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
                Color = Color.Red,
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

    private void Grid_Tapped(object sender, EventArgs e)
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
                element.Children.Add(Piece());

                //Step2: Check Next
                int prevIndex = ElemIndex;
                int nextElemRow = ElemRow + 1;
                int nextIndex = ElemIndex + size;
                for (int a = nextElemRow; a < size; a++)
                {
                    if (((Grid)((Grid)element.Parent).Children[nextIndex]).Children.Count > 0)
                    {
                        break;
                    }
                    else
                    {
                        //Remove Previous
                        ((Grid)((Grid)element.Parent).Children[prevIndex]).Children.Clear();
                        //Add New
                        ((Grid)((Grid)element.Parent).Children[nextIndex]).Children.Add(Piece());
                        prevIndex = nextIndex;
                        nextElemRow = a;
                    }

                    nextIndex = nextIndex + size;
                }

                //StepLast: Add in Board
                //_board[ElemRow, ElemCol] = _player;
                _board[nextElemRow, ElemCol] = _player;

                //-----------------------------------------------------------------------

                

                //Grid elm = new Grid()
                //{
                //    HeightRequest = 40,
                //    WidthRequest = 40,
                //    Margin = new Thickness(0.1),
                //    BackgroundColor = Color.WhiteSmoke
                //};
                //TapGestureRecognizer tapped = new TapGestureRecognizer();
                //tapped.Tapped += Grid_Tapped;
                //elm.GestureRecognizers.Add(tapped);
                //elm.SetValue(Grid.ColumnProperty, (int)element.GetValue(Grid.ColumnProperty));
                //elm.SetValue(Grid.RowProperty, (int)element.GetValue(Grid.RowProperty) + 1);
                //elm.Children.Add(Piece());
                //((Grid)element.Parent).Children.Add(elm);

                //int i = (int)element.GetValue(Grid.RowProperty) + 1;
                //while (i < size)
                //{
                //    if (((Grid)((Grid)element.Parent).Children[i + size]).Children.Count <= 0)
                //    {
                //        ((Grid)((Grid)element.Parent).Children[i + size]).Children.Add(Piece());
                //    }
                //    else
                //    {
                //        break;
                //    }
                //}

                //var y = ((Grid)((Grid)element.Parent).Children[0]).Children.Count;
                
                //((Grid)((Grid)element.Parent).Children[1]).Children.Count
                //Animation Algorithm
                //int i = (int)element.GetValue(Grid.RowProperty) + 1;
                //do
                //{
                //    Grid elm = new Grid()
                //    {
                //        HeightRequest = 40,
                //        WidthRequest = 40,
                //        Margin = new Thickness(0.1),
                //        BackgroundColor = Color.WhiteSmoke
                //    };

                //    TapGestureRecognizer tapped = new TapGestureRecognizer();
                //    tapped.Tapped += Grid_Tapped;

                //    elm.GestureRecognizers.Add(tapped);
                //    elm.SetValue(Grid.ColumnProperty, (int)element.GetValue(Grid.ColumnProperty));
                //    //elm.SetValue(Grid.RowProperty, (int)element.GetValue(Grid.RowProperty) + 1);
                //    elm.SetValue(Grid.RowProperty, i);
                //    elm.Children.Add(Piece());
                //    ((Grid)element.Parent).Children.Add(elm);

                //    i++;
                //} while (i < size);
                //


                //-----------------------------------------------------------------------
                if (FourInRowLogic.Winner(_board, _player))
                {
                    _won = true;
                    Show($"{_player} wins!", app_title);
                }
                else if (FourInRowLogic.Drawn(_board))
                {
                    Show("Draw!", app_title);
                }
                else
                {
                    _player = (_player == Blue ? Red : Blue); // Swap Players
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

    public async void New(ContentPage page, Grid grid)
    {
        _page = page;
        Layout(ref grid);
        _won = false;
        _player = await ConfirmAsync("Who goes First?", app_title, "Red", "Blue") ? Red : Blue;
    }

}