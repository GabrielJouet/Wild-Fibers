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

        int numberOfStrikes = _availableEnemies.Count < _numberOfShots ? _availableEnemies.Count : _numberOfShots;

        SortEnemies();
        foreach (Enemy current in RecoverAvailableEnemies(numberOfStrikes))
            _projectilePool.GetOneProjectile().GetComponent<ChocSpikes>().Initialize(_damage, _armorThrough, current, _projectilePool);

        yield return new WaitForSeconds(_timeBetweenShots);
        _coroutineStarted = false;
    }
}