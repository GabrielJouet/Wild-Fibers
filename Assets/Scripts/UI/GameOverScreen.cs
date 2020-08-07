using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private Text _mainText;
    [SerializeField]
    private GameObject _gameScreen;

    [SerializeField]
    private PauseController _pauseController;



    public void Activate(bool win) 
    {
        gameObject.SetActive(true);
        StartCoroutine(DelayShow(win)); 
    }


    private IEnumerator DelayShow(bool win)
    {
        yield return new WaitForSeconds(1.5f);

        if (win)
        {
            _pauseController.PauseGame(_gameScreen);
            _mainText.text = "Win";
        }
        else
        {
            _pauseController.PauseGame(_gameScreen);
            _mainText.text = "Lose";
        }
    }
}