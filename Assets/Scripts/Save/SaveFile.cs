using System;
using System.Collections.Generic;

/// <summary>
/// Class used to handles every save objects.
/// </summary>
[Serializable]
public class SaveFile
{
    /// <summary>
    /// Current version number.
    /// </summary>
    public string VersionNumber { get; private set; }

    /// <summary>
    /// List of level save.
    /// </summary>
    public List<LevelSave> Saves { get; private set; }


    /// <summary>
    /// Sound level saved.
    /// </summary>
    public float Sound { get; set; }

    /// <summary>
    /// Does the sound is muted?
    /// </summary>
    public bool SoundMuted { get; set; }


    /// <summary>
    /// Music level saved.
    /// </summary>
    public float Music { get; set; }

    /// <summary>
    /// Does the music is muted?
    /// </summary>
    public bool MusicMuted { get; set; }


    /// <summary>
    /// Enemies unlocked.
    /// </summary>
    public List<bool> EnemiesUnlocked { get; set; } = new List<bool>();


    /// <summary>
    /// Tower level max.
    /// </summary>
    public int TowerLevelMax { get; set; }



    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="newVersion">New version number</param>
    /// <param name="newSaves">New list of saves</param>
    /// <param name="soundLevel">New sound level</param>
    /// <param name="musicLevel">New music level</param>
    /// <param name="numberOfEnemies">Number total of enemies</param>
    public SaveFile(string newVersion, List<LevelSave> newSaves, float soundLevel, float musicLevel, int numberOfEnemies)
    {
        VersionNumber = newVersion;
        Saves = new List<LevelSave>(newSaves);

        Sound = soundLevel;
        SoundMuted = false;

        Music = musicLevel;
        MusicMuted = false;

        for (int i = 0; i < numberOfEnemies; i++)
            EnemiesUnlocked.Add(false);

        TowerLevelMax = 1;
    }
}