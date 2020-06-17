using UnityEngine;
using UnityEngine.UI;

public class RessourceController : MonoBehaviour
{
    [SerializeField]
    private int _goldCount;

    [SerializeField]
    private int _lifeCount;


    [SerializeField]
    private Text _lifeText;

    [SerializeField]
    private Text _goldText;


    private bool _stopped = false;



    private void Start()
    {
        _goldText.text = _goldCount.ToString();
        _lifeText.text = _lifeCount.ToString();
    }



    public void AddGold(int count)
    {
        _goldCount += count;
        _goldText.text = _goldCount.ToString();
    }


    public void RemoveGold(int count)
    {
        _goldCount -= count;
        _goldText.text = _goldCount.ToString();
    }


    public void RemoveLives(int count)
    {
        if (!_stopped)
        {
            if (_lifeCount - count < 0)
                GameOver();
            else
                _lifeCount -= count;

            _lifeText.text = _lifeCount.ToString();
        }
    }


    private void GameOver()
    {
        _lifeCount = 0;
        _stopped = true;
    }


    public int GetGoldCount() { return _goldCount; }
}