using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Bramble tower is a tower that creates shockwaves
 */
public class BrambleTower : Tower
{
    //Does the attack started?
    private bool _coroutineStarted = false;


    //List of available waves (pool)
    private readonly List<BrambleShockWave> _availableWaves = new List<BrambleShockWave>();



    //Fixed Update method, called 50 times a second
    private void FixedUpdate()
    {
        if (_availableEnemies.Count > 0 && !_coroutineStarted)
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
            Instantiate(_projectileUsed, transform).GetComponent<BrambleShockWave>().Initialize(_damage, _armorThrough, this, _range);


        yield return new WaitForSeconds(_timeBetweenShots);
        _coroutineStarted = false;
    }



    //Method used to recover a desactivated wave
    //
    //Parameter => wave, desactivated wave to add to pool
    public void RecoverWave(BrambleShockWave wave)
    {
        if (!_availableWaves.Contains(wave))
            _availableWaves.Add(wave);
    }
}