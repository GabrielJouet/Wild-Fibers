using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    [SerializeField]
    private List<SkillUpgrades> _skills;
    
    
    private void Start()
    {
        Squad currentSquad = Controller.Instance.SquadControl.Squads[0];
        List<string> augmentations = Controller.Instance.SaveControl.SaveFile.SquadsProgression[0].AugmentationID;

        if (augmentations == null)
            augmentations = new List<string>();

        for (int i = 0; i < _skills.Count; i ++)
        {
            if (i == 4)
                _skills[i].Initialize(currentSquad.SquadSprite, currentSquad.Augmentations, augmentations);
            else
                _skills[i].Initialize(currentSquad.Towers[i].Sprite, currentSquad.Towers[i].Augmentations, augmentations);
        }
    }
}