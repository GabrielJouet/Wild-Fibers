using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    /// Description text object, used for skills.
    /// </summary>
    [SerializeField]
    private Text _description;



    /// <summary>
    /// Start method, called after Awake.
    /// </summary>
    private void Start()
    {
        Squad currentSquad = Controller.Instance.SquadController.CurrentSquad;
        List<int> augmentations = Controller.Instance.SaveController.SaveFile.SquadsProgression[0].AugmentationLevelMax;

        /*
        for (int i = 0; i < _skills.Count; i ++)
        {
            if (i == 4)
                _skills[i].Initialize(currentSquad.SquadSprite, currentSquad.Augmentations, augmentations[i], i, _description);
            else
                _skills[i].Initialize(currentSquad.Towers[i].Sprite, currentSquad.Towers[i].Augmentations, augmentations[i], i, _description);
        }
        */
    }
}