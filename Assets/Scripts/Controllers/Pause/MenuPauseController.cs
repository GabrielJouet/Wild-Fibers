using UnityEngine;

/// <summary>
/// Class used to handle map selection pause.
/// </summary>
public class MenuPauseController : PauseController
{
    /// <summary>
    /// Menu to pause.
    /// </summary>
    [SerializeField]
    private LevelSelection _alternativeMenuScript;


    /// <summary>
    /// Method used to pause and display menu.
    /// </summary>
    /// <param name="showMenu">Does the pause needs to display a menu ?</param>
    public override void PauseGame(bool showMenu)
    {
        _hider.SetActive(!_hider.activeSelf);

        if(_alternativeMenuScript.Opened)
            _alternativeMenuScript.DesactivateLevelSelectionMenu();
        else 
            _menuObject.SetActive(!_menuObject.activeSelf);
    }
}