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
        {
            Projectile buffer = _projectilePool.GetOneProjectile();
            buffer.transform.position = transform.position;
            buffer.Initialize(_towerData, current, _projectilePool);
        }

        yield return new WaitForSeconds(_towerData.TimeShots);
        _coroutineStarted = false;
    }
}