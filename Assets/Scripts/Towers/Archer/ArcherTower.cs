using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Archer tower shoots little projectile to enemy faster than other towers
 */
public class ArcherTower : Tower
{
    //List of availables projectiles (pool)
    private readonly List<ArcherArrow> _availableProjectiles = new List<ArcherArrow>();

    //All arrows without restriction
    private readonly List<ArcherArrow> _allArrows = new List<ArcherArrow>();



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
            {
                ArcherArrow bufferArrow = Instantiate(_projectileUsed, transform).GetComponent<ArcherArrow>();
                bufferArrow.Initialize(_damage, _armorThrough, _availableEnemies[i], this);
                _allArrows.Add(bufferArrow);
            }

        }

        _coroutineStartTime = DateTime.Now;
        _coroutineTimeNeeded = _timeBetweenShots;
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


    //Method used to pause tower behavior when pause button is hit
    public override void PauseBehavior()
    {
        if (!_paused)
        {
            StopAllCoroutines();
            _coroutineTimeNeeded -= (float)(DateTime.Now - _coroutineStartTime).TotalSeconds;
        }
        else
            StartCoroutine(UnPauseDelay());

        _paused = !_paused;

        foreach (ArcherArrow current in _allArrows)
            if (current.gameObject.activeSelf)
                current.StopBehavior();
    }
}