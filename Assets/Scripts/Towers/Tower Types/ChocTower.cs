using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocTower : Tower
{
    private bool _coroutineStarted = false;


    private List<ChocSpikes> _availableSpikes = new List<ChocSpikes>();


    private void FixedUpdate()
    {
        if (_availableEnemies.Count > 0 && !_coroutineStarted)
            StartCoroutine(SummonSpikes());
    }


    private IEnumerator SummonSpikes()
    {
        _coroutineStarted = true;

        while(true)
        {
            SortEnemies();

            int numberOfStrikes = _availableEnemies.Count < _numberOfShots ? _availableEnemies.Count : _numberOfShots;

            Debug.Log(numberOfStrikes);
            for(int i = 0; i < numberOfStrikes; i ++)
            {
                if(_availableSpikes.Count > 0)
                {
                    _availableSpikes[0].gameObject.SetActive(true);
                    _availableSpikes[0].Initialize(_damage, _availableEnemies[i], this);

                    _availableSpikes.Remove(_availableSpikes[0]);
                }
                else
                    Instantiate(_projectileUsed).GetComponent<ChocSpikes>().Initialize(_damage, _availableEnemies[i], this);

            }

            yield return new WaitForSeconds(_timeBetweenShots);
        }
    }


    public void RecoverSpike(ChocSpikes spikes)
    {
        if (!_availableSpikes.Contains(spikes))
            _availableSpikes.Add(spikes);
    }
}