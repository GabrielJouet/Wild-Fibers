using System.Collections;
using UnityEngine;

/// <summary>
/// Class used to handle map selection menus.
/// </summary>
public class DisplayController : MonoBehaviour
{
    /// <summary>
    /// Hider used.
    /// </summary>
    [SerializeField]
    private GameObject _hider;

    /// <summary>
    /// Can the current scene be paused?
    /// </summary>
    [SerializeField]
    private bool _canPause;

    /// <summary>
    /// Previous object displayed.
    /// </summary>
    [SerializeField]
    private GameObject _defaultMenu;

    /// <summary>
    /// Previous object displayed.
    /// </summary>
    private GameObject _displayedObject = null;


    /// <summary>
    /// Did the game is paused?
    /// </summary>
    public bool Paused { get; private set; } = false;



    /// <summary>
    /// Update is called every frame.
    /// </summary>
    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseGame(_defaultMenu);
    }


    /// <summary>
    /// Method used to pause the game and display an object.
    /// </summary>
    /// <param name="menuObject">The menu object to display</param>
    public void PauseGame(GameObject menuObject)
    {
        DisplayObject(menuObject);

        if(_canPause)
        {
            Time.timeScale = !Paused ? 0f : 1f;
            Paused = !Paused;
        }
    }


    /// <summary>
    /// Method used to pause the game.
    /// </summary>
    public void PauseGame()
    {
        if (_canPause)
        {
            Time.timeScale = !Paused ? 0f : 1f;
            Paused = !Paused;
        }
    }


    /// <summary>
    /// Method used to display menu.
    /// </summary>
    /// <param name="displayedObject">The new displayed object</param>
    public void DisplayObject(GameObject displayedObject)
    {
        if (displayedObject == _displayedObject)
        {
            _displayedObject.SetActive(false);
            _hider.SetActive(false);
            _displayedObject = null;
        }
        else
        {
            if(_displayedObject != null)
                _displayedObject.SetActive(false);

            _displayedObject = displayedObject;

            _displayedObject.SetActive(true);
            StartCoroutine(DelayHiderSpawn());
        }
    }

    private IEnumerator DelayHiderSpawn()
    {
        yield return new WaitForEndOfFrame();
        _hider.SetActive(true);
    }


    /// <summary>
    /// Method used to reset displayed menu.
    /// </summary>
    public void ResetDisplay()
    {
        _displayedObject.SetActive(false);
        _displayedObject = null;

        _hider.SetActive(false);

        PauseGame();
    }
}