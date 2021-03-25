using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used for game over screen and handling end of level.
/// </summary>
public class GameOverScreen : MonoBehaviour
{
    [Header("UI elements")]

    /// <summary>
    /// Main text component.
    /// </summary>
    [SerializeField]
    private Text _mainText;

    /// <summary>
    /// Game screen image component.
    /// </summary>
    [SerializeField]
    private Image _gameScreen;

    /// <summary>
    /// Sprite for win.
    /// </summary>
    [SerializeField]
    private Sprite _winScreen;

    /// <summary>
    /// Sprite for loose.
    /// </summary>
    [SerializeField]
    private Sprite _loseScreen;

    /// <summary>
    /// List of all seeds at the end of the level.
    /// </summary>
    [SerializeField]
    private List<Image> _seedScores;

    /// <summary>
    /// Activated seed sprite.
    /// </summary>
    [SerializeField]
    private Sprite _activatedSprite;


    [Header("Components")]

    /// <summary>
    /// Resource controller component.
    /// </summary>
    [SerializeField]
    private RessourceController _ressourceController;


    /// <summary>
    /// Box collider component.
    /// </summary>
    [SerializeField]
    private BoxCollider2D _boxCollider;

    /// <summary>
    /// Rect transform component.
    /// </summary>
    [SerializeField]
    private RectTransform _transform;

    /// <summary>
    /// Display controller object.
    /// </summary>
    [SerializeField]
    private DisplayController _displayController;



    /// <summary>
    /// Method used to activate and display the screen.
    /// </summary>
    /// <param name="win">Does the player wins this level?</param>
    public void Activate(bool win)
    {
        _displayController.PauseGame();
        _displayController.DisplayObject(gameObject);

        DelayShow(win); 
    }


    /// <summary>
    /// Coroutine used to delay the display.
    /// </summary>
    /// <param name="win">Does the player wins this level?</param>
    private void DelayShow(bool win)
    {
        _boxCollider.enabled = true;
        _boxCollider.size = new Vector2(Screen.width + _transform.sizeDelta.x, Screen.height + _transform.sizeDelta.y);

        _gameScreen.gameObject.SetActive(true);

        _gameScreen.sprite = win ? _winScreen : _loseScreen;
        _mainText.text = win ? "Win" : "Lose";

        if (win)
        {
            int livesLost = _ressourceController.LivesLost;

            if (livesLost <= 15)
            {
                _seedScores[2].sprite = _activatedSprite;

                if (livesLost <= 10)
                {
                    _seedScores[1].sprite = _activatedSprite;

                    if (livesLost <= 3)
                        _seedScores[0].sprite = _activatedSprite;
                }
            }

            Controller.Instance.SaveControl.SaveLevelData(livesLost);
        }
    }
}