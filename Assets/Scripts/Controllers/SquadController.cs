using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Squad controller class used to manage squads.
/// </summary>
public class SquadController : MonoBehaviour
{
    /// <summary>
    /// Squads available.
    /// </summary>
    [SerializeField]
    private List<Squad> _squads;
    public List<Squad> Squads 
    { 
        get 
        {
            _squads[0].UpdateTowerDatas(Controller.Instance.SaveControl.SaveFile.CurrentSquad.AugmentationLevelMax);
            return _squads;
        } 
    }

    /// <summary>
    /// Current squad loaded.
    /// </summary>
    public Squad CurrentSquad { get => Squads[0]; }


    /// <summary>
    /// Method used to recover bought augmentations of a squad.
    /// </summary>
    /// <param name="augmentationLevel">Augmentation level of the squad</param>
    /// <returns>A list of augmentation</returns>
    public List<Augmentation> RecoverBoughtAugmentations(List<int> augmentationLevel)
    {
        List<Augmentation> result = new List<Augmentation>();

        for (int j = 0; j < augmentationLevel.Count; j ++)
        {
            //We recover the squad augmentations
            if (j == 4)
                for (int i = 0; i < augmentationLevel[j]; i++)
                    result.Add(CurrentSquad.Augmentations[i]);
            //Then we recover the towers augmentations
            else
                for (int i = 0; i < augmentationLevel[j]; i++)
                    result.Add(CurrentSquad.Towers[j].Augmentations[i]);
        }

        return result;
    }
}