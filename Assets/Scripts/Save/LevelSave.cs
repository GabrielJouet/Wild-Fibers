using System;

/// <summary>
/// Class used to save one level progression.
/// </summary>
[Serializable]
public class LevelSave
{
    /// <summary>
    /// Number of lives lost on this level.
    /// </summary>
    public int LivesLost { get; private set; }

    /// <summary>
    /// Actual state of the level.
    /// </summary>
    public LevelState State { get; private set; }



    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="newLivesLost">Number of lives lost</param>
    /// <param name="newLevelState">New state</param>
    public LevelSave(int newLivesLost, LevelState newLevelState)
    {
        LivesLost = newLivesLost;
        State = newLevelState;
    }
}