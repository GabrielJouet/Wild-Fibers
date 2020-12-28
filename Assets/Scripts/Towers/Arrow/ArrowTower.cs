using System.Collections;
using UnityEngine;

/*
 * Archer tower shoots little projectile to enemy faster than other towers
 */
public class ArrowTower : Tower
{
    //Fixed Update method, called 50 times per second
    private void FixedUpdate()
    {
        if (_availableEnemies.Count > 0 && !_coroutineStarted)
            StartCoroutine(SummonArrows());
    }



    //Coroutine used to attack enemies
    private IEnumerator SummonArrows()
    {
        _coroutineStarted = true;

        int numberOfStrikes = _availableEnemies.Count < _towerData.Shots ? _availableEnemies.Count : _towerData.Shots;

        SortEnemies();
        foreach (Enemy current in RecoverAvailableEnemies(numberOfStrikes))
            _projectilePool.GetOneProjectile().GetComponent<Arrow>().Initialize(_towerData, current, transform, _projectilePool);

        yield return new WaitForSeconds(_towerData.TimeShots);
        _coroutineStarted = false;
    }
}