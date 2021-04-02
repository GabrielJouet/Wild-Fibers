using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUpgrades : MonoBehaviour
{
    [SerializeField]
    private List<Skill> _skills;

    [SerializeField]
    private Image _towerIcon;



    public void Initialize(Sprite newTower, List<Augmentation> newAugmentations)
    {
        for (int i = 0; i < _skills.Count; i ++)
            _skills[i].Initialize(newAugmentations[i]);

        _towerIcon.sprite = newTower;
    }
}