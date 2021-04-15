using System;
using System.Collections.Generic;

[Serializable]
public class SquadProgression
{
    public List<int> AugmentationLevelMax { get; private set; }


    /// <summary>
    /// Tower level max.
    /// </summary>
    public int TowerLevelMax { get; set; }


    /// <summary>
    /// List of level save.
    /// </summary>
    public List<LevelSave> Saves { get; private set; }



    public SquadProgression(List<LevelSave> newSaves)
    {
        Saves = new List<LevelSave>(newSaves);
        AugmentationLevelMax = new List<int> { 0, 0, 0, 0, 0 };
    }


    public int CurrencyAvailable 
    { 
        get
        {
            int buffer = 0;

            foreach (LevelSave current in Saves)
                buffer += current.SeedsGained + (current.State == LevelState.SIDED ? 1 : 0) + (current.State == LevelState.CHALLENGED ? 2 : 0);

            foreach (Augmentation current in Controller.Instance.SquadControl.RecoverBoughtAugmentations(AugmentationLevelMax))
                buffer -= current.Price;

            return buffer;
        }
    }


    public void AddNewAugmentation(int index, int newLevel)
    {
        AugmentationLevelMax[index] = newLevel;
    }
}