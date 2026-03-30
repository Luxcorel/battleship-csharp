namespace Battleship;

/// <summary>
/// This class is responsible for containing information about a game board. 
/// </summary>
public class GameBoardGrid
{
    private const int MaxGenerationAttempts = 100;

    private readonly List<Ship> _ships;
    private readonly int _rows;
    private readonly int _columns;
    private readonly Ship[,] _shipGrid;
    private readonly bool[,] _hitGrid;

    private int _numberOfMoves;
    private int _shipsLeft;

    /// <summary>
    /// Constructor for a game board grid. 
    /// </summary>
    /// <param name="ships">The ship types for the ships to populate the game board with.</param>
    /// <param name="rows">The amount of rows to use for the grid.</param>
    /// <param name="columns">The amount of columns to use for the grid.</param>
    /// <exception cref="InvalidOperationException">Thrown if the ship placement fails.</exception>
    public GameBoardGrid(List<ShipType> ships, int rows, int columns)
    {
        _ships = new List<Ship>(ships.Count);
        foreach (ShipType shipType in ships)
        {
            _ships.Add(new Ship(shipType));
        }

        _rows = rows;
        _columns = columns;

        _shipGrid = new Ship[_rows, _columns];
        _hitGrid = new bool[_rows, _columns];
        _numberOfMoves = 0;
        _shipsLeft = ships.Count;

        GenerateShips();
    }

    /// <summary>
    /// The number of moves that has been played on the board.
    /// </summary>
    public int NumberOfMoves { get => _numberOfMoves; }

    /// <summary>
    /// The number of ships that remain on the board.
    /// </summary>
    public int ShipsLeft { get => _shipsLeft; }

    /// <summary>
    /// Delivers a hit to the specified position, and returns a result describing the result of the hit.
    /// </summary>
    /// <param name="row">The row to hit.</param>
    /// <param name="column">The column to hit.</param>
    /// <returns>A result describing the result of the hit.</returns>
    /// <exception cref="ArgumentException">Thrown if the coordinates already are hit or are invalid.</exception>
    public GameBoardActionResult HitPosition(int row, int column)
    {
        if (!IsValidPosition(row, column)) throw new ArgumentException("Invalid position given.");
        if (_hitGrid[row, column] == true) throw new ArgumentException("The specified position has already been hit.");

        _numberOfMoves++;
        _hitGrid[row, column] = true;

        if (IsOccupied(row, column))
        {
            Ship ship = _shipGrid[row, column];
            ship.Hit();

            bool isShipSunk = ship.IsSunk;
            if (isShipSunk) _shipsLeft--;

            List<(int x, int y)> discoveredCoordinates = [];
            for (int i = 0; i < _shipGrid.GetLength(0); i++)
            {
                for (int j = 0; j < _shipGrid.GetLength(1); j++)
                {
                    Ship? potentialShip = _shipGrid[i, j];
                    if (potentialShip != null && potentialShip == ship)
                    {
                        discoveredCoordinates.Add((i, j));
                    }
                }
            }

            return new GameBoardActionResult(WasShipHit: true, WasShipSunk: isShipSunk, ShipSize: Ship.GetShipSize(ship.Type), DiscoveredCoordinates: discoveredCoordinates);
        }
        else
        {
            return new GameBoardActionResult(WasShipHit: false, WasShipSunk: false, ShipSize: -1, DiscoveredCoordinates: []);
        }
    }

    /// <summary>
    /// Randomly places the instance's ships on the board.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the ship placement fails.</exception>
    private void GenerateShips()
    {
        Random random = new Random();

        for (int boardGenerationAttempt = 0; boardGenerationAttempt < MaxGenerationAttempts; boardGenerationAttempt++)
        {
            bool boardFailed = false;
            Array.Clear(_shipGrid);

            foreach (Ship shipToPlace in _ships)
            {
                int shipSize = Ship.GetShipSize(shipToPlace.Type);
                bool placeShipVertically = random.Next(2) == 1;
                bool movePositiveDirection = random.Next(2) == 1;

                List<(int row, int column)> coordinatesToUse = [];
                (int row, int column) coordinatesToTry = (row: random.Next(_rows), column: random.Next(_columns));
                bool foundCoordinates = false;
                for (int generationAttempt = 0; generationAttempt < MaxGenerationAttempts; generationAttempt++)
                {
                    if (!IsValidPosition(coordinatesToTry.row, coordinatesToTry.column) || IsOccupied(coordinatesToTry.row, coordinatesToTry.column))
                    {
                        coordinatesToUse.Clear();
                        coordinatesToTry = (row: random.Next(_rows), column: random.Next(_columns));
                        continue;
                    }

                    coordinatesToUse.Add(coordinatesToTry);
                    if (coordinatesToUse.Count == shipSize)
                    {
                        foundCoordinates = true;
                        break;
                    }

                    if (placeShipVertically)
                    {
                        if (movePositiveDirection) coordinatesToTry.row++;
                        else coordinatesToTry.row--;
                    }
                    else
                    {
                        if (movePositiveDirection) coordinatesToTry.column++;
                        else coordinatesToTry.column--;
                    }
                }

                if (!foundCoordinates)
                {
                    boardFailed = true;
                    break;
                }

                foreach ((int row, int column) coordinate in coordinatesToUse)
                {
                    (int row, int column) = coordinate;
                    _shipGrid[row, column] = shipToPlace;
                }
            }

            if (!boardFailed) return;
        }

        throw new InvalidOperationException("Unable to place all ships on the board");
    }

    /// <summary>
    /// Gets whether the specified position is in bounds.
    /// </summary>
    /// <param name="row">The row to check.</param>
    /// <param name="column">The column to check.</param>
    /// <returns>Whether the position is in bounds.</returns>
    private bool IsValidPosition(int row, int column)
    {
        return row >= 0 && column >= 0 && row < _shipGrid.GetLength(0) && column < _shipGrid.GetLength(1);
    }

    /// <summary>
    /// Gets whether a valid board position is occupied by a ship.
    /// </summary>
    /// <param name="row">The row to check.</param>
    /// <param name="column">The column to check.</param>
    /// <returns>Whether the position is occupied.</returns>
    /// <exception cref="ArgumentException">Thrown if an out of bounds position is provided.</exception>
    private bool IsOccupied(int row, int column)
    {
        if (!IsValidPosition(row, column))
        {
            throw new ArgumentException("Out of bounds position was provided.");
        }

        return _shipGrid[row, column] != null;
    }

}
