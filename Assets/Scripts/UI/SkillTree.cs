using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    [SerializeField]
    private List<SkillUpgrades> _skills;

    private void Start()
    {
        Squad currentSquad = Controller.Instance.SquadControl.Squads[0];

        for (int i = 0; i < _skills.Count; i ++)
        {
            if (i == 4)
                _skills[i].Initialize(currentSquad.SquadSprite, currentSquad.Augmentations);
            else
                _skills[i].Initialize(currentSquad.Towers[i].Sprite, currentSquad.Towers[i].Augmentations);
        }
    }
}