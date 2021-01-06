using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used to handles level selection menu.
/// </summary>
public class LevelSelection : MonoBehaviour
{
    [Header("Menu Elements")]

    /// <summary>
    /// Level name text component.
    /// </summary>
    [SerializeField]
    private Text _levelName;

    /// <summary>
    /// Level Thumbnail.
    /// </summary>
    [SerializeField]
    private Image _levelPicture;

    /// <summary>
    /// Level description text component.
    /// </summary>
    [SerializeField]
    private Text _levelDescription;

    /// <summary>
    /// Launch battle menu button.
    /// </summary>
    [SerializeField]
    private Button _launchBattleMenu;

    /// <summary>
    /// Scores objects displayed.
    /// </summary>
    [SerializeField]
    private List<Image> _scores;

    /// <summary>
    /// Activated sprite of score items.
    /// </summary>
    [SerializeField]
    private Sprite _activatedSprite;

    /// <summary>
    /// Desactivated sprite of score items.
    /// </summary>
    [SerializeField]
    private Sprite _desactivatedSprite;


    [Header("Component")]

    /// <summary>
    /// Scene changer component.
    /// </summary>
    [SerializeField]
    private ChangeScene _sceneChanger;

    /// <summary>
    /// Display controller component.
    /// </summary>
    [SerializeField]
    private DisplayController _displayController;

    [SerializeField]
    private LevelButtonController _controller;


    /// <summary>
    /// Method used to open level selection menu.
    /// </summary>
    /// <param name="newParameters">The new level parameters to use</param>
    public void ActivateLevelSelectionMenu(Level newParameters)
    {
        _displayController.DisplayObject(gameObject);

        _levelName.text = newParameters.Name;
        _levelPicture.sprite = newParameters.Picture;
        _levelDescription.text = newParameters.Description;

        foreach (Image current in _scores)
            current.sprite = _desactivatedSprite;

        for (int i = 0; i < _controller.SaveController.SaveFile.Saves[newParameters.Number].SeedsGained; i ++)
            _scores[i].sprite = _activatedSprite;

        _launchBattleMenu.onClick.RemoveAllListeners();
        _launchBattleMenu.onClick.AddListener(() => _sceneChanger.LoadScene(newParameters.Scene));
    }
}