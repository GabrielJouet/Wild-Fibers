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
}
