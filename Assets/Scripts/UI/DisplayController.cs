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
    /// Method used to pause the game.
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0f;
        Paused = true;
    }


    /// <summary>
    /// Method used to pause the game.
    /// </summary>
    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        Paused = false;
    }

    public void TooglePause()
    {
        Paused = !Paused;
        Time.timeScale = Paused ? 0f : 1f;
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

        UnPauseGame();
    }
}