using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUpgrades : MonoBehaviour
{
    [SerializeField]
    private List<Skill> _skills;

    [SerializeField]
    private Image _towerIcon;



    public void Initialize(Sprite newTower, List<Augmentation> newAugmentations, List<string> augmentations)
    {
        for (int i = 0; i < _skills.Count; i ++)
        {
            AugmentationState newState;

            if (augmentations.Contains(newAugmentations[i].name))
                newState = AugmentationState.BOUGHT;
            else if (i == 0 || (i > 0 && augmentations.Contains(newAugmentations[i - 1].name)))
                newState = AugmentationState.AVAILABLE;
            else
                newState = AugmentationState.LOCKED;

            _skills[i].Initialize(newAugmentations[i], newState);
        }

        _towerIcon.sprite = newTower;
    }


    public void PurchaseAugmentation(Skill purchased)
    {
        int index = _skills.IndexOf(purchased);

        if (index + 1 < _skills.Count)
            _skills[index + 1].Activate();
    }
}