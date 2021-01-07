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
    public int SeedsGained { get; private set; }

    public bool Sided { get; private set; }

    public bool Challenged { get; private set; }


    /// <summary>
    /// Actual state of the level.
    /// </summary>
    public LevelState State { get; private set; }


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="newSeedsGained">Number of seeds gained (related to lives lost)</param>
    /// <param name="newLevelState">New state</param>
    /// <param name="sided">Side level finished?</param>
    /// <param name="challenged">Challenge level finished?</param>
    public LevelSave(int newSeedsGained, LevelState newLevelState, bool sided, bool challenged)
    {
        SeedsGained = newSeedsGained;
        State = newLevelState;

        Sided = sided;
        Challenged = challenged;
    }
}