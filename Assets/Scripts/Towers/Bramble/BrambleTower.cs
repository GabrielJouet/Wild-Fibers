﻿using System.Collections;
using UnityEngine;

/*
 * Bramble tower is a tower that creates shockwaves
 */
public class BrambleTower : Tower
{
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

        _projectilePool.GetOneProjectile().GetComponent<BrambleShockWave>().Initialize(_damage, _armorThrough, transform, _projectilePool, _range);

        yield return new WaitForSeconds(_timeBetweenShots);
        _coroutineStarted = false;
    }
}