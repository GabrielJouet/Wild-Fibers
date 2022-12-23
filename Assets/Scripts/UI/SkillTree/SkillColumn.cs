using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used to handle skill upgrades in the skill tree.
/// </summary>
public class SkillColumn : MonoBehaviour
{
    /// <summary>
    /// List of skill availables.
    /// </summary>
    [SerializeField]
    private List<Skill> _skills;

    /// <summary>
    /// Tower icon component.
    /// </summary>
    [SerializeField]
    private Image _towerIcon;


    /// <summary>
    /// Index buffered.
    /// </summary>
    private int _index;



    /// <summary>
    /// Initialize method.
    /// </summary>
    /// <param name="newTower">Tower sprite displayed</param>
    /// <param name="newAugmentations">Augmentation to display</param>
    /// <param name="augmentationLevel">Current augmentation level of the skill</param>
    /// <param name="newIndex">Buffered index</param>
    public void Initialize(Sprite newTower, List<Augmentation> newAugmentations, int augmentationLevel, int newIndex)
    {
        for (int i = 0; i < _skills.Count; i ++)
        {
            AugmentationState newState;

            if (augmentationLevel > i)
                newState = AugmentationState.BOUGHT;
            else if (i == 0 || augmentationLevel == i)
                newState = AugmentationState.AVAILABLE;
            else
                newState = AugmentationState.LOCKED;

            _skills[i].Initialize(newAugmentations[i], newState);
        }

        _index = newIndex;
        _towerIcon.sprite = newTower;
    }


    /// <summary>
    /// Method called when a skill is purchased.
    /// </summary>
    /// <param name="purchased">Skill purchased</param>
    public void PurchaseAugmentation(Skill purchased)
    {
        int index = _skills.IndexOf(purchased);

        Controller.Instance.SaveController.SaveTowerAugmentationLevel(_index, index + 1);

        if (index + 1 < _skills.Count)
            _skills[index + 1].Activate();
    }
}