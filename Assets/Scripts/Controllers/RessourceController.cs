using UnityEngine;
using UnityEngine.UI;

/*
 * Class used in handling ressources of the game 
 */
public class RessourceController : MonoBehaviour
{
    [Header("Start conditions")]
    //Player money count, used in purchases
    [SerializeField]
    private int _goldCount;
    //Player life count, when dropping to 0 the game is finished
    [SerializeField]
    private int _lifeCountMax;
    private int _lifeCount;


    [Header("UI Elements")]
    //Life UI text element
    [SerializeField]
    private Text _lifeText;
    //Gold UI text element
    [SerializeField]
    private Text _goldText;
    //Game Over screen UI elements
    //TO CHANGE SHOULD BE PAUSE CONTROLLER
    [SerializeField]
    private GameOverScreen _gameOverScreen;


    //Did the game actually stopped or not?
    private bool _stopped = false;


    //Start Method
    //Called when the game object is initialized
    private void Start()
    {
        _lifeCount = _lifeCountMax;
        _goldText.text = _goldCount.ToString();
        _lifeText.text = _lifeCount.ToString();
    }



    /*Gold Related*/
    #region
    //Method used to add gold from count
    //
    //Parameter => Amount of gold added
    public void AddGold(int count)
    {
        _goldCount += count;
        _goldText.text = _goldCount.ToString();
    }


    //Method used to remove gold from count
    //
    //Parameter => Amount of gold removed
    public void RemoveGold(int count)
    {
        _goldCount -= count;
        _goldText.text = _goldCount.ToString();
    }


    //Getter
    //
    //Return => Current gold count
    public int GetGoldCount() { return _goldCount; }
    #endregion



    /*Lives related*/
    #region
    //Method used to remove lives from count
    //When the count reaches 0 the game over is triggered
    //
    //Parameter => Amount of lives removed
    public void RemoveLives(int count)
    {
        if (!_stopped)
        {
            if (_lifeCount - count <= 0)
                GameOver();
            else
                _lifeCount -= count;

            _lifeText.text = _lifeCount.ToString();
        }
    }


    //Method used to stop the game and display it
    //TO CHANGE SHOULD BE PAUSECONTROLLER
    private void GameOver()
    {
        _lifeCount = 0;
        _stopped = true;

        _gameOverScreen.gameObject.SetActive(true);
        _gameOverScreen.Activate(false);
    }
    #endregion


    //Getter
    public int GetLivesLost() { return _lifeCountMax - _lifeCount; }
}