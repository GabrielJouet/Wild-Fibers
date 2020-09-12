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

    //Text component handling level number
    [SerializeField]
    private Text _levelIndex;

    //Image component handling level thumbnail
    [SerializeField]
    private Image _levelPicture;

    //Text component handling level description
    [SerializeField]
    private Text _levelDescription;

    //Button component handling level start
    [SerializeField]
    private Button _launchBattleMenu;


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

    

    //Method used to activate level selection menu
    //
    //Parameter => newParameters, loaded level parameters
    public void ActivateLevelSelectionMenu(Level newParameters)
    {
        RevertState();

        _levelName.text = newParameters.GetName();
        _levelIndex.text = newParameters.GetNumber().ToString();
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