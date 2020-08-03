using System.Collections;
using UnityEngine;

public class ChocSpikes : MonoBehaviour
{
    [SerializeField]
    private float _timeToStrike;

    private float _damage;

    private Enemy _enemyToTrack;

    private ChocTower _parentTower;

    private bool _stopped = false;



    public void Initialize(float newDamage, Enemy newEnemy, ChocTower newParent)
    {
        _stopped = false;
        transform.position = newEnemy.transform.position;
        _enemyToTrack = newEnemy;
        _damage = newDamage;
        _parentTower = newParent;

        StartCoroutine(Strike());
    }


    private void Update()
    {
        if(!_stopped)
            transform.position = _enemyToTrack.transform.position;
    }


    private IEnumerator Strike()
    {
        yield return new WaitForSeconds(_timeToStrike);
        _enemyToTrack.TakeDamage(_damage);
        _stopped = true;

        yield return new WaitForSeconds(_timeToStrike / 3f);
        gameObject.SetActive(false);
        _parentTower.RecoverSpike(this);
    }
}