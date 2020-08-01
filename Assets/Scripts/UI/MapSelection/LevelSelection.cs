using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField]
    private Text _levelName;

    [SerializeField]
    private Text _levelIndex;

    [SerializeField]
    private Image _levelPicture;

    [SerializeField]
    private Text _levelDescription;

    [SerializeField]
    private Button _launchBattleMenu;

    [SerializeField]
    private ChangeScene _sceneChanger;


    [SerializeField]
    private GameObject _levelSelectionMenu;

    [SerializeField]
    private GameObject _hider;


    private bool _opened = false;

    
    public void ActivateLevelSelectionMenu(LevelParameters newParameters)
    {
        _levelSelectionMenu.SetActive(!_opened);
        _hider.SetActive(!_opened);

        _levelName.text = newParameters.GetName();
        _levelIndex.text = newParameters.GetNumber().ToString();

        _levelPicture.sprite = newParameters.GetPicture();

        _levelDescription.text = newParameters.GetDescription();

        _launchBattleMenu.onClick.RemoveAllListeners();
        _launchBattleMenu.onClick.AddListener(() => _sceneChanger.LoadScene(newParameters.GetPlaySceneName()));

        _opened = !_opened;
    }


    public void DesactivateLevelSelectionMenu()
    {
        _levelSelectionMenu.SetActive(!_opened);
        _hider.SetActive(!_opened);

        _opened = !_opened;
    }
}