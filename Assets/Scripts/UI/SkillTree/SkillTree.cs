using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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
    private List<SkillColumn> _skillColumns;

    /// <summary>
    /// Description text game object.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _description;

    /// <summary>
    /// Confirmation button game object.
    /// </summary>
    [SerializeField]
    private Button _confirmationButton;



    /// <summary>
    /// Start method, called after Awake.
    /// </summary>
    private void Start()
    {
        Squad currentSquad = Controller.Instance.SquadController.CurrentSquad;
        List<int> augmentations = Controller.Instance.SaveController.SaveFile.SquadsProgression[0].AugmentationLevelMax;

        for (int i = 0; i < _skillColumns.Count; i ++)
        {
            if (i == 4)
                _skillColumns[i].Initialize(currentSquad.SquadSprite, currentSquad.Augmentations, augmentations[i], i);
            else
                _skillColumns[i].Initialize(currentSquad.Towers[i].Icon, currentSquad.Towers[i].Augmentations, augmentations[i], i);
        }
    }


    /// <summary>
    /// Method called when a skill is pressed.
    /// </summary>
    /// <param name="callBack">Callback method that make augmentation bought</param>
    /// <param name="description">The current description displayed</param>
    public void SelectAugmentation(UnityAction callBack, string description)
    {
        _description.text = description;
        _confirmationButton.onClick.RemoveAllListeners();
        _confirmationButton.onClick.AddListener(callBack);
    }
}