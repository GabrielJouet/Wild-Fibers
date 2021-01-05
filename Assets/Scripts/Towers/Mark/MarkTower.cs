using System.Collections;
using UnityEngine;

/*
 * Mark tower is a tower that shoots dot (Damage Over Time) projectile
 */
public class MarkTower : Tower
{
    //Fixed Update method, called  times a second
    private void FixedUpdate()
    {
        //If enemies are in range and we can start shooting, we shoot
        if (_availableEnemies.Count > 0 && !_coroutineStarted)
            StartCoroutine(SummonMarks());
    }



    //Coroutine used to summon dot projectiles
    private IEnumerator SummonMarks()
    {
        _coroutineStarted = true;

        int numberOfStrikes = _availableEnemies.Count < _towerData.Shots ? _availableEnemies.Count : _towerData.Shots;

        _availableEnemies.Shuffle();
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