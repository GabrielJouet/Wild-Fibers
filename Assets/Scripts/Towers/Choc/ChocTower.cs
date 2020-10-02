using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Choc tower summons spiky roots to attack
 */
public class ChocTower : Tower
{
    //Does the attack started?
    private bool _coroutineStarted = false;


    //List of available spikes (pool)
    private readonly List<ChocSpikes> _availableSpikes = new List<ChocSpikes>();

    private readonly List<ChocSpikes> _allSpikes = new List<ChocSpikes>();


    private bool _paused = false;

    private DateTime _coroutineStartTime;

    private float _coroutineTimeNeeded = 0f;



    //Fixed Update method, called times a second
    private void FixedUpdate()
    {
        if (_availableEnemies.Count > 0 && !_coroutineStarted && !_paused)
            StartCoroutine(SummonSpikes());
    }



    //Coroutine used to summon spike in order to attack
    private IEnumerator SummonSpikes()
    {
        _coroutineStarted = true;
        SortEnemies();

        int numberOfStrikes = _availableEnemies.Count < _numberOfShots ? _availableEnemies.Count : _numberOfShots;

        for(int i = 0; i < numberOfStrikes; i ++)
        {
            if(_availableSpikes.Count > 0)
            {
                _availableSpikes[0].gameObject.SetActive(true);
                _availableSpikes[0].Initialize(_damage, _armorThrough, _availableEnemies[i], this);

                _availableSpikes.Remove(_availableSpikes[0]);
            }
            else
            {
                ChocSpikes bufferSpike = Instantiate(_projectileUsed, transform).GetComponent<ChocSpikes>();
                bufferSpike.Initialize(_damage, _armorThrough, _availableEnemies[i], this);
                _allSpikes.Add(bufferSpike);
            }

        }

        _coroutineStartTime = DateTime.Now;
        _coroutineTimeNeeded = _timeBetweenShots;
        yield return new WaitForSeconds(_timeBetweenShots);
        _coroutineStarted = false;
    }


    //Method used to recover spike and place it into the pool
    //
    //Parameter => spikes, the new desactivated spike
    public void RecoverSpike(ChocSpikes spikes)
    {
        if (!_availableSpikes.Contains(spikes))
            _availableSpikes.Add(spikes);
    }


    public override void PauseBehavior()
    {
        if (!_paused)
        {
            StopAllCoroutines();
            _coroutineTimeNeeded -= (float)(DateTime.Now - _coroutineStartTime).TotalSeconds;
        }
        else
            StartCoroutine(SummonSpikes());

        _paused = !_paused;

        foreach (ChocSpikes current in _allSpikes)
            current.StopBehavior();
    }
}