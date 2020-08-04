using System.Collections;
using UnityEngine;

public class ChocSpikes : MonoBehaviour
{
    [SerializeField]
    private float _timeToStrike;

    private float _damage;

    private float _armorThrough;

    private Enemy _enemyToTrack;

    private ChocTower _parentTower;

    private bool _stopped = false;



    public void Initialize(float newDamage, float newArmorThrough, Enemy newEnemy, ChocTower newParent)
    {
        _stopped = false;
        transform.position = newEnemy.transform.position;
        _enemyToTrack = newEnemy;
        _damage = newDamage;
        _parentTower = newParent;
        _armorThrough = newArmorThrough;

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
        if(_enemyToTrack)
            _enemyToTrack.TakeDamage(_damage, _armorThrough);
        _stopped = true;

        yield return new WaitForSeconds(_timeToStrike / 3f);
        gameObject.SetActive(false);
        _parentTower.RecoverSpike(this);
    }
}