using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUpgrades : MonoBehaviour
{
    [SerializeField]
    private List<Skill> _skills;

    [SerializeField]
    private Image _towerIcon;

    private int _index;



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


    public void PurchaseAugmentation(Skill purchased)
    {
        int index = _skills.IndexOf(purchased);

        Controller.Instance.SaveControl.SaveTowerAugmentationLevel(_index, index + 1);

        if (index + 1 < _skills.Count)
            _skills[index + 1].Activate();
    }
}