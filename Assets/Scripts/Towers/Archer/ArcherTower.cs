using System.Collections;
using UnityEngine;

/*
 * Archer tower shoots little projectile to enemy faster than other towers
 */
public class ArcherTower : Tower
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

        int numberOfStrikes = _availableEnemies.Count < _numberOfShots ? _availableEnemies.Count : _numberOfShots;

        SortEnemies();
        Debug.Log("attack");
        for (int i = 0; i < numberOfStrikes; i++)
            _projectilePool.GetOneProjectile().GetComponent<ArcherArrow>().Initialize(_damage, _armorThrough, _availableEnemies[i], transform, _projectilePool);

        yield return new WaitForSeconds(_timeBetweenShots);
        _coroutineStarted = false;
    }
}