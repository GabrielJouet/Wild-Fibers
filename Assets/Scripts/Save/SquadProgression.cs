using System;
using System.Collections.Generic;

[Serializable]
public class SquadProgression
{
    public List<string> AugmentationID { get; private set; }

    /// <summary>
    /// List of level save.
    /// </summary>
    public List<LevelSave> Saves { get; private set; }



    public SquadProgression(List<LevelSave> newSaves)
    {
        Saves = new List<LevelSave>(newSaves);
    }


    public int CurrencyAvailable 
    { 
        get
        {
            int buffer = 0;

            foreach (LevelSave current in Saves)
                buffer += current.SeedsGained + (current.State == LevelState.SIDED ? 1 : 0) + (current.State == LevelState.CHALLENGED ? 2 : 0);

            if (AugmentationID != null)
                foreach (Augmentation current in Controller.Instance.SquadControl.RecoverBoughtAugmentations(AugmentationID))
                    buffer -= current.Price;

            return buffer;
        }
    }


    public void AddNewAugmentation(string newId)
    {
        if (AugmentationID == null)
            AugmentationID = new List<string> { newId };
        else if (!AugmentationID.Contains(newId))
            AugmentationID.Add(newId);
    }
}