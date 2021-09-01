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
    /// Text component of the side level (and challenge).
    /// </summary>
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

    /// <summary>
    /// Side and challenge score icon.
    /// </summary>
    [SerializeField]
    private Image _sideScore;

    /// <summary>
    /// Side button component.
    /// </summary>
    [SerializeField]
    private Button _sideButton;

    /// <summary>
    /// Challenge button component.
    /// </summary>
    [SerializeField]
    private Button _challengeButton;


    /// <summary>
    /// Classic layout parent object.
    /// </summary>
    [SerializeField]
    private GameObject _classicLayout;

    /// <summary>
    /// Side and challenge layout parent object.
    /// </summary>
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

    /// <summary>
    /// Sprite of the side activated.
    /// </summary>
    [SerializeField]
    private Sprite _sideActivated;

    /// <summary>
    /// Sprite of the side desactivated.
    /// </summary>
    [SerializeField]
    private Sprite _sideDesactivated;

    /// <summary>
    /// Side icon component.
    /// </summary>
    [SerializeField]
    private Image _sideIcon;


    /// <summary>
    /// Sprite of the challenge activated.
    /// </summary>
    [SerializeField]
    private Sprite _challengeActivated;

    /// <summary>
    /// Sprite of the challenge desactivated.
    /// </summary>
    [SerializeField]
    private Sprite _challengeDesactivated;

    /// <summary>
    /// Challenge icon component.
    /// </summary>
    [SerializeField]
    private Image _challengeIcon;



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

    /// <summary>
    /// Level data buffered.
    /// </summary>
    private LevelData _level;

    /// <summary>
    /// Display type of this level.
    /// </summary>
    private LevelType _displayType;


    /// <summary>
    /// Method used to open level selection menu.
    /// </summary>
    /// <param name="buttonUsed">Which button was used</param>
    public void ActivateLevelSelectionMenu(LevelButton buttonUsed)
    {
        _level = buttonUsed.LevelData;

        LevelState buffer = Controller.Instance.SaveControl.RecoverLevelSave(_level.Classic).State;
        bool sidedOrChallenged = buffer == LevelState.SIDED || buffer == LevelState.CHALLENGED;
        bool notLocked = buffer == LevelState.COMPLETED || sidedOrChallenged;

        _sideButton.interactable = notLocked;
        _sideIcon.sprite = notLocked ? _sideActivated : _sideDesactivated;

        _challengeButton.interactable = sidedOrChallenged;
        _challengeIcon.sprite = sidedOrChallenged ? _challengeActivated : _challengeDesactivated;

        _displayController.DisplayObject(gameObject);

        ActivateBaseMenu();
    }


    /// <summary>
    /// Method called to activate the base menu.
    /// </summary>
    public void ActivateBaseMenu()
    {
        _classicLayout.SetActive(true);
        _sideLayout.SetActive(false);

        _displayType = LevelType.CLASSIC;

        _levelName.text = _level.Classic.Name;
        _levelPicture.sprite = _level.Classic.Picture;
        _levelDescription.text = _level.Classic.Description;

        int seedGainedBuffer = Controller.Instance.SaveControl.RecoverLevelSave(_level.Classic).SeedsGained;
        for (int i = 0; i < _scores.Count; i++)
            _scores[i].sprite = seedGainedBuffer > i ? _activatedSprite : _desactivatedSprite;

        _launchBattleMenu.onClick.RemoveAllListeners();
        _launchBattleMenu.onClick.AddListener(() =>
        {
            Controller.Instance.SaveControl.LoadedLevel = _level.Classic;
            _sceneChanger.LoadScene(_level.Classic.Scene);
        });
    }


    /// <summary>
    /// Method called to activate alternate menu (side or challenge).
    /// </summary>
    /// <param name="levelType">The type of level activated, side or challenge</param>
    public void ActivateAlternateMenu(string levelType)
    {
        LevelType enumBuffer = (LevelType)System.Enum.Parse(typeof(LevelType), levelType);

        if (enumBuffer == _displayType)
            ActivateBaseMenu();
        else
        {
            _displayType = enumBuffer;

            Level buffer = enumBuffer == LevelType.SIDE ? _level.Side : _level.Challenge;

            _classicLayout.SetActive(false);
            _sideLayout.SetActive(true);

            _levelName.text = buffer.Name;
            _levelPicture.sprite = buffer.Picture;
            _sideDescription.text = buffer.Description;

            _sideScore.sprite = _desactivatedSprite;

            LevelState bufferState = Controller.Instance.SaveControl.RecoverLevelSave(buffer).State;

            if (enumBuffer == LevelType.SIDE && bufferState == LevelState.SIDED || bufferState == LevelState.CHALLENGED)
                _sideScore.sprite = _activatedSprite;
            else if (enumBuffer == LevelType.CHALLENGE && bufferState == LevelState.CHALLENGED)
                _sideScore.sprite = _activatedSprite;

            _launchBattleMenu.onClick.RemoveAllListeners();
            _launchBattleMenu.onClick.AddListener(() =>
            {
                Controller.Instance.SaveControl.LoadedLevel = buffer;
                _sceneChanger.LoadScene(buffer.Scene);
            });
        }
    }
}