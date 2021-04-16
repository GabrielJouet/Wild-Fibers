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

    public int CurrentSquad { get; set; } = 0;


    public List<Augmentation> RecoverBoughtAugmentations(List<int> augmentationLevel)
    {
        List<Augmentation> result = new List<Augmentation>();

        for (int j = 0; j < augmentationLevel.Count; j ++)
        {
            if (j == 4)
                for (int i = 0; i < augmentationLevel[j]; i++)
                    result.Add(Squads[CurrentSquad].Augmentations[i]);
            else
                for (int i = 0; i < augmentationLevel[j]; i++)
                    result.Add(Squads[CurrentSquad].Towers[j].Augmentations[i]);
        }

        return result;
    }
}