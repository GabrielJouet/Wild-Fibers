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
    public List<Squad> Squads { get => _squads; }

    public int CurrentSquad { get; set; } = 0;


    public List<Augmentation> RecoverBoughtAugmentations(List<int> augmentationIDs)
    {
        List<Augmentation> result = new List<Augmentation>();

        foreach (Augmentation current in Squads[CurrentSquad].Augmentations)
            if (augmentationIDs.Contains(current.ID))
                result.Add(current);

        foreach (TowerData current in Squads[CurrentSquad].Towers)
            foreach (Augmentation augmentation in current.Augmentations)
                if (augmentationIDs.Contains(augmentation.ID))
                    result.Add(augmentation);

        return result;
    }
}
