using UnityEngine;

/// <summary>
/// Class used to manage pause and menu display.
/// </summary>
public class PauseController : MonoBehaviour
{
    [Header("UI Elements")]

    /// <summary>
    /// Menu object that will be displayed when paused.
    /// </summary>
    [SerializeField]
    protected GameObject _menuObject;

    /// <summary>
    /// Object that hides with an alpha.
    /// </summary>
    [SerializeField]
    protected GameObject _hider;


    /// <summary>
    /// Did the game is paused?
    /// </summary>
    protected bool _paused = false;



    /// <summary>
    /// Update is called every frame.
    /// </summary>
    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseGame(true);
    }


    /// <summary>
    /// Method used to pause the game.
    /// </summary>
    /// <param name="showMenu">Does the pause game needs to display a menu?</param>
    public virtual void PauseGame(bool showMenu) 
    {
        if (showMenu)
            _menuObject.SetActive(!_paused);

        _hider.SetActive(!_paused);

        Time.timeScale = !_paused ? 0f : 1f;
        _paused = !_paused;
    }
}
