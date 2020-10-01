using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Mark tower is a tower that shoots dot (Damage Over Time) projectile
 */
public class MarkTower : Tower
{
    [Header("Dot related")]
    //Armor through negative effect on enemy
    [SerializeField]
    private float _armorThroughMalus;

    //How much damage the dot will do?
    [SerializeField]
    private float _damageOverTime;

    //Duration of the dot in seconds
    [SerializeField]
    private float _dotDuration;


    //Does the coroutine already started?
    private bool _coroutineStarted = false;


    //List of available marks (pool)
    private readonly List<MarkDot> _availableMarks = new List<MarkDot>();

    private readonly List<MarkDot> _allMarks = new List<MarkDot>();


    private bool _stopped = false;

    private DateTime _projectileCreationCoroutineStartTime;

    private float _projectileCreationCoroutineTimeNeeded = 0f;


    //Fixed Update method, called  times a second
    private void FixedUpdate()
    {
        //If enemies are in range and we can start shooting, we shoot
        if (_availableEnemies.Count > 0 && !_coroutineStarted && !_stopped)
            StartCoroutine(SummonMarks());
    }



    //Coroutine used to summon dot projectiles
    private IEnumerator SummonMarks()
    {
        _coroutineStarted = true;

        if (_projectileCreationCoroutineTimeNeeded != 0f)
        {
            _projectileCreationCoroutineStartTime = DateTime.Now;
            yield return new WaitForSeconds(_projectileCreationCoroutineTimeNeeded);
            _projectileCreationCoroutineTimeNeeded = 0f;
        }

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
            {
                MarkDot bufferDot = Instantiate(_projectileUsed, transform).GetComponent<MarkDot>();
                bufferDot.Initialize(_damage, _armorThrough, _availableEnemies[i], this, _armorThroughMalus, _damageOverTime, _dotDuration);
                _allMarks.Add(bufferDot);
            }
        }

        _projectileCreationCoroutineStartTime = DateTime.Now;
        _projectileCreationCoroutineTimeNeeded = _timeBetweenShots;
        yield return new WaitForSeconds(_timeBetweenShots);
        _coroutineStarted = false;
    }


    //Method used to recover dot and add it to the pool
    //
    //Parameter => dot, the new projectile to add to the pool
    public void RecoverDot(MarkDot dot)
    {
        if (!_availableMarks.Contains(dot))
            _availableMarks.Add(dot);
    }


    public override void PauseBehavior()
    {
        if (!_stopped)
        {
            StopAllCoroutines();
            _projectileCreationCoroutineTimeNeeded -= (float)(DateTime.Now - _projectileCreationCoroutineStartTime).TotalSeconds;
        }
        else
            StartCoroutine(SummonMarks());

        _stopped = !_stopped;

        foreach (MarkDot current in _allMarks)
            current.StopBehavior();
    }
}