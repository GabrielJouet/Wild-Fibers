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

    [SerializeField]
    private BoxCollider2D _boxCollider;

    [SerializeField]
    private RectTransform _transform;


    [Header("Components")]
    //Pause controller used to stop everything
    [SerializeField]
    private GamePauseController _pauseController;
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
        _boxCollider.enabled = true;
        _boxCollider.size = new Vector2(Screen.width + _transform.sizeDelta.x, Screen.height + _transform.sizeDelta.y);
        _gameScreen.gameObject.SetActive(true);
        _gameScreen.sprite = win ? _winScreen : _loseScreen;
        _mainText.text = win ? "Win" : "Lose";

        if (win)
            FindObjectOfType<SaveController>().SaveLevelData(_levelController.GetLevelIndex(), _ressourceController.GetLivesLost(), LevelState.COMPLETED);

        _pauseController.PauseGame(false);
    }
}