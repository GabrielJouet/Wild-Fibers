using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Class used in level selection scene where every level is available
 */
public class LevelSelection : MonoBehaviour
{
    [Header("Menu Elements")]
    //Text component handling level name
    [SerializeField]
    private Text _levelName;

    //Image component handling level thumbnail
    [SerializeField]
    private Image _levelPicture;

    //Text component handling level description
    [SerializeField]
    private Text _levelDescription;

    //Button component handling level start
    [SerializeField]
    private Button _launchBattleMenu;

    //Level buttons on level button selection screen
    [SerializeField]
    private List<LevelButton> _levelButtons;


    [Header("Component")]
    //Scene Loader used to change level
    [SerializeField]
    private ChangeScene _sceneChanger;

    [SerializeField]
    private List<Image> _scores;

    [SerializeField]
    private Sprite _activatedSprite;


    [Header("UI Elements")]
    //Hider used to shade everything
    [SerializeField]
    private GameObject _hider;
    [SerializeField]
    private GameObject _activer;


    //Does the level selection screen already opened?
    private bool _opened = false;

    private SaveController _saveController;


    //Start method
    private void Start()
    {
        _saveController = FindObjectOfType<SaveController>();
        SetButtonStates();
    }



    //Method used to retrieve saved data and apply it to level button
    private void SetButtonStates()
    {
        //For each level we check the save to see if level are unlocked, finished and completed
        List<LevelSave> levelSaves = _saveController.GetSaveFile().GetLevelsData();

        for (int i = 0; i < levelSaves.Count; i++)
        {
            switch (levelSaves[i].GetState())
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



    //Method used to activate level selection menu
    //
    //Parameter => newParameters, loaded level parameters
    public void ActivateLevelSelectionMenu(Level newParameters)
    {
        RevertState();

        _levelName.text = newParameters.GetName();
        _levelPicture.sprite = newParameters.GetPicture();
        _levelDescription.text = newParameters.GetDescription();

        DisplayRanking(_saveController.GetSaveFile().GetLevelData(newParameters.GetNumber()).GetLivesLostCount());

        _launchBattleMenu.onClick.RemoveAllListeners();
        _launchBattleMenu.onClick.AddListener(() => _sceneChanger.LoadScene(newParameters.GetPlaySceneName()));
    }


    //Method used to desactivate level selection menu
    public void DesactivateLevelSelectionMenu()
    {
        RevertState();
    }


    //Method used to display ranking for each stage
    //
    //Parameter => livesLost, the amount of lives lost in this stage
    private void DisplayRanking(int livesLost)
    {
        if (livesLost <= 15)
        {
            _scores[2].sprite = _activatedSprite;

            if (livesLost <= 10)
            {
                _scores[1].sprite = _activatedSprite;

                if (livesLost <= 3)
                    _scores[0].sprite = _activatedSprite;
            }
        }
    }


    //Method used to swap level selection menu state
    private void RevertState()
    {
        _activer.SetActive(!_opened);

        _hider.SetActive(!_opened);

        _opened = !_opened;
    }
}