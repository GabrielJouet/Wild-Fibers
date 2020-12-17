using System;
using System.Collections.Generic;

/// <summary>
/// Class used to handles every save objects.
/// </summary>
[Serializable]
public class SaveFile
{
    /// <summary>
    /// List of level save.
    /// </summary>
    public List<LevelSave> Saves { get; private set; }

    /// <summary>
    /// Sound level saved.
    /// </summary>
    public float Sound { get; set; }

    /// <summary>
    /// Music level saved.
    /// </summary>
    public float Music { get; set; }



    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="newSaves">New list of saves</param>
    /// <param name="soundLevel">New sound level</param>
    /// <param name="musicLevel">New music level</param>
    public SaveFile(List<LevelSave> newSaves, float soundLevel, float musicLevel)
    {
        Saves = new List<LevelSave>(newSaves);

        Sound = soundLevel;
        Music = musicLevel;
    }
}