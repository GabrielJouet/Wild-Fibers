using System;
using System.Collections.Generic;

[Serializable]
public class SaveFile
{
    private readonly List<LevelSave> _levelSaves;

    private float _soundLevel;

    private float _musicLevel;


    //Constructor
    public SaveFile(List<LevelSave> newSaves, float soundLevel, float musicLevel)
    {
        _levelSaves = new List<LevelSave>(newSaves);

        _soundLevel = soundLevel;
        _musicLevel = musicLevel;
    }



    public void UpdateLevelSave(int index, LevelSave newSave)
    {
        _levelSaves[index] = newSave;
    }


    public void UpdateSoundAndMusicSave(float newSound, float newMusic)
    {
        _soundLevel = newSound;
        _musicLevel = newMusic;
    }



    //Getters
    public LevelSave GetLevelData(int index) { return _levelSaves[index]; }

    public List<LevelSave> GetLevelsData() { return _levelSaves; }

    public float GetSoundLevel() { return _soundLevel; }
    
    public float GetMusicLevel() { return _musicLevel; }
}