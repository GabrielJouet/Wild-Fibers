using Levels;
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

    /// <summary>
    /// Classic level data.
    /// </summary>
    public Level Classic { get => _classic; }


    /// <summary>
    /// Side level data.
    /// </summary>
    [SerializeField]
    private Level _side;

    /// <summary>
    /// Side level data.
    /// </summary>
    public Level Side { get => _side; }


    /// <summary>
    /// Challenge level data.
    /// </summary>
    [SerializeField]
    private Level _challenge;

    /// <summary>
    /// Challenge level data.
    /// </summary>
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
