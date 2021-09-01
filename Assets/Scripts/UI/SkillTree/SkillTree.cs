using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to handle the skill tree.
/// </summary>
public class SkillTree : MonoBehaviour
{
    /// <summary>
    /// List of skill component.
    /// </summary>
    [SerializeField]
    private List<SkillUpgrades> _skills;



    /// <summary>
    /// Start method, called after Awake.
    /// </summary>
    private void Start()
    {
        Squad currentSquad = Controller.Instance.SquadControl.CurrentSquad;
        List<int> augmentations = Controller.Instance.SaveControl.SaveFile.SquadsProgression[0].AugmentationLevelMax;

        for (int i = 0; i < _skills.Count; i ++)
        {
            if (i == 4)
                _skills[i].Initialize(currentSquad.SquadSprite, currentSquad.Augmentations, augmentations[i], i);
            else
                _skills[i].Initialize(currentSquad.Towers[i].Sprite, currentSquad.Towers[i].Augmentations, augmentations[i], i);
        }
    }
}