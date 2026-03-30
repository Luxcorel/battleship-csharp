namespace Battleship;

/// <summary>
/// This class is responsible for the game board UI of the application.
/// </summary>
public partial class GameBoard : Form
{
    const float HundredPercent = 100f;
    private readonly int _rows;
    private readonly int _columns;

    private readonly Controller _controller;

    private readonly Button[,] _playerOneButtons;
    private readonly Button[,] _playerTwoButtons;
    private bool _firingOnPlayerTwoBoard;

    /// <summary>
    /// The constructor for the game board.
    /// </summary>
    /// <param name="controller">The controller to communicate with.</param>
    public GameBoard(Controller controller)
    {
        _controller = controller;
        _rows = controller.Rows;
        _columns = controller.Columns;

        _playerOneButtons = new Button[_rows, _columns];
        _playerTwoButtons = new Button[_rows, _columns];

        InitializeComponent();
        InitializeGui();
        ResetGame();
    }

    /// <summary>
    /// Sets up the UI.
    /// </summary>
    private void InitializeGui()
    {
        // Disable resizing the window.
        MaximizeBox = false;
        FormBorderStyle = FormBorderStyle.FixedSingle;

        StartPosition = FormStartPosition.CenterScreen;

        BackColor = CatppuccinMacchiato.Surface0;
        lblPlayerOneBoard.ForeColor = CatppuccinMacchiato.Text;
        lblPlayerTwoBoard.ForeColor = CatppuccinMacchiato.Text;

        // Left grid setup.
        panelLeft.RowCount = _rows;
        panelLeft.ColumnCount = _columns;
        panelLeft.RowStyles.Clear();
        panelLeft.ColumnStyles.Clear();
        for (int i = 0; i < panelLeft.RowCount; i++)
        {
            panelLeft.RowStyles.Add(new RowStyle(SizeType.Percent, HundredPercent / panelLeft.RowCount));
        }
        for (int i = 0; i < panelLeft.ColumnCount; i++)
        {
            panelLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, HundredPercent / panelLeft.ColumnCount));
        }

        // Right grid setup.
        panelRight.RowCount = _rows;
        panelRight.ColumnCount = _columns;
        panelRight.RowStyles.Clear();
        panelRight.ColumnStyles.Clear();
        for (int i = 0; i < panelRight.RowCount; i++)
        {
            panelRight.RowStyles.Add(new RowStyle(SizeType.Percent, HundredPercent / panelRight.RowCount));
        }
        for (int i = 0; i < panelRight.ColumnCount; i++)
        {
            panelRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, HundredPercent / panelRight.ColumnCount));
        }

        SetupBoard(panelLeft, _playerOneButtons, Player.ONE);
        SetupBoard(panelRight, _playerTwoButtons, Player.TWO);
    }

    /// <summary>
    /// Sets up an individual board with styling and listeners.
    /// </summary>
    /// <param name="panel">The table layout that the board is part of.</param>
    /// <param name="board">The button 2D array to use.</param>
    /// <param name="boardOwner">The player that owns the board.</param>
    private void SetupBoard(TableLayoutPanel panel, Button[,] board, Player boardOwner)
    {
        for (int row = 0; row < board.GetLength(0); row++)
        {
            for (int column = 0; column < board.GetLength(1); column++)
            {
                // Variables used to avoid stale closures.
                int currentRow = row;
                int currentColumn = column;

                Button button = new Button
                {
                    BackColor = CatppuccinMacchiato.Overlay1,
                    Dock = DockStyle.Fill,
                    Text = "",
                    FlatStyle = FlatStyle.Flat,
                    TabStop = false
                };
                panel.Controls.Add(button, column, row);
                board[row, column] = button;
                button.Click += (sender, e) =>
                {
                    ButtonListener(boardOwner, button, currentRow, currentColumn);
                };
            }
        }
    }

    /// <summary>
    /// Resets the board to its original state.
    /// </summary>
    /// <param name="board">The board to reset.</param>
    private static void ResetBoard(Button[,] board)
    {
        for (int row = 0; row < board.GetLength(0); row++)
        {
            for (int column = 0; column < board.GetLength(1); column++)
            {
                board[row, column].BackColor = CatppuccinMacchiato.Overlay1;
                board[row, column].Enabled = true;
                board[row, column].Text = "";
            }
        }
    }

    /// <summary>
    /// Resets the game to its orignal state.
    /// </summary>
    private void ResetGame()
    {
        _controller.ResetGame();
        _firingOnPlayerTwoBoard = true;

        ResetBoard(_playerOneButtons);
        ResetBoard(_playerTwoButtons);
    }

    /// <summary>
    /// Listener for the game buttons. Each button corresponds to a positon on the grid.
    /// </summary>
    /// <param name="boardOwner">The owner of the board which contains the button.</param>
    /// <param name="button">The button that was clicked.</param>
    /// <param name="row">The row of the button.</param>
    /// <param name="column">The column of the button.</param>
    private void ButtonListener(Player boardOwner, Button button, int row, int column)
    {
        if (boardOwner == Player.ONE && _firingOnPlayerTwoBoard || boardOwner == Player.TWO && !_firingOnPlayerTwoBoard)
        {
            MessageBox.Show($"It's player {(_firingOnPlayerTwoBoard ? "one" : "two")}'s turn!");
            return;
        }

        button.Enabled = false;

        GameBoardActionResult result;
        try
        {
            result = _controller.HitPosition(row, column);
        }
        catch (ArgumentException e)
        {
            button.Enabled = true;
            MessageBox.Show($"Internal error: {e.Message}");
            return;
        }
        _firingOnPlayerTwoBoard = !_firingOnPlayerTwoBoard;

        (bool wasShipHit, bool wasShipSunk, int shipSize, List<(int x, int y)> discoveredCoordinates) = result;
        if (!wasShipHit)
        {
            button.BackColor = CatppuccinMacchiato.Surface0;
            return;
        }

        button.Text = $"{shipSize}";
        button.BackColor = CatppuccinMacchiato.Teal;

        if (!wasShipSunk) return;

        if (boardOwner == Player.ONE)
        {
            foreach ((int x, int y) in discoveredCoordinates)
            {
                _playerOneButtons[x, y].BackColor = CatppuccinMacchiato.Red;
            }
        }
        else
        {
            foreach ((int x, int y) in discoveredCoordinates)
            {
                _playerTwoButtons[x, y].BackColor = CatppuccinMacchiato.Red;
            }
        }

        if (_controller.IsGameOver)
        {
            Player winner = _controller.GetWhoWon();
            DialogResult dialogResult = MessageBox.Show($"Player {winner.ToPrettyString()} Won! Play Again?", "Game Over", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                ResetGame();
            }
            else
            {
                Close();
            }
        }
    }
}
