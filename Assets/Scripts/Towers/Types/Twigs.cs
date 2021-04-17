using System.Collections;
using UnityEngine;

public class Twigs : Tower
{
    protected bool _canShootThrough = false;


    protected override void LevelOneAugmentation()
    {
        _towerData.Range *= 1.1f;
    }


    protected override void LevelTwoAugmentation() 
    {
        _towerData.ArmorThrough += 5;
    }


    protected override void LevelThreeAugmentation() 
    {
        _towerData.Dot = 1;
        _towerData.DotDuration = 3;
        _towerData.ArmorThroughMalus = 0;
    }


    protected override void LevelFourAugmentation() 
    {
        _towerData.TimeShots *= 0.95f;
    }


    protected override void LevelFiveAugmentation()
    {
        _canShootThrough = true;
    }


    /// <summary>
    /// Coroutine used to delay attacks.
    /// </summary>
    protected override IEnumerator SummonProjectile()
    {
        _coroutineStarted = true;

        int numberOfStrikes = _availableEnemies.Count < _towerData.Shots ? _availableEnemies.Count : _towerData.Shots;

        if (!_towerData.ShotsRandomly)
            SortEnemies();

        float buffer = _towerData.ArmorThrough;
        _towerData.ArmorThrough = (_canShootThrough && Random.Range(0, 100) < 10) ? 100 : buffer;

        foreach (Enemy current in RecoverAvailableEnemies(numberOfStrikes))
            _projectilePool.GetOneProjectile().Initialize(_towerData, current, _projectilePool, transform);

        _towerData.ArmorThrough = buffer;

        yield return new WaitForSeconds(_towerData.TimeShots);
        _coroutineStarted = false;
    }
}