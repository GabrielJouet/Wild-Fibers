/*
 * Class used to handle pause either when winning or pausing game
 */
using UnityEngine;

public class GamePauseController : PauseController
{
    //Method used to pause the game and freeze time
    public override void PauseGame()
    {
        _menuObject.SetActive(!_paused);
        _hider.SetActive(!_paused);

        Time.timeScale = !_paused? 0f : 1f;
        _paused = !_paused;
    }
}