using UnityEngine;

/*
 * Class used to handle pause either when winning or pausing game
 */
public class MenuPauseController : PauseController
{
    [SerializeField]
    private LevelSelection _alternativeMenuScript;

    [SerializeField]
    private GameObject _alternativeMenu;



    //Method used to pause the game and freeze time
    public override void PauseGame()
    {
        _hider.SetActive(!_hider.activeSelf);

        if(_alternativeMenu.activeSelf)
            _alternativeMenuScript.DesactivateLevelSelectionMenu();
        else 
            _menuObject.SetActive(!_menuObject.activeSelf);
    }
}