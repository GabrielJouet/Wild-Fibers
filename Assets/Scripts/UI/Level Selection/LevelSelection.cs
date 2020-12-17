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
    /// List of level buttons to handle.
    /// </summary>
    [SerializeField]
    private List<LevelButton> _levelButtons;

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
    /// Save controller component.
    /// </summary>
    private SaveController _saveController;



    /// <summary>
    /// Start method used to initialize.
    /// </summary>
    private void Start()
    {
        _saveController = FindObjectOfType<SaveController>();
        SetButtonStates();
        gameObject.SetActive(false);
    }


    /// <summary>
    /// Method used to set button states at startup.
    /// </summary>
    private void SetButtonStates()
    {
        List<LevelSave> levelSaves = _saveController.SaveFile.Saves;

        for (int i = 0; i < levelSaves.Count; i++)
        {
            switch (levelSaves[i].State)
            {
                case LevelState.LOCKED:
                    _levelButtons[i].LockLevel();
                    break;
                case LevelState.UNLOCKED:
                    _levelButtons[i].UnlockLevel();
                    break;
                case LevelState.COMPLETED:
                    _levelButtons[i].SetCompleted();
                    break;
                case LevelState.SIDED:
                    _levelButtons[i].SetSided();
                    break;
                case LevelState.CHALLENGED:
                    _levelButtons[i].SetChallenged();
                    break;
            }
        }
    }


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

        int livesLost = _saveController.SaveFile.Saves[newParameters.Number].LivesLost;

        _scores[2].sprite = livesLost <= 15 ? _activatedSprite : _scores[2].sprite;
        _scores[1].sprite = livesLost <= 10 ? _activatedSprite : _scores[2].sprite;
        _scores[0].sprite = livesLost <= 3 ? _activatedSprite : _scores[2].sprite;

        _launchBattleMenu.onClick.RemoveAllListeners();
        _launchBattleMenu.onClick.AddListener(() => _sceneChanger.LoadScene(newParameters.Scene));
    }
}