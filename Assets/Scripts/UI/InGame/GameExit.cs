using UnityEngine;

/// <summary>
/// Class used to handles exit game.
/// </summary>
public class GameExit : MonoBehaviour
{
    /// <summary>
    /// Method used to exit the game.
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}