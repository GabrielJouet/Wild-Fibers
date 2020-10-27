using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*
 * Object used in game over behavior, show a game over screen
 */
public class GameOverScreen : MonoBehaviour
{
    [Header("UI elements")]
    //Text component that handles lose / win text
    [SerializeField]
    private Text _mainText;

    //Game screen object
    [SerializeField]
    private Image _gameScreen;

    [SerializeField]
    private Sprite _winScreen;

    [SerializeField]
    private Sprite _loseScreen;


    [Header("Components")]
    //Pause controller used to stop everything
    [SerializeField]
    private PauseController _pauseController;
    [SerializeField]
    private LevelController _levelController;
    [SerializeField]
    private RessourceController _ressourceController;

    private bool _win;


    //Does the tower is currently paused? (by Pause Controller)
    protected bool _paused = false;

    //Coroutine Start Time (used if the tower is paused)
    protected DateTime _coroutineStartTime;

    //Coroutine time needed to reset
    protected float _coroutineTimeNeeded = 0f;



    //Method used to activate game over screen
    //
    //Parameter => win, does the player win this level?
    public void Activate(bool win) 
    {
        StartCoroutine(DelayShow(win)); 
    }


    //Coroutine used to delay a bit game over screen popup
    private IEnumerator DelayShow(bool win)
    {
        _win = win;
        yield return new WaitForSeconds(1f);
        ShowBehavior();
    }


    //Method used to show game over screen (used for pause handling)
    private void ShowBehavior()
    {
        _gameScreen.gameObject.SetActive(true);

        if (_win)
        {
            _gameScreen.sprite = _winScreen;
            FindObjectOfType<SaveController>().SaveLevelData(_levelController.GetLevelIndex(), _ressourceController.GetLivesLost(), LevelState.COMPLETED);
            _mainText.text = "Win";
        }
        else
        {
            _gameScreen.sprite = _loseScreen;
            _mainText.text = "Lose";
        }

        _gameScreen.SetNativeSize();
        _pauseController.PauseGame(_gameScreen.gameObject);
    }


    //Method used by pause controller to pause behavior
    public void PauseBehavior()
    {
        if (!_paused)
        {
            StopAllCoroutines();
            _coroutineTimeNeeded -= (float)(DateTime.Now - _coroutineStartTime).TotalSeconds;
        }
        else if (_coroutineTimeNeeded > 0f)
            StartCoroutine(DelayUnPause());

        _paused = !_paused;
    }


    //Coroutine used to delay unpause after being paused
    private IEnumerator DelayUnPause()
    {
        yield return new WaitForSeconds(_coroutineTimeNeeded);
        ShowBehavior();
    }
}