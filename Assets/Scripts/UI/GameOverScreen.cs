using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private Text _mainText;

    [SerializeField]
    private GameObject _gameScreen;

    [SerializeField]
    private GameObject _hider;



    public void ActivateWin()
    {
        _hider.SetActive(true);
        _gameScreen.SetActive(true);
        _mainText.text = "Win";
    }


    public void ActivateLose()
    {
        _hider.SetActive(false);
        _gameScreen.SetActive(true);
        _mainText.text = "Lose";
    }
}