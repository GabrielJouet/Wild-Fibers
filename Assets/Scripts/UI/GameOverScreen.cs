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
        _gameScreen.gameObject.SetActive(true);

        if (win)
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
}