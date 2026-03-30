namespace Battleship;

/// <summary>
/// This enum contains the two players in the game.
/// </summary>
public enum Player
{
    ONE,
    TWO
}

/// <summary>
/// This extension class is responsible for providing extra features to the Player enum.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Prints the player enum with the first letter capitalized and the following letters in lower case.
    /// </summary>
    /// <param name="player">The player enum to format.</param>
    /// <returns>Formatted string representation.</returns>
    public static string ToPrettyString(this Player player)
    {
        string str = player.ToString().ToLower();
        if (str.Length < 2)
        {
            return str.ToUpper();
        }

        return $"{char.ToUpper(str[0])}{str[1..]}";
    }
}
