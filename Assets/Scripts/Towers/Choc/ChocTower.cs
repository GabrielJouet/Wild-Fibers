using System.Collections;
using UnityEngine;

/*
 * Choc tower summons spiky roots to attack
 */
public class ChocTower : Tower
{
    //Fixed Update method, called times a second
    private void FixedUpdate()
    {
        if (_availableEnemies.Count > 0 && !_coroutineStarted)
            StartCoroutine(SummonSpikes());
    }



    //Coroutine used to summon spike in order to attack
    private IEnumerator SummonSpikes()
    {
        _coroutineStarted = true;
        SortEnemies();

        int numberOfStrikes = _availableEnemies.Count < _numberOfShots ? _availableEnemies.Count : _numberOfShots;

        for(int i = 0; i < numberOfStrikes; i ++)
            _projectilePool.GetOneProjectile().GetComponent<ChocSpikes>().Initialize(_damage, _armorThrough, _availableEnemies[i], _projectilePool);

        yield return new WaitForSeconds(_timeBetweenShots);
        _coroutineStarted = false;
    }
}