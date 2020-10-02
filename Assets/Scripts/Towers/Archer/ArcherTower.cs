using System;
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

    private readonly List<ArcherArrow> _allArrows = new List<ArcherArrow>();


    private bool _paused = false;

    private DateTime _coroutineStartTime;

    private float _coroutineTimeNeeded = 0f;



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

        if (_coroutineTimeNeeded != 0f)
        {
            _coroutineStartTime = DateTime.Now;
            yield return new WaitForSeconds(_coroutineTimeNeeded);
            _coroutineTimeNeeded = 0f;
        }

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


    public override void PauseBehavior()
    {
        if (!_paused)
        {
            StopAllCoroutines();
            _coroutineTimeNeeded -= (float)(DateTime.Now - _coroutineStartTime).TotalSeconds;
        }
        else
            StartCoroutine(SummonArrows());

        _paused = !_paused;

        foreach (ArcherArrow current in _allArrows)
            current.StopBehavior();
    }
}