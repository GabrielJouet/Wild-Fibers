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

    [SerializeField]
    private Text _sideDescription;

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

    [SerializeField]
    private Image _sideScore;

    [SerializeField]
    private Button _sideButton;

    [SerializeField]
    private Button _challengeButton;


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


    private LevelButton _loadedButton;

    /// <summary>
    /// Method used to open level selection menu.
    /// </summary>
    /// <param name="newParameters">The new level parameters to use</param>
    public void ActivateLevelSelectionMenu(LevelButton button, bool sideUnlocked, bool challengeUnlocked)
    {
        _sideButton.enabled = sideUnlocked;
        _challengeButton.enabled = challengeUnlocked;

        _loadedButton = button;

        _displayController.DisplayObject(gameObject);

        _levelName.text = _loadedButton.Level.Name;
        _levelPicture.sprite = _loadedButton.Level.Picture;
        _levelDescription.text = _loadedButton.Level.Description;

        foreach (Image current in _scores)
            current.sprite = _desactivatedSprite;

        for (int i = 0; i < _controller.SaveController.SaveFile.Saves[_controller.SaveController.FindLevelSaveWithName(_loadedButton.Level.Name)].SeedsGained; i ++)
            _scores[i].sprite = _activatedSprite;

        _launchBattleMenu.onClick.RemoveAllListeners();
        _launchBattleMenu.onClick.AddListener(() => _sceneChanger.LoadScene(_loadedButton.Level.Scene));
    }


    public void ActivateSideMenu()
    {
        _levelName.text = _loadedButton.Side.Name;
        _levelPicture.sprite = _loadedButton.Side.Picture;
        _sideDescription.text = _loadedButton.Side.Description;

        _sideScore.sprite = _desactivatedSprite;
        _sideScore.sprite = _controller.SaveController.SaveFile.Saves[_controller.SaveController.FindLevelSaveWithName(_loadedButton.Side.Name)].SeedsGained == 1 ? _activatedSprite : _scores[4].sprite;

        _launchBattleMenu.onClick.RemoveAllListeners();
        _launchBattleMenu.onClick.AddListener(() => _sceneChanger.LoadScene(_loadedButton.Side.Scene));
    }


    public void ActivateChallengeMenu()
    {
        _levelName.text = _loadedButton.Challenge.Name;
        _levelPicture.sprite = _loadedButton.Challenge.Picture;
        _sideDescription.text = _loadedButton.Challenge.Description;

        _sideScore.sprite = _desactivatedSprite;
        _sideScore.sprite = _controller.SaveController.SaveFile.Saves[_controller.SaveController.FindLevelSaveWithName(_loadedButton.Challenge.Name)].SeedsGained == 1 ? _activatedSprite : _scores[4].sprite;

        _launchBattleMenu.onClick.RemoveAllListeners();
        _launchBattleMenu.onClick.AddListener(() => _sceneChanger.LoadScene(_loadedButton.Challenge.Scene));
    }
}