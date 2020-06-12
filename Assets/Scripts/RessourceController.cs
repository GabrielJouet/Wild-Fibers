using UnityEngine;

public class RessourceController : MonoBehaviour
{
    [SerializeField]
    private int _goldCount;

    [SerializeField]
    private int _lifeCount;

    private bool _stopped = false;


    public void AddGold(int count)
    {
        _goldCount += count;
    }


    public void RemoveGold(int count)
    {
        _goldCount -= count;
    }


    public void RemoveLives(int count)
    {
        Debug.Log(_lifeCount);

        if (!_stopped)
        {
            if (_lifeCount - count < 0)
                GameOver();
            else
                _lifeCount -= count;
        }
    }


    private void GameOver()
    {
        _lifeCount = 0;
        _stopped = true;
        Debug.Log("Perdu!");
    }


    public int GetGoldCOunt() { return _goldCount; }
}