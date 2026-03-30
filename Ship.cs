namespace Battleship;

/// <summary>
/// This class is responsible for containing information about a ship.
/// </summary>
public class Ship
{
    private int _numberOfParts;
    private readonly ShipType _shipType;

    /// <summary>
    /// Constructor for a ship. 
    /// </summary>
    /// <param name="shipType">The type of ship to create.</param>
    public Ship(ShipType shipType)
    {
        _numberOfParts = GetShipSize(shipType);
        _shipType = shipType;
    }

    /// <summary>
    /// The type of ship.
    /// </summary>
    public ShipType Type { get => _shipType; }

    /// <summary>
    /// Whether the ship is sunk.
    /// </summary>
    public bool IsSunk { get => _numberOfParts < 1; }

    /// <summary>
    /// Delivers one hit to the ship.
    /// </summary>
    public void Hit()
    {
        _numberOfParts--;
    }

    /// <summary>
    /// Gets the number of parts that a specific ship has.
    /// </summary>
    /// <param name="shipType">The ship type.</param>
    /// <returns>The number of parts for the ship type.</returns>
    /// <exception cref="NotImplementedException">Thrown if an invalid enum is passed.</exception>
    public static int GetShipSize(ShipType shipType)
    {
        return shipType switch
        {
            ShipType.ONE => 1,
            ShipType.TWO => 2,
            ShipType.THREE => 3,
            ShipType.FOUR => 4,
            ShipType.FIVE => 5,
            _ => throw new NotImplementedException(),
        };
    }

}
