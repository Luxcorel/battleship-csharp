namespace Battleship;

/// <summary>
/// This class is responsible for communication between the model and view classes in the program.
/// </summary>
public class Controller
{
    private readonly int _rows = 9;
    private readonly int _columns = 9;

    private readonly List<ShipType> _ships =
    [
        ShipType.ONE,
        ShipType.TWO,
        ShipType.THREE,
        ShipType.FOUR,
        ShipType.FIVE,
    ];

    private bool _isGameOver;
    private Player _lastPlayer;
    private GameBoardGrid _playerOneBoard;
    private GameBoardGrid _playerTwoBoard;

    public Controller()
    {
        _playerOneBoard = new GameBoardGrid(_ships, _rows, _columns);
        _playerTwoBoard = new GameBoardGrid(_ships, _rows, _columns);
        _isGameOver = false;
        _lastPlayer = Player.TWO;
    }

    /// <summary>
    /// Whether the game is over.
    /// </summary>
    public bool IsGameOver { get => _isGameOver; }

    /// <summary>
    /// The number of rows for the game board.
    /// </summary>
    public int Rows { get => _rows; }

    /// <summary>
    /// The number of columns for the game board.
    /// </summary>
    public int Columns { get => _columns; }

    /// <summary>
    /// Resets the game state.
    /// </summary>
    public void ResetGame()
    {
        _playerOneBoard = new GameBoardGrid(_ships, _rows, _columns);
        _playerTwoBoard = new GameBoardGrid(_ships, _rows, _columns);
        _isGameOver = false;
        _lastPlayer = Player.TWO;
    }

    /// <summary>
    /// Gets the player that was last playing.
    /// </summary>
    /// <returns></returns>
    public Player GetWhoWon()
    {
        return _lastPlayer;
    }

    /// <summary>
    /// Deliver a hit to the specifed position.
    /// </summary>
    /// <param name="row">The row to hit.</param>
    /// <param name="column">The column to hit.</param>
    /// <returns>The result of the hit.</returns>
    /// <exception cref="ArgumentException">Thrown if the coordinates already are hit or are invalid.</exception>
    public GameBoardActionResult HitPosition(int row, int column)
    {
        Player currentPlayer = _lastPlayer == Player.ONE ? Player.TWO : Player.ONE;

        GameBoardActionResult result;
        if (currentPlayer == Player.ONE)
        {
            result = _playerTwoBoard.HitPosition(row, column);
            if (result.WasShipSunk) _isGameOver = _playerTwoBoard.ShipsLeft == 0;
        }
        else
        {
            result = _playerOneBoard.HitPosition(row, column);
            if (result.WasShipSunk) _isGameOver = _playerOneBoard.ShipsLeft == 0;
        }

        _lastPlayer = currentPlayer;

        return result;
    }

}
