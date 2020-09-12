using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Archer tower shoots little projectile to enemy faster than other towers
 */
public class ArcherTower : Tower
{
    //Does the attack already started?
    private bool _coroutineStarted = false;


    //List of availables projectiles (pool)
    private readonly List<ArcherArrow> _availableProjectiles = new List<ArcherArrow>();



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

        for (int i = 0; i < numberOfStrikes; i++)
        {
            if (_availableProjectiles.Count > 0)
            {
                _availableProjectiles[0].gameObject.SetActive(true);
                _availableProjectiles[0].Initialize(_damage, _armorThrough, _availableEnemies[i], this);

                _availableProjectiles.Remove(_availableProjectiles[0]);
            }
            else
                Instantiate(_projectileUsed, transform).GetComponent<ArcherArrow>().Initialize(_damage, _armorThrough, _availableEnemies[i], this);

        }

        yield return new WaitForSeconds(_timeBetweenShots);
        _coroutineStarted = false;
    }


    //Method used to recover one projectile
    //
    //Parameter => arrow, the new desactivated projectile to recover
    public void RecoverArrow(ArcherArrow arrow)
    {
        if (!_availableProjectiles.Contains(arrow))
            _availableProjectiles.Add(arrow);
    }
}