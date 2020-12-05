using System;
using System.Collections.Generic;

[Serializable]
public class SaveFile
{
    public List<LevelSave> Saves { get; private set; }

    public float Sound { get; set; }

    public float Music { get; set; }


    //Constructor
    public SaveFile(List<LevelSave> newSaves, float soundLevel, float musicLevel)
    {
        Saves = new List<LevelSave>(newSaves);

        Sound = soundLevel;
        Music = musicLevel;
    }
}