using System.Collections.Generic;
using TMPro;
using Towers;
using UnityEngine;

namespace UI.SkillTree
{
    /// <summary>
    /// Class used to handle the skill tree.
    /// </summary>
    public class SkillTree : MonoBehaviour
    {
        /// <summary>
        /// List of skill component.
        /// </summary>
        [SerializeField]
        private List<SkillUpgrades> skills;

        /// <summary>
        /// Description text object, used for skills.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI description;



        /// <summary>
        /// Start method, called after Awake.
        /// </summary>
        private void Start()
        {
            Squad currentSquad = Controller.Instance.SquadController.CurrentSquad;
            List<int> augmentations = Controller.Instance.SaveController.SaveFile.SquadsProgression[0].AugmentationLevelMax;

            for (int i = 0; i < skills.Count; i ++)
            {
                if (i == 4)
                    skills[i].Initialize(currentSquad.SquadSprite, currentSquad.Augmentations, augmentations[i], i, description);
                else
                    skills[i].Initialize(currentSquad.Towers[i].Icon, currentSquad.Towers[i].Augmentations, augmentations[i], i, description);
            }
        }
    }
}