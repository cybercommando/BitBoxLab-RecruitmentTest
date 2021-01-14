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
    private char _piece = blank;
    private char[,] _board = new char[size, size];

    public void Show(string content, string title)
    {
        Device.BeginInvokeOnMainThread(() => {
            _page.DisplayAlert(title, content, "Ok");
        });
    }

    private async Task<bool> ConfirmAsync(string content, string title, string ok, string cancel)
    {
        return await _page.DisplayAlert(title, content, ok, cancel);
    }


    private bool Winner()
    {
        return false;
        //return checkVertical(_board, _piece)
        //  || checkHorizontal(_board, _piece)
        //  || checkDiagonal1(_board, _piece)
        //  || checkDiagonal2(_board, _piece);
    }

    private bool Drawn()
    {
        return false;
        //bool decision = false;
        //for (int i = 0; i < size; i++)
        //{
        //    for (int j = 0; j < size; j++)
        //    {
        //        if (_board[i, j] == blank)
        //        {
        //            return false;
        //        }
        //    }
        //}

        //return decision;
    }

    private Grid Piece()
    {
        Grid grid = new Grid()
        {
            HeightRequest = 30,
            WidthRequest = 30,
        };
        if (_piece == Blue)
        {
            BoxView Dot = new BoxView()
            {
                Color = Color.Blue,
                HeightRequest = 20,
                WidthRequest = 20,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                CornerRadius = 10
            };
            grid.Children.Add(Dot);
        }
        else if (_piece == Red)
        {
            BoxView Dot = new BoxView()
            {
                Color = Color.Red,
                HeightRequest = 20,
                WidthRequest = 20,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                CornerRadius = 10
            };
            grid.Children.Add(Dot);
        }
        return grid;
    }

    private void Add(ref Grid grid, int row, int column)
    {
        Grid element = new Grid()
        {
            HeightRequest = 30,
            WidthRequest = 30,
            Margin = new Thickness(0.1),
            BackgroundColor = Color.WhiteSmoke
        };
        TapGestureRecognizer tapped = new TapGestureRecognizer();
        tapped.Tapped += (sender, e) =>
        {
            if (!_won)
            {
                element = (Grid)sender;
                if ((element.Children.Count < 1))
                {
                    element.Children.Add(Piece());
                    _board[(int)element.GetValue(Grid.RowProperty),
                    (int)element.GetValue(Grid.ColumnProperty)] = _piece;

                    if (Winner())
                    {
                        _won = true;
                        Show($"{_piece} wins!", app_title);
                    }
                    else if (Drawn())
                    {
                        Show("Draw!", app_title);
                    }
                    else
                    {
                        _piece = (_piece == Blue ? Red : Blue); // Swap Players
                    }
                }
            }
            else
            {
                Show("Game Over!", app_title);
            }
        };
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
        _piece = await ConfirmAsync("Who goes First?", app_title, "Red", "Blue") ? Red : Blue;
    }

    //#region GameLogic
    //private bool checkVertical(char[,] field, char player)
    //{
    //    for (int i = 0; i < size; ++i)
    //    {
    //        if (field[0][i] == player
    //            && field[1][i] == player
    //            && field[2][i] == player
    //            && field[3][i] == player
    //        ) return true;

    //        if (field[1][i] == player
    //            && field[2][i] == player
    //            && field[3][i] == player
    //            && field[4][i] == player
    //        ) return true;
    //    }
    //    return false;
    //}

    //private bool checkHorizontal(char[,] field, char player)
    //{
    //    for (i = 0; i < 5; ++i)
    //    {
    //        if (field[i][0] == player
    //            && field[i][1] == player
    //            && field[i][2] == player
    //            && field[i][3] == player
    //        ) return true;

    //        if (field[i][1] === player
    //            && field[i][2] == player
    //            && field[i][3] == player
    //            && field[i][4] == player
    //        ) return true;
    //    }
    //    return false;
    //}

    //private bool checkDiagonal1(char[,] field, char player)
    //{
    //    // exercise for the reader
    //    return false;
    //}

    //private bool checkDiagonal2(char[,] field, char player)
    //{
    //    // exercise for the reader
    //    return false;
    //}
    //#endregion
}