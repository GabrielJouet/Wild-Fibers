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
        {
            if(_availableSpikes.Count > 0)
            {
                _availableSpikes[0].gameObject.SetActive(true);
                _availableSpikes[0].Initialize(_damage, _armorThrough, _availableEnemies[i], this);

                _availableSpikes.Remove(_availableSpikes[0]);
            }
            else
                Instantiate(_projectileUsed, transform).GetComponent<ChocSpikes>().Initialize(_damage, _armorThrough, _availableEnemies[i], this);

        }

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

    }
}