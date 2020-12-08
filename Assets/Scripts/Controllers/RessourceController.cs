using UnityEngine;
using UnityEngine.UI;

/*
 * Class used in handling ressources of the game 
 */
public class RessourceController : MonoBehaviour
{
    [SerializeField]
    private LevelController _levelController;


    [Header("UI Elements")]
    //Life UI text element
    [SerializeField]
    private Text _lifeText;

    //Gold UI text element
    [SerializeField]
    private Text _goldText;

    [SerializeField]
    private Animator _goldIconAnimator;

    //Game Over screen UI elements
    [SerializeField]
    private GameOverScreen _gameOverScreen;


    //Player money count, used in purchases
    public int GoldCount { get; private set; }

    //Lives lost for this level
    public int LivesLost { get => _lifeCountMax - _lifeCount; }


    //Player life count, when dropping to 0 the game is finished
    private int _lifeCountMax;
    private int _lifeCount;


    //Start Method
    //Called when the game object is initialized
    private void Start()
    {
        _lifeCountMax = _levelController.LoadedLevel.Lives;
        _lifeCount = _lifeCountMax;
        GoldCount = _levelController.LoadedLevel.Gold;
        _goldText.text = GoldCount.ToString();
        _lifeText.text = _lifeCount.ToString();
    }



    /*Gold Related*/
    #region
    //Method used to add gold from count
    //
    //Parameter => Amount of gold added
    public void AddGold(int count)
    {
        GoldCount += count;
        _goldText.text = GoldCount.ToString();
    }


    //Method used to remove gold from count
    //
    //Parameter => Amount of gold removed
    public void RemoveGold(int count)
    {
        GoldCount -= count;
        _goldText.text = GoldCount.ToString();
    }
    #endregion



    /*Lives related*/
    #region
    //Method used to remove lives from count
    //When the count reaches 0 the game over is triggered
    //
    //Parameter => Amount of lives removed
    public void RemoveLives(int count)
    {
        if (_lifeCount > 0)
        {
            _goldIconAnimator.SetTrigger("lose");

            if (_lifeCount - count <= 0)
                GameOver();
            else
                _lifeCount -= count;

            _lifeText.text = _lifeCount.ToString();
        }
    }


    //Method used to stop the game and display it
    private void GameOver()
    {
        _lifeCount = 0;
        _levelController.Ended = true;
        _gameOverScreen.Activate(false);
    }
    #endregion
}