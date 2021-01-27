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


    [SerializeField]
    private GameObject _classicLayout;

    [SerializeField]
    private GameObject _sideLayout;

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
    private List<LevelButton> _levelButtons;

    private SaveController _saveController;


    private int _levelIndex;

    private LevelData _level;


    /// <summary>
    /// Method used to open level selection menu.
    /// </summary>
    public void ActivateLevelSelectionMenu(LevelButton buttonUsed)
    {
        if (_saveController == null)
            _saveController = FindObjectOfType<SaveController>();

        for (int i = 0; i < _levelButtons.Count; i ++)
        {
            if (_levelButtons[i] == buttonUsed)
            {
                _levelIndex = i;
                _level = _saveController.Levels[i];
                break;
            }
        }

        _saveController.LevelIndex = _levelIndex;

        LevelState buffer = _saveController.SaveFile.Saves[_levelIndex].State;
        _sideButton.enabled = buffer == LevelState.COMPLETED || buffer == LevelState.SIDED || buffer == LevelState.CHALLENGED;
        _challengeButton.enabled = buffer == LevelState.SIDED || buffer == LevelState.CHALLENGED;

        _classicLayout.SetActive(true);
        _sideLayout.SetActive(false);

        _levelName.text = _level.Classic.Name;
        _levelPicture.sprite = _level.Classic.Picture;
        _levelDescription.text = _level.Classic.Description;

        foreach (Image current in _scores)
            current.sprite = _desactivatedSprite;

        for (int i = 0; i < _saveController.SaveFile.Saves[_levelIndex].SeedsGained; i ++)
            _scores[i].sprite = _activatedSprite;

        _displayController.DisplayObject(gameObject);

        _launchBattleMenu.onClick.RemoveAllListeners();
        _launchBattleMenu.onClick.AddListener(() =>
        {
            _saveController.LoadedLevel = LevelType.CLASSIC;
            _sceneChanger.LoadScene(_level.Classic.Scene);
        });
    }


    public void ActivateAlternateMenu(string levelType)
    {
        LevelType enumBuffer = (LevelType)System.Enum.Parse(typeof(LevelType), levelType);
        Level buffer = enumBuffer == LevelType.SIDE ? _level.Side : _level.Challenge;

        _classicLayout.SetActive(false);
        _sideLayout.SetActive(true);

        _levelName.text = buffer.Name;
        _levelPicture.sprite = buffer.Picture;
        _sideDescription.text = buffer.Description;

        _sideScore.sprite = _desactivatedSprite;

        LevelState bufferState = _saveController.SaveFile.Saves[_levelIndex].State;

        if (enumBuffer == LevelType.SIDE && bufferState == LevelState.SIDED)
            _sideScore.sprite = _activatedSprite;
        else if (enumBuffer == LevelType.CHALLENGE && bufferState == LevelState.CHALLENGED)
            _sideScore.sprite = _activatedSprite;

        _launchBattleMenu.onClick.RemoveAllListeners();
        _launchBattleMenu.onClick.AddListener(() =>
        {
            _saveController.LoadedLevel = enumBuffer;
            _sceneChanger.LoadScene(_level.Classic.Scene);
        });
    }
}