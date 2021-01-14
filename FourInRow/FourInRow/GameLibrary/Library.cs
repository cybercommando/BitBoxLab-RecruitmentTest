using System.Threading.Tasks;
using Xamarin.Forms;

public class Library
{
    private const string app_title = "Four In A Row !!!";
    private const char blank = ' ';
    private const char nought = 'R';
    private const char cross = 'B';
    private const int size = 8;

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

    private async Task<bool> ConfirmAsync(string content,
        string title, string ok, string cancel)
    {
        return await _page.DisplayAlert(title, content, ok, cancel);
    }

    private bool Winner()
    {
        return false;
    }

    private bool Drawn()
    {
        return false;
    }

    private Grid Piece()
    {
        Grid grid = new Grid()
        {
            HeightRequest = 30,
            WidthRequest = 30,
        };
        if (_piece == cross)
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
        else if (_piece == nought)
        {
            BoxView Dot = new BoxView()
            {
                Color = Color.Black,
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
            BackgroundColor = Color.WhiteSmoke,
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
                        _piece = (_piece == cross ? nought : cross); // Swap Players
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
        _piece = await ConfirmAsync("Who goes First?", app_title,
            nought.ToString(), cross.ToString()) ? nought : cross;
    }
}