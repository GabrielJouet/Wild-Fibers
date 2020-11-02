using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Bramble tower is a tower that creates shockwaves
 */
public class BrambleTower : Tower
{
    //List of available waves (pool)
    private readonly List<BrambleShockWave> _availableWaves = new List<BrambleShockWave>();

    //All waves without restriction
    private readonly List<BrambleShockWave> _allWaves = new List<BrambleShockWave>();



    //Fixed Update method, called 50 times a second
    private void FixedUpdate()
    {
        if (_availableEnemies.Count > 0 && !_coroutineStarted && !_paused)
            StartCoroutine(SummonWave());
    }



    //Couroutine used to attack and summon a wave
    private IEnumerator SummonWave()
    {
        _coroutineStarted = true;

        if (_availableWaves.Count > 0)
        {
            _availableWaves[0].gameObject.SetActive(true);
            _availableWaves[0].Initialize(_damage, _armorThrough, this, _range);

            _availableWaves.Remove(_availableWaves[0]);
        }
        else
        {
            BrambleShockWave bufferWave = Instantiate(_projectileUsed, transform).GetComponent<BrambleShockWave>();
            bufferWave.Initialize(_damage, _armorThrough, this, _range);
            _allWaves.Add(bufferWave);
        }

        _coroutineStartTime = DateTime.Now;
        _coroutineTimeNeeded = _timeBetweenShots;
        yield return new WaitForSeconds(_timeBetweenShots);
        UnPauseMethod();
    }



    //Method used to recover a desactivated wave
    //
    //Parameter => wave, desactivated wave to add to pool
    public void RecoverWave(BrambleShockWave wave)
    {
        if (!_availableWaves.Contains(wave))
            _availableWaves.Add(wave);
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

        foreach (BrambleShockWave current in _allWaves)
            if (current.gameObject.activeSelf)
                current.enabled = _paused;

        _paused = !_paused;
    }
}