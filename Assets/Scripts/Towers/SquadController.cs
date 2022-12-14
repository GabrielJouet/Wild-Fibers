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

    /// <summary>
    /// Squads available.
    /// </summary>
    private List<Squad> _updatedSquads = new List<Squad>();

    /// <summary>
    /// Squads available.
    /// </summary>
    public List<Squad> Squads 
    { 
        get 
        {
            if (_updatedSquads.Count == 0)
            {
                foreach(Squad squad in _squads)
                {
                    Squad buffer = ScriptableObject.CreateInstance<Squad>();
                    buffer.Populate(squad);

                    buffer.UpdateTowerDatas(Controller.Instance.SaveController.SaveFile.CurrentSquad.AugmentationLevelMax);
                    _updatedSquads.Add(buffer);
                }
            }

            return _updatedSquads;
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


    /// <summary>
    /// Method used to reset data when a change on save is done.
    /// </summary>
    public void ResetData()
    {
        _updatedSquads = new List<Squad>();
    }
}