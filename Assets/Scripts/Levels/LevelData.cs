using UnityEngine;

/// <summary>
/// Class used to handles level data for classic, side and challenge ones.
/// </summary>
[CreateAssetMenu(menuName = "Levels/Data")]
public class LevelData : ScriptableObject
{
    /// <summary>
    /// Classic level data.
    /// </summary>
    [SerializeField]
    private Level _classic;
    public Level Classic { get => _classic; }

    /// <summary>
    /// Side level data.
    /// </summary>
    [SerializeField]
    private Level _side;
    public Level Side { get => _side; }

    /// <summary>
    /// Challenge level data.
    /// </summary>
    [SerializeField]
    private Level _challenge;
    public Level Challenge { get => _challenge; }


    /// <summary>
    /// Method used to check if the level exists in this data.
    /// </summary>
    /// <param name="levelChecked">The level to check</param>
    /// <returns>Returns true if found, false otherwise</returns>
    public bool LevelExists(Level levelChecked)
    {
        return levelChecked == Classic || levelChecked == Side || levelChecked == Challenge;
    }
}
