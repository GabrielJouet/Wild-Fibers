using System.Collections;
using UnityEngine;

public class Twigs : Tower
{
    protected override void LevelOneAugmentation()
    {
        _towerData.Range *= 1.1f;
    }


    protected override void LevelTwoAugmentation() 
    {
        _towerData.ArmorThrough += 5;
    }


    protected override void LevelFourAugmentation() 
    {
        _towerData.TimeShots *= 0.95f;
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
        _towerData.ArmorThrough = (Data.AugmentationLevel > 3 && Random.Range(0, 100) < 10) ? 100 : buffer;

        foreach (Enemy current in RecoverAvailableEnemies(numberOfStrikes))
            _projectilePool.GetOneProjectile().GetComponent<Twig>().Initialize(_attack, current, _projectilePool, transform, Data.AugmentationLevel > 1);

        _towerData.ArmorThrough = buffer;

        yield return new WaitForSeconds(_towerData.TimeShots);
        _coroutineStarted = false;
    }
}