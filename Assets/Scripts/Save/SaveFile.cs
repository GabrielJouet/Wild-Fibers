using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFile
{
    private readonly List<LevelSave> _levelSaves;



    //Constructor
    public SaveFile(List<LevelSave> newSaves)
    {
        _levelSaves = new List<LevelSave>(newSaves);
    }



    public void UpdateSave(int index, LevelSave newSave)
    {
        _levelSaves[index] = newSave;
    }



    public LevelSave GetLevelData(int index) { return _levelSaves[index]; }

    public List<LevelSave> GetLevelsData() { return _levelSaves; }
}