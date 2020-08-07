using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrambleTower : Tower
{
    private bool _coroutineStarted = false;


    private readonly List<BrambleShockWave> _availableWaves = new List<BrambleShockWave>();



    private void FixedUpdate()
    {
        if (_availableEnemies.Count > 0 && !_coroutineStarted)
            StartCoroutine(SummonWave());
    }



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


    public void RecoverWave(BrambleShockWave wave)
    {
        if (!_availableWaves.Contains(wave))
            _availableWaves.Add(wave);
    }
}