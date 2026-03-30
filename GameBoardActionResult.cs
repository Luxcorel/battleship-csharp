namespace Battleship;

/// <summary>
/// A result from a hit to a position on the <see cref="GameBoardGrid"/>.
/// </summary>
/// <param name="WasShipHit">Whether a ship was hit.</param>
/// <param name="WasShipSunk">Whether a ship was sunk.</param>
/// <param name="ShipSize">If a ship was hit, the size of the hit ship, -1 otherwise.</param>
/// <param name="DiscoveredCoordinates">If a ship was hit, the currently discovered coordinates of the ship, otherwise an empty list.</param>
public record GameBoardActionResult(bool WasShipHit, bool WasShipSunk, int ShipSize, List<(int x, int y)> DiscoveredCoordinates);
