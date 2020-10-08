﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*
 * Object used in game over behavior, show a game over screen
 */
public class GameOverScreen : MonoBehaviour
{
    //Text component that handles lose / win text
    [SerializeField]
    private Text _mainText;

    //Game screen object
    [SerializeField]
    private GameObject _gameScreen;

    [SerializeField]
    private Sprite _winScreen;

    [SerializeField]
    private Sprite _loseScreen;


    //Pause controller used to stop everything
    [SerializeField]
    private PauseController _pauseController;



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
        yield return new WaitForSeconds(1f);
        _gameScreen.SetActive(true);

        if (win)
        {
            _gameScreen.GetComponent<Image>().sprite = _winScreen;
            FindObjectOfType<SaveController>().SaveLevelData(FindObjectOfType<LevelController>().GetLevelIndex(),
                                                             FindObjectOfType<RessourceController>().GetLivesLost(),
                                                             LevelState.COMPLETED);
            _mainText.text = "Win";
        }
        else
        {
            _gameScreen.GetComponent<Image>().sprite = _loseScreen;
            _mainText.text = "Lose";
        }

        _pauseController.PauseGame(_gameScreen);
    }
}