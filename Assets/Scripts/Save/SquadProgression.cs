using System;
using System.Collections.Generic;

/// <summary>
/// Class used to handle and save a specific squad progression.
/// </summary>
[Serializable]
public class SquadProgression
{
    /// <summary>
    /// Augmentation level max of this squad.
    /// </summary>
    public List<int> AugmentationLevelMax { get; private set; }


    /// <summary>
    /// Tower level max.
    /// </summary>
    public int TowerLevelMax { get; set; }


    /// <summary>
    /// List of level save.
    /// </summary>
    public List<LevelSave> Saves { get; private set; }


    /// <summary>
    /// How much skill points the squad can use?
    /// </summary>
    public int CurrencyAvailable
    {
        get
        {
            int buffer = 0;

            foreach (LevelSave current in Saves)
                buffer += current.SeedsGained + (current.State == LevelState.SIDED ? 1 : 0) + (current.State == LevelState.CHALLENGED ? 2 : 0);

            foreach (Augmentation current in Controller.Instance.SquadController.RecoverBoughtAugmentations(AugmentationLevelMax))
                buffer -= current.Price;

            return buffer;
        }
    }



    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="newSaves">New save for this squad</param>
    public SquadProgression(List<LevelSave> newSaves)
    {
        Saves = new List<LevelSave>(newSaves);
        AugmentationLevelMax = new List<int> { 0, 0, 0, 0, 0 };
    }


    /// <summary>
    /// Method called when a new augmentation is bought.
    /// </summary>
    /// <param name="index">The augmentation index</param>
    /// <param name="newLevel">The new augmentation level</param>
    public void AddNewAugmentation(int index, int newLevel)
    {
        AugmentationLevelMax[index] = newLevel;
    }
}