using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : Tower
{
    private bool _coroutineStarted = false;


    private readonly List<ArcherArrow> _availableMarks = new List<ArcherArrow>();



    private void FixedUpdate()
    {
        if (_availableEnemies.Count > 0 && !_coroutineStarted)
            StartCoroutine(SummonArrows());
    }



    private IEnumerator SummonArrows()
    {
        _coroutineStarted = true;

        int numberOfStrikes = _availableEnemies.Count < _numberOfShots ? _availableEnemies.Count : _numberOfShots;

        SortEnemies();

        for (int i = 0; i < numberOfStrikes; i++)
        {
            if (_availableMarks.Count > 0)
            {
                _availableMarks[0].gameObject.SetActive(true);
                _availableMarks[0].Initialize(_damage, _armorThrough, _availableEnemies[i], this);

                _availableMarks.Remove(_availableMarks[0]);
            }
            else
                Instantiate(_projectileUsed, transform).GetComponent<ArcherArrow>().Initialize(_damage, _armorThrough, _availableEnemies[i], this);

        }

        yield return new WaitForSeconds(_timeBetweenShots);
        _coroutineStarted = false;
    }


    public void RecoverArrow(ArcherArrow arrow)
    {
        if (!_availableMarks.Contains(arrow))
            _availableMarks.Add(arrow);
    }
}