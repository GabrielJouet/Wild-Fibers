using System.Collections;
using UnityEngine;

public class Vines : Tower
{
    protected override void LevelOneAugmentation()
    {
        _attack.Damage++;
    }


    protected override void LevelFiveAugmentation()
    {
        _towerData.Range *= 1.15f;
    }


    /// <summary>
    /// Coroutine used to delay attacks.
    /// </summary>
    protected override IEnumerator SummonProjectile()
    {
        _coroutineStarted = true;

        _projectilePool.GetOneProjectile().GetComponent<ShockWave>().Initialize(_attack, _projectilePool, transform, Data.AugmentationLevel > 3, Data.Range);
        yield return new WaitForSeconds(_towerData.TimeShots);

        _coroutineStarted = false;
    }
}