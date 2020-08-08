using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkTower : Tower
{
    [Header("Dot related")]
    [SerializeField]
    private float _armorThroughMalus;
    [SerializeField]
    private float _damageOverTime;
    [SerializeField]
    private float _dotDuration;


    private bool _coroutineStarted = false;


    private readonly List<MarkDot> _availableMarks = new List<MarkDot>();



    private void FixedUpdate()
    {
        if (_availableEnemies.Count > 0 && !_coroutineStarted)
            StartCoroutine(SummonMarks());
    }



    private IEnumerator SummonMarks()
    {
        _coroutineStarted = true;

        int numberOfStrikes = _availableEnemies.Count < _numberOfShots ? _availableEnemies.Count : _numberOfShots;

        _availableEnemies.Shuffle();

        for (int i = 0; i < numberOfStrikes; i++)
        {
            if (_availableMarks.Count > 0)
            {
                _availableMarks[0].gameObject.SetActive(true);
                _availableMarks[0].Initialize(_damage, _armorThrough, _availableEnemies[i], this, _armorThroughMalus, _damageOverTime, _dotDuration);

                _availableMarks.Remove(_availableMarks[0]);
            }
            else
                Instantiate(_projectileUsed, transform).GetComponent<MarkDot>().Initialize(_damage, _armorThrough, _availableEnemies[i], this, _armorThroughMalus, _damageOverTime, _dotDuration);

        }

        yield return new WaitForSeconds(_timeBetweenShots);
        _coroutineStarted = false;
    }


    public void RecoverDot(MarkDot dot)
    {
        if (!_availableMarks.Contains(dot))
            _availableMarks.Add(dot);
    }
}