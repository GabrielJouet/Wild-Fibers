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
    /// Squad progression (each squad has its own list of level saves.
    /// </summary>
    public List<SquadProgression> SquadsProgression { get; private set; }

    /// <summary>
    /// Current squad used.
    /// </summary>
    public SquadProgression CurrentSquad { get => SquadsProgression[0]; }

    /// <summary>
    /// Current save of the current squad.
    /// </summary>
    public List<LevelSave> CurrentSave { get => SquadsProgression[0].Saves; }


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="newVersion">New version number</param>
    /// <param name="newSaves">New list of saves</param>
    /// <param name="soundLevel">New sound level</param>
    /// <param name="musicLevel">New music level</param>
    /// <param name="numberOfEnemies">Number total of enemies</param>
    /// <param name="numberOfSquads">Number total of squads</param>
    public SaveFile(string newVersion, List<LevelSave> newSaves, float soundLevel, float musicLevel, int numberOfEnemies, int numberOfSquads)
    {
        SquadsProgression = new List<SquadProgression>();

        for (int i = 0; i < numberOfSquads; i++)
            SquadsProgression.Add(new SquadProgression(newSaves));

        VersionNumber = newVersion;

        Sound = soundLevel;
        SoundMuted = false;

        Music = musicLevel;
        MusicMuted = false;

        for (int i = 0; i < numberOfEnemies; i++)
            EnemiesUnlocked.Add(false);

        CurrentSquad.TowerLevelMax = 0;
    }


    /// <summary>
    /// Method called when an old save has been detected.
    /// </summary>
    /// <param name="numberOfLevels">Number of levels missing</param>
    /// <param name="numberOfEnemies">Number of enemies missing</param>
    /// <param name="numberOfSquads">Number of squads missing</param>
    /// <param name="blankSave">A blank save without anything in it</param>
    public void UpdateOldSave(int numberOfLevels, int numberOfEnemies, int numberOfSquads, List<LevelSave> blankSave)
    {
        LevelState lastLevelState = CurrentSave[CurrentSave.Count - 1].State;

        int oldSquadCount = SquadsProgression.Count;
        for (int j = 0; j < SquadsProgression.Count + numberOfSquads; j ++)
        {
            if (j < oldSquadCount)
            {
                for (int i = 0; i < numberOfLevels; i++)
                {
                    if (i == 0 && (lastLevelState != LevelState.LOCKED && lastLevelState != LevelState.UNLOCKED))
                        CurrentSave.Add(new LevelSave(0, LevelState.UNLOCKED));
                    else
                        CurrentSave.Add(new LevelSave(0, LevelState.LOCKED));
                }
            }
            else
                SquadsProgression.Add(new SquadProgression(blankSave));
        }

        for (int g = 0; g < numberOfEnemies; g++)
            EnemiesUnlocked.Add(false);
    }
}