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


    [Header("UI Elements")]
    //Level selection object is the frame for every other components
    [SerializeField]
    private GameObject _levelSelectionMenu;

    //Hider used to shade everything
    [SerializeField]
    private GameObject _hider;


    //Does the level selection screen already opened?
    private bool _opened = false;



    //Start method
    private void Start()
    {
        SetButtonStates();
    }



    //Method used to retrieve saved data and apply it to level button
    private void SetButtonStates()
    {
        //For each level we check the save to see if level are unlocked, finished and completed
        List<LevelSave> levelSaves = FindObjectOfType<SaveController>().GetSaveFile().GetLevelsData();

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
        _levelIndex.text = (newParameters.GetNumber() + 1).ToString();
        _levelPicture.sprite = newParameters.GetPicture();
        _levelDescription.text = newParameters.GetDescription();

        _launchBattleMenu.onClick.RemoveAllListeners();
        _launchBattleMenu.onClick.AddListener(() => _sceneChanger.LoadScene(newParameters.GetPlaySceneName()));
    }


    //Method used to desactivate level selection menu
    public void DesactivateLevelSelectionMenu()
    {
        RevertState();
    }


    //Method used to swap level selection menu state
    private void RevertState()
    {
        _levelSelectionMenu.SetActive(!_opened);
        _hider.SetActive(!_opened);

        _opened = !_opened;
    }
}